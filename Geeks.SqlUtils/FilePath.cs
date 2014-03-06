using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.IO;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString FileExtension(SqlString text)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;
        return new SqlString(Path.GetExtension(text.Value), text.LCID);
    }

    [SqlFunction]
    public static SqlString FileNameWithoutExtension(SqlString text)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;
        return new SqlString(Path.GetFileNameWithoutExtension(text.Value), text.LCID);
    }

    [SqlFunction]
    public static SqlString DirectoryName(SqlString text)
    {
        if (text.IsNull || String.IsNullOrWhiteSpace(text.Value)) return SqlString.Null;
        return new SqlString(Path.GetDirectoryName(text.Value), text.LCID);
    }
}
