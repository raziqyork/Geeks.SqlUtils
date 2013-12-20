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
    public static SqlGuid OrGuid(
        SqlGuid value0,
        SqlGuid value1)
    {
        return OrSqlGuid(value0, value1);
    }

    [SqlFunction]
    public static SqlGuid OrGuid3(
        SqlGuid value0,
        SqlGuid value1,
        SqlGuid value2)
    {
        return OrSqlGuid(value0, value1, value2);
    }

    [SqlFunction]
    public static SqlGuid OrGuid4(
        SqlGuid value0,
        SqlGuid value1,
        SqlGuid value2,
        SqlGuid value3)
    {
        return OrSqlGuid(value0, value1, value2, value3);
    }

    [SqlFunction]
    public static SqlGuid OrGuid5(
        SqlGuid value0,
        SqlGuid value1,
        SqlGuid value2,
        SqlGuid value3,
        SqlGuid value4)
    {
        return OrSqlGuid(value0, value1, value2, value3, value4);
    }

    private static SqlGuid OrSqlGuid(params SqlGuid[] values)
    {
        foreach (var value in values.Where(value => !value.IsNull))
        {
            return value;
        }

        return SqlGuid.Null;
    }
}
