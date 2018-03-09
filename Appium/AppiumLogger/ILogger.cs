// -----------------------------------------------------------------------
// <copyright file="ILogger.cs" company="Ladbrokes Pvt Ltd">
// Copyright (c) Ladbrokes. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace AutonitroLogger
{
    using System.Runtime.InteropServices;
    /// <summary>
    /// Specifies the TraceLevel
    /// </summary>
    [ComVisible(false)]
    public enum TraceLevel
    {
        /// <summary>
        /// Only error messages
        /// </summary>
        Error,

        /// <summary>
        /// Print all messages
        /// </summary>
        All,

        /// <summary>
        /// Anything but verbose
        /// </summary>
        Info
    }

    /// <summary>
    /// Common interface to be implemented by all loggers classes.
    /// </summary>
    [ComVisible(false)]
    public interface ILogger
    {
        /// <summary>
        /// Add an error message
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        void TraceError(string strMsg, params object[] args);

        /// <summary>
        /// Add a warning message
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        void TraceWarning(string strMsg, params object[] args);

        /// <summary>
        /// Add a regular trace message
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        void TraceMessage(string strMsg, params object[] args);

        /// <summary>
        /// Add a verbose message. This is added to log/displayed on screen if the log level is set to verbose.
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        void TraceVerbose(string strMsg, params object[] args);

        /// <summary>
        /// Initialize the logger
        /// </summary>
        /// <param name="console">Should log messages be printing on the screen</param>
        /// <param name="append">If the log file exists then the log should be appended to the file or not</param>
        void Initialize(bool console, bool append);

        /// <summary>
        /// Add a success message
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        void TraceSuccess(string strMsg, params object[] args);

        /// <summary>
        /// Adds a starting test message to the logger
        /// </summary>
        /// <param name="strTest">Test name</param>
        /// <param name="args">Any args for string.Format</param>
        void StartTest(string strTest, params object[] args);

        /// <summary>
        /// Adds an end test message to the logger. The test name used in StartTest is used to indicate the test being completed.
        /// </summary>
        void EndTest();

        /// <summary>
        /// Stops the logging
        /// </summary>
        void Cleanup();

        /// <summary>
        /// Sets the trace level for the logger
        /// </summary>
        /// <param name="traceLevel">Specify the trace level</param>
        void SetTraceLevel(TraceLevel traceLevel);
    }
}
