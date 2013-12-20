//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString TrimSpace(SqlString text)
    {
        if (text.IsNull || text.Value == null) return SqlString.Null;

        return new SqlString(text.Value.Trim(), text.LCID);
    }

    [SqlFunction]
    public static SqlString Trim(SqlString text, SqlString trimText)
    {
        if (text.IsNull || text.Value == null) return SqlString.Null;

        if (trimText.IsNull || trimText.Value == null) return text;

        return new SqlString(text.Value.Trim(trimText.Value.ToCharArray()), text.LCID);
    }

    [SqlFunction]
    public static SqlString TrimStartText(SqlString text, SqlString trimText)
    {
        if (text.IsNull || text.Value == null) return SqlString.Null;

        if (trimText.IsNull || trimText.Value == null) return text;

        return new SqlString(text.Value.TrimStart(trimText.Value.ToCharArray()), text.LCID);
    }

    [SqlFunction]
    public static SqlString TrimEndText(SqlString text, SqlString trimText)
    {
        if (text.IsNull || text.Value == null) return SqlString.Null;

        if (trimText.IsNull || trimText.Value == null) return text;

        return new SqlString(text.Value.TrimEnd(trimText.Value.ToCharArray()), text.LCID);
    }
}
