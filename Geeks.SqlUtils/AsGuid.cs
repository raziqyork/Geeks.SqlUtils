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
    public static SqlGuid ToGuid(SqlInt64 numTop, SqlInt64 numBottom)
    {
        if (numTop.IsNull || numBottom.IsNull) return SqlGuid.Null;

        var bytes = new byte[16];

        BitConverter.GetBytes(numTop.Value).CopyTo(bytes, 0);

        BitConverter.GetBytes(numBottom.Value).CopyTo(bytes, 8);

        var guid = new Guid(bytes);

        return new SqlGuid(guid);
    }

    [SqlFunction]
    public static SqlGuid AsGuid(SqlInt64 num)
    {
        return ToGuid(0L, num);
    }

    //// Cannot use as affects code access permissions
    ////var guid = new DecimalGuidConverter { Decimal = Convert.ToDecimal(num.Value) }.Guid;
    //[StructLayout(LayoutKind.Explicit)]
    //private struct DecimalGuidConverter
    //{
    //    [FieldOffset(0)]
    //    public decimal Decimal;

    //    [FieldOffset(0)]
    //    public Guid Guid;
    //}
}
