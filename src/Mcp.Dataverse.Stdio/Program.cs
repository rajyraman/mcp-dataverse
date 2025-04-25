using DataverseMcpServer.Core.Prompts;
using DataverseMcpServer.Core.Tools;
using Mcp.Dataverse.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.SetMinimumLevel(LogLevel.None);

builder.AddDataverse();
builder.Services.AddMemoryCache();
builder.Services.AddMcpServer()
    .WithStdioServerTransport()
    .WithPrompts<QueryPrompts>()
    .WithPrompts<DataversePrompts>()
    .WithTools<DataverseTool>();
await builder.Build().RunAsync();