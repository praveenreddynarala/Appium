using Appium.CommonCls;
using Appium.Helpers;
using Appium.WatsUp.Library;
using Appium.WatsUp.PageControls;
using AutonitroFramework.Report;
using MbUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Appium.WatsUp.TestCases
{
    [TestFixture(ApartmentState = ApartmentState.STA, TimeOut = 3000)]
    public class LinkedInTest : BaseTest
    {
        Resources resource = new Resources();

        #region Variables
        private Stopwatch timer = new Stopwatch();
        #endregion

        [Test]
        public void WebViewAndNativeApp()
        {
            CommonUserActions UA = new CommonUserActions();
            CommonUserCheckPoints CP = new CommonUserCheckPoints();

            try
            {
                timer.Restart();

                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byxpath, LinkedInPage.LinkedInHomePage.joinnowBtn, "Join Now", 120);
                testSteps.Add("Successfully waited for Join Now button");
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, LinkedInPage.LinkedInHomePage.linkedInScreen, "Home Screen", 30);
                #region Swipe screen left side
                UA.SwipeActions(CommonActions.Action.SwipeScreenLeft, LinkedInPage.LinkedInHomePage.linkedInScreen, CommonActions.LocatorType.byid);
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, LinkedInPage.LinkedInHomePage.linkedInScreen, "Home Screen", 30);
                UA.SwipeActions(CommonActions.Action.SwipeScreenLeft, LinkedInPage.LinkedInHomePage.linkedInScreen, CommonActions.LocatorType.byid);
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, LinkedInPage.LinkedInHomePage.linkedInScreen, "Home Screen", 30);
                UA.SwipeActions(CommonActions.Action.SwipeScreenLeft, LinkedInPage.LinkedInHomePage.linkedInScreen, CommonActions.LocatorType.byid);
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, LinkedInPage.LinkedInHomePage.linkedInScreen, "Home Screen", 30);
                UA.SwipeActions(CommonActions.Action.SwipeScreenLeft, LinkedInPage.LinkedInHomePage.linkedInScreen, CommonActions.LocatorType.byid);
                #endregion
                UA.Click_On_Button(LinkedInPage.LinkedInHomePage.joinnowBtn, CommonActions.LocatorType.xpath, "Join Now", "Join Now");
                testSteps.Add("Successfully clicked on Join Now button");
                UA.WaitAction(CommonActions.Wait.TimeWait, CommonActions.LocatorType.emptyLocatorType, string.Empty, string.Empty, 120);
                testSteps.Add("Successfully implimented Implicit Wait");
                UA.GetText(CommonActions.Action.GetText, LinkedInPage.LinkedInRegistration.browserURL, CommonActions.LocatorType.byid, "URL");
                //UA.Switching_Contexts(CommonActions.Action.SwitchToDefaultContext, string.Empty);
                //testSteps.Add("Successfully switched from Native App");
                //UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW_com.android.browser");
                //testSteps.Add("Successfully switched from Native App to WEBVIEW App");
                ////UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW");
                //testSteps.Add("Successfully switched from Native App to WEBVIEW App");
                //UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.name, LinkedInPage.LinkedInRegistration.lastNameTxt, "Last Name", 120);
                ////UA.Enter_Text(CommonActions.LocatorType.name, LinkedInPage.LinkedInRegistration.firstNameTxt, "First Name", "Praveenreddy");
                ////UA.Enter_Text(CommonActions.LocatorType.name, LinkedInPage.LinkedInRegistration.lastNameTxt, "Last Name", "Narala");
                ////UA.Enter_Text(CommonActions.LocatorType.id, LinkedInPage.LinkedInRegistration.emailTxt, "Email", "text@test.com");

                timer.Stop();
                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }

        [Test]
        public void TestApp()
        {
            CommonUserActions UA = new CommonUserActions();
            CommonUserCheckPoints CP = new CommonUserCheckPoints();

            try
            {
                timer.Restart();
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byname, LinkedInPage.TestApp.buttonStartWebviewCD, "Chrome Button", 120);
                UA.Click_On_Button(LinkedInPage.TestApp.buttonStartWebviewCD, CommonActions.LocatorType.byname, "Chrome", "ChromeBtn");
                UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW");
                //UA.GetControlText(CommonActions.Action.GetURL, CommonActions.LocatorType.emptyLocatorType);
                UA.Enter_Text(CommonActions.LocatorType.byid, LinkedInPage.TestApp.nameTxt, "Testing", "Testing");
                timer.Stop();
                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }

        [Test]
        public void TestTapOnElement()
        {
            CommonUserActions UA = new CommonUserActions();
            CommonUserCheckPoints CP = new CommonUserCheckPoints();

            try
            {
                timer.Restart();
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byname, LinkedInPage.TestApp.buttonStartWebviewCD, "Chrome Button", 120);
                UA.UserActions(CommonActions.Action.TapOnElement, LinkedInPage.TestApp.buttonStartWebviewCD, CommonActions.LocatorType.byname, "Chrome");
                testSteps.Add("Successfully Taped");
                //UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW");
                //UA.GetControlText(CommonActions.Action.GetURL, CommonActions.LocatorType.emptyLocatorType);
                //UA.Enter_Text(CommonActions.LocatorType.byid, LinkedInPage.TestApp.nameTxt, "Testing", "Testing");
                timer.Stop();
                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }

        [Test]
        public void TestTapOnScreen()
        {
            CommonUserActions UA = new CommonUserActions();
            CommonUserCheckPoints CP = new CommonUserCheckPoints();

            try
            {
                timer.Restart();
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byname, LinkedInPage.TestApp.buttonStartWebviewCD, "Chrome Button", 120);
                UA.UserActions(CommonActions.Action.TapOnScreen, string.Empty, CommonActions.LocatorType.emptyLocatorType, "Chrome", string.Empty, string.Empty, -1, -1, -1, -1, 120, 150);
                testSteps.Add("Successfully Taped on screen");
                //UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW");
                //UA.GetControlText(CommonActions.Action.GetURL, CommonActions.LocatorType.emptyLocatorType);
                //UA.Enter_Text(CommonActions.LocatorType.byid, LinkedInPage.TestApp.nameTxt, "Testing", "Testing");
                timer.Stop();
                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }

        [Test]
        public void LongPressOnElement()
        {
            CommonUserActions UA = new CommonUserActions();
            CommonUserCheckPoints CP = new CommonUserCheckPoints();

            try
            {
                timer.Restart();
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byname, LinkedInPage.TestApp.buttonStartWebviewCD, "Chrome Button", 120);
                UA.UserActions(CommonActions.Action.LongPress, LinkedInPage.TestApp.buttonStartWebviewCD, CommonActions.LocatorType.byname, "Chrome Button");
                testSteps.Add("Successfully Taped on screen");
                //UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW");
                //UA.GetControlText(CommonActions.Action.GetURL, CommonActions.LocatorType.emptyLocatorType);
                //UA.Enter_Text(CommonActions.LocatorType.byid, LinkedInPage.TestApp.nameTxt, "Testing", "Testing");
                timer.Stop();
                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }
    }
}
