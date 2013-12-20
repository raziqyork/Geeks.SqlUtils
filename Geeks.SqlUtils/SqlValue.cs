//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

/// <summary>
/// Utility intermediary class for extracting values from parameters
/// </summary>
class SqlValue
{
    public object Value { get; set; }

    public SqlValue()
    {
    }

    private SqlValue(object value, bool isNull)
    {
        if (!isNull) this.Value = value;
    }

    public static SqlValue From(object sqlValue)
    {
        if (sqlValue == null) return new SqlValue();

        if (sqlValue is SqlString) return (SqlString)sqlValue;
        if (sqlValue is SqlInt16) return (SqlInt16)sqlValue;
        if (sqlValue is SqlInt32) return (SqlInt32)sqlValue;
        if (sqlValue is SqlInt64) return (SqlInt64)sqlValue;
        if (sqlValue is SqlSingle) return (SqlSingle)sqlValue;
        if (sqlValue is SqlMoney) return (SqlMoney)sqlValue;
        if (sqlValue is SqlDouble) return (SqlDouble)sqlValue;
        if (sqlValue is SqlDecimal) return (SqlDecimal)sqlValue;
        if (sqlValue is SqlDateTime) return (SqlDateTime)sqlValue;
        if (sqlValue is SqlGuid) return (SqlGuid)sqlValue;
        if (sqlValue is SqlByte) return (SqlByte)sqlValue;
        if (sqlValue is SqlBinary) return (SqlBinary)sqlValue;
        if (sqlValue is SqlChars) return (SqlChars)sqlValue;
        if (sqlValue is SqlBytes) return (SqlBytes)sqlValue;
        if (sqlValue is SqlXml) return (SqlXml)sqlValue;

        throw new NotSupportedException();
    }

    public static implicit operator SqlValue(SqlString value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlInt16 value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlInt32 value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlInt64 value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlSingle value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlMoney value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlDouble value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlDecimal value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlDateTime value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlGuid value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlByte value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlBinary value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlChars value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlBytes value) { return new SqlValue(value.Value, value.IsNull); }
    public static implicit operator SqlValue(SqlXml value) { return new SqlValue(value.Value, value.IsNull); }
}
