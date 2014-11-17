using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString Escaped(SqlString text, SqlChars escapeChar)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;

        if (escapeChar.IsNull || escapeChar.Length != 1)
            throw new SqlTypeException("Escape character length must be 1.");

        var wildcards = new[] {'%', '_', '[', ']'};
        char escapeCh = escapeChar.Value[0];

        var output = new StringBuilder();

        foreach (char ch in text.Value)
        {
            if (wildcards.Contains(ch))
                output.Append(escapeCh);
            output.Append(ch);
        }

        return new SqlString(output.ToString(), text.LCID);
    }
}