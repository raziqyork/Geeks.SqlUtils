//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString ToTitleCase(SqlString text)
    {
        if (text.IsNull || text.Value == null) return SqlString.Null;

        var lowerCase = text.Value.ToLower();

        var result = System.Threading.Thread.CurrentThread
            .CurrentCulture
            .TextInfo
            .ToTitleCase(lowerCase);

        return new SqlString(result, text.LCID);
    }
}
