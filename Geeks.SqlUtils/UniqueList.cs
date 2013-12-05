//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString UniqueList2(
        SqlString text0,
        SqlString text1)
    {
        return UniqueList(text0, text1);
    }

    [SqlFunction]
    public static SqlString UniqueList3(
        SqlString text0,
        SqlString text1,
        SqlString text2)
    {
        return UniqueList(text0, text1, text2);
    }

    [SqlFunction]
    public static SqlString UniqueList4(
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3)
    {
        return UniqueList(text0, text1, text2, text3);
    }

    [SqlFunction]
    public static SqlString UniqueList5(
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4)
    {
        return UniqueList(text0, text1, text2, text3, text4);
    }

    private static SqlString UniqueList(params SqlString[] texts)
    {
        var unique = texts.Where(t => !t.IsNull && !String.IsNullOrWhiteSpace(t.Value))
            .Select(t => t.Value)
            .Distinct();

        var joined = String.Join("|", unique);
        return String.IsNullOrEmpty(joined) ? SqlString.Null : new SqlString(joined);
    }
}
