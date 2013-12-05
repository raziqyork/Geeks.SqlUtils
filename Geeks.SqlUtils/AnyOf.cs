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
    public static SqlString AnyOf2(
        SqlString text0,
        SqlString text1)
    {
        return AnyOf(text0, text1);
    }

    [SqlFunction]
    public static SqlString AnyOf3(
        SqlString text0,
        SqlString text1,
        SqlString text2)
    {
        return AnyOf(text0, text1, text2);
    }

    [SqlFunction]
    public static SqlString AnyOf4(
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3)
    {
        return AnyOf(text0, text1, text2, text3);
    }

    [SqlFunction]
    public static SqlString AnyOf5(
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4)
    {
        return AnyOf(text0, text1, text2, text3, text4);
    }

    private static SqlString AnyOf(params SqlString[] texts)
    {
        foreach (var text in texts)
            if (text != null && !text.IsNull && !String.IsNullOrWhiteSpace(text.Value))
                return text;

        return SqlString.Null;
    }
}
