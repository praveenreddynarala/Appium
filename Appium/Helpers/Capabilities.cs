using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.Helpers
{
    public class Capabilities
    {
        public static DesiredCapabilities getIos71Caps(string app)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("browserName", "");
            capabilities.SetCapability("appium-version", "1.0");
            capabilities.SetCapability("platformName", "iOS");
            capabilities.SetCapability("platformVersion", "7.1");
            capabilities.SetCapability("deviceName", "iPhone Simulator");
            capabilities.SetCapability("app", app);
            return capabilities;
        }

        public static DesiredCapabilities getAndroidCaps(string androidVersion, string platformName, string browser, string deviceName, string automationName, string platformVirsion, 
                                                            string appFile, string appPackage, string appActivity)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            if (androidVersion.Equals("4.2.2") || androidVersion.Equals("4.3.1"))
            {
                capabilities.SetCapability("browserName", browser);
                capabilities.SetCapability("appium-version", "1.2.4.1");
                capabilities.SetCapability("deviceName", deviceName);
                capabilities.SetCapability("platformVersion", platformVirsion);
                capabilities.SetCapability("platformName", platformName);
                capabilities.SetCapability("AutomationName", automationName);
                capabilities.SetCapability("app", appFile);
                capabilities.SetCapability("appPackage", appPackage);
                capabilities.SetCapability("appActivity", appActivity);
                capabilities.SetCapability("takesScreenshot", true);
            }
            else
            {
                capabilities.SetCapability("browserName", browser);
                //capabilities.SetCapability("autoWebview", true);
                capabilities.SetCapability("deviceName", deviceName);
                capabilities.SetCapability("platformVersion", platformVirsion);
                capabilities.SetCapability("platformName", platformName);
                //capabilities.SetCapability("autoWebview", "true");
                capabilities.SetCapability("app", appFile);
                //capabilities.SetCapability("appPackage", appPackage);
                //capabilities.SetCapability("appActivity", appActivity);
                capabilities.SetCapability("takesScreenshot", true);
            }
            return capabilities;
        }

        public static DesiredCapabilities getAndroid19Caps(string app)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("browserName", "");
            capabilities.SetCapability("appium-version", "1.0");
            capabilities.SetCapability("platformName", "Android");
            capabilities.SetCapability("platformVersion", "4.4.2");
            capabilities.SetCapability("deviceName", "Android Emulator");
            capabilities.SetCapability("app", app);
            return capabilities;
        }

        public static DesiredCapabilities getiOSCaps(string app)
        {
            DesiredCapabilities capabilities = new DesiredCapabilities();
            capabilities.SetCapability("deviceName", "iPad");
            capabilities.SetCapability("platformName", "iOS");

            capabilities.SetCapability("browserName", "Safari");
            capabilities.SetCapability("platformVersion", "7.1");
            capabilities.SetCapability(CapabilityType.BrowserName, "Safari");
            capabilities.SetCapability("app", app);
            return capabilities;
        }
    }
}
