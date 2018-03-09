// -----------------------------------------------------------------------
// <copyright file="LogWriter.cs" company="Ladbrokes Pvt Ltd">
// Copyright (c) Ladbrokes. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;


#pragma warning disable 618

namespace AutonitroLogger.Logger.BaseLogger
{
    #region Helpers

    /// <summary>
    /// Contains the constants for the Log Writer
    /// </summary>
    [ComVisible(false)]
    internal sealed class Constants
    {
        internal const string FileOpenedMsg = @"File is already opened by logger";
        internal const string TruncatedMsg = @"<<<TruncatedMsg>>>";
    }

    #region internal access

    #region Foreground Color enumeration
    /// <summary>
    /// Used internally for writing to the console.
    /// </summary>
    [ComVisible(false)]
    internal enum Foreground : int
    {
        /// <summary>
        /// Foreground: Black
        /// </summary>
        Black = 0,

        /// <summary>
        /// Foreground: Red
        /// </summary>
        Red = CLR.FOREGROUND_RED,

        /// <summary>
        /// Foreground: Light Red
        /// </summary>
        LightRed = CLR.FOREGROUND_RED | CLR.FOREGROUND_INTENSITY,

        /// <summary>
        /// Foreground Green
        /// </summary>
        Green = CLR.FOREGROUND_GREEN,

        /// <summary>
        /// Foreground Light Green
        /// </summary>
        LightGreen = CLR.FOREGROUND_GREEN | CLR.FOREGROUND_INTENSITY,

        /// <summary>
        /// Foreground Blue
        /// </summary>
        Blue = CLR.FOREGROUND_BLUE,

        /// <summary>
        /// Foreground Light Blue
        /// </summary>
        LightBlue = CLR.FOREGROUND_BLUE | CLR.FOREGROUND_INTENSITY,

        /// <summary>
        /// Foreground Magenta
        /// </summary>
        Magenta = CLR.FOREGROUND_RED | CLR.FOREGROUND_BLUE,

        /// <summary>
        /// Foreground LightMagenta
        /// </summary>
        LightMagenta = CLR.FOREGROUND_RED | CLR.FOREGROUND_BLUE | CLR.FOREGROUND_INTENSITY,


        /// <summary>
        /// Foreground Brown
        /// </summary>
        Brown = CLR.FOREGROUND_RED | CLR.FOREGROUND_GREEN,

        /// <summary>
        /// Foreground Yellow
        /// </summary>
        Yellow = CLR.FOREGROUND_RED | CLR.FOREGROUND_GREEN | CLR.FOREGROUND_INTENSITY,

        /// <summary>
        /// Foreground Cyan
        /// </summary>
        Cyan = CLR.FOREGROUND_GREEN | CLR.FOREGROUND_BLUE,

        /// <summary>
        /// Foreground LightCyan
        /// </summary>
        LightCyan = CLR.FOREGROUND_GREEN | CLR.FOREGROUND_BLUE | CLR.FOREGROUND_INTENSITY,

        /// <summary>
        /// Foreground DarkGray
        /// </summary>
        DarkGray = CLR.FOREGROUND_INTENSITY,

        /// <summary>
        /// Foreground Gray
        /// </summary>
        Gray = CLR.FOREGROUND_RED | CLR.FOREGROUND_GREEN | CLR.FOREGROUND_BLUE,

        /// <summary>
        /// Foreground White
        /// </summary>
        White =
                        CLR.FOREGROUND_RED |
                        CLR.FOREGROUND_GREEN |
                        CLR.FOREGROUND_BLUE |
                        CLR.FOREGROUND_INTENSITY
    } // end of enum
    #endregion

    #region Backgrounde color enumeration

    /// <summary>
    /// Used internally for writing to the console
    /// </summary>
    [ComVisible(false)]
    internal enum Background : int
    {

        /// <summary>
        /// 
        /// </summary>
        Black = 0,

        /// <summary>
        /// 
        /// </summary>
        Red = CLR.BACKGROUND_RED,

        /// <summary>
        /// 
        /// </summary>
        LightRed = CLR.BACKGROUND_RED | CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        Green = CLR.BACKGROUND_GREEN,

        /// <summary>
        /// 
        /// </summary>
        LightGreen = CLR.BACKGROUND_GREEN | CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        Blue = CLR.BACKGROUND_BLUE,

        /// <summary>
        /// 
        /// </summary>
        LightBlue = CLR.BACKGROUND_BLUE | CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        Magenta = CLR.BACKGROUND_RED | CLR.BACKGROUND_BLUE,

        /// <summary>
        /// 
        /// </summary>
        LightMagenta = CLR.BACKGROUND_RED | CLR.BACKGROUND_BLUE | CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        Brown = CLR.BACKGROUND_RED | CLR.BACKGROUND_GREEN,

        /// <summary>
        /// 
        /// </summary>
        Yellow = CLR.BACKGROUND_RED | CLR.BACKGROUND_GREEN | CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        Cyan = CLR.BACKGROUND_GREEN | CLR.BACKGROUND_BLUE,

        /// <summary>
        /// 
        /// </summary>
        LightCyan = CLR.BACKGROUND_GREEN | CLR.BACKGROUND_BLUE | CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        DarkGray = CLR.BACKGROUND_INTENSITY,

        /// <summary>
        /// 
        /// </summary>
        Gray = CLR.BACKGROUND_RED | CLR.BACKGROUND_GREEN | CLR.BACKGROUND_BLUE,

        /// <summary>
        /// 
        /// </summary>
        White =
                        CLR.BACKGROUND_RED |
                        CLR.BACKGROUND_GREEN |
                        CLR.BACKGROUND_BLUE |
                        CLR.BACKGROUND_INTENSITY
    } // end of enum

    #endregion

    //-------------------------------------------------------------------
    ///
    /// <summary>
    /// Provides basic color printing services for the console.
    /// </summary>
    ///
    /// <remarks>
    ///    Note1: I saw plans for this to be implemented in Everet.
    ///    Note2: Doesn't support writing to STDERR.
    /// </remarks>
    ///
    //-------------------------------------------------------------------
    // TODO:  Having stateful model vs. stateless might cut some
    // redundant calls. E.g. cache Cons Buff Info.
    [ComVisible(false)]
    internal sealed class ColorConsole
    {

        private ColorConsole()
        {

        }

        #region internal access

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fColor"></param>
        internal static void Write(string str, Foreground fColor)
        {
            if (!GetConsBufInfo(StdOut, out csbi))
            {
                // TODO: Fall back under non-console environment (e.g. dbgclr),
                // need to communicate to caller up the stack
                Console.Write(str);
                return;
            }
            Background bColor = (Background)(csbi.wAttributes & 0xF0);
            WriteHelper(str, fColor, bColor, csbi.wAttributes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="fColor"></param>
        /// <param name="bColor"></param>
        internal static void Write(string str, Foreground fColor, Background bColor)
        {
            if (!GetConsBufInfo(StdOut, out csbi))
            {
                // Fall back under non-console environment (e.g. dbgclr).
                // TODO: Need to communicate failure to caller up the stack.
                Console.Write(str);
                return;
            }
            WriteHelper(str, fColor, bColor, csbi.wAttributes);
        }
        #endregion


        #region Private access
        static void WriteHelper(string str, Foreground fColor, Background bColor, short attr)
        {
            IntPtr h = StdOut;
            SetConsTextAttr(h, (short)((ushort)fColor | (ushort)bColor));
            Console.Write(str);
            // Restore old settings
            SetConsTextAttr(h, attr);
        }

        static void SetConsTextAttr(
                IntPtr hConsoleOutput,
                short wAttributes)
        {
            if (!SetConsoleTextAttribute(
                    hConsoleOutput, wAttributes))
            {
                //// ToDo: LoggerExpection was removed to work around TFS thing.
                //// throw new LoggerException("Unable to set console text attr.");
                throw new InvalidOperationException("Unable to set console text attr.");
            }
        }

        static bool GetConsBufInfo(
                IntPtr hConsoleOutput,
                out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInf)
        {
            if (GetConsoleScreenBufferInfo(hConsoleOutput, out lpConsoleScreenBufferInf) == 0)
            {
                // Fall back under non-console environment (e.g. dbgclr).
                return false;
            }

            return true;
        }

        static IntPtr StdOut
        {
            get
            {
                //// TODO: Might throw (see above)
                IntPtr h = GetStdHandle(STD_HANDLES.STD_OUTPUT_HANDLE);
                if (h == STD_HANDLES.INVALID_HANDLE_VALUE)
                {
                    //// throw new LoggerException("Unable to get STDOUT handle");ToDo: LoggerExpection was removed to work around TFS thing.
                    //// throw new LoggerException("Unable to get STDOUT handle");
                    throw new InvalidOperationException("Unable to set console text attr.");


                }
                return h;
            }
        }

        [DllImport("kernel32")]
        static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32")]
        static extern int GetConsoleScreenBufferInfo(
                IntPtr hConsoleOutput,
                out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

        [DllImport("kernel32")]
        static extern bool SetConsoleTextAttribute(
                IntPtr hConsoleOutput,
                short wAttributes);

        static CONSOLE_SCREEN_BUFFER_INFO csbi =
                new CONSOLE_SCREEN_BUFFER_INFO();

        #endregion
    }


    #endregion

    #region Internal access

    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    internal struct CONSOLE_SCREEN_BUFFER_INFO
    {
        internal COORD dwSize;
        internal COORD dwCursorPosition;
        internal short wAttributes;
        internal SMALL_RECT srWindow;
        internal COORD dwMaximumWindowSize;
    }

    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    internal struct COORD
    {
        internal short X;
        internal short Y;
    }

    [ComVisible(false), StructLayout(LayoutKind.Sequential)]
    internal struct SMALL_RECT
    {
        internal short Left;
        internal short Top;
        internal short Right;
        internal short Bottom;
    }

    //-------------------------------------------------------------------
    ///
    /// <summary>
    /// RGB colors for the console.
    /// </summary>
    ///
    /// <remarks>
    /// It is possible to combine colors with corresponding 
    /// intensities. E.g. following will create intesive red text with 
    /// intensive yellow background:
    /// ----------------------------------------
    /// FOREGROUND_RED | FOREGROUND_INTENSITY | 
    /// BACKGROUND_YELLOW | BACKGROUND_INTENSITY
    /// ---------------------------------------- 
    /// </remarks>
    ///
    //-------------------------------------------------------------------
    [ComVisible(false)]
    internal class CLR
    {
        #region Text
        internal const short FOREGROUND_BLUE = 0x0001;
        internal const short FOREGROUND_GREEN = 0x0002;
        internal const short FOREGROUND_RED = 0x0004;
        internal const short FOREGROUND_INTENSITY = 0x0008;
        #endregion

        #region Background
        internal const short BACKGROUND_BLUE = 0x0010;
        internal const short BACKGROUND_GREEN = 0x0020;
        internal const short BACKGROUND_RED = 0x0040;
        internal const short BACKGROUND_INTENSITY = 0x0080;
        #endregion
    }

    [ComVisible(false)]
    internal class STD_HANDLES
    {
        internal const int STD_INPUT_HANDLE = -10;
        internal const int STD_OUTPUT_HANDLE = -11;
        internal const int STD_ERROR_HANDLE = -12;

        internal static readonly IntPtr INVALID_HANDLE_VALUE =
                new IntPtr(-1);
    }
    #endregion

    #endregion

    #region LogWriter class

    //-------------------------------------------------------------------
    ///
    /// <summary>
    /// Performs formated logging to misc. destinations.
    /// </summary>
    ///
    //-------------------------------------------------------------------
    internal class LogWriter : IDisposable
    {
        private bool disposed;

        #region .ctors and dispose
        /// <summary>
        /// 
        /// </summary>
        internal LogWriter()
        {
            this.scratchSW = new StringWriter(CultureInfo.CurrentCulture);
            this.scratchXTW = new XmlTextWriter(this.scratchSW);
        }

        /// <summary>
        /// Implementing IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Implementing IDisposable.
        /// </summary>
        /// <param name="disposing">Has Dispose already been called?</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.Close();
                }

                // Note disposing has been done.
                this.disposed = true;
            }
        }

        #endregion

        #region internal access

        #region Properties
        /// <summary>
        /// Specifies whether to append to existing log file.
        /// </summary>
        /// 
        internal bool Append
        {
            get { return (this.append); }
            set { this.append = value; }
        }

        /// <summary>
        /// Specifies whether to write date.
        /// </summary>
        internal bool DateOn
        {
            get { return (this.dateOn); }
            set { this.dateOn = value; }
        }

        /// <summary>
        /// Specifies log file name.
        /// </summary>
        internal string Log
        {
            get { return (this.log); }
            set { this.log = value; }
        }

        /// <summary>
        /// Specifies verbose log file name.
        /// </summary>
        internal string LogVerbose
        {
            get { return (this.logVrb); }
            set { this.logVrb = value; }
        }

        /// <summary>
        /// Specifies xml log file name.
        /// </summary>
        internal string LogXml
        {
            get { return (this.logXml); }
            set { this.logXml = value; }
        }

        /// <summary>
        /// Specifies invoking module name to be logged.
        /// </summary>
        internal string ModuleName
        {
            get { return (this.moduleName); }
            set { this.moduleName = value; }
        }

        internal OutputCategory OutputCategory
        {
            get { return (this.outCat); }
            set { this.outCat = value; }
        }

        internal OutputDestination OutputDestination
        {
            get { return (this.outDest); }
            // TODO: Is this really necessary? There was a DCR asking
            // for this but it wouldn't work well under e.g. stress,
            // i.e. long run. Base line - remove overwriting to log -
            // '| OutputDestination.Log'
            set { this.outDest = value | OutputDestination.Log; }
        }

        /// <summary>
        /// Specifies sharing mode for log files.
        /// </summary>
        internal bool SharedWrite
        {
            get { return (this.sharedWrite); }
            set { this.sharedWrite = value; }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        internal void StartLogging()
        {
            this.Open();
            this.WriteStartMessage();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void StopLogging()
        {
            this.WriteEndMessage();
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string FormatProlog(OutputCategory cat, string msg)
        {
            return this.FormatPrologEx(
                    cat, msg, this.procId, AppDomain.GetCurrentThreadId());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <returns></returns>
        internal string[] FormatProlog(OutputCategory cat)
        {
            return this.FormatProlog(cat, GetTokens());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        internal string[] FormatProlog(OutputCategory cat, string[] tokens)
        {
            return this.FormatPrologEx(
                    cat, this.procId, AppDomain.GetCurrentThreadId(), tokens);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        /// <param name="procId"></param>
        /// <param name="threadId"></param>
        /// <returns></returns>
        internal string FormatPrologEx(
                OutputCategory cat, string msg, int procId, int threadId)
        {
            if (msg == null)
                throw new ArgumentNullException("Invalid parameter 'msg'");

            lock (this.lockLogBuf)
            {

                // Date & Time
                DateTime dt = DateTime.Now;
                this.logBuf.Length = 0;
                this.logBuf.Append("[");
                if (this.dateOn)
                {
                    string d =
                            dt.ToString("yy-MM-dd ", DateTimeFormatInfo.InvariantInfo);
                    this.logBuf.Append(d);
                }
                string t = dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
                this.logBuf.Append(t);

                this.logBuf.Append("]");

                this.logBuf.Append(" - ");

                // message body
                if (msg.Length > maxMessageLength)
                {
                    this.logBuf.Append(msg.Substring(
                            0, maxMessageLength - Constants.TruncatedMsg.Length));
                    this.logBuf.Append(Constants.TruncatedMsg);
                }
                else
                {
                    this.logBuf.Append(msg);
                }

                return this.logBuf.ToString();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="procId"></param>
        /// <param name="threadId"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        internal string[] FormatPrologEx(
                OutputCategory cat, int procId, int threadId, string[] tokens)
        {
            // Date & Time
            DateTime dt = DateTime.Now;
            if (this.dateOn)
            {
                string d =
                        dt.ToString("yy-MM-dd ", DateTimeFormatInfo.InvariantInfo);
                tokens[(int)LogTokens.TS] = d;
            }
            string t = dt.ToString("T", DateTimeFormatInfo.InvariantInfo);
            tokens[(int)LogTokens.TS] += t;

            // process id
            tokens[(int)LogTokens.PId] = procId.ToString(CultureInfo.CurrentCulture);

            // thread id
            tokens[(int)LogTokens.TId] = threadId.ToString(CultureInfo.CurrentCulture);

            // module name
            tokens[(int)LogTokens.Mod] = this.moduleName;

            return tokens;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="line"></param>
        internal void WriteLine(OutputCategory cat, string line)
        {
            this.WriteHelper(cat, line);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string[] WriteTokens(OutputCategory cat, string msg)
        {
            return this.WriteTokens(cat, msg, GetTokens());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        internal string[] WriteTokens(OutputCategory cat, string msg, string[] tokens)
        {
            if (tokens[(int)LogTokens.Msg] == null)
                tokens[(int)LogTokens.Msg] = msg;

            tokens[(int)LogTokens.Cat] = cat.ToString();
            this.WriteHelper(cat, tokens);

            return tokens;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string[] Comment(OutputCategory cat, string msg)
        {
            this.WriteLine(cat, "// " + msg);
            return this.WriteTokens(cat, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="res"></param>
        /// <param name="msg"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        internal string[] WriteResult(OutputCategory cat, ExeResult res, string msg, string[] tokens)
        {
            tokens[(int)LogTokens.Res] = res.ToString();
            return this.WriteTokens(cat, msg, tokens);
        }

        /// <summary>
        /// Method to trace log always.
        /// </summary>
        /// <param name="msg">String with message that is displayed on console and log file.</param>
        internal virtual void Always(string msg)
        {
            this.WriteMsg(OutputCategory.Alw, msg);
        }

        /// <summary>
        /// Method to trace log for error level.
        /// </summary>
        /// <param name="msg">String with message that is displayed on console and log file.</param>
        internal virtual void Error(string msg)
        {
            this.WriteMsg(OutputCategory.Err, msg);
        }

        /// <summary>
        /// Method to trace log at warning level.
        /// </summary>
        /// <param name="msg"></param>
        internal virtual void Warning(string msg)
        {
            this.WriteMsg(OutputCategory.Wrn, msg);
        }

        /// <summary>
        /// Method to trace log at trace level.
        /// </summary>
        /// <param name="msg"></param>
        internal virtual void Trc(string msg)
        {
            this.WriteMsg(OutputCategory.Trc, msg);
        }

        /// <summary>
        /// Tracing at a custom trace level based on category
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        internal void Custom(int cat, string msg)
        {
            this.WriteMsg((OutputCategory)cat, msg);
        }

        internal static string[] GetTokens()
        {
            return new string[Enum.GetValues(typeof(LogTokens)).Length + 1];
        }
        #endregion

        #region Private access
        void Open()
        {
            if ((this.outDest & OutputDestination.Log) != 0)
            {
                this.Open(ref this.logTW, this.log);
                this.Open(ref this.logVTW, this.logVrb);
            }

            if ((this.outDest & OutputDestination.XmlLog) != 0)
            {
                this.Open(ref this.logXTW, this.logXml);
            }
        }

        void Open(ref TextWriter sw, string file)
        {
            if (sw != null)
            {
                //// ToDo: LoggerExpection was removed to work around TFS thing.
                //// throw new LoggerException(Constants.FileOpenedMsg);
            }
            if (file == null || file.Length == 0)
                return;

            sw = TextWriter.Synchronized(new StreamWriter(
                    File.Open(
                    file,
                    this.append ? FileMode.Append : FileMode.Create,
                    FileAccess.Write,
                    this.sharedWrite ? FileShare.ReadWrite : FileShare.Read)) { AutoFlush = true });

        }

        void Close()
        {
            this.Close(ref this.logTW);
            this.Close(ref this.logVTW);
            this.Close(ref this.logXTW);
        }

        void Close(ref TextWriter sw)
        {
            if (sw != null)
            {
                sw.Close();
                sw = null;
            }
        }

        void WriteOutputDebugString(OutputCategory cat, string line)
        {
            if ((this.outCat & cat) == 0)
                return;

            if ((this.outDest & OutputDestination.DbgOut) != 0)
            {
                lock (this.lockOutputDebug)
                {
                    OutputDebugString(line + '\r' + '\n');
                }
            }
        }

        void WriteStdOut(OutputCategory cat, string line)
        {
            if ((this.outCat & cat) == 0)
                return;

            if ((this.outDest & OutputDestination.StdOut) != 0)
            {

                // --- hack ---
                // TODO: color console hack
                // scanning for VAR_PASS, VAR_FAIL, ABORT, VAR_ABORT, GRP_ABORT
                Foreground color = Foreground.Black;

                if (line.IndexOf(OutputCategory.Alw.ToString(CultureInfo.CurrentCulture), StringComparison.CurrentCulture) != -1)
                {
                    color = Foreground.LightGreen;
                }
                else if (line.IndexOf(OutputCategory.Wrn.ToString(CultureInfo.CurrentCulture), StringComparison.CurrentCulture) != -1)
                {
                    color = color = Foreground.Yellow;
                }
                else if (line.IndexOf(OutputCategory.Err.ToString(CultureInfo.CurrentCulture), StringComparison.CurrentCulture) != -1)
                {
                    color = Foreground.LightRed;
                }
                else if (
                    //// line.IndexOf("<<<EXCEPTION>>>") != -1 ||
                                        line.IndexOf("LOG_START", StringComparison.CurrentCulture) != -1 ||
                                        line.IndexOf("LOG_DONE", StringComparison.CurrentCulture) != -1)
                {
                    color = Foreground.White;
                }

                lock (this.lockConsole)
                {
                    if (color != Foreground.Black)
                    {
                        ColorConsole.Write(line, color);
                        Console.WriteLine();
                    }
                    else
                    { // everything else
                        Console.WriteLine(line);
                    }
                }
                // --- end hack ---
            }
        }

        void WriteLog(OutputCategory cat, string line)
        {
            if ((this.outCat & cat) == 0)
                return;

            if ((this.outDest & OutputDestination.Log) != 0)
            {
                this.WriteLogHelper(this.logTW, line);
            }
        }

        void WriteVerboseLog(OutputCategory cat, string line)
        {
            if ((this.outDest & OutputDestination.VerboseLog) != 0)
            {
                this.WriteLogHelper(this.logVTW, line);
            }
        }

        void WriteXmlLog(OutputCategory cat, string[] tokens)
        {
            if ((this.outDest & OutputDestination.XmlLog) != 0)
            {
                if (this.logXTW != null)
                {
                    lock (this.lockXmlLog)
                    {

                        this.scratchXTW.WriteStartElement("Rec");

                        for (int i = (int)LogTokens.Msg + 1; i < tokens.Length; i++)
                            if (tokens[i] != null)
                                this.scratchXTW.WriteAttributeString(
                                        ((LogTokens)i).ToString(), tokens[i]);

                        if (tokens[(int)LogTokens.Msg] != null)
                            this.scratchXTW.WriteString(tokens[(int)LogTokens.Msg]);

                        this.scratchXTW.WriteEndElement();

                        this.logXTW.WriteLine(this.scratchSW.ToString());
                        this.scratchSW.GetStringBuilder().Length = 0;
                    }
                }
            }
        }

        void WriteLogHelper(TextWriter sw, string line)
        {
            if (sw != null)
            {
                try
                {
                    sw.WriteLine(line);
                }
                catch (Exception)
                {
                    sw.Close();
                }
            }
        }

        void WriteHelper(OutputCategory cat, string line)
        {
            this.WriteVerboseLog(cat, line);
            this.WriteOutputDebugString(cat, line);
            this.WriteStdOut(cat, line);
            this.WriteLog(cat, line);
        }

        void WriteHelper(OutputCategory cat, string[] tokens)
        {
            this.WriteXmlLog(cat, tokens);
        }

        void WriteMsg(OutputCategory cat, string msg)
        {
            this.WriteLine(cat, this.FormatProlog(cat, msg));
            this.WriteTokens(cat, msg, this.FormatProlog(cat));
        }

        void WriteStartMessage()
        {
            const string prologue = "*LOG_START*-";
            const OutputCategory cat = OutputCategory.Alw;
            this.WriteLine(cat, prologue);
            this.WriteTokens(cat, prologue);
        }

        void WriteEndMessage()
        {
            const string epilogue = "*LOG_DONE*";
            const OutputCategory cat = OutputCategory.Alw;
            this.WriteLine(cat, epilogue);
            this.WriteTokens(cat, epilogue);
        }

        static void OutputDebugString(string msg)
        {
            System.Diagnostics.Debugger.Log(0, string.Empty, msg);
        }

        #region Fields
        bool append = true;
        bool dateOn = false;
        int procId = Process.GetCurrentProcess().Id;
        bool sharedWrite = false;

        string log = null; // log file name
        string logVrb = null; // verbose log file name
        string logXml = null; // xml log file name
        TextWriter logTW = null;
        TextWriter logVTW = null; // Verbose
        TextWriter logXTW = null; // Xml
        StringWriter scratchSW = null;
        XmlTextWriter scratchXTW = null;

        string moduleName = AppDomain.CurrentDomain.FriendlyName; // Invoking module
        StringBuilder logBuf = new StringBuilder(maxLineLength, maxLineLength);
        StringBuilder msgBuf = new StringBuilder(maxMessageLength, maxMessageLength);
        OutputDestination outDest = OutputDestination.None;
        OutputCategory outCat = OutputCategory.None;

        const int maxMessageLength = 1024 * 64;
        const int maxLineLength = maxMessageLength + 100;

        // The object for locking the logBug
        private readonly object lockLogBuf = new object();
        // The object for locking the scratchSW and scratchXTW
        private readonly object lockXmlLog = new object();
        // The object for locking function OutputDebugString since it is not thread-safe
        private readonly object lockOutputDebug = new object();
        // The object for locking ColorConsole since it is not thread-safe
        private readonly object lockConsole = new object();


        #endregion
        #endregion
    }
    #endregion

    #region Logger class
    //-------------------------------------------------------------------
    ///
    /// <summary>
    /// Performs formated logging to misc. destinations.
    /// </summary>
    ///
    //-------------------------------------------------------------------
    [ClassInterface(ClassInterfaceType.AutoDual)]
    internal class Logger : IDisposable
    {
        #region Private access
        private bool disposed;
        private LogWriter _LogWriter = new LogWriter();
        #endregion

        #region .ctors
        internal Logger() { }

        #region Properties
        /// <summary>
        /// Specifies whether to append to existing log file.
        /// </summary>
        internal bool Append
        {
            get { return this._LogWriter.Append; }
            set { this._LogWriter.Append = value; }
        }

        /// <summary>
        /// Specifies whether to write date.
        /// </summary>
        internal bool DateOn
        {
            get { return this._LogWriter.DateOn; }
            set { this._LogWriter.DateOn = value; }
        }

        /// <summary>
        /// Specifies log file name.
        /// </summary>
        internal string Log
        {
            get { return this._LogWriter.Log; }
            set { this._LogWriter.Log = value; }
        }

        /// <summary>
        /// Specifies verbose log file name.
        /// </summary>
        internal string LogVerbose
        {
            get { return this._LogWriter.LogVerbose; }
            set { this._LogWriter.LogVerbose = value; }
        }

        /// <summary>
        /// Specifies xml log file name.
        /// </summary>
        internal string LogXml
        {
            get { return this._LogWriter.LogXml; }
            set { this._LogWriter.LogXml = value; }
        }

        /// <summary>
        /// Specifies invoking module name to be logged.
        /// </summary>
        internal string ModuleName
        {
            get { return this._LogWriter.ModuleName; }
            set { this._LogWriter.ModuleName = value; }
        }

        /// <summary>
        /// Property for setting and getting the OutputCategory.
        /// </summary>
        /// <example>
        /// Logger.OutputCategory = OutputCategory.Always
        /// </example>
        /// <value>What is the value field?</value>
        internal OutputCategory OutputCategory
        {
            get { return this._LogWriter.OutputCategory; }
            set { this._LogWriter.OutputCategory = value; }
        }

        /// <summary>
        /// Property for setting and getting the OutputDestination.
        /// </summary>
        internal OutputDestination OutputDestination
        {
            get { return this._LogWriter.OutputDestination; }
            // TODO: Is this really necessary? There was a DCR asking
            // for this but it wouldn't work well under e.g. stress,
            // i.e. long run. Base line - remove overwriting to log -
            // '| OutputDestination.Log'
            set { this._LogWriter.OutputDestination = value; }
        }

        /// <summary>
        /// Specifies sharing mode for log files.
        /// </summary>
        internal bool SharedWrite
        {
            get { return (this._LogWriter.SharedWrite); }
            set { this._LogWriter.SharedWrite = value; }
        }
        #endregion

        /// <summary>
        /// Implementing IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #region GetLogger method
        /// <summary>
        /// Static method that returns an initialized logger.
        /// </summary>
        /// <param name="moduleName">
        /// Name of the module from where you are tracing.
        /// </param>
        /// <param name="fileName">
        /// Name of log the file where logging occurs.
        /// </param>
        /// <param name="verboseFileName">
        /// Name of the log file where trace/verbose logging occurs.
        /// </param>
        /// <returns>An initialized logger method with the StartLogging method called.</returns>
        /// <param name="appendLog">true to append to log</param>
        internal static Logger GetLogger(string moduleName, string fileName, string verboseFileName, bool appendLog)
        {
            return Logger.GetLogger(
                moduleName,
                fileName,
                verboseFileName,
                appendLog,
                OutputCategory.Alw | OutputCategory.Err | OutputCategory.Wrn,
                OutputDestination.StdOut | OutputDestination.Log | OutputDestination.VerboseLog | OutputDestination.DbgOut);
        }
        /// <summary>
        /// Static method that returns an initialized logger.
        /// </summary>
        /// <param name="moduleName">
        /// Name of the module from where you are tracing.
        /// </param>
        /// <param name="fileName">
        /// Name of log the file where logging occurs.
        /// </param>
        /// <param name="verboseFileName">
        /// Name of the log file where trace/verbose logging occurs.
        /// </param>
        /// <returns>An initialized logger method with the StartLogging method called.</returns>
        internal static Logger GetLogger(string moduleName, string fileName, string verboseFileName)
        {
            return Logger.GetLogger(moduleName, fileName, verboseFileName, true);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="fileName"></param>
        /// <param name="verboseFileName"></param>
        /// <param name="outputCategory"></param>
        /// <param name="outputDestination"></param>
        ///  <param name="appendLog">true to append to log</param>
        /// <returns></returns>
        /// 
        internal static Logger GetLogger(
            string moduleName,
            string fileName,
            string verboseFileName,
                bool appendLog,
            OutputCategory outputCategory,
            OutputDestination outputDestination)
        {
            Logger logger = new Logger();

            logger.Log = fileName;
            logger.LogVerbose = verboseFileName;
            logger.SharedWrite = true;
            logger.OutputCategory = outputCategory;
            logger.OutputDestination = outputDestination;
            logger.DateOn = true;
            logger.ModuleName = moduleName;
            logger.Append = appendLog;
            logger.StartLogging();

            return logger;
        }
        #endregion

        #region internal access



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        internal static string[] GetTokens()
        {
            return LogWriter.GetTokens();
        }

        /// <summary>
        /// Method to start logging.
        /// </summary>
        internal void StartLogging()
        {
            this._LogWriter.StartLogging();
        }

        /// <summary>
        /// 
        /// </summary>
        internal void StopLogging()
        {
            this._LogWriter.StopLogging();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        internal string FormatProlog(OutputCategory cat, string msg)
        {
            return this._LogWriter.FormatProlog(cat, msg);
        }

        internal string[] FormatProlog(OutputCategory cat)
        {
            return this._LogWriter.FormatProlog(cat);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        internal string[] FormatProlog(OutputCategory cat, string[] tokens)
        {
            return this._LogWriter.FormatProlog(cat, tokens);
        }

        internal string FormatPrologEx(
                OutputCategory cat, string msg, int procId, int threadId)
        {
            return this._LogWriter.FormatPrologEx(cat, msg, procId, threadId);
        }

        internal string[] FormatPrologEx(
                OutputCategory cat, int procId, int threadId, string[] tokens)
        {
            return this._LogWriter.FormatPrologEx(cat, procId, threadId, tokens);
        }

        internal string WriteLine(OutputCategory cat, string line)
        {
            this._LogWriter.WriteLine(cat, line);

            return line;
        }

        internal string[] WriteTokens(OutputCategory cat, string msg)
        {
            return this._LogWriter.WriteTokens(cat, msg);
        }

        internal string[] WriteTokens(OutputCategory cat, string msg, string[] tokens)
        {
            return this._LogWriter.WriteTokens(cat, msg, tokens);
        }

        internal string[] Comment(OutputCategory cat, string msg)
        {
            return this._LogWriter.Comment(cat, msg);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="res"></param>
        /// <param name="msg"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        internal string[] WriteResult(OutputCategory cat, ExeResult res, string msg, string[] tokens)
        {
            return this._LogWriter.WriteResult(cat, res, msg, tokens);
        }

        /// <summary>
        /// Always to write trace to console and log file(s).
        /// </summary>
        /// <param name="msg">
        /// Message to display/write.
        /// </param>
        internal virtual string Always(string msg)
        {
            this._LogWriter.Always(msg);

            return msg;
        }

        /// <summary>
        /// Always to write trace to console and log file(s).
        /// </summary>
        /// <param name="format">
        /// Message to display/write.
        /// </param>
        /// <param name="args"></param>
        internal virtual string Always(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            this._LogWriter.Always(msg);

            return msg;
        }

        /// <summary>
        /// Write to trace when error level is set to err.
        /// </summary>
        /// <param name="msg">
        /// Message to display/write.
        /// </param>
        internal virtual string Error(string msg)
        {
            this._LogWriter.Error(msg);

            return msg;
        }




        /// <summary>
        /// Write to trace when error level is set to err.
        /// </summary>
        /// <param name="format">
        /// Message to display/write.
        /// </param>
        /// <param name="args"></param>
        internal virtual string Error(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            this._LogWriter.Error(msg);

            return msg;
        }

        /// <summary>
        /// Always to write trace to console and log file(s).
        /// </summary>
        /// <param name="msg">
        /// Message to display/write.
        /// </param>
        internal virtual string Warning(string msg)
        {
            this._LogWriter.Warning(msg);

            return msg;
        }


        /// <summary>
        /// Always to write trace to console and log file(s).
        /// </summary>
        /// <param name="format">
        /// Message to display/write.
        /// </param>
        /// <param name="args"></param>
        internal virtual string Warning(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            this._LogWriter.Warning(msg);

            return msg;
        }
        /// <summary>
        /// Always to write trace to console and log file(s).
        /// </summary>
        /// <param name="msg">
        /// Message to display/write.
        /// </param>
        internal virtual string Trace(string msg)
        {
            this._LogWriter.Trc(msg);

            return msg;
        }

        /// <summary>
        /// Always to write trace to console and log file(s).
        /// </summary>
        /// 
        /// <param name="format">
        /// Message to display/write.
        /// </param>
        /// <param name="args"></param>
        internal virtual string Trace(string format, params object[] args)
        {
            string msg = string.Format(format, args);
            this._LogWriter.Trc(msg);

            return msg;
        }


        /// <summary>
        /// trace the calling method
        /// </summary>
        internal virtual string TraceCallingMethod()
        {
            StackFrame sf = new StackFrame(1, true);
            StackTrace st = new StackTrace(true);

            return this.Trace(
                "IN METHOD {0}({1})",
                string.Empty.PadLeft(st.FrameCount, '*'),
                sf.GetMethod().Name);
        }
        /// <summary>
        /// trace the calling method
        /// </summary>
        /// <param name="format">
        /// Message to display/write.
        /// </param>
        /// <param name="args">Arguments that should be logged in trace</param>
        internal virtual string TraceCallingMethod(string format, params object[] args)
        {
            StackFrame sf = new StackFrame(1, true);
            StackTrace st = new StackTrace(true);

            return this.Trace(
                "IN METHOD {0}({1})--{2}",
                string.Empty.PadLeft(st.FrameCount, '*'),
                sf.GetMethod().Name,
                string.Format(format, args));
        }
        /// <summary>
        /// Method to trace using custom category.
        /// </summary>
        /// <param name="cat"></param>
        /// <param name="msg"></param>
        internal string Custom(int cat, string msg)
        {
            this._LogWriter.Custom(cat, msg);

            return msg;
        }


        #endregion

        /// <summary>
        /// Implementing IDisposable.
        /// </summary>
        /// <param name="disposing">Has Dispose already been called?</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this._LogWriter.Dispose();
                }

                // Note disposing has been done.
                this.disposed = true;
            }
        }
        #endregion


    }
    #endregion
}
