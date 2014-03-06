//------------------------------------------------------------------------------
// <copyright file="CSSqlAggregate.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

[Serializable]
[SqlUserDefinedAggregate(
    Format.Native,
    IsInvariantToDuplicates = false, // receiving the same value again 

    // changes the result
    IsInvariantToNulls = false,      // receiving a NULL value changes the result
    IsInvariantToOrder = true,       // the order of the values doesn't 

    // affect the result
    IsNullIfEmpty = false,           // if no values are given the result is null
    Name = "HasAny"                  // name of the aggregate
)]
public struct HasAny
{
    private bool result;

    public void Init()
    {
        result = false;
    }

    public void Accumulate(SqlString value, SqlString comparison)
    {
        result = result || SqlString.Equals(value, comparison).Value;
    }

    public void Merge(HasAny group)
    {
        result = result || group.result;
    }

    public SqlBoolean Terminate()
    {
        return new SqlBoolean(result);
    }
}
