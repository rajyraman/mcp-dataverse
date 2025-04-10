using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace DataverseMcpServer.Prompts;
[McpServerPromptType]
public class DataversePrompts
{
    [McpServerPrompt, Description("Creates a prompt to get recent rows based on modifiedon by table.")]
    public static ChatMessage DataverseGeneralPrompt() =>
        new(ChatRole.User, File.ReadAllText("../"));
}