using System;

namespace SSDEEP.NET
{
    [Flags]
    public enum FuzzyHashMode
    {
        None = 0,
        ///<summary>Eliminate sequences of more than three identical characters</summary>
        EliminateSequences = 1,
        ///<summary>Do not truncate the second part to SPAMSUM_LENGTH/2 characters</summary>
        DoNotTruncate = 2
    }
}