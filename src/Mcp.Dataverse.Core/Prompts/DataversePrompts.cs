using Mcp.Dataverse.Core.Properties;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text;

namespace DataverseMcpServer.Core.Prompts;
[McpServerPromptType]
public class DataversePrompts
{
    [McpServerPrompt, Description("General Prompt to interact with Dataverse")]
    public static ChatMessage MainPrompt() =>
        new(ChatRole.User, Encoding.UTF8.GetString(Resources.dataverse_main_prompt));
}
