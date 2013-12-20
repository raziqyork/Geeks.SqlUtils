//------------------------------------------------------------------------------
// <copyright file="CSSqlFunction.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using Microsoft.SqlServer.Server;
using System;
using System.Data.SqlTypes;
using System.Text.RegularExpressions;

public partial class UserDefinedFunctions
{
    [SqlFunction]
    public static SqlString RegexMatch(SqlString text, SqlString pattern, SqlInt16 groupIndex)
    {
        var patternValue = pattern.IsNull ? null : pattern.Value;
        if (patternValue == null)
            throw new ArgumentNullException("pattern");

        var textValue = text.IsNull ? null : text.Value;
        if (textValue == null) return SqlString.Null;

        var groupIndexValue = groupIndex.IsNull ? 0 : groupIndex.Value;

        var match = Regex.Match(text.Value, pattern.Value, RegexOptions.Compiled /*| RegexOptions.IgnoreCase*/);
        if (!match.Success) return SqlString.Null;

        var group = match.Groups[groupIndexValue];
        if (!group.Success) return SqlString.Null;

        return new SqlString(group.Value, text.LCID);
    }

    [SqlFunction]
    public static SqlBoolean RegexIsMatch(SqlString text, SqlString pattern /*, SqlInt16 groupIndex*/)
    {
        var patternValue = pattern.IsNull ? null : pattern.Value;
        if (patternValue == null)
            throw new ArgumentNullException("pattern");

        var textValue = text.IsNull ? null : text.Value;
        if (textValue == null) return SqlBoolean.False;

        var isMatch = Regex.IsMatch(text.Value, pattern.Value, RegexOptions.Compiled /*| RegexOptions.IgnoreCase*/);
        return new SqlBoolean(isMatch);

        //var groupIndexValue = groupIndex.IsNull ? 0 : groupIndex.Value;

        //var match = Regex.Match(text.Value, pattern.Value, RegexOptions.Compiled);
        //if (!match.Success) return SqlBoolean.False;

        //var group = match.Groups[groupIndexValue];
        //if (!group.Success) return SqlBoolean.False;

        //return SqlBoolean.True;
    }
}
