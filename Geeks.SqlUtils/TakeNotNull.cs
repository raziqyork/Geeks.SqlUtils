//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Linq;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString TakeNotNull2(
        SqlInt32 index,
        SqlString text0,
        SqlString text1)
    {
        return TakeNotNull(index, text0, text1);
    }

    [SqlFunction]
    public static SqlString TakeNotNull3(
        SqlInt32 index,
        SqlString text0,
        SqlString text1,
        SqlString text2)
    {
        return TakeNotNull(index, text0, text1, text2);
    }

    [SqlFunction]
    public static SqlString TakeNotNull4(
        SqlInt32 index,
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3)
    {
        return TakeNotNull(index, text0, text1, text2, text3);
    }

    [SqlFunction]
    public static SqlString TakeNotNull5(
        SqlInt32 index,
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4)
    {
        return TakeNotNull(index, text0, text1, text2, text3, text4);
    }

    [SqlFunction]
    public static SqlString TakeNotNull6(
        SqlInt32 index,
        SqlString text0,
        SqlString text1,
        SqlString text2,
        SqlString text3,
        SqlString text4,
        SqlString text5)
    {
        return TakeNotNull(index, text0, text1, text2, text3, text4, text5);
    }

    private static SqlString TakeNotNull(SqlInt32 index, params SqlString[] texts)
    {
        var nonNull = texts.Where(t => !t.IsNull); // && !String.IsNullOrWhiteSpace(t.Value));

        var selected = nonNull.ElementAtOrDefault(index.Value);

        return selected;
    }
}
