using Appium.CommonCls;
using Appium.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.WatsUp.Library
{
    public class CommonUserActions : BaseTest
    {
        private CommonActions common = null;
        private AppiumDriver driver = null;

        public CommonUserActions()
        {
            common = new CommonActions();
            driver = BaseTest.appiumDriver;
        }

        public void Click_On_Button(string locator, CommonActions.LocatorType eLocatorType, string controlText, string sControlName, uint waitTime = 0)
        {
            common.UserAction(CommonActions.Action.Click, eLocatorType, locator, controlText, sControlName);
        }

        public void Enter_Text(CommonActions.LocatorType eLocatorType, string locator, string sLocatorName, string text, uint waitTime = 0)
        {
            common.UserAction(CommonActions.Action.TypeText, eLocatorType, locator, sLocatorName, text);
        }

        public void WaitAction(CommonActions.Wait eWait, CommonActions.LocatorType eLocatorType, string locator, string sLocatorName, uint duration)
        {
            common.WaitFunctionality(eWait, eLocatorType, locator, sLocatorName, duration);
        }

        public void GetControlText(CommonActions.Action eAction, CommonActions.LocatorType eLocatorType, string locator = "", string sLocatorName = "")
        {
            common.UserAction(eAction, eLocatorType, string.Empty, string.Empty);
        }

        /// <summary>
        /// Switching Contexts
        /// </summary>
        /// <param name="contextType">Type of context...Native_App/WEBVIEW_1</param>
        /// <param name="contextName">Context Name-By Name script will switch contexts. If it is empty, switches done by index</param>
        public void Switching_Contexts(CommonActions.Action eContext, string context)
        {
            common.UserAction(eContext, CommonActions.LocatorType.emptyLocatorType, string.Empty, string.Empty, context);
        }

        public void SwipeActions(CommonActions.Action eAction, string locator, CommonActions.LocatorType eLocatorType)
        {
            common.UserAction(eAction, eLocatorType, locator);
        }

        public void GetText(CommonActions.Action eAction, string locator, CommonActions.LocatorType eLocatorType, string sLocatorName)
        {
            common.UserAction(eAction, eLocatorType, locator, sLocatorName);
        }

        public void UserActions(CommonActions.Action eAction, string locator, CommonActions.LocatorType eLocatorType, string sLocatorName = "", string text = "", string frameSwicthingType = "", int ddIndex = -1,
                                int swipeFromIndex = -1, int swipeToIndex = -1, int duration = -1, int xCoordinate = -1, int yCoordinate = -1, bool fail = false)
        {
            common.UserAction(eAction, eLocatorType, locator, sLocatorName, text, frameSwicthingType, ddIndex, swipeFromIndex, swipeToIndex, duration, xCoordinate, yCoordinate);
        }

        ~CommonUserActions()
        {
            common = null;
            driver = null;
        }
    }
}
