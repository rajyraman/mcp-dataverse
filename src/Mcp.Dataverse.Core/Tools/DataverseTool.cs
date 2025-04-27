using Azure.Core;
using MarkMpn.Sql4Cds.Engine;
using MarkMpn.Sql4Cds.Engine.FetchXml;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.PowerPlatform.Dataverse.Client;
using ModelContextProtocol;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Net.Http.Json;
using System.ServiceModel.Channels;
using System.Text.Json;

namespace DataverseMcpServer.Core.Tools;

[McpServerToolType]
public sealed class DataverseTool
{
    private static readonly TimeSpan _defaultCachingDuration = TimeSpan.FromMinutes(2);

    [McpServerTool, Description("Get metadata for all tables in Dataverse.")]
    public static async Task<string> GetMetadataForAllTables(
        Sql4CdsConnection sql4cdsConnection,
        IMemoryCache cache,
        [Description(@"The metadata columns to retrieve e.g. [""metadataid"", ""logicalname""")] string[] metadataFieldNames,
        [Description("Condition to filter down the table metadata e.g. isactivity = 1 AND islogicalentity = 1")] string? conditions)
    {
        var cacheKey = $"GetMetadataForAllTables_{string.Join(",", metadataFieldNames)}_{conditions}";
        if (cache.TryGetValue(cacheKey, out string? cachedResult)) return cachedResult!;

        var query = metadataFieldNames.Length > 0 ? $"SELECT {string.Join(",", metadataFieldNames)} FROM metadata.entity" : $"SELECT * FROM metadata.entity";
        if (!string.IsNullOrEmpty(conditions))
        {
            query += $" WHERE ({conditions.ToLower()})";
        }
        var result = await ExecuteSQL(query, sql4cdsConnection);
        cache.Set(cacheKey, result, _defaultCachingDuration);
        return result;
    }

    [McpServerTool, Description("Get metadata for a specific table.")]
    public static async Task<string> GetMetadataByTableName(
        Sql4CdsConnection sql4cdsConnection,
        IMemoryCache cache,
        [Description("The table's logical name e.g. contact, account")] string tableName,
        [Description(@"The metadata columns to retrieve e.g. [""metadataid"", ""logicalname""")] string[] metadataFieldNames)
    {
        var cacheKey = $"GetMetadataByTableName_{tableName}_{string.Join(",", metadataFieldNames)}";
        if (cache.TryGetValue(cacheKey, out string? cachedResult)) return cachedResult!;

        var query = metadataFieldNames.Length > 0 ? $"SELECT {string.Join(",", metadataFieldNames)} FROM metadata.entity" : $"SELECT * FROM metadata.entity";
        var result = await ExecuteSQL($"{query} WHERE logicalname = '{tableName}'", sql4cdsConnection);
        cache.Set(cacheKey, result, _defaultCachingDuration);
        return result;
    }

    [McpServerTool, Description("Get metadata for fields in a specific table.")]
    public static async Task<string> GetFieldMetadataByTableName(
        Sql4CdsConnection sql4cdsConnection,
        IMemoryCache cache,
        [Description("The table's logical name e.g. contact, account")] string tableName,
        [Description(@"The metadata columns to retrieve e.g. [""metadataid"", ""isvalidforread""")] string[] metadataFieldNames,
        [Description("Condition to filter down the attribute metadata e.g. isfilterable = 1 AND isvalidforupdate = 1")] string? conditions)
    {
        var cacheKey = $"GetFieldMetadataByTableName_{tableName}_{string.Join(",", metadataFieldNames)}_{conditions}";
        if (cache.TryGetValue(cacheKey, out string? cachedResult)) return cachedResult!;

        var query = metadataFieldNames.Length > 0 ? $"SELECT {string.Join(",", metadataFieldNames)} FROM metadata.attribute" : $"SELECT * FROM metadata.attribute";
        query += $" WHERE attribute.entitylogicalname = '{tableName}'";
        if (!string.IsNullOrEmpty(conditions))
        {
            query += $" AND ({conditions.ToLower()})";
        }
        var result = await ExecuteSQL(query, sql4cdsConnection);
        cache.Set(cacheKey, result, _defaultCachingDuration);
        return result;
    }

    [McpServerTool, Description("Retrieve rows for a specific table.")]
    public static async Task<string> GetRowsForTable(
        Sql4CdsConnection sql4cdsConnection,
        [Description("The table's logical name e.g. contact, account")] string tableName,
        [Description(@"The field names to retrieve from the table e.g. [""contactid"", ""fullname""")] string[] fieldNames,
        [Description("Condition to filter down the table")] string? conditions,
        [Description("The sort order for the results e.g. fullname DESC.)")] string? sortOrder,
        [Description("The number of rows to retrieve. Defaults to 50.")] int? rowCount = 50)
    {
        var query = fieldNames.Length > 0 ? $"SELECT TOP({rowCount}) {string.Join(",", fieldNames)} FROM dbo.{tableName}" : $"SELECT TOP({rowCount}) * FROM dbo.{tableName}";
        if (!string.IsNullOrEmpty(conditions))
        {
            query += $" WHERE ({conditions})";
        }
        if (!string.IsNullOrEmpty(sortOrder))
        {
            query += $" ORDER BY {sortOrder}";
        }
        var result = await ExecuteSQL(query, sql4cdsConnection);
        return result;
    }

    [McpServerTool, Description("Executes an SQL query.")]
    public static async Task<string> ExecuteSQL(
        Sql4CdsConnection sql4cdsConnection,
        [Description("The SQL query to execute. Must be only SELECT.")] string sqlQuery)
    {
        if (!sqlQuery.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
        {
            throw new McpException("Only SELECT statements are allowed.");
        }
        var result = await ExecuteSQL(sqlQuery, sql4cdsConnection);
        return result;
    }

    [McpServerTool, Description("Convert FetchXml query to SQL query.")]
    public static async Task<string> ConvertFetchXmlToSql(
        Sql4CdsConnection sql4cdsConnection,
        [Description("FetchXml query")] string fetchXml)
    {
        var result = await ExecuteSQL($"SELECT Response FROM FetchXMLToSQL('{fetchXml}',0)", sql4cdsConnection);
        return result;
    }
    private static async Task<string> ExecuteSQL(string query, Sql4CdsConnection sql4cdsConnection)
    {
        using Sql4CdsCommand cmd = sql4cdsConnection.CreateCommand();
        cmd.CommandText = query;
        var table = new List<Dictionary<string, object>>();
        try
        {
            var reader = await cmd.ExecuteReaderAsync();
            int rowCount = 1;
            while (await reader.ReadAsync())
            {
                var rows = new Dictionary<string, object>();
                for (var i = 0; i < reader.FieldCount; i++)
                {
                    if (i == 0)
                        rows["#"] = rowCount++;
                    rows[reader.GetName(i) ?? $"column_{i + 1}"] = reader.GetValue(i);
                }
                table.Add(rows);
            }
            var result = JsonSerializer.Serialize(table, options: new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            return $"""
            <environment>
                https://{sql4cdsConnection.DataSource}
            </environment>
            <json_output>
                {result}
            </json_output>
            """;
        }
        catch (Sql4CdsException ex)
        {
            return $"""
            <error>
                {ex.Message}
            </error>
            """;
        }
        catch (Exception ex)
        {
            return $"""
            <error>
                {ex.Message}
            </error>
            """;
        }
    }
}