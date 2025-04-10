using Azure.Identity;
using DataverseMcpServer.Prompts;
using DataverseMcpServer.Tools;
using MarkMpn.Sql4Cds.Engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using System.Diagnostics;

//var builder = Host.CreateApplicationBuilder(args);
//builder.Logging.SetMinimumLevel(LogLevel.Critical);
//builder.Logging.ClearProviders();
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(3001);
});
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithPrompts<QueryPrompts>()
    .WithTools<DataverseTool>();
#if DEBUG
if (!Debugger.IsAttached)
{
    Debugger.Launch();
}
#endif
var credentialOptions = new DefaultAzureCredentialOptions
{
    ExcludeWorkloadIdentityCredential = true,
    ExcludeAzurePowerShellCredential = true,
    ExcludeAzureDeveloperCliCredential = true,
    ExcludeSharedTokenCacheCredential = true,
    ExcludeManagedIdentityCredential = true,
    ExcludeEnvironmentCredential = true,
};
if (Environment.GetEnvironmentVariable("DOCKER_CONTAINER") == bool.TrueString)
{
    credentialOptions.ExcludeInteractiveBrowserCredential = true;
    credentialOptions.ExcludeVisualStudioCodeCredential = true;
    credentialOptions.ExcludeAzureCliCredential = true;
    credentialOptions.ExcludeVisualStudioCredential = true;
}

builder.Services.AddSingleton(serviceProvider =>
{
    var dataverseClient = AzAuth.CreateServiceClient(
        Environment.GetEnvironmentVariable("DATAVERSE_ENVIRONMENT_URL") ?? args[1],
        credentialOptions: credentialOptions
    );
    return dataverseClient;
});

builder.Services.AddSingleton(serviceProvider =>
{
    var dataverseClient = serviceProvider.GetRequiredService<ServiceClient>();
    return new Sql4CdsConnection(dataverseClient) { UseLocalTimeZone = true };
});

//await builder.Build().RunAsync();
var app = builder.Build();
app.MapMcp();
app.Run();