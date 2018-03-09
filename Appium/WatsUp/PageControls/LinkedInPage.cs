using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WatsUp.PageControls
{
    public class LinkedInPage
    {
        public class LinkedInHomePage
        {
            //public static string linkedInScreen = "com.linkedin.android:id/logo_container";
            public static string linkedInScreen = "com.linkedin.android:id/bg";
            public static string joinnowBtn = "//android.widget.Button[@text='Join now']";
        }

        public class LinkedInRegistration
        {
            public static string browserURL = "com.android.browser:id/url";
            public static string firstNameTxt = "firstName";
            public static string lastNameTxt = "lastName";
            public static string emailTxt = "email-coldRegistrationForm";
        }

        public class TestApp
        {
            public static string buttonStartWebviewCD = "buttonStartWebviewCD";
            public static string nameTxt = "name_input";
        }
    }
}
