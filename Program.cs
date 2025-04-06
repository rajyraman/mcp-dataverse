using Azure.Identity;
using MarkMpn.Sql4Cds.Engine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.PowerPlatform.Dataverse.Client;
using ModelContextProtocol.Protocol.Types;
using System.Diagnostics;
using System.Net.Http.Headers;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.ClearProviders();
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithToolsFromAssembly();
#if DEBUG
if(!Debugger.IsAttached)
{
    Debugger.Launch();
}
#endif

try
{
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
        var dataverseClient = AzAuth.CreateServiceClient(Environment.GetEnvironmentVariable("DATAVERSE_ENVIRONMENT_URL") ?? args[1], credentialOptions: credentialOptions);
        var sql4cdsConnection = new Sql4CdsConnection(dataverseClient) { UseLocalTimeZone = true };
        return sql4cdsConnection;
    });

    await builder.Build().RunAsync();
}
catch (Exception ex)
{
    throw;
}