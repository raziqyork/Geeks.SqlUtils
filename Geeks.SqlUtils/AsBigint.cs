//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Runtime.InteropServices;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlInt64 ToBigint(SqlString text)
    {
        long value;
        return !text.IsNull && !String.IsNullOrWhiteSpace(text.Value) && Int64.TryParse(text.Value.Trim(), out value)
            ? new SqlInt64(value)
            : SqlInt64.Null;
    }
}
