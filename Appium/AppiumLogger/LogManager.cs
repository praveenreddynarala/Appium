using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AutonitroLogger
{
    /// <summary>
    /// The LogManager allows you to have a collection of different loggers that can be used to log using different instances of 
    /// ILogger. This class expects an instantiated and initialized instance of ILogger that can be simply added to the Loggers list 
    /// and gets used when logging using standard ILogger APIs.
    /// </summary>
    [ComVisible(false)]
    public class LogManager
    {
        /// <summary>
        /// Collection of loggers to which the messages should me fwd'd to
        /// </summary>
        public List<ILogger> Loggers { get; private set; }

        /// <summary>
        /// C'tor
        /// </summary>
        public LogManager()
        {
            this.Loggers = new List<ILogger>();
        }

        /// <summary>
        /// Add a success message (console color: green)
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceSuccess(string strMsg, params object[] args)
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.TraceSuccess(strMsg, args);
            }
        }

        /// <summary>
        /// Add an error message (console color: red)
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceError(string strMsg, params object[] args)
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.TraceError(strMsg, args);
            }
        }

        /// <summary>
        /// Add a verbose message. This is added to log/displayed on screen if the log level is set to verbose.
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceVerbose(string strMsg, params object[] args)
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.TraceVerbose(strMsg, args);
            }
        }

        /// <summary>
        /// Add a regular trace message
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceMessage(string strMsg, params object[] args)
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.TraceMessage(strMsg, args);
            }
        }

        /// <summary>
        /// Add a warning message (console color: yellow)
        /// </summary>
        /// <param name="strMsg">Message to be printed</param>
        /// <param name="args">Any args for string.Format</param>
        public void TraceWarning(String strMsg, params object[] args)
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.TraceWarning(strMsg, args);
            }
        }

        /// <summary>
        /// Cleans up all the initilaized loggers
        /// </summary>
        public void Cleanup()
        {
            foreach (ILogger logger in this.Loggers)
            {
                logger.Cleanup();
            }
        }

        /// <summary>
        /// Adds a starting test message to the logger
        /// </summary>
        /// <param name="strTest">Test name</param>
        /// <param name="args">Any args for string.Format</param>
        public void StartTest(string strTest, params object[] args)
        {
            foreach (ILogger logger in Loggers)
            {
                logger.StartTest(strTest, args);
            }
        }

        /// <summary>
        /// Adds an end test message to the logger. The test name used in StartTest is used to indicate the test being completed.
        /// </summary>
        public void EndTest()
        {
            foreach (ILogger logger in Loggers)
            {
                logger.EndTest();
            }
        }
    }
}
