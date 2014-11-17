//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Data.SqlTypes;
using Geeks.SqlUtils;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    [SqlFunction(DataAccess = DataAccessKind.Read)]
    public static SqlInt32 DistanceBetween(SqlString text, SqlString compText)
    {
        if (text == null || text.IsNull) return SqlInt32.Null;

        if (compText == null || compText.IsNull) return new SqlInt32(text.Value.Length);

        //int distance = new Levenshtein().iLD(text.Value, compText.Value);
        int distance = CalculateLevenshteinDistance(text.Value, compText.Value);

        return new SqlInt32(distance);
    }

    private static Int32 CalculateLevenshteinDistance(String a, String b)
    {
        if (String.IsNullOrEmpty(a))
        {
            return String.IsNullOrEmpty(b) ? 0 : b.Length;
        }

        if (String.IsNullOrEmpty(b))
        {
            return String.IsNullOrEmpty(a) ? 0 : a.Length;
        }

        var d = new int[a.Length + 1, b.Length + 1];

        for (var i = 0; i <= d.GetUpperBound(0); i += 1)
        {
            d[i, 0] = i;
        }

        for (var i = 0; i <= d.GetUpperBound(1); i += 1)
        {
            d[0, i] = i;
        }

        for (var i = 1; i <= d.GetUpperBound(0); i += 1)
        {
            for (var j = 1; j <= d.GetUpperBound(1); j += 1)
            {
                var cost = Convert.ToInt32(a[i - 1] != b[j - 1]);

                var min1 = d[i - 1, j] + 1;
                var min2 = d[i, j - 1] + 1;
                var min3 = d[i - 1, j - 1] + cost;
                d[i, j] = Math.Min(Math.Min(min1, min2), min3);
            }
        }

        return d[d.GetUpperBound(0), d.GetUpperBound(1)];

    }
}