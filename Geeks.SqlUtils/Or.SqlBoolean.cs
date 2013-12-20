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
    public static SqlInt32 OrInt(
        SqlInt32 value0,
        SqlInt32 value1)
    {
        return OrSqlInt32(value0, value1);
    }

    [SqlFunction]
    public static SqlInt32 OrInt3(
        SqlInt32 value0,
        SqlInt32 value1,
        SqlInt32 value2)
    {
        return OrSqlInt32(value0, value1, value2);
    }

    [SqlFunction]
    public static SqlInt32 OrInt4(
        SqlInt32 value0,
        SqlInt32 value1,
        SqlInt32 value2,
        SqlInt32 value3)
    {
        return OrSqlInt32(value0, value1, value2, value3);
    }

    [SqlFunction]
    public static SqlInt32 OrInt5(
        SqlInt32 value0,
        SqlInt32 value1,
        SqlInt32 value2,
        SqlInt32 value3,
        SqlInt32 value4)
    {
        return OrSqlInt32(value0, value1, value2, value3, value4);
    }

    private static SqlInt32 OrSqlInt32(params SqlInt32[] values)
    {
        foreach (var value in values.Where(value => !value.IsNull))
        {
            return value;
        }

        return SqlInt32.Null;
    }
}
