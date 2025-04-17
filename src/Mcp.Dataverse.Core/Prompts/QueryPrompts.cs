using Mcp.Dataverse.Core.Properties;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Text;

namespace DataverseMcpServer.Core.Prompts;
[McpServerPromptType]
public class QueryPrompts
{
    [McpServerPrompt, Description("Prompt to get recent rows based on modifiedon by table.")]
    public static ChatMessage RecentRecords([Description("The table's logical name e.g. contact, account")] string tableName) =>
        new(ChatRole.User, $"Please retrieve 50 recent records for {tableName}, sorted by modifiedon descending");

    [McpServerPrompt, Description("Prompt to get the solution components in a solution.")]
    public static ChatMessage SolutionComponents([Description("Unique name of the solution")] string solutionUniqueName) =>
    new(ChatRole.User, $"""
        Please retrieve all the solutioncomponent records for solution with uniquename for {solutionUniqueName}. 
        Summarise the results in a table, grouped by component type.
        """);
        

    [McpServerPrompt, Description("Prompt to get row counts of tables in a specific app.")]
    public static ChatMessage GetTableCountByApp([Description("Name of the app")] string appName) =>
    new(ChatRole.User, $"""
        Show the row count for the tables in the {appName} app. Use the below query to get the list of tables in the app:

        ```sql
        SELECT appmodule.name,entity.logicalname FROM appmodule
        JOIN appmodulecomponent ON appmodule.appmoduleidunique = appmodulecomponent.appmoduleidunique
        JOIN metadata.entity ON appmodulecomponent.objectid=entity.metadataid
        WHERE name='{appName}'
        ```

        Present the results as a table with index column, table name and row count. Order the results by row count descending.
        """);
}