using MarkMpn.Sql4Cds.Engine;
using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Data.Common;
using System.Drawing;
using System.Net.Http.Json;
using System.Text.Json;

namespace DataverseMcpServer.Tools;

[McpServerToolType]
public static class DataverseTool
{
    [McpServerTool, Description("Query for a specific table metadata in Dataverse without the fields.")]
    public static async Task<string> GetDataverseTables(
        Sql4CdsConnection sql4cdsConnection,
        [Description("The table's logical name e.g. contact, account")] string tableName)
    {
        var result = await ExecuteSQL($"SELECT * FROM metadata.entity where logicalname = '{tableName}'", sql4cdsConnection);
        return result;
    }

    [McpServerTool, Description("Query for a specific table metadata in Dataverse including the metadata for the fields.")]
    public static async Task<string> GetDataverseTableWithFields(
        Sql4CdsConnection sql4cdsConnection,
        [Description("The table's logical name e.g. contact, account")] string tableName)
    {
        var query = $"""
        SELECT 
        entity.logicalname AS table_logicalname, entity.displayname AS table_displayname, entity.displaycollectionname AS table_displaycollectionname, 
        entity.isactivity AS table_isactivityname, entity.iscustomentity AS table_iscustomentityname, 
        entity.iscustomizable AS table_iscustomizable, entity.description AS table_description, entity.entitysetname AS table_entitysetname, 
        entity.dayssincerecordlastmodified AS table_dayssincerecordlastmodified, entity.objecttypecode AS table_objecttypecode, 
        entity.createdon AS table_createdon, entity.modifiedon AS table_modifiedon, entity.metadataid AS table_metadataid, 
        entity.tabletype AS table_tabletype, entity.primaryidattribute AS table_primaryidattribute, entity.primarynameattribute AS table_primarynameattribute,
        attribute.attributetypename AS attribute_attributetypename, attribute.columnnumber AS attribute_columnnumber, attribute.logicalname AS attribute_logicalname, 
        attribute.displayname AS attribute_displayname, attribute.iscustomizable AS attribute_iscustomizable, attribute.iscustomattribute AS attribute_iscustomattribute, 
        attribute.isauditenabled AS attribute_isauditenabled, attribute.islogical AS attribute_islogical, attribute.ismanaged AS attribute_ismanaged, 
        attribute.isprimaryname AS attribute_isprimaryname, attribute.isprimaryid AS attribute_isprimaryid, attribute.isauditenabled AS attribute_isauditenabled, 
        attribute.isretrievable AS attribute_isretrievable, attribute.isvalidforcreate AS attribute_isvalidforcreate, attribute.isvalidforread AS attribute_isvalidforread, 
        attribute.isvalidforupdate AS attribute_isvalidforupdate
        FROM metadata.entity
        JOIN metadata.attribute ON entity.logicalname = attribute.entitylogicalname
        WHERE entity.logicalname = '{tableName}'
        """;
        var result = await ExecuteSQL(query, sql4cdsConnection);
        return result;
    }

    [McpServerTool, Description("Retrieve rows for a specific table.")]
    public static async Task<string> GetRowsForTable(
        Sql4CdsConnection sql4cdsConnection,
        [Description("The table's logical name e.g. contact, account")] string tableName,
        [Description(@"The field names to retrieve from the table e.g. [""contactid"", ""fullname""")] string[] fieldNames,
        [Description("The sort order for the results e.g. fullname DESC. Defaults to modifiedon DESC)")] string sortOrder = "modifiedon DESC",
        [Description("The number of rows to retrieve. Defaults to 500.")] int? rowCount= 500)
    {
        var result = await ExecuteSQL($"SELECT TOP({rowCount}) {string.Join(",", fieldNames)} FROM dbo.{tableName} ORDER BY {sortOrder}", sql4cdsConnection);
        return result;
    }

    private static async Task<string> ExecuteSQL(string query, Sql4CdsConnection sql4cdsConnection)
    {
        var cmd = sql4cdsConnection.CreateCommand();
        cmd.CommandText = query;
        var table = new List<Dictionary<string, object>>();
        var reader = await cmd.ExecuteReaderAsync();
        int rowCount = 1;
        while (await reader.ReadAsync())
        {
            var rows = new Dictionary<string, object>();
            for (var i = 0; i < reader.FieldCount; i++)
            {
                if (i == 0)
                    rows["#"] = rowCount++;
                rows[reader.GetName(i)] = reader.GetValue(i);
            }
            table.Add(rows);
        }
        return JsonSerializer.Serialize(table, options: new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}