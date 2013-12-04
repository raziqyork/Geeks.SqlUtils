//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using Microsoft.SqlServer.Server;

public partial class UserDefinedFunctions
{
    /// <summary>
    /// Pattern matches the first sentence of a string, as well as subsequent sentences
    /// </summary>
    private static readonly Regex SentenceStartPattern = new Regex(@"(^[a-z])|\.\s+(.)",
        RegexOptions.ExplicitCapture | RegexOptions.Compiled);

    [SqlFunction]
    public static SqlString ToSentenceCase(SqlString text)
    {
        if (text.IsNull || text.Value == null) return SqlString.Null;

        var lowerCase = text.Value.ToLower();

        // MatchEvaluator delegate defines replacement of setence starts to uppercase
        var result = SentenceStartPattern.Replace(lowerCase, s => s.Value.ToUpper());

        return new SqlString(result);
    }
}
