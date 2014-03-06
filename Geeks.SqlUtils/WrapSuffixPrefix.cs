using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString Prefix(SqlString text, SqlString prefix)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;

        var textValue = text.Value;
        var prefixValue = prefix.Value;
        var result = prefixValue + textValue;

        return new SqlString(result, text.LCID);
    }

    [SqlFunction]
    public static SqlString Suffix(SqlString text, SqlString suffix)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;

        var textValue = text.Value;
        var suffixValue = suffix.Value;
        var result = textValue + suffixValue;

        return new SqlString(result, text.LCID);
    }

    [SqlFunction]
    public static SqlString Wrap(SqlString text, SqlString prefix, SqlString suffix)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;

        var textValue = text.Value;
        var prefixValue = prefix.Value;
        var suffixValue = suffix.Value;
        var result = prefixValue + textValue + suffixValue;

        return new SqlString(result, text.LCID);
    }
}
