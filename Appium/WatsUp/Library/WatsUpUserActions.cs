using Appium.CommonCls;
using Appium.Helpers;
using Appium.WatsUp.PageControls;
using OpenQA.Selenium.Appium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WatsUp.Library
{
    public class WatsUpUserActions : CommonUserActions
    {
        private CommonActions common = null;
        private AppiumDriver driver = null;

        public WatsUpUserActions()
        {
            common = new CommonActions();
            driver = BaseTest.appiumDriver;
        }

        public void Enter_Registration_Details()
        {
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List", 120);
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List", 100);
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List");
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", 180);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 6, 1, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 6, 1, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 6, 1, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 1, 6, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 1, 6, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 1, 6, -1);
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.selectCountry, "India");
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.phoneTxt, "Phone Textbox", 100);
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.okBtn, "Ok Button");
            common.Assertion(CommonActions.Check.TextExists, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.confirmMsg, "Phone Number Validations Message", "Please enter your phone number.");
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.okBtn, "Ok Button");
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.phoneTxt, "Phone Textbox", 100);
            common.UserAction(CommonActions.Action.TypeText, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.phoneTxt, "Phone Textbox", "9440659594");
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.okBtn, "Ok Button");
        }

        /// <summary>
        /// Tap element
        /// </summary>
        public void TapElement()
        {
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List", 120);
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List", 100);
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List");
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", 180);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 6, 1, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 6, 1, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 6, 1, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 1, 6, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 1, 6, -1);
            common.UserAction(CommonActions.Action.SwipeByElements, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", "", "", -1, 1, 6, -1);
            common.UserAction(CommonActions.Action.TapOnElement, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.selectCountry, "India");
        }

        /// <summary>
        /// Handle validations
        /// </summary>
        public void HandleValidations(AppType eAppType)
        {
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List", 120);
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDLBtn, "Country Drop Down List");
            common.WaitFunctionality(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.countryDDL, "Country List", 180);
            common.UserAction(CommonActions.Action.TapOnElement, CommonActions.LocatorType.byxpath, WatsUpWelcomePage.WatsUp.selectCountry, "India");
            common.UserAction(CommonActions.Action.Click, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.okBtn, "Ok Button");
            //common.UserAction(CommonActions.Action.GetAlertText);
            common.Assertion(CommonActions.Check.TextExists, CommonActions.LocatorType.byid, WatsUpWelcomePage.WatsUp.confirmMsg, "Phone Number Validations Message", "Please enter your phone number.");

        }
    }
}
