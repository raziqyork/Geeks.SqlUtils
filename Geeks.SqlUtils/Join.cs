//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Linq;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString Join2(
        SqlString separator,
        SqlString text0,
        SqlString text1)
    {
        return JoinString(separator, text0, text1);
    }

    [SqlFunction]
    public static SqlString Join3(
        SqlString separator,
        SqlString text0,
        SqlString text1,
        SqlString text2)
    {
        return JoinString(separator, text0, text1, text2);
    }

    [SqlFunction]
    public static SqlString Join4(
        SqlString separator,
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3)
    {
        return JoinString(separator, text0, text1, text2, text3);
    }

    [SqlFunction]
    public static SqlString Join5(
        SqlString separator,
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4)
    {
        return JoinString(separator, text0, text1, text2, text3, text4);
    }

    [SqlFunction]
    public static SqlString Join6(
        SqlString separator,
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4,
        SqlString text5)
    {
        return JoinString(separator, text0, text1, text2, text3, text4, text5);
    }

    private static SqlString JoinString(SqlString separator, params SqlString[] texts)
    {
        var joined = String.Join(separator.Value,
            texts
                .Where(t => !t.IsNull)
                .Select(t => t.Value)
                .Where(s => !String.IsNullOrWhiteSpace(s)));

        return new SqlString(joined, separator.LCID);
    }
}
