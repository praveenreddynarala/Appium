using Appium.BaseClasses;
using Appium.CommonCls;
using Appium.Helpers;
using AutonitroLogger;
using Gallio.Framework;
using MbUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Safari;
using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutonitroFramework.Report;
using Appium.Report;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium
{
    public class BaseTest
    {
        #region Variable Diclaration
        private static bool killTestOnNextMove = false;
        public static AppiumDriver appiumDriver = null;
        public RemoteWebDriver remoteDriver = null;
        DesiredCapabilities capabilities;
        protected DateTime CurrentDateTime;
        //public static AppiumWebElement element = null;
        public static IList<IWebElement> elements = null;
        public static string context = null;
        public static List<string> contexts = null;
        public static string currentContext = null;
        public static Actions _actionBuilder;
        public static bool isFail = false;
        public RemoteWebElement element = null;
        public static List<string> testSteps = new List<string>();
        private static readonly log4net.ILog OutputLog = log4net.LogManager.GetLogger(typeof(BaseTest));
        public static string fileReport = null;
        public static string getText = string.Empty;
        public static ITouchAction touchActions = null;
        public static string WebViewURL = string.Empty;
        public static string CurrentActivityAndroid = string.Empty;

        /// <summary>
        /// Setting this value to anything else besides null will cause this to be
        /// the HTML of the page seen in the logs. This is designed for web services
        /// or feeds that may not actually need the browser
        /// </summary>
        public static string CustomHTMLOfThePage = null;

        private static TimeSpan _maxTs = new TimeSpan(0, 2, 0);
        #endregion

        #region Choose Selection - TestFixtureSetUp
        /// <summary>
        /// Device Selection
        /// </summary>
        [TestFixtureSetUp]
        public void BeforeAll()
        {
            HTMLReportCleaner htmlCleaner = new HTMLReportCleaner();
            htmlCleaner.TSsetUp();

            string fixtureName = this.GetType().Name.ToString();
            FrameGlobals.Init();
            FrameGlobals.isNewFixture = true;
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            fileReport = FileReader.GetFilePath(FilePath.Report);
            FrameGlobals.htmlPath = fileReport;
            doc.Load(FileReader.GetFilePath(FilePath.Report));
            //doc.Load(FrameGlobals.htmlPath);
            HtmlNode node = doc.GetElementbyId("list");
            HtmlNode newAnchor = HtmlNode.CreateNode("<Span><a href=\"JavaScript:showDiv('" + fixtureName + "');\" ><h2 class=\"menu-font\">" + fixtureName + "</h2></a></span><br/>");
            node.AppendChild(newAnchor);
            
            doc.Save(FileReader.GetFilePath(FilePath.Report));
            ReportLibrary.WriteFixtureDivToRepoert(fixtureName);
        }
        #endregion

        #region Select, Configure and Launch Application - SetUp
        /// <summary>
        /// Application instatiation with various Capabilities.
        /// </summary>
        /// <exception cref="ApplicationException"></exception>
        [SetUp]
        public virtual void Init()
        {
            testSteps.Clear();
            
            #region WebApp
            if (FrameGlobals.appType == AppType.WebApp)
            {
                if (FrameGlobals.DeviceToTest == DeviceTypes.ANDROID)
                {
                    capabilities = Capabilities.getAndroidCaps(FrameGlobals.AndroidVersion, FrameGlobals.PlatformName, FrameGlobals.BrowserName, FrameGlobals.DeviceName, FrameGlobals.AutomationName,
                                                                FrameGlobals.PlatformVersion, FrameGlobals.App_File, FrameGlobals.AppPackage, FrameGlobals.AppActivity);
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.iOS)
                {
                    capabilities = Capabilities.getiOSCaps("iOS");
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.WINDOWS)
                {
                    //To Do
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.BLACKBERRY)
                {
                    //To Do
                }
                //AddTestSteps("Open Android Application", "User Opened " + FrameGlobals.App_File + " successfully");
                Uri serverUri = Env.isServer() ? AppiumServers.serverURI : AppiumServers.localURI;
                appiumDriver = new AppiumDriver(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
                appiumDriver.Manage().Timeouts().ImplicitlyWait(Env.INIT_TIMEOUT_SEC);
            }
            #endregion

            #region NativeApp
            else if (FrameGlobals.appType == AppType.NativeApp)
            {
                if (FrameGlobals.DeviceToTest == DeviceTypes.ANDROID)
                {
                    capabilities = Capabilities.getAndroidCaps(FrameGlobals.AndroidVersion, "Android", FrameGlobals.BrowserName, FrameGlobals.DeviceName, FrameGlobals.AutomationName,
                                                                FrameGlobals.PlatformVersion,FrameGlobals.App_File, FrameGlobals.AppPackage, FrameGlobals.AppActivity);
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.iOS)
                {
                    capabilities = Capabilities.getiOSCaps("iOS");
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.WINDOWS)
                {
                    //To Do
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.BLACKBERRY)
                {
                    //To Do
                }
                //AddTestSteps("Open Android Application", "User Opened " + FrameGlobals.App_File + " successfully");
                Uri serverUri = Env.isServer() ? AppiumServers.serverURI : AppiumServers.localURI;
                //Uri serverUri = new Uri("http://127.0.0.1:4723/wd/hub");
                appiumDriver = new AppiumDriver(serverUri, capabilities);
                appiumDriver.Manage().Timeouts().ImplicitlyWait(Env.INIT_TIMEOUT_SEC);
            }
            #endregion

            #region HybridApp
            else if (FrameGlobals.appType == AppType.HybridApp)
            {
                if (FrameGlobals.DeviceToTest == DeviceTypes.ANDROID)
                {
                    //To Do
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.iOS)
                {
                    //To Do
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.WINDOWS)
                {
                    //To Do
                }
                else if (FrameGlobals.DeviceToTest == DeviceTypes.BLACKBERRY)
                {
                    //To Do
                }
                //AddTestSteps("Open Android Application", "User Opened " + FrameGlobals.App_File + " successfully");
                Uri serverUri = Env.isServer() ? AppiumServers.serverURI : AppiumServers.localURI;
                appiumDriver = new AppiumDriver(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
                appiumDriver.Manage().Timeouts().ImplicitlyWait(Env.INIT_TIMEOUT_SEC);
            }
            #endregion

            #region WebBrowser
            else
            {
                if (BaseTest.killTestOnNextMove)
            {
                throw new ApplicationException("The test was asked to be stopped.");
            }
            try
            {
                //Reset the custom HTML variable
                CustomHTMLOfThePage = null;

                //FireFox
                if (FrameGlobals.browserType == BrowserTypes.Ff)
                {
                    //Creating a browser profile and setting the required user agent.
                    FirefoxProfile ffProfile = new FirefoxProfile();
                    if (FrameGlobals.userAgent == "Yes")
                    {
                        ffProfile.SetPreference("general.useragent.override", FrameGlobals.userAgentValue);
                    }
                    ////Creating a webdriver with above created firefox profile
                    remoteDriver = new FirefoxDriver(ffProfile);
                }
                //IE
                else if (FrameGlobals.browserType == BrowserTypes.Ie)
                {
                    //Deleting cookies in ie browser through command line.
                    var procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c RunDll32.exe InetCpl.cpl,ClearMyTracksByProcess 2");
                    var proc = new System.Diagnostics.Process { StartInfo = procStartInfo };
                    if (FrameGlobals.userAgent == "Yes")
                    {
                        //proc(FrameGlobals.userAgentValue);
                    }
                    proc.Start();
                    remoteDriver = new InternetExplorerDriver();
                }
                //Chrome    
                else if (FrameGlobals.browserType == BrowserTypes.Chrome)
                {
                    ChromeOptions chromeCapabilities = new ChromeOptions();


                    if (FrameGlobals.userAgent == "Yes")
                    {
                        //Adding certain capablilities like maximize the window, ignore untrusted certificates, disable popup blocking...
                        var arr = new string[7] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins", "--user-agent= " + FrameGlobals.userAgentValue };
                        chromeCapabilities.AddArguments(arr);
                    }
                    else
                    {
                        var arr = new string[6] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins" };
                        chromeCapabilities.AddArguments(arr);
                    }
                    Type type = this.GetType().UnderlyingSystemType;
                    string className = type.FullName;

                    remoteDriver = new ChromeDriver(chromeCapabilities);

                    // _seleniumContainer.Add(Gallio.Framework.TestContext.CurrentContext.Test.Name, WebDriverObj);
                }
                //Safari    
                else if (FrameGlobals.browserType == BrowserTypes.Safari)
                {
                    if (FrameGlobals.userAgent == "Yes")
                    {
                        //Adding certain capablilities like maximize the window, ignore untrusted certificates, disable popup blocking...
                        var arr = new string[7] { "--start-maximized", "--ignore-certificate-errors", "--disable-popup-blocking", "--disable-default-apps", "--auto-launch-at-startup", "--always-authorize-plugins", "--user-agent= " + FrameGlobals.userAgentValue };
                    }
                    remoteDriver = new SafariDriver();
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Exception in Init:" + ex.ToString());
                CaptureScreenshot(appiumDriver, "");
            }
            #endregion

            }
        }
        #endregion

        #region Closing Driver
        /// <summary>
        /// Clean up code to kill the browser instances after every test case execution.
        /// </summary>
        [TearDown]
        public virtual void Cleanup()
        {
            // for Mobile Device
            if (FrameGlobals.automationApproach == AutomationApproach.Mobile)
            {
                appiumDriver.Quit();
                // WebDriverObj.Dispose();
            }
            // for WEB
            else
            {
                if (_seleniumContainer[Gallio.Framework.TestContext.CurrentContext.Test.Name] != null)
                {
                    try
                    {
                        _seleniumContainer[Gallio.Framework.TestContext.CurrentContext.Test.Name].Quit();
                        _seleniumContainer[Gallio.Framework.TestContext.CurrentContext.Test.Name].Dispose();
                        _seleniumContainer.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                    }
                    catch (Exception)
                    {
                        _seleniumContainer.Remove(Gallio.Framework.TestContext.CurrentContext.Test.Name);
                    }
                }
            }
        }

        [TestFixtureTearDown]
        public virtual void Clear()
        {
            appiumDriver.Quit();
        }//end Clear
        #endregion

        #region Assert class
        public class Assert
        {
            public static void Equals<T>(T value1, T value2)
            {
                MbUnit.Framework.Assert.AreEqual(value1, value2);
            }
            public static void Equals<T>(T value1, T value2, string format)
            {
                MbUnit.Framework.Assert.AreEqual(value1, value2, format);
            }
            public static void Equals<T>(T value1, T value2, string format, params object[] args)
            {
                MbUnit.Framework.Assert.AreEqual(value1, value2, format, args);
            }
            public static void IsTrue(bool condition, string message)
            {
                if (!condition)
                {
                    BaseTest.Fail(message);
                }
                MbUnit.Framework.Assert.IsTrue(condition, message);
            }//end IsTrue
            public static void IsFalse(bool condition, string message)
            {
                if (condition)
                {
                    BaseTest.Fail(message);
                }//end if
                MbUnit.Framework.Assert.IsFalse(condition, message);
            }//end IsFalse
            public static void Fail(string message)
            {
                BaseTest.Fail(message);
                MbUnit.Framework.Assert.Fail(message);
            }//end Fail
            public static void Skip(string message)
            {
                BaseTest.Skip(message);
                MbUnit.Framework.Assert.Ignore(message);
            }//end Skip
        }//end class

        Dictionary<string, AppiumDriver> _seleniumContainer = new Dictionary<string, AppiumDriver>();
        private static Queue _tcList = new Queue();

        public struct TestCase
        {
            public string detail;
            public string expectedResult;
        }//end struct

        public static void AddTestSteps(string detail, string expectedResult)
        {
            if (OutputLog.IsDebugEnabled) { OutputLog.DebugFormat("Adding TC: {0}, Expected Result: {1}", detail, expectedResult); }

            TestCase tc = new TestCase();
            tc.detail = detail;
            tc.expectedResult = expectedResult;
            _tcList.Enqueue(tc);
        }//end AddTestCase

        public static void Pass(string whichOne)
        {
            if (_tcList.Count > 0)
            {

                TestCase tc = (TestCase)_tcList.Dequeue();
                string name = Guid.NewGuid().ToString();
                Gallio.Framework.TestLog.EmbedHtml("P" + name.Substring(name.Length - 2),
                 "<u><i style =\"font-family:arial;color:green;font-size:8px;\">Done</i>" +
                 "<b style =\"font-family:arial;color:green;font-size:15px;\">&#10004</b></u>" +
                 "<b style=\"font-family:arial;color:blue;font-size:15px;\" >&nbsp Test Case: </b>" +
                  "<i>" + tc.detail + "</i>" +
                  "<br>"
                    //  "<b style=\"font-family:arial;color:brown;font-size:15px;\" >Result: </b>" +
                    //  "<i>" + whichOne + "</i>"                   
                  );

                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<PASS Detail=\"" + tc.detail + "\" Result:\"" + tc.expectedResult + "\" />"); }

            }
        }//end Pass

        public static void Fail(string message)
        {
            if (_tcList.Count > 0)
            {
                string name = Guid.NewGuid().ToString();
                TestCase tc = (TestCase)_tcList.Dequeue();
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
            }
            MbUnit.Framework.Assert.Fail(message);
        }//end Fail

        public static void FailIndi(string message)
        {
            if (_tcList.Count > 0)
            {
                TestCase tc = (TestCase)_tcList.Dequeue();
                Console.WriteLine("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />");
                if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
            }
        }//end Fail_Indi

        public static void Skip(string message)
        {
            if (_tcList.Count > 0)
            {
                TestCase tc = (TestCase)_tcList.Dequeue();
                Console.WriteLine("<SKIP Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />");
                //if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<SKIP Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
            }//end if
            SkipAll();
        }//end Fail

        public static void SkipIndi(string message)
        {
            if (_tcList.Count > 0)
            {
                TestCase tc = (TestCase)_tcList.Dequeue();
                Console.WriteLine("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />");
                //if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" Actual=\"" + message + "\" />"); }
            }//end if
        }//end SKip_Indi

        public static void SkipAll()
        {
            TestCase tc;
            for (; _tcList.Count > 0; )
            {
                tc = (TestCase)_tcList.Dequeue();
                Console.WriteLine("<SKIP Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />");
                //if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<SKIP Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />"); }
            }//end for

        }//end Fail

        public static void FailAll()
        {
            TestCase tc;

            for (; _tcList.Count > 0; )
            {
                tc = (TestCase)_tcList.Dequeue();
                //Console.WriteLine("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />");
                //if (OutputLog.IsDebugEnabled) { OutputLog.Debug("<FAIL Detail=\"" + tc.detail + "\" Result=\"" + tc.expectedResult + "\" />"); }
            }//end for
        }//end Fail

        #endregion

        #region Capture Screenshot
        /// <summary>
        /// Take Screenshot
        /// </summary>
        /// <param name="myBrowser"></param>
        public static string CaptureScreenshot(AppiumDriver driver, string tcName)
        {
            string resultsFolder = null;

            try
            {
                if (driver != null)
                {
                    string fileName, filePath, screenShotsPath, directoryPath = null;
                    DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                    resultsFolder = currentDirectory.Parent.FullName;
                    screenShotsPath = FileReader.GetFilePath(FilePath.ScreenShot);

                    #region Assign Directory Path
                    if (FrameGlobals.automationApproach == AutomationApproach.Mobile)
                    {
                        if (FrameGlobals.appType == AppType.WebApp)
                        {
                            switch (FrameGlobals.DeviceToTest)
                            {
                                case DeviceTypes.ANDROID:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Andriod";
                                    break;
                                case DeviceTypes.iOS:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Iphone";
                                    break;
                                case DeviceTypes.WINDOWS:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Windows";
                                    break;
                                case DeviceTypes.BLACKBERRY:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Blackberry";
                                    break;
                                default:
                                    Fail("Selected Moble Device type is incorrect. Please select 0-Andriod,1-Iphone,2-Windows,2-BlackBerry");
                                    break;
                            }
                        }
                        else if (FrameGlobals.appType == AppType.NativeApp)
                        {
                            switch (FrameGlobals.DeviceToTest)
                            {
                                case DeviceTypes.ANDROID:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Android";
                                    break;
                                case DeviceTypes.iOS:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Iphone";
                                    break;
                                case DeviceTypes.WINDOWS:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Windows";
                                    break;
                                case DeviceTypes.BLACKBERRY:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Blackberry";
                                    break;
                                default:
                                    Fail("Selected Moble Device type is incorrect. Please select 0-Andriod,1-Iphone,2-Windows,2-BlackBerry");
                                    break;
                            }
                        }
                        else if (FrameGlobals.appType == AppType.HybridApp)
                        {
                            switch (FrameGlobals.DeviceToTest)
                            {
                                case DeviceTypes.ANDROID:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Android";
                                    break;
                                case DeviceTypes.iOS:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Iphone";
                                    break;
                                case DeviceTypes.WINDOWS:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Windows";
                                    break;
                                case DeviceTypes.BLACKBERRY:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Blackberry";
                                    break;
                                default:
                                    Fail("Selected Moble Device type is incorrect. Please select 0-Andriod,1-Iphone,2-Windows,2-BlackBerry");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //FF
                        if (FrameGlobals.browserType == BrowserTypes.Ff)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\FF";
                        }
                        //IE
                        else if (FrameGlobals.browserType == BrowserTypes.Ie)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\IE";
                        }
                        //Chrome    
                        else if (FrameGlobals.browserType == BrowserTypes.Chrome)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\Chrome";
                        }
                        //Safari    
                        else if (FrameGlobals.browserType == BrowserTypes.Safari)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\Safari";
                        }
                    }

                    #endregion

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    if (string.IsNullOrEmpty(tcName))
                    {
                        fileName = DateTime.Now.Day.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Month.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Year.ToString(CultureInfo.CurrentCulture) + "_" + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Minute.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Second.ToString(CultureInfo.CurrentCulture) + ".Jpeg";
                    }
                    else
                    {
                        fileName = tcName + ".Jpeg";
                    }
                    resultsFolder = directoryPath + "\\" + fileName;
                    Screenshot snapShot = ((ITakesScreenshot)driver).GetScreenshot();
                    snapShot.SaveAsFile(resultsFolder, ImageFormat.Jpeg);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(resultsFolder);
                    Gallio.Framework.TestLog.EmbedImage("Failure Point", img);
                    Gallio.Framework.TestLog.AttachImage("<Click here for SnapShot>", img);
                    isFail = true;
                    resultsFolder = resultsFolder.Replace("\\", "//");
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                Log.Info(ex.StackTrace);
            }
            return resultsFolder;
        }

        public static void CaptureDeviceScreenshot(AppiumDriver driverObj, string tcName)
        {
            try
            {
                if (driverObj != null)
                {
                    string resultsFolder = null;
                    string fileName, filePath, screenShotsPath, directoryPath = null;
                    DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                    resultsFolder = currentDirectory.Parent.FullName;
                    screenShotsPath = resultsFolder + "\\ScreenShots\\";

                    #region Assign Directory Path
                    if (FrameGlobals.automationApproach == AutomationApproach.Mobile)
                    {
                        if (FrameGlobals.appType == AppType.WebApp)
                        {
                            switch (FrameGlobals.DeviceToTest)
                            {
                                case DeviceTypes.ANDROID:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Andriod";
                                    break;
                                case DeviceTypes.iOS:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Iphone";
                                    break;
                                case DeviceTypes.WINDOWS:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Windows";
                                    break;
                                case DeviceTypes.BLACKBERRY:
                                    directoryPath = screenShotsPath + "\\Mobile\\WebApp\\Blackberry";
                                    break;
                                default:
                                    Fail("Selected Moble Device type is incorrect. Please select 0-Andriod,1-Iphone,2-Windows,2-BlackBerry");
                                    break;
                            }
                        }
                        else if (FrameGlobals.appType == AppType.NativeApp)
                        {
                            switch (FrameGlobals.DeviceToTest)
                            {
                                case DeviceTypes.ANDROID:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Android";
                                    break;
                                case DeviceTypes.iOS:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Iphone";
                                    break;
                                case DeviceTypes.WINDOWS:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Windows";
                                    break;
                                case DeviceTypes.BLACKBERRY:
                                    directoryPath = screenShotsPath + "\\Mobile\\NativeApp\\Blackberry";
                                    break;
                                default:
                                    Fail("Selected Moble Device type is incorrect. Please select 0-Andriod,1-Iphone,2-Windows,2-BlackBerry");
                                    break;
                            }
                        }
                        else if (FrameGlobals.appType == AppType.HybridApp)
                        {
                            switch (FrameGlobals.DeviceToTest)
                            {
                                case DeviceTypes.ANDROID:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Android";
                                    break;
                                case DeviceTypes.iOS:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Iphone";
                                    break;
                                case DeviceTypes.WINDOWS:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Windows";
                                    break;
                                case DeviceTypes.BLACKBERRY:
                                    directoryPath = screenShotsPath + "\\Mobile\\HybridApp\\Blackberry";
                                    break;
                                default:
                                    Fail("Selected Moble Device type is incorrect. Please select 0-Andriod,1-Iphone,2-Windows,2-BlackBerry");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        //FF
                        if (FrameGlobals.browserType == BrowserTypes.Ff)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\FF";
                        }
                        //IE
                        else if (FrameGlobals.browserType == BrowserTypes.Ie)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\IE";
                        }
                        //Chrome    
                        else if (FrameGlobals.browserType == BrowserTypes.Chrome)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\Chrome";
                        }
                        //Safari    
                        else if (FrameGlobals.browserType == BrowserTypes.Safari)
                        {
                            if (FrameGlobals.userAgent == "Yes")
                                directoryPath = screenShotsPath + "\\Desktop\\UserAgent\\" + FrameGlobals.userAgentName;
                            else
                                directoryPath = screenShotsPath + "\\Desktop\\Browser\\Safari";
                        }
                    }

                    #endregion

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }
                    if (string.IsNullOrEmpty(tcName))
                    {
                        fileName = DateTime.Now.Day.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Month.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Year.ToString(CultureInfo.CurrentCulture) + "_" + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Minute.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Second.ToString(CultureInfo.CurrentCulture) + ".Jpeg";
                    }
                    else
                    {
                        fileName = tcName + ".Jpeg";
                    }
                    filePath = directoryPath + "\\" + fileName;
                    Screenshot snapShot = ((ITakesScreenshot)driverObj).GetScreenshot();
                    snapShot.SaveAsFile(filePath, ImageFormat.Jpeg);
                    System.Drawing.Image img = System.Drawing.Image.FromFile(filePath);
                    Gallio.Framework.TestLog.EmbedImage("Failure Point", img);
                    Gallio.Framework.TestLog.AttachImage("<Click here for SnapShot>", img);
                    isFail = true;
                }
            }
            catch (Exception ex) { Console.Error.WriteLine(ex.Message); Console.Error.Flush(); }
        }
        #endregion
    }
}
