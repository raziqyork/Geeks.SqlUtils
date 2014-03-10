//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Text;

public static class Extensions
{
    /// <summary>
    /// Negates the value of this SqlBoolean. Returns Null if Null.
    /// </summary>
    public static SqlBoolean Negate(this SqlBoolean value)
    {
        return value.IsNull ? SqlBoolean.Null : (value.IsTrue ? SqlBoolean.False : SqlBoolean.True);
    }
}
