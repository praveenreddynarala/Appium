using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.CommonCls
{
    /// <summary>
    /// This class provides data for error handling in framework
    /// </summary>
    public class ErrorHandlingParam
    {
        public Exception Ex { get; set; }
        public string Message { get; set; }
        public AppiumDriver Driver { get; set; }
        public IWebElement Element { get; set; }
        public string Keyword { get; set; }
        public string TestName { get; set; }
        public string Window { get; set; }

        /// <summary>
        /// Initializes a new instance of the ErrorHandlingParam class
        /// </summary>
        public ErrorHandlingParam()
        {
            Ex = null;
            Message = "";
            Driver = null;
            Element = null;
            Keyword = null;
            TestName = null;
            Window = null;
        }
    }
}
