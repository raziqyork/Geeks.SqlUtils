using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString ToTitleCase(SqlString text)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;

        var lowerCase = text.Value.ToLower().Trim();

        var result = System.Threading.Thread.CurrentThread
            .CurrentCulture
            .TextInfo
            .ToTitleCase(lowerCase);

        return new SqlString(result, text.LCID);
    }
}
