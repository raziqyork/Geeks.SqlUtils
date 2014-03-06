//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System.Text.RegularExpressions;
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
        int value;
        return Int32.TryParse(found, out value) ? new SqlInt32(value) : SqlInt32.Null;
    }

    [SqlFunction]
    public static SqlString LookupText(SqlString param, SqlString lookupValues)
    {
        var found = LookupValue(param, lookupValues);
        return !String.IsNullOrWhiteSpace(found)
            ? new SqlString(found, param.LCID)
            : SqlString.Null;
    }

    static readonly Regex NullValuePattern = new Regex(@"\|\s*\*\s*$", RegexOptions.Compiled | RegexOptions.Singleline);
    static readonly Regex NullSubstPattern = new Regex(@"\*NULL\*", RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.Singleline);

    private static string LookupValue(SqlString param, SqlString lookupValues)
    {
        if (lookupValues.IsNull || String.IsNullOrWhiteSpace(lookupValues.Value))
            throw new ArgumentNullException("lookupValues");

        var values = lookupValues.Value;

        string paramValue;
        if (param.IsNull || String.IsNullOrWhiteSpace(param.Value))
        {
            if (!NullSubstPattern.IsMatch(values)) return null;
            paramValue = "*NULL*";
        }
        else
        {
            paramValue = param.Value.Trim();
        }

        var match = NullValuePattern.Match(values);
        var returnParamIfNull = match.Success;
        if (returnParamIfNull) values = values.Substring(0, match.Index);

        var lookupTable = values.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(e => new Pair(e));

        var query = from entry in lookupTable
                    where entry.IsKey(paramValue)
                    select entry.Value;

        //var lookupTable = values.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries)
        //    .Select(e => e.Split(':'));

        //var query = from entry in lookupTable
        //            where paramValue.Equals(entry[0].Trim(), StringComparison.OrdinalIgnoreCase)
        //            select entry[1].Trim();

        var found = query.FirstOrDefault();
        if (found != null) return found;

        if (returnParamIfNull)
        {
            found = paramValue;
        }
        else
        {
            var entry = lookupTable.Last();
            if (entry.IsKey("*"))
            {
                found = entry.Value;
            }
        }
        return found;
    }

    private struct Pair
    {
        public string Key;

        public string Value;

        static readonly Regex Pattern = new Regex(@"^\s*(.+)\s*:\s*(.+)\s*$", RegexOptions.Compiled | RegexOptions.Singleline);

        public bool IsKey(string key)
        {
            return (key == null && Key == null) || Key.Equals(key, StringComparison.OrdinalIgnoreCase);
        }

        public Pair(string raw)
        {
            Key = null;
            Value = null;

            var match = Pattern.Match(raw);
            if (!match.Success) return;

            Key = match.Groups[1].Value;

            var group2 = match.Groups[2];
            if (group2.Success) Value = group2.Value;
        }
    }
}
