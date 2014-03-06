using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString ToSafeFileName(SqlString text)
    {
        return ToSafeFileNameWith(text, new SqlChars(new[] { '_' }));
    }

    [SqlFunction]
    public static SqlString ToSafeFileNameWith(SqlString text, SqlChars substitution)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;

        var textValue = text.Value.Trim();

        var substitutionChar = substitution.Value[0];

        var fileName = Path.GetInvalidFileNameChars().Aggregate(textValue, (current, c) => current.Replace(c, substitutionChar));

        return new SqlString(fileName, text.LCID);
    }
}
