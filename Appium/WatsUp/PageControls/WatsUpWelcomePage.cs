using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WatsUp.PageControls
{
    public class WatsUpWelcomePage
    {
        public static string termsAndConditionsBtn = "Terms and conditions";
        public static string agreeAndContinueBtn = "Agree and continue";

        public class WatsUp
        {
            public static string countryDDLBtn = "com.whatsapp:id/registration_country";
            public static string countryDDL = "//android.widget.ListView/android.widget.LinearLayout";
            public static string searchCountryTxt = "com.whatsapp:id/search_et";
            public static string selectCountry = "//android.widget.TextView[@text='India']";
            public static string phoneTxt = "com.whatsapp:id/registration_phone";
            //public static string okBtn = "com.whatsapp:id/registration_submit";
            public static string okBtn = "//android.widget.Button[@text='OK']";
            public static string confirmMsg = "android:id/message";

        }

        public class TermaAndConditions
        {
            public static string tabSwitcherBtn = "com.android.browser:id/tab_switcher";
            #region WebView Controls
            public static string rightMenuBtn = "//div[@id='nav']/a";
            #endregion
        }

        public class GeneralControls
        {
            public static string linkedinApp = "//android.widget.TextView[@text='LinkedIn']";
        }
    }

   
}
