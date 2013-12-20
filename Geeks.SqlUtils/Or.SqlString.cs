//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString OrNull(SqlString text)
    {
        return !text.IsNull && !String.IsNullOrWhiteSpace(text.Value) ? text : SqlString.Null;
    }

    [SqlFunction]
    public static SqlString Or(
        SqlString text0,
        SqlString text1)
    {
        return OrSqlString(text0, text1);
    }

    [SqlFunction]
    public static SqlString Or3(
        SqlString text0,
        SqlString text1,
        SqlString text2)
    {
        return OrSqlString(text0, text1, text2);
    }

    [SqlFunction]
    public static SqlString Or4(
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3)
    {
        return OrSqlString(text0, text1, text2, text3);
    }

    [SqlFunction]
    public static SqlString Or5(
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4)
    {
        return OrSqlString(text0, text1, text2, text3, text4);
    }

    private static SqlString OrSqlString(params SqlString[] texts)
    {
        foreach (var text in texts)
        {
            if (text == null) continue;
            if (text.IsNull) continue;
            if (String.IsNullOrWhiteSpace(text.Value)) continue;
            return text;
        }
        //if (text != null && !text.IsNull && !String.IsNullOrWhiteSpace(text.Value)) // doesn't work
        //    return text;

        return SqlString.Null;
    }
}
