using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;
using System.Drawing;
using Appium.Helpers;
using MbUnit.Framework;
using Appium.WatsUp.Library;
using Appium.WatsUp.PageControls;
using System.Diagnostics;
using AutonitroFramework.Report;
using Appium.CommonCls;

namespace Appium.WatsUp.TestCases
{
    [TestFixture(ApartmentState = ApartmentState.STA, TimeOut = 3000)]
    public class WhatsApp : BaseTest
    {
        Resources resource = new Resources();

        #region Variables
        private Stopwatch timer = new Stopwatch();
        #endregion

        /// <summary>
        /// Registration Test Menthod
        /// </summary>
        [Test]
        public void WhatsApp_Registration()
        {

            WatsUpUserActions UA = new WatsUpUserActions();
            WatsUpUserCheckPoints CP = new WatsUpUserCheckPoints();

            try
            {
                timer.Restart();

                UA.Click_On_Button(WatsUpWelcomePage.agreeAndContinueBtn, CommonActions.LocatorType.byname, "Agree and continue", "Agree And Continue Button", 120);
                testSteps.Add("Successfully landed on Country List View");
                UA.Enter_Registration_Details();
                testSteps.Add("Successfully registred");

                timer.Stop();

                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }

        /// <summary>
        /// Tap element Test Method
        /// </summary>
        [Test]
        public void TapElement()
        {

            WatsUpUserActions UA = new WatsUpUserActions();
            WatsUpUserCheckPoints CP = new WatsUpUserCheckPoints();

            try
            {
                timer.Restart();

                //UA.Click_On_Button("Terms And Conditions", WatsUpWelcomePage.termsAndConditionsBtn, "Name", "Terms And Conditions Buttons", 120);
                //testSteps.Add("Successfully clicked on Terms And Conditions");
                UA.Click_On_Button(WatsUpWelcomePage.agreeAndContinueBtn, CommonActions.LocatorType.byname, "Agree and continue", "Agree And Continue Button", 120);
                testSteps.Add("Successfully landed on Country List View");
                UA.TapElement();
                testSteps.Add("Successfully Taped");

                timer.Stop();
                ReportLibrary.logResult(ResultStatus.Pass, testSteps, null, null, this.timer.Elapsed.Minutes.ToString() + " Mins");
            }
            catch (Exception ex)
            {
                ReportLibrary.logResult(ResultStatus.Fail, testSteps, ex.Message, appiumDriver, this.timer.Elapsed.Seconds.ToString() + " Seconds");
                throw ex;
            }
        }

        /// <summary>
        /// Swicthing betwen HybridApp and NativeApp
        /// </summary>
        [Test]
        public void SwitchingContexts()
        {

            WatsUpUserActions UA = new WatsUpUserActions();
            WatsUpUserCheckPoints CP = new WatsUpUserCheckPoints();

            try
            {
                timer.Restart();

                UA.Click_On_Button(WatsUpWelcomePage.termsAndConditionsBtn, CommonActions.LocatorType.byname, "Terms And Conditions", "Terms And Conditions Buttons", 120);
                testSteps.Add("Successfully clicked on Terms And Conditions");
                UA.WaitAction(CommonActions.Wait.WaitForElement, CommonActions.LocatorType.byid, WatsUpWelcomePage.TermaAndConditions.tabSwitcherBtn, "Tab Swicther", 180);
                testSteps.Add("Successfully waited fot element");
                UA.Switching_Contexts(CommonActions.Action.SwitchContext, "WEBVIEW");
                testSteps.Add("Successfully switched to WEBVIEW_1");
                UA.Click_On_Button(WatsUpWelcomePage.TermaAndConditions.rightMenuBtn, CommonActions.LocatorType.byxpath, "Show Menu", "Show Menu Control");
                testSteps.Add("Successfully clicked on Show Menu");
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

