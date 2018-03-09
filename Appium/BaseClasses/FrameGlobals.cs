using Appium.BaseClasses;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.Helpers
{

    public class ConfigSectionHandler : IConfigurationSectionHandler
    {
        public ConfigSectionHandler()
            : base()
        {
        }

        public object Create(object parent, object configContext, System.Xml.XmlNode section)
        {
            return parent;
        }
    }

    #region enums
    internal enum DeviceTypes
    {
        ANDROID = 0,
        iOS = 1,
        WINDOWS = 2,
        BLACKBERRY = 3
    }//end enum

    public enum FilePath
    {
        ScreenShot,
        Report,
        LogReport,
        TestData
    }


    public enum ResultStatus
    {
        Pass, Fail, Warning
    }
    public enum TestCompleteTime
    {
        TestCompletiomTime
    }

    internal enum AutomationApproach
    {
        Web = 0,
        Mobile = 1
    }

    public enum AppType
    {
        WebApp = 0,
        NativeApp = 1,
        HybridApp = 2
    }

    internal enum BrowserTypes
    {
        Ie = 0,
        Ff = 1,
        Chrome = 2,
        Safari = 3
    }
    #endregion

    public static class FrameGlobals
    {
        public static string testFixtureName = null;
        public static string BaseUrl = null;
        public static string App_File = null;
        public static string AndroidVersion = null;
        public static string BrowserName = null;
        public static string DeviceName = null;
        public static string AutomationName = null;
        public static string PlatformVersion = null;
        public static string PlatformName = null;
        public static string AppPackage = null;
        public static string AppActivity = null;
        public static string implicitWait = null;
        public static string waitForScript = null;
        public static string LoginBase = null;
        public static string workingEnvironment = null;
        public static bool UseCulturePrompt = false;
        public static string ServiceAddress = "";
        public static bool ShowBrowser = true;
        public static string CapturescreenshotforPassSetps = null;
        public static DateTime StartedTime = DateTime.MinValue;
        internal static DeviceTypes DeviceToTest;
        public static string TestDataPath;
        public static string htmlPath = null;
        internal static AutomationApproach automationApproach;
        internal static AppType appType;
        internal static BrowserTypes browserType;
        public static string userAgent = null;
        public static string userAgentName = null;
        public static string userAgentValue = null;
        //Used for Fiddler Integration
        public static Configuration _frameGlobalsConfig = null;
        public static string Fiddler2ConfigFileLocation = null;
        public static bool isNewFixture = false;
        public static string language = null;

        public static TestEnvironment WorkingEnvironment;
            
        public enum TestEnvironment
        {
            Production,
            QA
        }//end ServerEnvironment

        public static void Init()
        {
            try
            {
                ExeConfigurationFileMap ecf = new ExeConfigurationFileMap();
                DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
                string constFileName = currentDirectory.ToString().Replace("\\bin\\Debug", "") + "\\TestSettings.config";
                //string constFileName = currentDirectory.ToString().Replace("\\bin\\Release", "") + "\\TestSettings.config";
                ecf.ExeConfigFilename = constFileName;
                Configuration dllConfig = ConfigurationManager.OpenMappedExeConfiguration(ecf, ConfigurationUserLevel.None);
                _frameGlobalsConfig = dllConfig;
                StartedTime = DateTime.Now;
                testFixtureName = dllConfig.AppSettings.Settings["TestFixtureName"].Value;
                BaseUrl = dllConfig.AppSettings.Settings["Base_URL"].Value;
                App_File = dllConfig.AppSettings.Settings["APP_File"].Value;
                AndroidVersion = dllConfig.AppSettings.Settings["AndroidVersion"].Value;
                userAgent = dllConfig.AppSettings.Settings["UserAgent"].Value;
                userAgentName = dllConfig.AppSettings.Settings["UserAgentName"].Value.ToString();
                BrowserName = dllConfig.AppSettings.Settings["browserName"].Value;
                DeviceName = dllConfig.AppSettings.Settings["DeviceName"].Value;
                AutomationName = dllConfig.AppSettings.Settings["AutomationName"].Value;
                PlatformVersion = dllConfig.AppSettings.Settings["platformVersion"].Value;
                PlatformName = dllConfig.AppSettings.Settings["platformName"].Value;
                AppPackage = dllConfig.AppSettings.Settings["appPackage"].Value;
                AppActivity = dllConfig.AppSettings.Settings["appActivity"].Value;
                implicitWait = dllConfig.AppSettings.Settings["ImplicitTimeOut"].Value;
                waitForScript = dllConfig.AppSettings.Settings["WaitForScript"].Value;
                DeviceToTest = (DeviceTypes)Enum.Parse(typeof(DeviceTypes), dllConfig.AppSettings.Settings["DeviceType"].Value.ToString(CultureInfo.InvariantCulture));
                browserType = (BrowserTypes)Enum.Parse(typeof(BrowserTypes), dllConfig.AppSettings.Settings["BrowserType"].Value.ToString(CultureInfo.InvariantCulture));
                automationApproach = (AutomationApproach)Enum.Parse(typeof(AutomationApproach), dllConfig.AppSettings.Settings["AutomationApproach"].Value.ToString(CultureInfo.InvariantCulture));
                appType = (AppType)Enum.Parse(typeof(AppType), dllConfig.AppSettings.Settings["AppType"].Value.ToString(CultureInfo.InvariantCulture));
                CapturescreenshotforPassSetps = dllConfig.AppSettings.Settings["CapturescreenshotforAllsteps"].Value;
                //TestDataPath = dllConfig.AppSettings.Settings["TestDataPath"].Value.ToString(CultureInfo.InvariantCulture);
                htmlPath = FileReader.GetFilePath(FilePath.Report);
                //htmlPath = BaseTest.fileReport;
                language = dllConfig.AppSettings.Settings["Language"].Value;

                // Assing values
            if (FrameGlobals.automationApproach != AutomationApproach.Mobile)
            {
                if (userAgent == "Yes")
                {
                    switch (userAgentName)
                    {
                        //iPhone ios4
                        case "ios4":
                            userAgentValue = "Mozilla/5.0 (iPhone; U; CPU iPhone OS 4_3_2 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8H7 Safari/6533.18.5";
                            break;
                        //iPhone ios4s
                        case "ios4s":
                            userAgentValue = "Mozilla/5.0 (iPhone; CPU iPhone OS6_1_3 like Mac OS X)App|eWebKit/536.26 (KHTML, likeGecko) Version/6.0 Mobile/10B329Safari/8536.25";
                            break;
                        //iPhone ios5
                        case "ios5":
                            userAgentValue = "Mozilla/5.0 (iPhone; CPU iPhone OS 5_0 like Mac OS X) AppleWebKit/534.46 (KHTML, like Gecko) Version/5.1 Mobile/9A334 Safari/7534.48";
                            break;
                        // iPod IoS3
                        case "ipod":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.4; en-gb; GT-I9300 Build/IMM76D) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
                            break;
                        // iPad Mini
                        case "ipadmini":
                            userAgentValue = "Mozilla/5.0 (iPad; CPU 08 6_1 like Mac OS X)AppIeWebKit/536.26 (KHTML, like Gecko)Version/6.0 Mobile/10B141 Safari/8536.25";
                            break;
                        // ipad 
                        case "ipad":
                            userAgentValue = "Mozilla/5.0 (iPad; U; CPU OS 3_2 like Mac OS X; en-us) AppleWebKit/531.21.10 (KHTML, like Gecko) Version/4.0.4 Mobile/7B334b Safari/531.21.10";
                            break;
                        // iPad2                        
                        case "ipad2":
                            userAgentValue = "Mozilla/5.0(iPad; U; CPU OS 4_3 like Mac OS X; en-us) AppleWebKit/533.17.9 (KHTML, like Gecko) Version/5.0.2 Mobile/8F191 Safari/6533.18.5";
                            break;
                        //Nexus 4 (4.2)
                        case "nexus4":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.2; Nexus 4 Build/JVP15Q) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Mobile Safari/535.19";
                            break;
                        //Nexus 5 (4.4)
                        case "nexus5":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.4; Nexus 5 Build/KRT16M) AppleWebKit/537.36 (KHTML, like Gecko) Version/4.0 Chrome/30.0.0.0 Mobile Safari/537.36";
                            break;
                        //Nexus 7 (4.1.1)
                        case "nexus7":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.1.1; Nexus 7 Build/JRO03D) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.166 Safari/535.19";
                            break;
                        //HTC pyramid (2.3.4)
                        case "htcp":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 2.3.3; zh-tw; HTC_Pyramid Build/GRI40) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1";
                            break;
                        //HTC One (4.0.3)
                        case "htc1":
                            userAgentValue = "Mozilla/5.0 (Linux; Android 4.0.3; HTC One X Build/IML74K) AppleWebKit/535.19 (KHTML, like Gecko) Chrome/18.0.1025.133 Mobile Safari/535.19";
                            break;
                        //Sony - experia arc (2.3)
                        case "sonyxarc":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 2.3; xx-xx; SonyEricssonLT26i Build/6.0.A.0.507) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1";
                            break;
                        //Windows Phone 7 (Nokia Lumia 800 - OS7.5:IE9)
                        case "win7":
                            userAgentValue = "Mozilla/5.0 (compatible; MSIE 9.0; Windows Phone OS 7.5; Trident/5.0; IEMobile/9.0; NOKIA; Lumia 800)";
                            break;
                        //Windows Phone 8 (Nokia Lumia 920: OS8:IE10)
                        case "win8":
                            userAgentValue = "Mozilla/5.0 (compatible; MSIE 10.0; Windows Phone 8.0; Trident/6.0; IEMobile/10.0; ARM; Touch; NOKIA; Lumia 920)";
                            break;
                        //BlackBerry 9800 (6.0)
                        case "BB9800":
                            userAgentValue = "Mozilla/5.0 (BlackBerry; U; BlackBerry 9800; en) AppleWebKit/534.1+ (KHTML, like Gecko) Version/6.0.0.337 Mobile Safari/534.1+2011-10-16 20:21:10";
                            break;
                        //BlackBerry 9320 (7.1)
                        case "BB9320":
                            userAgentValue = "Mozilla/5.0 (BlackBerry; U; BlackBerry 9320; en-GB) AppleWebKit/534.11+ (KHTML, like Gecko) Version/7.1.0.398 Mobile Safari/534.11+";
                            break;
                        //Samsung - galaxy ace (2.2.1)
                        case "samace":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 2.2.1; en-gb; GT-S5830 Build/FROYO) AppleWebKit/525.10 (KHTML, like Gecko) Version/3.0.4 Mobile Safari/523.12.2";
                            break;
                        //Samsung - S2 (4.0.3)
                        case "sams2":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.3; fr-fr; GT-I9100 Samsung Galaxy S2 Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
                            break;
                        //Samsung - S3 (4.1.1)
                        case "sams3":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.1.1; en-us; Samsung Galaxy S3) AppleWebKit/533.1 (KHTML, like Gecko) Version/4.0 Mobile Safari/533.1";
                            break;
                        //Samsung - S4 (4.0.3)
                        case "sams4":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.3; fr-fr; SGH-I1337 Samsung Galaxy S4 Build/IML74K) AppleWebKit/534.30 (KHTML, like Gecko) Version/4.0 Mobile Safari/534.30";
                            break;
                        //Samsung Tab
                        case "samtab":
                            userAgentValue = "Mozilla/5.0 (Linux; U; Android 4.0.4; en-gb; GT-N8000Buildl|MM76D) App|eWebKitl534.30 (KHTML, like Gecko)Version/4.0 Safaril534.30";
                            break;
                    }
                  }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //}//end if
        }//end Init

        /// <summary>
        /// Time Out from Configuration file.
        /// </summary>
        /// <param name="iTimeFromConfig">Key Name.</param>
        /// <returns>Timeout.</returns>
        public static uint TimeOutConfig(string iTimeFromConfig)
        {
            try
            {
                return uint.Parse(ConfigurationManager.AppSettings[iTimeFromConfig]);
            }
            catch (Exception)
            {
                return 10;
            }
        }

    }//end class

    
}
