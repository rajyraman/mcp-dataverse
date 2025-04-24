using DataverseMcpServer.Core.Prompts;
using DataverseMcpServer.Core.Tools;
using Mcp.Dataverse.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenLocalhost(3001);
});

builder.AddDataverse();
builder.Services.AddMemoryCache();
builder.Services.AddMcpServer()
    .WithPrompts<QueryPrompts>()
    .WithPrompts<DataversePrompts>()
    .WithTools<DataverseTool>();
var app = builder.Build();
app.MapMcp();
app.Run();