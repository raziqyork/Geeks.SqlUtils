//------------------------------------------------------------------------------
// <copyright file="CSSqlClassFile.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>
//------------------------------------------------------------------------------
using System;
using Microsoft.SqlServer.Server;

/// <summary>
/// Concatenates the strings with a given delimiter
/// </summary>
[Serializable]
[SqlUserDefinedAggregate(
   Format.UserDefined,
   IsInvariantToDuplicates = false, // Receiving the same value again 
    // changes the result
   IsInvariantToNulls = false,      // Receiving a NULL value changes the result
   IsInvariantToOrder = false,      // The order of the values affects the result
   IsNullIfEmpty = true,            // If no values are given the result is null
   MaxByteSize = -1,                // Maximum size of the aggregate instance. 
    // -1 represents a value larger than 8000 bytes,
    // up to 2 gigabytes
   Name = "Concatenate"             // Name of the aggregate
)]
public struct Concatenate : IBinarySerialize
{
    /// <summary>
    /// Used to store the concatenated string
    /// </summary>
    public System.Text.StringBuilder Result { get; private set; }

    /// <summary>
    /// Used to store the delimiter
    /// </summary>
    public System.Data.SqlTypes.SqlString Delimiter { get; private set; }

    /// <summary>
    /// Used to inform if the string has a value
    /// </summary>
    public bool HasValue { get; private set; }

    /// <summary>
    /// Used to inform if the string is NULL
    /// </summary>
    public bool IsNull { get; private set; }

    /// <summary>
    /// Is the concatenation resulting a NULL if some of the values contain NULL
    /// </summary>
    public bool NullYieldsToNull { get; private set; }

    /// <summary>
    /// Initializes a new Concatenate for a group
    /// </summary>
    public void Init()
    {
        this.Result = new System.Text.StringBuilder("");
        this.HasValue = false;
        this.IsNull = false;
    }

    /// <summary>
    /// Inserts a new string into the existing already concatenated string
    /// </summary>
    /// <param name="stringval">Value to include</param>
    /// <param name="delimiter">Delimiter to use</param>
    /// <param name="nullYieldsToNull">Is the concatenation resulting a NULL 
    ///                                if some of the values contain NULL</param>
    public void Accumulate(System.Data.SqlTypes.SqlString stringval,
                           System.Data.SqlTypes.SqlString delimiter,
                           System.Data.SqlTypes.SqlBoolean nullYieldsToNull)
    {
        if (!this.HasValue)
        {
            // if this is the first value received
            if (nullYieldsToNull && stringval.IsNull)
            {
                this.IsNull = true;
            }
            else if (stringval.IsNull)
            {
            }
            else
            {
                this.Result.Append(stringval.Value);
            }
            this.Delimiter = delimiter;
            this.NullYieldsToNull = nullYieldsToNull.Value;
        }
        else if (this.IsNull && nullYieldsToNull.Value)
        {
            //if the concatenated value is null, stay that way
        }
        else if (stringval.IsNull && nullYieldsToNull.Value)
        {
            //if the value is null the result is null
            this.IsNull = true;
        }
        else
        {
            //concatenate the values (only if the new value is not null)
            if (!stringval.IsNull)
            {
                this.Result.AppendFormat("{0}{1}", delimiter.Value, stringval.Value);
            }
        }
        // true if a value has already been set or the string to be added is not null
        this.HasValue = this.HasValue ||
          !(stringval.IsNull && !nullYieldsToNull.Value);
    }

    /// <summary>
    /// Merges this group to another group instantiated for the concatenation
    /// </summary>
    /// <param name="group"></param>
    public void Merge(Concatenate group)
    {
        // Merge only if the group has a value
        if (group.HasValue)
        {
            this.Accumulate(group.Result.ToString(),
           this.Delimiter, this.NullYieldsToNull);
        }
    }

    /// <summary>
    /// Ends the operation and returns the result
    /// </summary>
    /// <returns></returns>
    public System.Data.SqlTypes.SqlString Terminate()
    {
        return this.IsNull ? System.Data.SqlTypes.SqlString.Null :
                      this.Result.ToString();
    }

    #region IBinarySerialize
    /// <summary>
    /// Writes the values to the stream in order to be stored
    /// </summary>
    /// <param name="writer">The BinaryWriter stream</param>
    public void Write(System.IO.BinaryWriter writer)
    {
        writer.Write(this.Result.ToString());
        writer.Write(this.Delimiter.Value);
        writer.Write(this.HasValue);
        writer.Write(this.NullYieldsToNull);
        writer.Write(this.IsNull);
    }

    /// <summary>
    /// Reads the values from the stream
    /// </summary>
    /// <param name="reader">The BinaryReader stream</param>
    public void Read(System.IO.BinaryReader reader)
    {
        this.Result = new System.Text.StringBuilder(reader.ReadString());
        this.Delimiter = new System.Data.SqlTypes.SqlString(reader.ReadString());
        this.HasValue = reader.ReadBoolean();
        this.NullYieldsToNull = reader.ReadBoolean();
        this.IsNull = reader.ReadBoolean();
    }
    #endregion IBinarySerialize
}