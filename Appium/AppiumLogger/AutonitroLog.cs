using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AutonitroLogger
{


    /// <summary>
    /// The logger class, which wraps the logger.
    /// </summary>
    [ComVisible(false)]
    public class AutonitroLog : ILogger, IDisposable
    {
        /// <summary>
        /// Disposed flag
        /// </summary>
        private bool disposed;

        /// <summary>
        /// Logger object
        /// </summary>
        /// 

        private AutonitroLogger.Logger.BaseLogger.Logger logger;

        /// <summary>
        /// Initializes a new instance of the MercuryLogger class.
        /// </summary>
        /// <param name="logFile">Log file you want to save logs to. Put as empty if file logging is not needed.</param>
        /// <param name="console">Should log messages be printing on the screen</param>
        /// <param name="append">If the log file exists then the log should be appended to the file or not</param>
        public AutonitroLog(string logFile, bool console, bool append)
        {
            this.Initialize(logFile, console, append);
        }

        /// <summary>
        /// Gets or sets test name.
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Implementing IDisposable.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Add a success message (console color: green)
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceSuccess(string strMsg, params object[] args)
        {
            this.logger.Always(string.Format(strMsg, args));
        }

        /// <summary>
        /// Add an error message (console color: red)
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceError(string strMsg, params object[] args)
        {
            this.logger.Error(string.Format(strMsg, args));
        }

        /// <summary>
        /// Add a verbose message. This is added to log/displayed on screen if the log level is set to verbose.
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceVerbose(string strMsg, params object[] args)
        {
            strMsg = string.Format(strMsg, args);

            this.logger.WriteLine(OutputCategory.Ver, this.logger.FormatProlog(OutputCategory.Ver, strMsg));
            this.logger.WriteTokens(OutputCategory.Ver, strMsg, this.logger.FormatProlog(OutputCategory.Ver));
        }

        /// <summary>
        /// Add a regular trace message
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceMessage(string strMsg, params object[] args)
        {
            strMsg = string.Format(strMsg, args);

            this.logger.WriteLine(OutputCategory.Trc, this.logger.FormatProlog(OutputCategory.Trc, strMsg));
            this.logger.WriteTokens(OutputCategory.Trc, strMsg, this.logger.FormatProlog(OutputCategory.Trc));
        }

        /// <summary>
        /// Add a warning message (console color: yellow)
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceWarning(string strMsg, params object[] args)
        {
            this.logger.Warning(string.Format(strMsg, args));
        }

        /// <summary>
        /// Initialize the logger
        /// </summary>
        /// <param name="console">Should log messages be printing on the screen</param>
        /// <param name="append">If the log file exists then the log should be appended to the file or not</param>
        public void Initialize(bool console, bool append)
        {
            this.logger = new Logger.BaseLogger.Logger();

            if (true == console)
            {
                this.logger.OutputDestination = OutputDestination.StdOut;
            }

            this.logger.Append = append;
        }

        /// <summary>
        /// Initialize the logger
        /// </summary>
        /// <param name="logFile">Log file you want to save logs to. Put as empty if file logging is not needed.</param>
        /// <param name="console">Should log messages be printing on the screen</param>
        /// <param name="append">If the log file exists then the log should be appended to the file or not</param>
        public void Initialize(string logFile, bool console, bool append)
        {
            this.Initialize(console, append);

            if (false == string.IsNullOrEmpty(logFile))
            {
                this.logger.Log = logFile;
                this.logger.OutputDestination = this.logger.OutputDestination | OutputDestination.Log;
            }

            this.logger.DateOn = true;
            this.SetTraceLevel(TraceLevel.Info);

            this.logger.StartLogging();
        }

        /// <summary>
        /// Adds a starting test message to the logger
        /// </summary>
        /// <param name="strTest">Test name</param>
        /// <param name="args">Any args for string.Format</param>
        public void StartTest(string strTest, params object[] args)
        {
            strTest = string.Format(strTest, args);
            ConsoleColor prev = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                this.TraceMessage("Starting test: " + strTest);
                this.TestName = strTest;
            }
            finally
            {
                Console.ForegroundColor = prev;
            }
        }

        /// <summary>
        /// Adds an end test message to the logger. The test name used in StartTest is used to indicate the test being completed.
        /// </summary>
        public void EndTest()
        {
            ConsoleColor prev = Console.ForegroundColor;

            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                this.TraceMessage("Ending test: " + this.TestName);
            }
            finally
            {
                Console.ForegroundColor = prev;
            }
        }

        /// <summary>
        /// Stops the logging
        /// </summary>
        public void Cleanup()
        {
            //// Calling this in the d'tor throws an exception from OldLogger.Logger class
            this.logger.StopLogging();
        }

        /// <summary>
        /// Sets the trace level for the logger
        /// </summary>
        /// <param name="traceLevel">Specify the trace level</param>
        public void SetTraceLevel(TraceLevel traceLevel)
        {
            switch (traceLevel)
            {
                case TraceLevel.All:
                    this.logger.OutputCategory = OutputCategory.Alw | OutputCategory.Trc | OutputCategory.Wrn | OutputCategory.Err | OutputCategory.Ver;
                    break;

                case TraceLevel.Error:
                    this.logger.OutputCategory = OutputCategory.Err | OutputCategory.Wrn;
                    break;

                case TraceLevel.Info:
                    this.logger.OutputCategory = OutputCategory.Alw | OutputCategory.Err | OutputCategory.Trc | OutputCategory.Wrn;
                    break;
            }
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
                    this.logger.Dispose();
                }

                // Note disposing has been done.
                this.disposed = true;
            }
        }
    }
}

