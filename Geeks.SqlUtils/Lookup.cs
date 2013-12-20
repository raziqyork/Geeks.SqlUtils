//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.SqlServer.Server;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;

public partial class UserDefinedFunctions
{
    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static object Lookup(object param, SqlString lookupQuery)
    {
        if (lookupQuery.IsNull || String.IsNullOrWhiteSpace(lookupQuery.Value))
            throw new ArgumentNullException("lookupQuery");

        var pparam = SqlValue.From(param);
        if (pparam == null || pparam.Value == null) return null;

        using (var conn = new SqlConnection("context connection=true"))
        using (var cmd = new SqlCommand(lookupQuery.Value, conn) { CommandType = CommandType.Text })
        {
            cmd.Parameters.Add(new SqlParameter("@value", pparam.Value));
            conn.Open();

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var response = reader[0];
                    return response;
                }
            }

            conn.Close();
        }

        return null;
    }

    [SqlFunction]
    public static SqlGuid LookupGuid(SqlString param, SqlString lookupValues)
    {
        var found = LookupValue(param, lookupValues);
        Guid guid;
        return Guid.TryParse(found, out guid) ? new SqlGuid(guid) : SqlGuid.Null;
    }

    [SqlFunction]
    public static SqlInt32 LookupInt(SqlString param, SqlString lookupValues)
    {
        var found = LookupValue(param, lookupValues);
        int guid;
        return Int32.TryParse(found, out guid) ? new SqlInt32(guid) : SqlInt32.Null;
    }

    private static string LookupValue(SqlString param, SqlString lookupValues)
    {
        if (lookupValues.IsNull || String.IsNullOrWhiteSpace(lookupValues.Value))
            throw new ArgumentNullException("lookupValues");

        if (param.IsNull || param.Value == null) return null;

        var lookupTable = lookupValues.Value.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(e => e.Split(':'));

        var value = param.Value;

        var query = from entry in lookupTable
                    where value.Equals(entry[0].Trim(), StringComparison.OrdinalIgnoreCase)
                    select entry[1].Trim();

        return query.FirstOrDefault();
    }
}
