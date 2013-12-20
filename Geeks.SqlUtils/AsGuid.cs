using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlGuid ToGuidFromHash(SqlInt64 numTop, SqlString text)
    {
        if (numTop.IsNull || text.IsNull) return SqlGuid.Null;

        var hash = text.Value.GetHashCode();

        var guid = ConvertToGuid(numTop.Value, hash);

        return new SqlGuid(guid);
    }

    [SqlFunction]
    public static SqlGuid ToGuid(SqlInt64 numTop, SqlInt64 numBottom)
    {
        if (numTop.IsNull || numBottom.IsNull) return SqlGuid.Null;

        var guid = ConvertToGuid(numTop.Value, numBottom.Value);

        return new SqlGuid(guid);
    }

    [SqlFunction]
    public static SqlGuid AsGuid(SqlInt64 num)
    {
        return ToGuid(0L, num);
    }

    /// <summary>
    /// Converts two Int64 numbers into a Guid.
    /// </summary>
    private static Guid ConvertToGuid(long top, long bottom)
    {
        var bytes = new byte[16];

        BitConverter.GetBytes(top).CopyTo(bytes, 0);

        BitConverter.GetBytes(bottom).CopyTo(bytes, 8);

        var guid = new Guid(bytes);

        return guid;
    }

    //// Cannot use as affects code access permissions
    ////var guid = new DecimalGuidConverter { Decimal = Convert.ToDecimal(num.Value) }.Guid;
    //[StructLayout(LayoutKind.Explicit)]
    //private struct DecimalGuidConverter
    //{
    //    [FieldOffset(0)]
    //    public decimal Decimal;
    //
    //    [FieldOffset(0)]
    //    public Guid Guid;
    //}
}
