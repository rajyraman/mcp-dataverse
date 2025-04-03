using Azure.Identity;
using MarkMpn.Sql4Cds.Engine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Serilog;
using System.Diagnostics;
using System.Net.Http.Headers;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
#if DEBUG
if(!Debugger.IsAttached)
{
    Debugger.Launch();
}
#endif
Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Verbose() // Capture all log levels
           .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "MCP.DataverseSQL.log"),
               outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
           .WriteTo.Debug()
           .CreateLogger();
var loggerFactory = new LoggerFactory().AddSerilog(Log.Logger);
var logger = loggerFactory.CreateLogger("Dataverse");

try
{
    Log.Information("Starting MCP Server..");
    builder.Services.AddSingleton(_ =>
    {
        var credentialOptions = new DefaultAzureCredentialOptions
        {
            ExcludeWorkloadIdentityCredential = true,
            ExcludeAzurePowerShellCredential = true,
            ExcludeAzureDeveloperCliCredential = true,
            ExcludeSharedTokenCacheCredential = true,
            ExcludeManagedIdentityCredential = true,
        };
        if (Environment.GetEnvironmentVariable("DOCKER_CONTAINER") == bool.TrueString)
        {
            credentialOptions = new DefaultAzureCredentialOptions
            {
                ExcludeWorkloadIdentityCredential = true,
                ExcludeAzurePowerShellCredential = true,
                ExcludeAzureDeveloperCliCredential = true,
                ExcludeSharedTokenCacheCredential = true,
                ExcludeInteractiveBrowserCredential = true,
                ExcludeVisualStudioCodeCredential = true,
                ExcludeAzureCliCredential = true,
                ExcludeManagedIdentityCredential = true,
                ExcludeVisualStudioCredential = true,
            };
        }
        var dataverseClient = AzAuth.CreateServiceClient(Environment.GetEnvironmentVariable("DATAVERSE_ENVIRONMENT_URL") ?? args[1], credentialOptions: credentialOptions, logger: logger);
        var sql4cdsConnection = new Sql4CdsConnection(dataverseClient) { UseLocalTimeZone = true };
        return sql4cdsConnection;
    });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    logger.LogError(ex, "MCP Server Error.");
    throw;
}
finally
{
    Log.CloseAndFlush();
}