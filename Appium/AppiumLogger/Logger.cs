//-----------------------------------------------------------------------------
//
// company: Ladbrokes
//
// copyright: 
//
// summary:
//    The enums used by logger are defined here
//
// history:
//     March 22nd 2013   Karthikeyan   
//                                    
//-----------------------------------------------------------------------------
using System;

namespace AutonitroLogger
{

    #region Exceptions
    #endregion

    #region Enums

    //// -------------------------------------------------------------------
    /// <summary>
    /// Logger token set.
    /// </summary>
    /// <remarks></remarks>
    //// -------------------------------------------------------------------
    public enum LogTokens : int // System.Int32
    {
        /// <summary>
        /// Make OACR happy by adding this
        /// </summary>
        NA = 0,

        /// <summary>
        /// 
        /// </summary>
        Msg = 1,  // message

        /// <summary>
        /// 
        /// </summary>
        SId = 2,  // step id

        /// <summary>
        /// 
        /// </summary>
        TS = 3,  // time stamp

        /// <summary>
        /// 
        /// </summary>
        PId = 4,  // process id

        /// <summary>
        /// 
        /// </summary>
        TId = 5,  // thread id

        /// <summary>
        /// 
        /// </summary>
        Cat = 6,  // category

        /// <summary>
        /// 
        /// </summary>
        Mod = 7,  // module name

        /// <summary>
        /// 
        /// </summary>
        Res = 8,  // result type

        /// <summary>
        /// 
        /// </summary>
        Set = 9,  // variation set

        /// <summary>
        /// 
        /// </summary>
        Lvl = 10, // variation level

        /// <summary>
        /// 
        /// </summary>
        VId = 11, // variation id

        /// <summary>
        /// 
        /// </summary>
        Cid = 12, // custom id

        /// <summary>
        /// 
        /// </summary>
        PermId = 13, // permutation id

        /// <summary>
        /// 
        /// </summary>
        Mode = 14, // Manual or Auto

        /// <summary>
        /// 
        /// </summary>
        BugId = 15  // bug id
    }

    //-------------------------------------------------------------------
    /// <summary>
    /// Defines types of log output.
    /// </summary>
    /// <remarks></remarks>
    //-------------------------------------------------------------------
    [Flags]
    public enum LogTypes
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0x00,

        /// <summary>
        /// 
        /// </summary>
        Result = 0x01,

        /// <summary>
        /// 
        /// </summary>
        Comment = 0x02,

        /// <summary>
        /// 
        /// </summary>
        Exception = 0x04
    }

    //-------------------------------------------------------------------
    /// <summary>
    /// Defines log output destinations.
    /// </summary>
    /// <remarks></remarks>
    //-------------------------------------------------------------------
    [Flags]
    public enum OutputDestination
    {
        /// <summary>
        /// No output
        /// </summary>
        None = 0x00,
        /// <summary>
        /// Output is sent to debug output (can view using dbgview tool
        /// </summary>
        DbgOut = 0x01,

        /// <summary>
        /// Output is sent to standard output
        /// </summary>
        StdOut = 0x02,

        /// <summary>
        /// Output is sent to log file
        /// </summary>
        Log = 0x04,

        /// <summary>
        /// Output is sent to verbose log file
        /// </summary>
        VerboseLog = 0x08,

        /// <summary>
        /// Output is sent to XML log file 
        /// </summary>
        XmlLog = 0x10,

        /// <summary>
        /// 
        /// </summary>
        DB = 0x20
    }

    //-------------------------------------------------------------------
    /// <summary>
    /// Defines results of execution (groups, vars, framework).
    /// </summary>
    /// <remarks></remarks>
    //-------------------------------------------------------------------
    public enum ExeResult : int
    {
        /// <summary>
        /// 
        /// </summary>
        VAR_PASS = 1,  // success

        /// <summary>
        /// 
        /// </summary>
        VAR_FAIL = 2,  // variation failed

        /// <summary>
        /// 
        /// </summary>
        ABORT = 3,  // entire execution was aborted

        /// <summary>
        /// 
        /// </summary>
        VAR_ABORT = 4,  // execution of variation was aborted

        /// <summary>
        /// 
        /// </summary>
        GRP_ABORT = 5,  // group and all its children's execution was aborted

        /// <summary>
        /// 
        /// </summary>
        VAR_UNSUPPORTED = 6,

        /// <summary>
        /// 
        /// </summary>
        VAR_NOTRUN = 7
    }

    //-------------------------------------------------------------------
    /// <summary>
    /// Defines output categories used for tracing.
    /// Bits: 0-15 for framework use, bits: 16-31 for custom
    /// use/categories.
    /// </summary>
    /// <remarks>Defined categories are:
    /// Unknown, Always, Error, Warning, Trace.
    /// The rest are reserved or custom categories.</remarks>
    //-------------------------------------------------------------------
    public enum OutputCategory : int // System.Int32
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0x00000000,
        //----------------------
        /// <summary>
        /// 
        /// </summary>
        Alw = 0x00000001, // 0----------+
        /// <summary>
        /// 
        /// </summary>
        Err = 0x00000002, // 1          | FRAMEWORK
        /// <summary>
        /// 
        /// </summary>
        Wrn = 0x00000004, // 2          |
        /// <summary>
        /// 
        /// </summary>
        Trc = 0x00000008, // 3----------+
        /// <summary>
        /// 
        /// </summary>
        Ver = 0x00000010, // 4 ---------+
        /// <summary>
        /// 
        /// </summary>
        R05 = 0x00000020, // 5          |
        /// <summary>
        /// 
        /// </summary>
        R06 = 0x00000040, // 6          |
        /// <summary>
        /// 
        /// </summary>
        R07 = 0x00000080, // 7          |
        //----------------------                |
        /// <summary>
        /// 
        /// </summary>
        R08 = 0x00000100, // 8          |
        /// <summary>
        /// 
        /// </summary>
        R09 = 0x00000200, // 9          | RESERVED
        /// <summary>
        /// 
        /// </summary>
        R10 = 0x00000400, // 10         |
        /// <summary>
        /// 
        /// </summary>
        R11 = 0x00000800, // 11         |
        /// <summary>
        /// 
        /// </summary>
        R12 = 0x00001000, // 12         |
        /// <summary>
        /// 
        /// </summary>
        R13 = 0x00002000, // 13         |
        /// <summary>
        /// 
        /// </summary>
        R14 = 0x00004000, // 14         |
        /// <summary>
        /// 
        /// </summary>
        R15 = 0x00008000, // 15---------+
        //----------------------
        /// <summary>
        /// 
        /// </summary>
        C16 = 0x00010000, // 16---------+
        /// <summary>
        /// 
        /// </summary>
        C17 = 0x00020000, // 17         |
        /// <summary>
        /// 
        /// </summary>
        C18 = 0x00040000, // 18         |
        /// <summary>
        /// 
        /// </summary>
        C19 = 0x00080000, // 19         |
        /// <summary>
        /// 
        /// </summary>
        C20 = 0x00100000, // 20         |
        /// <summary>
        /// 
        /// </summary>
        C21 = 0x00200000, // 21         |
        /// <summary>
        /// 
        /// </summary>
        C22 = 0x00400000, // 22         |
        /// <summary>
        /// 
        /// </summary>
        C23 = 0x00800000, // 23         |
        //----------------------                | CUSTOM
        /// <summary>
        /// 
        /// </summary>
        C24 = 0x01000000, // 24         |
        /// <summary>
        /// 
        /// </summary>
        C25 = 0x02000000, // 25         |
        /// <summary>
        /// 
        /// </summary>
        C26 = 0x04000000, // 26         |
        /// <summary>
        /// 
        /// </summary>
        C27 = 0x08000000, // 27         |
        /// <summary>
        /// 
        /// </summary>
        C28 = 0x10000000, // 28         |
        /// <summary>
        /// 
        /// </summary>
        C29 = 0x20000000, // 29         |
        /// <summary>
        /// 
        /// </summary>
        C30 = 0x40000000, // 30         |
        /// <summary>
        /// 
        /// </summary>
        C31 = unchecked((int)0x80000000) // 31-+
    } // end of enum

    #endregion
}
