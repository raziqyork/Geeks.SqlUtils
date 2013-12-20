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
    public static SqlDateTime CombineDateTime(SqlDateTime dateComponent, SqlDateTime timeComponent)
    {
        if (dateComponent.IsNull) return SqlDateTime.Null;

        var date = dateComponent.Value;

        if (!timeComponent.IsNull)
        {
            date = date.Add(timeComponent.Value.TimeOfDay);
        }

        return new SqlDateTime(date);
    }
}
