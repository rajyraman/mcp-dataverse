using Azure.Core;
using Azure.Identity;
using DataverseMcpServer.Core.Prompts;
using DataverseMcpServer.Core.Tools;
using MarkMpn.Sql4Cds.Engine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.PowerPlatform.Dataverse.Client.Model;
using ModelContextProtocol;
using System;
using System.Runtime.Caching;
namespace Mcp.Dataverse.Core.Extensions;

public static class BuilderExtensions
{
    //create extension method for IHostApplicationBuilder
    public static void AddDataverse(this IHostApplicationBuilder builder)
    {
        var environmentUrl = Environment.GetEnvironmentVariable("DATAVERSE_ENVIRONMENT_URL");
        var clientId = Environment.GetEnvironmentVariable("AZURE_CLIENT_ID");
        var clientSecret = Environment.GetEnvironmentVariable("AZURE_CLIENT_SECRET");
        var tenantId = Environment.GetEnvironmentVariable("AZURE_TENANT_ID");
        var isContainer = Environment.GetEnvironmentVariable("DOCKER_CONTAINER")?.Equals(bool.TrueString, StringComparison.CurrentCultureIgnoreCase) ?? false;
        //var loggerFactory = LoggerFactory.Create(b => b.AddSimpleConsole());
        //var logger = loggerFactory.CreateLogger("dataverse");

        if (string.IsNullOrEmpty(environmentUrl))
        {
            throw new McpException("DATAVERSE_ENVIRONMENT_URL environment variable is not set.", McpErrorCode.InvalidRequest);
        }
        var credentialOptions = new DefaultAzureCredentialOptions
        {
            ExcludeWorkloadIdentityCredential = true,
            ExcludeManagedIdentityCredential = true,
            ExcludeEnvironmentCredential = true
        };

        if (isContainer)
        {
            credentialOptions.ExcludeEnvironmentCredential = false;
        }

        builder.Services.AddSingleton(serviceProvider =>
        {
            try
            {
                var dataverseClient = AzAuth.CreateServiceClient(
                    new ConnectionOptions { ServiceUri = new Uri(environmentUrl) }, 
                    credentialOptions: credentialOptions, 
                    configurationOptions: new ConfigurationOptions { EnableAffinityCookie = false }
                );
                return dataverseClient;
            }
            catch (Exception ex)
            {
                throw new McpException(ex.Message, ex.InnerException);
            }
        });

        builder.Services.AddSingleton(serviceProvider =>
        {
            var dataverseClient = serviceProvider.GetRequiredService<ServiceClient>();
            return new Sql4CdsConnection(dataverseClient) { UseLocalTimeZone = true };
        });
    }
}
