using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace DataverseMcpServer.Prompts;
[McpServerPromptType]
public class QueryPrompts
{
    [McpServerPrompt, Description("Creates a prompt to get recent rows based on modifiedon by table.")]
    public static ChatMessage RecentRecords([Description("The table's logical name e.g. contact, account")] string tableName) =>
        new(ChatRole.User, $"Please retrieve 50 recent records for {tableName}, sorted by modifiedon descending");

    [McpServerPrompt, Description("Creates a prompt to get the solution components in a solution.")]
    public static ChatMessage SolutionComponents([Description("Unique name of the solution")] string solutionUniqueName) =>
    new(ChatRole.User, $"Please all the solutioncomponent records for solution with uniquename for {solutionUniqueName}. Summarise in a table, grouped by component type.");
}