//-----------------------------------------------------------------------------
//
// company: Ladbrokes
//
// copyright: 
//
// summary:
//    The logreader class is to parse the log files created by the logwriter. 
//	  It returns the var results along with exception information and traces.
//
// history:
//     Apr-2013 Modified
//-----------------------------------------------------------------------------

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;

namespace AutonitroLogger.BaseLogger
{
    #region Interfaces

    /// <summary>
    /// Interface representing a Trace in the log
    /// </summary>
    /// <remarks></remarks>
    public interface ITraceInfo
    {
        /// <summary>
        /// Gets the trace time.
        /// </summary>
        /// <remarks></remarks>
        DateTime TraceTime { get; }
        /// <summary>
        /// Gets the trace level.
        /// </summary>
        /// <remarks></remarks>
        OutputCategory TraceLevel { get; }
        /// <summary>
        /// Gets the trace text.
        /// </summary>
        /// <remarks></remarks>
        string TraceText { get; }
    };


    /// <summary>
    /// Interface representing a Var in the log
    /// </summary>
    /// <remarks></remarks>

    public interface IVarInfo
    {
        /// <summary>
        /// Gets the test id.
        /// </summary>
        /// <remarks></remarks>
        UInt32 TestId { get; }/////smitam: There is probably no need for this
        /// <summary>
        /// Gets the set.
        /// </summary>
        /// <remarks></remarks>
        int Set { get; }
        /// <summary>
        /// Gets the level.
        /// </summary>
        /// <remarks></remarks>
        int Level { get; }
        /// <summary>
        /// Gets the var.
        /// </summary>
        /// <remarks></remarks>
        int Var { get; }
        /// <summary>
        /// Gets the cid.
        /// </summary>
        /// <remarks></remarks>
        string Cid { get; }
        /// <summary>
        /// Gets the pid.
        /// </summary>
        /// <remarks></remarks>
        string Pid { get; }
        /// <summary>
        /// Gets the bug id.
        /// </summary>
        /// <remarks></remarks>
        string BugId { get; }
        /// <summary>
        /// Gets the mode.
        /// </summary>
        /// <remarks></remarks>
        string Mode { get; }
        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <remarks></remarks>
        UInt32 Seed { get; }
        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <remarks></remarks>
        string Exception { get; }
        /// <summary>
        /// Gets the varmap.
        /// </summary>
        /// <remarks></remarks>
        string Varmap { get; }
        /// <summary>
        /// Gets the completion time.
        /// </summary>
        /// <remarks></remarks>
        DateTime CompletionTime { get; }
        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <remarks></remarks>
        ExeResult Result { get; }

        /// <summary>
        /// Gets the trace info.
        /// </summary>
        /// <remarks></remarks>
        ITraceInfo[] TraceInfo { get; }

        /// <summary>
        /// Gets the var description.
        /// </summary>
        /// <remarks></remarks>
        string VarDescription { get; }
    };


    /// <summary>
    /// Interface representing the top level log data
    /// </summary>
    /// <remarks></remarks>
    internal interface ILogInfo
    {
        /// <summary>
        /// Gets the log file path.
        /// </summary>
        /// <remarks></remarks>
        string LogFilePath { get; }
        /// <summary>
        /// Gets the last modified time.
        /// </summary>
        /// <remarks></remarks>
        DateTime LastModifiedTime { get; }
        /// <summary>
        /// Gets the name of the varmap.
        /// </summary>
        /// <remarks></remarks>
        string VarmapName { get; }
        /// <summary>
        /// Gets the seed.
        /// </summary>
        /// <remarks></remarks>
        uint Seed { get; }
        /// <summary>
        /// Gets the var info.
        /// </summary>
        /// <remarks></remarks>
        IVarInfo[] VarInfo { get; }
        /// <summary>
        /// Gets the trace info.
        /// </summary>
        /// <remarks></remarks>
        ITraceInfo[] TraceInfo { get; }
        /// <summary>
        /// Gets the group aborts.
        /// </summary>
        /// <remarks></remarks>
        string[] GroupAborts { get; }
        /// <summary>
        /// Gets the abort.
        /// </summary>
        /// <remarks></remarks>
        string[] Abort { get; }
    };

    #endregion

    #region Base Classes

    /// <summary>
    /// Base class for Vars and Logs (which both contain collections of traces)
    /// </summary>
    /// <remarks></remarks>
    [ComVisible(false)]
    //[TestClass]
    public abstract class TraceCollection
    {
        /// <summary>
        /// Adds the trace to the collection
        /// </summary>
        /// <param name="NewTrace">The new trace.</param>
        /// <remarks></remarks>
        public void AddTrace(TraceInfo NewTrace)
        {
            // Verify there is a trace
            if (null == NewTrace)
            {
                throw new ArgumentNullException("NewTrace cannot be NULL!");
            }

            // If there are already traces
            if (null != _TraceInfo)
            {
                ArrayList TraceList = new ArrayList(_TraceInfo);
                TraceList.Add(NewTrace);
                _TraceInfo = (ITraceInfo[])TraceList.ToArray(typeof(ITraceInfo));
            }
            else
            {
                _TraceInfo = new TraceInfo[1];
                _TraceInfo[0] = NewTrace;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        internal ITraceInfo[] _TraceInfo = null;
    }
    #endregion

    #region Classes

    /// <summary>
    /// Class which represents a trace
    /// </summary>
    /// <remarks></remarks>
    [ComVisible(false)]
    //[TestClass]
    public class TraceInfo : ITraceInfo
    {
        #region Class setup

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks></remarks>
        internal TraceInfo() { }

        /// <summary>
        /// Constructor which sets all of the properties
        /// </summary>
        /// <param name="TraceTime">The trace time.</param>
        /// <param name="TraceLevel">The trace level.</param>
        /// <param name="TraceText">The trace text.</param>
        /// <remarks></remarks>
        internal TraceInfo(DateTime TraceTime,
            OutputCategory TraceLevel,
            string TraceText)
        {
            _TraceTime = TraceTime;
            _TraceLevel = TraceLevel;
            _TraceText = TraceText;
        }

        #endregion

        #region ITraceInfo Members

        /// <summary>
        /// Returns the Time of the trace
        /// </summary>
        /// <remarks></remarks>
        public DateTime TraceTime
        {
            get { return _TraceTime; }
        }

        /// <summary>
        /// Returns the trace level for the trace
        /// </summary>
        /// <remarks></remarks>
        public OutputCategory TraceLevel
        {
            get { return _TraceLevel; }
        }

        /// <summary>
        /// Returns the trace text for the trace
        /// </summary>
        /// <remarks></remarks>
        public string TraceText
        {
            get { return _TraceText; }
        }

        #endregion

        #region Internal members

        /// <summary>
        /// 
        /// </summary>
        internal DateTime _TraceTime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        internal OutputCategory _TraceLevel = 0;
        /// <summary>
        /// 
        /// </summary>
        internal string _TraceText = null;

        #endregion
    }


    /// <summary>
    /// Class which represents a Var
    /// </summary>
    /// <remarks></remarks>
    [ComVisible(false)]
    //[TestClass]
    public class VarInfo : TraceCollection,
        IVarInfo
    {
        #region Class setup

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks></remarks>
        internal VarInfo() { }

        /// <summary>
        /// Internal constructor sets all of the properties
        /// </summary>
        /// <param name="TestId">The test id.</param>
        /// <param name="Set">The set.</param>
        /// <param name="Level">The level.</param>
        /// <param name="Var">The var.</param>
        /// <param name="Cid">The cid.</param>
        /// <param name="Pid">The pid.</param>
        /// <param name="BugId">The bug id.</param>
        /// <param name="Mode">The mode.</param>
        /// <param name="Seed">The seed.</param>
        /// <param name="Exception">The exception.</param>
        /// <param name="Varmap">The varmap.</param>
        /// <param name="CompletionTime">The completion time.</param>
        /// <param name="Result">The result.</param>
        /// <param name="TraceInfo">The trace info.</param>
        /// <param name="VarDescription">The var description.</param>
        /// <remarks></remarks>
        internal VarInfo(UInt32 TestId,
            int Set,
            int Level,
            int Var,
            string Cid,
            string Pid,
            string BugId,
            string Mode,
            UInt32 Seed,
            string Exception,
            string Varmap,
            DateTime CompletionTime,
            ExeResult Result,
            TraceInfo[] TraceInfo,
            string VarDescription)
        {
            _TestId = TestId;
            _Set = Set;
            _Level = Level;
            _Var = Var;
            _Cid = Cid;
            _Pid = Pid;
            _BugId = BugId;
            _Mode = Mode;
            _Seed = Seed;
            _Exception = Exception;
            _Varmap = Varmap;
            _CompletionTime = CompletionTime;
            _Result = Result;
            _TraceInfo = TraceInfo;
            _VarDescription = VarDescription;
        }

        #endregion

        #region IVarInfo Members

        /// <summary>
        /// Returns the TestId for this var
        /// </summary>
        /// <remarks></remarks>
        public UInt32 TestId
        {
            get { return _TestId; }
        }

        /// <summary>
        /// Returns the set #
        /// </summary>
        /// <remarks></remarks>
        public int Set
        {
            get { return _Set; }
        }

        /// <summary>
        /// Returns the level #
        /// </summary>
        /// <remarks></remarks>
        public int Level
        {
            get { return _Level; }
        }

        /// <summary>
        /// Returns the Var #
        /// </summary>
        /// <remarks></remarks>
        public int Var
        {
            get { return _Var; }
        }

        /// <summary>
        /// Returns the Cid for this var
        /// </summary>
        /// <remarks></remarks>
        public string Cid
        {
            get { return _Cid; }
        }

        /// <summary>
        /// Returns the Pid for this var
        /// </summary>
        /// <remarks></remarks>
        public string Pid
        {
            get { return _Pid; }
        }

        /// <summary>
        /// Returns the BugId for this var
        /// </summary>
        /// <remarks></remarks>
        public string BugId
        {
            get { return _BugId; }
        }

        /// <summary>
        /// Returns the Mode for this var
        /// </summary>
        /// <remarks></remarks>
        public string Mode
        {
            get { return _Mode; }
        }

        /// <summary>
        /// Returns the seed used for the var
        /// </summary>
        /// <remarks></remarks>
        public UInt32 Seed
        {
            get { return _Seed; }
        }

        /// <summary>
        /// Returns the exception if the var throws any
        /// </summary>
        /// <remarks></remarks>
        public string Exception
        {
            get { return _Exception; }
        }

        /// <summary>
        /// Returns the varmap name for this variation
        /// </summary>
        /// <remarks></remarks>
        public string Varmap
        {
            get { return _Varmap; }
        }

        /// <summary>
        /// Returns the time the Var completed
        /// </summary>
        /// <remarks></remarks>
        public DateTime CompletionTime
        {
            get { return _CompletionTime; }
        }

        /// <summary>
        /// Returns the result for this var
        /// </summary>
        /// <remarks></remarks>
        public ExeResult Result
        {
            get { return _Result; }
        }

        /// <summary>
        /// Returns the list of traces which occurred in this var
        /// </summary>
        /// <remarks></remarks>
        public ITraceInfo[] TraceInfo
        {
            get { return _TraceInfo; }
        }

        /// <summary>
        /// Returns the Variation Description for this var
        /// </summary>
        /// <remarks></remarks>
        public string VarDescription
        {
            get { return _VarDescription; }
        }

        #endregion

        #region Internal members

        /// <summary>
        /// 
        /// </summary>
        internal UInt32 _TestId = 0;
        /// <summary>
        /// 
        /// </summary>
        internal int _Set = 0;
        /// <summary>
        /// 
        /// </summary>
        internal int _Level = 0;
        /// <summary>
        /// 
        /// </summary>
        internal int _Var = 0;
        /// <summary>
        /// 
        /// </summary>
        internal string _Cid = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal string _Pid = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal string _BugId = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal string _Mode = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal UInt32 _Seed = 0;
        /// <summary>
        /// 
        /// </summary>
        internal string _Exception = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal string _Varmap = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal DateTime _CompletionTime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        internal ExeResult _Result = 0;
        /// <summary>
        /// 
        /// </summary>
        internal string _VarDescription = string.Empty;

        #endregion
    }


    /// <summary>
    /// Class which represents a Log
    /// </summary>
    /// <remarks></remarks>
    [ComVisible(false)]
    //[TestClass]
    public class LogInfo : TraceCollection,
        ILogInfo
    {
        #region Class setup

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <remarks></remarks>
        internal LogInfo() { }

        /// <summary>
        /// Internal constructor sets all of the class properties
        /// </summary>
        /// <param name="LogFilePath">The log file path.</param>
        /// <param name="LastModifiedTime">The last modified time.</param>
        /// <param name="VarmapName">Name of the varmap.</param>
        /// <param name="Seed">The seed.</param>
        /// <param name="VarInfo">The var info.</param>
        /// <param name="TraceInfo">The trace info.</param>
        /// <param name="GroupAborts">The group aborts.</param>
        /// <param name="Abort">The abort.</param>
        /// <remarks></remarks>
        internal LogInfo(string LogFilePath,
            DateTime LastModifiedTime,
            string VarmapName,
            uint Seed,
            IVarInfo[] VarInfo,
            TraceInfo[] TraceInfo,
            string[] GroupAborts,
            string[] Abort)
        {
            _LogFilePath = LogFilePath;
            _LastModifiedTime = LastModifiedTime;
            _VarmapName = VarmapName;
            _Seed = Seed;
            _VarInfo = VarInfo;
            _TraceInfo = TraceInfo;
            _GroupAborts = GroupAborts;
            _Abort = Abort;
        }

        #endregion

        #region ILogInfo Members

        /// <summary>
        /// Returns the log file path for the log
        /// </summary>
        /// <remarks></remarks>
        public string LogFilePath
        {
            get { return _LogFilePath; }
        }

        /// <summary>
        /// Returns the last modified time of the log file
        /// </summary>
        /// <remarks></remarks>
        public DateTime LastModifiedTime
        {
            get { return _LastModifiedTime; }
        }

        /// <summary>
        /// Returns the VarmapName for the log
        /// </summary>
        /// <remarks></remarks>
        public string VarmapName
        {
            get { return _VarmapName; }
        }

        /// <summary>
        /// Returns the Seed for the log
        /// </summary>
        /// <remarks></remarks>
        public uint Seed
        {
            get { return _Seed; }
        }

        /// <summary>
        /// Returns the list of Vars for the log
        /// </summary>
        /// <remarks></remarks>
        public IVarInfo[] VarInfo
        {
            get { return _VarInfo; }
        }

        /// <summary>
        /// Returns the top-level traces for the log
        /// </summary>
        /// <remarks></remarks>
        public ITraceInfo[] TraceInfo
        {
            get { return _TraceInfo; }
        }

        /// <summary>
        /// Returns the Group aborts found in the log
        /// </summary>
        /// <remarks></remarks>
        public string[] GroupAborts
        {
            get { return _GroupAborts; }
        }

        /// <summary>
        /// Returns the Aborts found in the log
        /// </summary>
        /// <remarks></remarks>
        public string[] Abort
        {
            get { return _Abort; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Method which adds the variation to the list of vars
        /// </summary>
        /// <param name="NewVar">The new var.</param>
        /// <remarks></remarks>
        internal void AddVar(VarInfo NewVar)
        {
            // Check Args
            if (null == NewVar)
            {
                throw new ArgumentNullException("NewVar",
                    "Cannot add a null Variation to the collection!");
            }

            // If this is not the first in the list
            if (null != _VarInfo)
            {
                ArrayList VarList = new ArrayList(_VarInfo);
                VarList.Add(NewVar);
                _VarInfo = (VarInfo[])VarList.ToArray(typeof(VarInfo));
            }
            else
            {
                _VarInfo = new VarInfo[1];
                _VarInfo[0] = NewVar;
            }
        }

        ///////// <summary>
        ///////// Method which adds the varmap to the list of varmaps
        ///////// </summary>
        ///////// <param name="NewVar"></param>
        //////internal void AddVarmapName(string varmapName)
        //////{
        //////    // Check Args
        //////    if (null == varmapName)
        //////    {
        //////        throw new ArgumentNullException("varmapName",
        //////            "Cannot add a null varmapName to the collection!");
        //////    }

        //////    // If this is not the first in the list
        //////    if (null != _VarmapName)
        //////    {
        //////        ////BUGBUG:
        //////        throw new ArgumentException("HERE");
        //////        ArrayList VarmapNameList = new ArrayList();
        //////        VarmapNameList.Add(varmapName);
        //////        _VarmapName = (string[])VarmapNameList.ToArray(typeof(string));
        //////    }
        //////    else
        //////    {
        //////        _VarmapName = new string[1];
        //////        _VarmapName[0] = varmapName;
        //////    }
        //////}

        #endregion

        #region Internal members

        /// <summary>
        /// 
        /// </summary>
        internal string _LogFilePath = null;
        /// <summary>
        /// 
        /// </summary>
        internal DateTime _LastModifiedTime = DateTime.Now;
        /// <summary>
        /// 
        /// </summary>
        internal string _VarmapName = string.Empty;
        /// <summary>
        /// 
        /// </summary>
        internal uint _Seed = 0;
        /// <summary>
        /// 
        /// </summary>
        internal IVarInfo[] _VarInfo = null;
        /// <summary>
        /// 
        /// </summary>
        internal string[] _GroupAborts = null;
        /// <summary>
        /// 
        /// </summary>
        internal string[] _Abort = null;
        #endregion
    }


    /// <summary>
    /// Class which parses the logs and returns the log data to the caller.
    /// </summary>
    /// <remarks></remarks>
    [ClassInterface(ClassInterfaceType.AutoDual)]
    //[TestClass]
    public class LogReader
    {
        #region Class setup

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object"/> class.
        /// </summary>
        /// <remarks></remarks>
        public LogReader()
        {
        }

        #endregion

        #region Accessors

        /// <summary>
        /// Sets the log file path used to read the logs, can be local or
        /// remote.
        /// </summary>
        /// <value>The log file path.</value>
        /// <remarks></remarks>
        public string LogFilePath
        {
            get { return m_LogFilePath; }
            set
            {
                // If there was a change
                if (0 != string.Compare(m_LogFilePath, value, true, CultureInfo.CurrentCulture))
                {
                    // Reset the class to retrieve the log data
                    //m_LogInfo = null;
                    m_LogFilePath = value;
                }
            }
        }


        /// <summary>
        /// The trace level mask to use when reading the log file.
        /// </summary>
        /// <value>The trace level.</value>
        /// <remarks></remarks>
        public int TraceLevel
        {
            get { return m_TraceLevel; }
            set
            {
                // If there was a change
                if (value != m_TraceLevel)
                {
                    // Reset the class to retrieve the log data
                    //m_LogInfo = null;
                    m_TraceLevel = value;
                }
            }
        }


        /// <summary>
        /// Read only, reads the specified log and returns the log info
        /// </summary>
        /// <remarks></remarks>
        internal ILogInfo LogInfo
        {
            get
            {
                if (null == m_LogInfo)
                {
                    // Get the log data into m_LogInfo
                    ParseLogFile();
                }
                return m_LogInfo;
            }
        }


        #endregion

        #region Private methods

        /// <summary>
        /// Gets the log file data from the specified path at the specified
        /// trace level
        /// </summary>
        /// <remarks></remarks>
        private void ParseLogFile()
        {
            // Reset the output
            m_LogInfo = null;

            // Check the arguments
            if (null == m_LogFilePath)
            {
                throw new ArgumentException("The LogFilePath property must " +
                    "be set to a valid log file!");
            }
            if (!File.Exists(m_LogFilePath))
            {
                throw new ArgumentException(
                    "The specified log file " + m_LogFilePath + " does not exist!");
            }

            // Parse the log file
            DateTime LastModifiedTime = File.GetLastWriteTime(m_LogFilePath);

            // The using statement also closes the StreamReader.
            using (StreamReader LogFile = new StreamReader(m_LogFilePath))
            {
                string CurLine = LogFile.ReadLine();

                // Create a new log info object
                m_LogInfo = new LogInfo();

                VarInfo CurVar = null;
                TraceInfo CurTrace = null;

                while (null != CurLine)
                {


                    if (GetDataForLog(ref CurLine, LogFile, m_LogInfo))
                        continue;

                    if (GetResult(ref CurLine, LogFile, ref CurVar, m_LogInfo))
                        continue;

                    if (GetGroupAborts(ref CurLine, LogFile, m_LogInfo))
                        continue;

                    if (GetAbort(ref CurLine, LogFile, m_LogInfo))
                        continue;

                    if (GetVar(ref CurLine, LogFile, ref CurVar, m_LogInfo))
                        continue;

                    if (GetTrace(ref CurLine, LogFile, out CurTrace))
                    {
                        if (null != CurVar)
                            CurVar.AddTrace(CurTrace);
                        else
                            m_LogInfo.AddTrace(CurTrace);
                        continue;
                    }

                    if (GetException(ref CurLine, LogFile, ref CurVar, m_LogInfo))
                        continue;
                    CurLine = LogFile.ReadLine();
                }
            }
        }


        /// <summary>
        /// Gets the date from the current line
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LineDate">The line date.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetDate(string Line, out DateTime LineDate)
        {
            bool RetVal = false;

            LineDate = DateTime.Now;

            // If there is a date start char in the first char
            if (0 == Line.IndexOf(Constants.DATE_START))
            {
                int DateEndIndex = Line.IndexOf(Constants.DATE_END);
                if (-1 != DateEndIndex)
                {
                    try
                    {
                        LineDate = DateTime.Parse(
                            Line.Substring(1, DateEndIndex - 1), CultureInfo.CurrentCulture);
                        RetVal = true;
                    }
                    catch (FormatException)
                    {
                        ;
                        // Eat this exception, if this is not a date simply 
                        //	return false
                    }

                }
            }
            return RetVal;
        }


        /// <summary>
        /// Returns the trace level if found
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="TraceLevel">The trace level.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetTraceLevel(string Line, out OutputCategory TraceLevel)
        {
            bool RetVal = false;

            TraceLevel = OutputCategory.None;

            // Find the end of the first token
            int CurIndex = Line.IndexOf(Constants.TOKEN_SEPARATOR);

            if (-1 != CurIndex)
            {
                // Find the end of the second token
                CurIndex = Line.IndexOf(Constants.TOKEN_SEPARATOR, ++CurIndex) + 1;

                // Isolate the third token
                string Token =
                    Line.Substring(CurIndex, Line.Length - CurIndex).TrimStart(
                    " ".ToCharArray());
                int TokenEndIndex = Token.IndexOf(" ", StringComparison.CurrentCulture);
                if (-1 != TokenEndIndex)
                    Token = Token.Substring(0, TokenEndIndex);

                try
                {

                    // Get the trace level from the third token
                    TraceLevel = (OutputCategory)Enum.Parse(typeof(OutputCategory),
                        Token);

                    RetVal = true;
                }
                catch (ArgumentException)
                {
                    ;

                    // Eat this exception, this simply means that we cannot get 
                    //	the trace level
                }
            }

            return RetVal;
        }


        /// <summary>
        /// Gets the string value for the specific tag.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="Tag">The tag.</param>
        /// <param name="Sep">The sep.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetStringValue(string Line,
            string Tag,
            char Sep,
            out string Value)
        {
            bool RetVal = false;
            int CurIndex = 0;

            // Reset the output
            Value = null;

            do
            {
                // Find the tag
                CurIndex = Line.IndexOf(Tag, CurIndex, StringComparison.CurrentCulture);
                if (-1 == CurIndex)
                {
                    break;
                }

                // Skip the white space
                CurIndex += Tag.Length;
                while (Line[CurIndex] == ' ' &&
                    ++CurIndex < Line.Length) ;

                // If this is the tag
                if (Line[CurIndex] == Sep)
                {
                    // Step past the separator
                    CurIndex++;

                    // Find the next non space character
                    while (Line[CurIndex] == ' ' &&
                        ++CurIndex < Line.Length) ;

                    // Handle quoted strings
                    int EndIndex;
                    if (Line[CurIndex] == '\"')
                    {
                        // Do not include the first quote
                        CurIndex++;
                        EndIndex = Line.IndexOf('\"', CurIndex);
                    }
                    else
                    {
                        //special handling for /m: for varmap parsing
                        //in that case we dont want to exclude . char

                        if (Tag == Constants.VAR_VARMAP)
                        {
                            // Look for the space char
                            EndIndex = Line.IndexOf(
                                ' ',
                                CurIndex + 1);
                        }
                        else
                        {
                            // Look for the value end char
                            EndIndex = Line.IndexOfAny(
                                Constants.VALUE_END.ToCharArray(),
                                CurIndex + 1);
                        }

                        // If value goes to the end of the line
                        if (-1 == EndIndex)
                            EndIndex = Line.Length - 1;
                    }

                    // If there was a start and end
                    if (-1 != EndIndex)
                    {
                        Value = Line.Substring(CurIndex,
                            EndIndex - CurIndex);
                        //we need a case-insensitive search for .xml
                        Value = Value.ToLower(CultureInfo.CurrentCulture);
                        if (Value.EndsWith(".xml", StringComparison.CurrentCulture))
                        {
                            Value = Value.Replace(".xml", "");
                        }
                        RetVal = true;
                    }
                }
            } while (!RetVal);

            return RetVal;
        }


        /// <summary>
        /// Gets the string value for the specific tag.
        /// Does not use comma as an end of data marker
        /// This takes the Value_End as an input
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="Tag">The tag.</param>
        /// <param name="Sep">The sep.</param>
        /// <param name="value_end_nocomma">The value_end_nocomma.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetStringValue(string Line,
            string Tag,
            char Sep,
            string value_end_nocomma,
            out string Value)
        {
            bool RetVal = false;
            int CurIndex = 0;

            // Reset the output
            Value = null;

            do
            {
                // Find the tag
                CurIndex = Line.IndexOf(Tag, CurIndex, StringComparison.CurrentCulture);
                if (-1 == CurIndex)
                {
                    break;
                }

                // Skip the white space
                CurIndex += Tag.Length;
                while (Line[CurIndex] == ' ' &&
                    ++CurIndex < Line.Length) ;

                // If this is the tag
                if (Line[CurIndex] == Sep)
                {
                    // Step past the separator
                    CurIndex++;

                    // Find the next non space character
                    while (Line[CurIndex] == ' ' &&
                        ++CurIndex < Line.Length) ;

                    // Handle quoted strings
                    int EndIndex;
                    if (Line[CurIndex] == '\"')
                    {
                        // Do not include the first quote
                        CurIndex++;
                        EndIndex = Line.IndexOf('\"', CurIndex);
                    }
                    else
                    {
                        //special handling for /m: for varmap parsing
                        //in that case we dont want to exclude . char

                        if (Tag == Constants.VAR_VARMAP)
                        {
                            // Look for the space char
                            EndIndex = Line.IndexOf(
                                ' ',
                                CurIndex + 1);
                        }
                        else
                        {
                            // Look for the value end char
                            EndIndex = Line.IndexOfAny(
                                value_end_nocomma.ToCharArray(),
                                CurIndex + 1);
                        }

                        // If value goes to the end of the line
                        if (-1 == EndIndex)
                            EndIndex = Line.Length - 1;
                    }

                    // If there was a start and end
                    if (-1 != EndIndex)
                    {
                        Value = Line.Substring(CurIndex,
                            EndIndex - CurIndex);
                        //we need a case-insensitive search for .xml
                        Value = Value.ToLower(CultureInfo.CurrentCulture);
                        if (Value.EndsWith(".xml", StringComparison.CurrentCulture))
                        {
                            Value = Value.Replace(".xml", "");
                        }
                        RetVal = true;
                    }
                }
            } while (!RetVal);

            return RetVal;
        }


        /// <summary>
        /// Gets the unsigned integer value for the specified tag.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="Tag">The tag.</param>
        /// <param name="Sep">The sep.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetUIntValue(string Line,
            string Tag,
            char Sep,
            out uint Value)
        {
            bool RetVal = false;
            string StringVal;

            Value = 0;

            // Get the string value
            if (GetStringValue(Line, Tag, Sep, out StringVal))
            {
                try
                {
                    Value = Convert.ToUInt32(StringVal, CultureInfo.CurrentCulture);
                    RetVal = true;
                }
                catch (FormatException)
                {
                    ;
                    // Eat this exception, this simply means that we cannot get 
                    //	the value
                }
            }
            return RetVal;
        }


        /// <summary>
        /// Gets the integer value for the specified tag.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="Tag">The tag.</param>
        /// <param name="Sep">The sep.</param>
        /// <param name="Value">The value.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetIntValue(string Line,
            string Tag,
            char Sep,
            out int Value)
        {
            bool RetVal = false;
            string StringVal;

            Value = 0;

            // Get the string value
            if (GetStringValue(Line, Tag, Sep, out StringVal))
            {
                try
                {
                    Value = Convert.ToInt32(StringVal, CultureInfo.CurrentCulture);
                    RetVal = true;
                }
                catch (FormatException)
                {
                    ;
                    // Eat this exception, this simply means that we cannot get 
                    //	the value
                }
            }
            return RetVal;
        }


        /// <summary>
        /// Gets the trace from the log file
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="Log">The log.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetDataForLog(ref string Line,
            StreamReader LogFile,
            LogInfo Log)
        {
            bool RetVal = false;
            //If this is start of log then from the next line.../ //m:, we can get the varmapname, seed,etc. for the log
            if (Line.StartsWith(Constants.LOG_START, StringComparison.CurrentCulture))
            {
                // Set to the next line as that should have the info for / //m:varmapName
                Line = LogFile.ReadLine();
                if (Line.StartsWith(Constants.VAR_START, StringComparison.CurrentCulture))
                {
                    RetVal = true;
                    string varmap = string.Empty;
                    uint Seed = 0;
                    GetStringValue(Line, Constants.VAR_VARMAP, Constants.VAR_SEPARATOR, out varmap);
                    m_LogInfo._VarmapName = varmap;
                    //m_LogInfo.AddVarmapName(varmap);
                    GetUIntValue(Line, Constants.VAR_SEED, Constants.VAR_SEPARATOR, out Seed);
                    m_LogInfo._Seed = Seed;
                }
            }

            return RetVal;
        }

        /// <summary>
        /// Gets the result from the line and if the var matches the result,
        /// updates the Var otherwise, adds a new var with the result to the
        /// log.
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="Var">The var.</param>
        /// <param name="Log">The log.</param>
        /// <returns>true if the line contains a result</returns>
        /// <remarks></remarks>
        private bool GetResult(ref string Line,
            StreamReader LogFile,
            ref VarInfo Var,
            LogInfo Log)
        {
            //string varmap=string.Empty;
            bool RetVal = false;
            DateTime LineDate;

            // Get the date
            if (Line != null && GetDate(Line, out LineDate))
            {
                // Results are traced as Always or ERR, for VAR_ABORTs they are traced as ERR
                OutputCategory TraceLevel;
                //				
                if ((GetTraceLevel(Line, out TraceLevel) &&
                    OutputCategory.Alw == TraceLevel) ||
                    (GetTraceLevel(Line, out TraceLevel) &&
                    OutputCategory.Err == TraceLevel))
                {
                    // Get the Set Level and Var values
                    int S = 0, L = 0, V = 0;

                    if (GetIntValue(Line, Constants.SET, '=', out S) &&
                        GetIntValue(Line, Constants.LEVEL, '=', out L) &&
                        GetIntValue(Line, Constants.VAR, '=', out V))
                    {

                        ExeResult Result;

                        string cid = string.Empty,
                            pid = string.Empty,
                            bugid = string.Empty,
                            vardescription = string.Empty,
                            mode = string.Empty;

                        // Parse the result from the line
                        int ResultIndex = Line.LastIndexOf(
                            Constants.RESULT_SEPARATOR);
                        int DescriptionStartIndex = -1;
                        int DescriptionEndIndex = Line.LastIndexOf(Constants.DATE_END);

                        //if it is not the ']' immediatley preceding the Result separator ':'
                        //then there is no description, this could be a Date separatpr, so ignore it
                        if (DescriptionEndIndex != (ResultIndex - 2))
                        {
                            DescriptionEndIndex = -1;
                        }
                        if (-1 != ResultIndex)
                        {
                            ResultIndex++;
                            try
                            {
                                Result = (ExeResult)Enum.Parse(typeof(ExeResult),
                                    Line.Substring(ResultIndex, Line.Length - ResultIndex));
                                RetVal = true;

                                // If the current var matches
                                if (null != Var &&
                                    Var.Set == S &&
                                    Var.Level == L &&
                                    Var.Var == V)
                                {

                                    GetStringValue(Line, Constants.CID, '=', out cid);
                                    GetStringValue(Line, Constants.PID, '=', Constants.VALUE_END_NoComma, out pid);
                                    GetStringValue(Line, Constants.BugId, '=', Constants.VALUE_END_SquareBrace, out bugid);
                                    GetStringValue(Line, Constants.MODE, '=', out mode);
                                    if (cid == null)
                                    {
                                        Var._Cid = String.Empty;
                                    }
                                    else
                                    {
                                        Var._Cid = cid;
                                    }
                                    if (pid == null)
                                    {
                                        Var._Pid = String.Empty;
                                    }
                                    else
                                    {
                                        Var._Pid = pid;
                                    }
                                    if (bugid == null)
                                    {
                                        Var._BugId = String.Empty;
                                    }
                                    else
                                    {
                                        Var._BugId = bugid;
                                    }
                                    if (mode == null)
                                    {
                                        Var._Mode = String.Empty;
                                    }
                                    else
                                    {
                                        Var._Mode = mode;
                                    }
                                    Var._Result = Result;
                                    Var._CompletionTime = LineDate;

                                    if (-1 != DescriptionEndIndex)
                                    {
                                        int tempIndex = -1;

                                        tempIndex = Line.IndexOf("Var=", StringComparison.CurrentCulture);
                                        DescriptionStartIndex = Line.IndexOf("[", tempIndex, StringComparison.CurrentCulture);

                                        if (-1 != DescriptionStartIndex)
                                        {
                                            vardescription = Line.Substring(DescriptionStartIndex, (DescriptionEndIndex - DescriptionStartIndex + 1));
                                            Var._VarDescription = vardescription;
                                        }
                                    }
                                }
                                else//if Var is null
                                {
                                    //	throw new ArgumentOutOfRangeException("VAR IS NULL< We'll handle it");
                                    // Create a new var
                                    //Why create the var with empty values??
                                    //We still need to read the values from the log
                                    //This can happen when for eg., it is a manual var 
                                    //and so it doesn't have an execution line like /m:varmap.xml /s:1,/l:1,/v:1
                                    //////m_LogInfo.AddVar(new VarInfo(0,
                                    //////    S,
                                    //////    L,
                                    //////    V,
                                    //////    cid,
                                    //////    pid,
                                    //////    bugid,
                                    //////    mode,
                                    //////    0,
                                    //////    string.Empty,
                                    //////    string.Empty,
                                    //////    LineDate,
                                    //////    Result,
                                    //////    null,
                                    //////    vardescription));

                                    GetStringValue(Line, Constants.CID, '=', out cid);
                                    GetStringValue(Line, Constants.PID, '=', Constants.VALUE_END_NoComma, out pid);
                                    GetStringValue(Line, Constants.BugId, '=', Constants.VALUE_END_SquareBrace, out bugid);
                                    GetStringValue(Line, Constants.MODE, '=', out mode);

                                    if (cid == null)
                                    {
                                        cid = String.Empty;
                                    }

                                    if (pid == null)
                                    {
                                        pid = String.Empty;
                                    }

                                    if (bugid == null)
                                    {
                                        bugid = String.Empty;
                                    }

                                    if (mode == null)
                                    {
                                        mode = String.Empty;
                                    }

                                    if (-1 != DescriptionEndIndex)
                                    {
                                        int tempIndex = -1;

                                        tempIndex = Line.IndexOf("Var=", StringComparison.CurrentCulture);
                                        DescriptionStartIndex = Line.IndexOf("[", tempIndex, StringComparison.CurrentCulture);

                                        if (-1 != DescriptionStartIndex)
                                        {
                                            vardescription = Line.Substring(DescriptionStartIndex, (DescriptionEndIndex - DescriptionStartIndex + 1));
                                        }
                                    }
                                    if (vardescription == null)
                                    {
                                        vardescription = String.Empty;
                                    }

                                    string varmap = string.Empty;
                                    ////////Getting varmap name
                                    //////int startindex = -1;
                                    //////int endindex = -1;
                                    //////startindex = Line.IndexOf("Alw");
                                    //////if (startindex == -1)
                                    //////    startindex = Line.IndexOf("ERR");
                                    //////if (startindex > -1)
                                    //////{
                                    //////    endindex = Line.IndexOf(" ",startindex+3);
                                    //////    varmap = Line.Substring(startindex+4, endindex);
                                    //////}

                                    //GetStringValue(Line, Constants.VAR_VARMAP, Constants.VAR_SEPARATOR, out varmap);

                                    if (m_LogInfo.VarmapName.Length != 0)
                                    {
                                        varmap = m_LogInfo.VarmapName;
                                    }

                                    if (varmap == null)
                                    {
                                        varmap = String.Empty;
                                    }


                                    uint seed = m_LogInfo.Seed;//This will either be 0 if it couldn't be found, or will be the common seed for launching the varmap


                                    m_LogInfo.AddVar(new VarInfo(0,
                                        S,
                                        L,
                                        V,
                                        cid,
                                        pid,
                                        bugid,
                                        mode,
                                        seed,
                                        string.Empty,
                                        varmap,
                                        LineDate,
                                        Result,
                                        null,
                                        vardescription));

                                    //m_LogInfo.AddVar(new VarInfo(0,
                                    //    S,
                                    //    L,
                                    //    V,
                                    //    cid,
                                    //    pid,
                                    //    bugid,
                                    //    mode,
                                    //    Var.Seed,
                                    //    Var.Exception,
                                    //    Var.Varmap,
                                    //    LineDate,
                                    //    Result,
                                    //    null,
                                    //    vardescription));
                                    //throw new ArgumentOutOfRangeException(m_LogInfo._VarInfo[0].Cid);
                                }

                                // Set to the next line as this one is parsed
                                Line = LogFile.ReadLine();

                            }
                            catch (ArgumentException)
                            {
                                // Eat this exception, this simply means that 
                                //	this line is not a result and should not 
                                //	cause an exception
                                ;
                            }
                        }

                    }

                }
            }
            return RetVal;
        }

        /// <summary>
        /// Gets the Group Aborts from the log
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="Log">The log.</param>
        /// <returns>true if the line contains a Group Abort</returns>
        /// <remarks></remarks>
        private bool GetGroupAborts(ref string Line,
            StreamReader LogFile,
            LogInfo Log)
        {
            bool RetVal = false;
            DateTime LineDate;

            // Get the date
            if (GetDate(Line, out LineDate))
            {
                // Group Aborts are traced as ERR
                OutputCategory TraceLevel;
                if ((GetTraceLevel(Line, out TraceLevel) &&
                    OutputCategory.Err == TraceLevel))
                {
                    ExeResult Result;
                    // Parse the result from the line
                    int ResultIndex = Line.LastIndexOf(
                        Constants.RESULT_SEPARATOR);

                    if (-1 != ResultIndex)
                    {
                        ResultIndex++;
                        try
                        {

                            Result = (ExeResult)Enum.Parse(typeof(ExeResult),
                                Line.Substring(ResultIndex, Line.Length - ResultIndex).Trim());
                            if (Result == ExeResult.GRP_ABORT)
                            {
                                RetVal = true;

                                string tempGroupAbort = string.Empty;
                                // If it is a GroupAbort then add the string 
                                //following it to GroupAborts[]

                                tempGroupAbort += Line;
                                // Set to the next line as this one is parsed
                                Line = LogFile.ReadLine();

                                //If first line starts wiuth an exception. 
                                //This will most likely be the case
                                if (Line.StartsWith(Constants.VAR_EXCEPTION, StringComparison.CurrentCulture))
                                {

                                    tempGroupAbort += Line;

                                    // Set to the next line as this one is parsed
                                    Line = LogFile.ReadLine();
                                }

                                do
                                {
                                    tempGroupAbort += Line;
                                    // Set to the next line as this one is parsed
                                    Line = LogFile.ReadLine();

                                } while (!((Line.StartsWith(Constants.VAR_START, StringComparison.CurrentCulture))
                                    || (Line.StartsWith(Constants.DATE_START.ToString(), StringComparison.CurrentCulture))
                                    || (Line.StartsWith(@"// Vars expected", StringComparison.CurrentCulture))));//Read till the next //char							

                                if (null != Log.GroupAborts)
                                {
                                    ArrayList GroupAbortsList = new ArrayList(Log.GroupAborts);
                                    GroupAbortsList.Add(tempGroupAbort);
                                    Log._GroupAborts = (string[])GroupAbortsList.ToArray(typeof(string));
                                }
                                else
                                {
                                    Log._GroupAborts = new string[1];
                                    Log._GroupAborts[0] = tempGroupAbort;
                                }
                                m_LogInfo._GroupAborts = Log._GroupAborts;

                            }

                        }
                        catch (ArgumentException)
                        {
                            // Eat this exception, this simply means that 
                            //	this line is not a result and should not 
                            //	cause an exception
                            ;
                        }

                    }
                }
            }
            return RetVal;
        }

        /// <summary>
        /// Gets the ABORT from the log
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="Log">The log.</param>
        /// <returns>true if the line contains a ABORT</returns>
        /// <remarks></remarks>
        private bool GetAbort(ref string Line,
            StreamReader LogFile,
            LogInfo Log)
        {
            bool RetVal = false;
            DateTime LineDate;

            // Get the date
            if (GetDate(Line, out LineDate))
            {
                // Group Aborts are traced as ERR
                OutputCategory TraceLevel;
                if ((GetTraceLevel(Line, out TraceLevel) &&
                    OutputCategory.Err == TraceLevel))
                {
                    ExeResult Result;
                    // Parse the result from the line
                    int ResultIndex = Line.LastIndexOf(
                        Constants.RESULT_SEPARATOR);

                    if (-1 != ResultIndex)
                    {
                        ResultIndex++;
                        try
                        {
                            Result = (ExeResult)Enum.Parse(typeof(ExeResult),
                                Line.Substring(ResultIndex, Line.Length - ResultIndex).Trim());
                            if (Result == ExeResult.ABORT)
                            {
                                RetVal = true;

                                string tempAbort = string.Empty;
                                // If it is a GroupAbort then add the string 
                                //following it to GroupAborts[]

                                tempAbort += Line;
                                // Set to the next line as this one is parsed
                                Line = LogFile.ReadLine();

                                //If first line starts wiuth an exception. 
                                //This will most likely be the case
                                if (Line.StartsWith(Constants.VAR_EXCEPTION, StringComparison.CurrentCulture))
                                {

                                    tempAbort += Line;

                                    // Set to the next line as this one is parsed
                                    Line = LogFile.ReadLine();
                                }

                                do
                                {
                                    tempAbort += Line;
                                    // Set to the next line as this one is parsed
                                    Line = LogFile.ReadLine();

                                } while (!((Line.StartsWith(Constants.VAR_START, StringComparison.CurrentCulture)) ||
                                    (Line.StartsWith(Constants.DATE_START.ToString(), StringComparison.CurrentCulture)) ||
                                    (Line.StartsWith(@"// Vars expected", StringComparison.CurrentCulture))));//Read till the next //char							

                                if (null != Log.Abort)
                                {
                                    ArrayList AbortList = new ArrayList(Log.Abort);
                                    AbortList.Add(tempAbort);
                                    Log._Abort = (string[])AbortList.ToArray(typeof(string));
                                }
                                else
                                {
                                    Log._Abort = new string[1];
                                    Log._Abort[0] = tempAbort;
                                }
                                m_LogInfo._Abort = Log._Abort;

                            }

                        }
                        catch (ArgumentException)
                        {
                            // Eat this exception, this simply means that 
                            //	this line is not a result and should not 
                            //	cause an exception
                            ;
                        }

                    }
                }
            }
            return RetVal;
        }

        /// <summary>
        /// Gets the variation start from the log file
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="CurVar">The cur var.</param>
        /// <param name="LogInfo">The log info.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetVar(ref string Line,
            StreamReader LogFile,
            ref VarInfo CurVar,
            LogInfo LogInfo)
        {
            bool RetVal = false;
            string previousLine = string.Empty;

            // Check the first token			
            if (Line.StartsWith(Constants.VAR_START, StringComparison.CurrentCulture))
            {
                int S, L, V;
                uint Seed;
                string varmap = string.Empty;
                if (GetIntValue(Line, Constants.VAR_SET, Constants.VAR_SEPARATOR, out S) &&
                    GetIntValue(Line, Constants.VAR_LEVEL, Constants.VAR_SEPARATOR, out L) &&
                    GetIntValue(Line, Constants.VAR_VAR, Constants.VAR_SEPARATOR, out V) &&
                    GetUIntValue(Line, Constants.VAR_SEED, Constants.VAR_SEPARATOR, out Seed))
                {
                    GetStringValue(Line, Constants.VAR_VARMAP, Constants.VAR_SEPARATOR, out varmap);
                    CurVar = new VarInfo(0,
                        S,
                        L,
                        V,
                        string.Empty,//setting cid,pid and bugid and mode to be empty for now
                        string.Empty,//I am not sure if it'll aprt of only the results or also the var line in the log
                        string.Empty,
                        string.Empty,
                        Seed,
                        string.Empty,
                        varmap,
                        DateTime.Now,
                        //ExeResult.VAR_UNSUPPORTED,
                        ExeResult.VAR_ABORT,
                        null,
                        string.Empty);
                    this.m_LogInfo.AddVar(CurVar);

                    // Setup up for the next line, this one is parsed
                    RetVal = true;

                    previousLine = Line;
                    Line = LogFile.ReadLine();
                    //If next line is identical, then we need to skip it...
                    if (Line == previousLine)
                    {
                        Line = LogFile.ReadLine();//skipping line
                    }
                }
            }
            return RetVal;
        }

        /// <summary>
        /// Gets the trace from the log file
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="Trace">The trace.</param>
        /// <returns></returns>
        /// <remarks></remarks>

        private bool GetTrace(ref string Line,
            StreamReader LogFile,
            out TraceInfo Trace)
        {
            bool RetVal = false;
            DateTime LineDate;
            string tempLine = string.Empty;

            Trace = null;

            // See if this is line starts with the date
            if (GetDate(Line, out LineDate))
            {
                // Get the trace level
                OutputCategory TraceLevel;
                if (GetTraceLevel(Line, out TraceLevel))
                {
                    string TraceText;
                    int TextIndex = Line.IndexOf(Constants.TRACE_SEPARATOR,
                        Line.IndexOf(Constants.DATE_END) + 1);
                    if (-1 != TextIndex)
                    {
                        TraceText = Line.Substring(TextIndex + 1);

                        DateTime TempDate;
                        do
                        {
                            Line = LogFile.ReadLine();
                            ////smitam///Think of a workaround for this. It should be OK for now						
                            // If this is a new object (trace, var, result, etc...)
                            // BUGBUG this is relatively strong checking, but 
                            //	if the user uses a "//" or a "[<date>]" as the
                            //	first char of a trace line it could miss some 
                            //	trace text.
                            if (GetDate(Line, out TempDate) ||
                                Line.StartsWith(Constants.VAR_START, StringComparison.CurrentCulture) ||
                                Line.StartsWith(Constants.LOG_START, StringComparison.CurrentCulture) ||
                                Line.StartsWith(Constants.LOG_END, StringComparison.CurrentCulture))
                                break;
                            //////If this is inFact a trace...such that the above is not true
                            //////Then we will advance the line.
                            //////This is done to not eat up variation results
                            ////Line = LogFile.ReadLine();
                            // Else add a new line in the trace text
                            TraceText = string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}", TraceText, Line);
                        }
                        while (-1 != LogFile.Peek());

                        // Setup to parse the next line, this one is done
                        Trace = new TraceInfo(LineDate, TraceLevel, TraceText);
                        RetVal = true;
                    }
                }

            }

            return RetVal;
        }


        /// <summary>
        /// Gets an exception from the log file
        /// </summary>
        /// <param name="Line">The line.</param>
        /// <param name="LogFile">The log file.</param>
        /// <param name="Var">The var.</param>
        /// <param name="Log">The log.</param>
        /// <returns></returns>
        /// <remarks></remarks>
        private bool GetException(ref string Line,
            StreamReader LogFile,
            ref VarInfo Var,
            LogInfo Log)
        {
            bool RetVal = false;

            if (Var != null)
            {
                //Append to exception string only if it is previously empty. 
                //This is to avoid Grp_Abort exceptions from getting appended to 
                //VAR exceptions. This can happen because the var context does not change
                if (Var._Exception.Length == 0)
                {
                    //The exception format looks like '// <<<EXCEPTION>>>
                    if (Line.StartsWith(Constants.VAR_EXCEPTION, StringComparison.CurrentCulture))
                    {
                        do
                        {
                            //Var._Exception += Line;
                            Var._Exception = string.Format(CultureInfo.CurrentCulture, "{0}\r\n{1}", Var._Exception, Line);
                            //Need to read all the lines till the next "// /m" tag
                            Line = LogFile.ReadLine();
                            //Npatel Added
                            //Since the Result is Added in between Exception TExt
                            //we have to check if any of the line is the results line,
                            //if so then we updated the VarInfo object with it,
                            //else we will always get VAR_UNSUPPORTED for exception scenarios

                            GetResult(ref Line, LogFile, ref Var, Log);

                        } while (Line != null && !Line.StartsWith(Constants.VAR_START, StringComparison.CurrentCulture)
                            && !Line.StartsWith(Constants.DATE_START.ToString(), StringComparison.CurrentCulture)
                            && !Line.StartsWith(@"// Vars expected", StringComparison.CurrentCulture));
                        RetVal = true;
                    }
                }
            }
            return RetVal;
        }

        #endregion

        #region Private members
        /// <summary>
        /// 
        /// </summary>
        private string m_LogFilePath = null;
        /// <summary>
        /// 
        /// </summary>
        private int m_TraceLevel = 0;
        /// <summary>
        /// 
        /// </summary>
        private LogInfo m_LogInfo = null;
        #endregion

        #region Constants

        /// <summary>
        /// Class which contains the parsing constants for the outer class
        /// </summary>
        /// <remarks></remarks>
        private class Constants
        {
            // Common
            /// <summary>
            /// 
            /// </summary>
            public const char DATE_START = '[';
            /// <summary>
            /// 
            /// </summary>
            public const char DATE_END = ']';
            /// <summary>
            /// 
            /// </summary>
            public const char TOKEN_SEPARATOR = '>';
            /// <summary>
            /// 
            /// </summary>
            public const string VALUE_END = " ,.;\\/"; //removing . from the VALUE_END range (This is needed for varmap parsing//" ,.;\\/";
            /// <summary>
            /// 
            /// </summary>
            public const string VALUE_END_NoComma = " .;\\/";//removing , since for data like Pid it is of the form pid=1,1,1 
            /// <summary>
            /// 
            /// </summary>
            public const string LOG_START = "*LOG_START*-";
            /// <summary>
            /// 
            /// </summary>
            public const string LOG_END = "*LOG_DONE*";
            /// <summary>
            /// 
            /// </summary>
            public const string VALUE_END_SquareBrace = "]";//this if for BugIds, since there can be multiple ids separated by spaces, for instance [bug= 123 456 789]

            // Result tokens
            /// <summary>
            /// 
            /// </summary>
            public const string SET = "Set";
            /// <summary>
            /// 
            /// </summary>
            public const string LEVEL = "Level";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR = "Var";
            /// <summary>
            /// 
            /// </summary>
            public const string CID = "Cid";
            /// <summary>
            /// 
            /// </summary>
            public const string PID = "Pid";
            /// <summary>
            /// 
            /// </summary>
            public const string BugId = "bug";
            /// <summary>
            /// 
            /// </summary>
            public const string MODE = "Mode";
            /// <summary>
            /// 
            /// </summary>
            public const char RESULT_SEPARATOR = ':';

            // Var tokens

            /* BUGBUG 
             * History
             * Npatel Changed 03/04/05
             * Start Change
             *Changing the VAR_START to // /m since previous one was incorrect
             */
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_START = "// /m";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_SET = "/s";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_LEVEL = "/l";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_VAR = "/v";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_SEED = "/seed";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_ID = "/ivar";
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_VARMAP = "/m";
            /// <summary>
            /// 
            /// </summary>
            public const char VAR_SEPARATOR = ':';
            /// <summary>
            /// 
            /// </summary>
            public const string VAR_EXCEPTION = @"// <<<EXCEPTION>>>";

            // Trace tokens
            /// <summary>
            /// 
            /// </summary>
            public const char TRACE_SEPARATOR = '-';


        }
        #endregion
    }

    #endregion
}
