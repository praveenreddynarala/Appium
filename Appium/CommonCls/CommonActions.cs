using Appium.Helpers;
using AutonitroLogger;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Appium.CommonCls
{
    public class CommonActions : BaseTest
    {
        #region Variables

        protected bool isWait = false;

        #endregion

        #region enums


        public enum LocatorType
        {
            [Description("xpath")]
            xpath = 0,
            [Description("id")]
            id = 1,
            [Description("class")]
            _class = 2,
            [Description("link")]
            link = 3,
            [Description("name")]
            name = 4,
            [Description("plink")]
            partialLink = 5,
            [Description("tag")]
            tag = 6,
            [Description("cssselector")]
            css = 7,
            [Description("byxpath")]
            byxpath = 8,
            [Description("byid")]
            byid = 9,
            [Description("byclass")]
            byclassname = 10,
            [Description("bylink")]
            bylink = 11,
            [Description("byname")]
            byname = 12,
            [Description("byplink")]
            bypartialLink = 13,
            [Description("bytag")]
            bytagname = 14,
            [Description("bycssselector")]
            bycss = 15,
            [Description("byAccessibilityId")]
            byAccessibilityId = 16,
            [Description("byAndroidUIAutomator")]
            byAndroidUIAutomator = 17,
            [Description("byIosUIAutomation")]
            byIosUIAutomation = 18,
            [Description("emptyLocator")]
            emptyLocatorType = 19
        }

        public enum Action
        {
            Click = 1,
            TypeText = 2,
            AppendText = 3,
            PrependText = 4,
            SelectDropdown = 5,
            MultiSelectByText = 6,
            MultiDeSelectByText = 7,
            MouseHover = 8,
            ScrollToElement = 9,
            NavigateToUrl = 10,
            SwipeByElements = 11,
            GetText = 12,
            SwitchFrame = 13,
            AcceptAlert = 14,
            DismissAlert = 15,
            GetAlertText = 16,
            SwitchWindow = 17,
            HighlightElement = 18,
            GetAttributeText = 19,
            ScrollUp = 20,
            SwitchToDefaultContent = 21,
            TapOnElement = 22,
            SwitchContext = 23,
            SwitchToContext = 24,
            SwitchToDefaultContext = 25,
            GetURL = 26,
            SetValue = 27,
            SwipeScreenLeft = 28,
            TapOnScreen = 29,
            LongPress = 30
        }

        public enum Check
        {
            ControlExists = 1,
            TextExists = 2,
            TextContains = 3,
            Count = 4,
            CheckBoxChecked = 5,
            RadioButtonChecked = 6,
            CheckDropdownByText = 7,
            CheckDropdownContainsText = 8,
            MultiSelectByText = 9,
            MultiDeSelectByText = 10,
            AssertAlertPresent = 11,
            AssertIfCookiePresent = 12,
            VerifyURLOfCurrentWindow = 13,
            VerifyTitleOfCurrentWindow = 14,
        }

        public enum Wait
        {
            TimeWait = 1,
            WaitForScriptTimeOut = 2,
            WaitForElement = 3,
            WaitForSpecificElement = 4,
            WaitForPageLoad = 5,
            WaitAndGetElement = 6,
            WaitAndGetElements = 7,
            WaitUntilElementIsClickable = 8,
            WaitForScreenLoad = 9
        }

        #endregion

        #region Element Locators and Element Creation

        /// <summary>
        /// Locate the Element. 
        /// </summary>
        /// <param name="eLocatorType">This is the type of UIMap to be utilized to attach to the web element.</param>
        /// <param name="locator">This is the specific UIMap attach parameter that will be used to attach to the web element.</param>
        /// <returns>By object.</returns>
        private By LocateElement(LocatorType eLocatorType, string locator)
        {
            switch (eLocatorType)
            {
                case LocatorType.id:
                    return By.Id(locator);
                case LocatorType.xpath:
                    return By.XPath(locator);
                case LocatorType.tag:
                    return By.TagName(locator);
                case LocatorType.partialLink:
                    return By.PartialLinkText(locator);
                case LocatorType.name:
                    return By.Name(locator);
                case LocatorType.link:
                    return By.LinkText(locator);
                case LocatorType.css:
                    return By.CssSelector(locator);
                case LocatorType._class:
                    return By.ClassName(locator);
                default:
                    return null;
            }
        }

        /// <summary>
        /// This is for the remote wire protocol usage.  In general external devices need this when connected through Appium or something.
        /// We will have to play this usage by ear as we setup our mobile environments.
        /// </summary>
        /// <param name="eLocatorType">Locator Identification</param>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected IWebElement GetElementWire(LocatorType eLocatorType, string locator)
        {
            if (!(eLocatorType == null))
            {
                switch (eLocatorType)
                {
                    case LocatorType.byid:
                        {
                            return appiumDriver.FindElementById(locator);
                        }
                    case LocatorType.byclassname:
                        {
                            return appiumDriver.FindElementByClassName(locator);
                        }
                    case LocatorType.bycss:
                        {
                            return appiumDriver.FindElementByCssSelector(locator);
                        }
                    case LocatorType.link:
                        {
                            return appiumDriver.FindElementByLinkText(locator);
                        }
                    case LocatorType.byname:
                        {
                            return appiumDriver.FindElementByName(locator);
                        }
                    case LocatorType.bypartialLink:
                        {
                            return appiumDriver.FindElementByPartialLinkText(locator);
                        }
                    case LocatorType.bytagname:
                        {
                            return appiumDriver.FindElementByTagName(locator);
                        }
                    case LocatorType.byxpath:
                        {
                            return appiumDriver.FindElementByXPath(locator);
                        }
                    case LocatorType.byAccessibilityId:
                        {
                            return appiumDriver.FindElementByAccessibilityId(locator);
                        }
                    case LocatorType.byAndroidUIAutomator:
                        {
                            return appiumDriver.FindElementByAndroidUIAutomator(locator);
                        }
                    case LocatorType.byIosUIAutomation:
                        {
                            return appiumDriver.FindElementByIosUIAutomation(locator);
                        }
                    default:
                        { //TODO: Create common error class and push this through that.
                            throw new Exception("Incorrect UIMap selected for object.  Please correct the UIMap input to have the correct map.");
                        }
                }
            }
            Log.Error("Locatortype is empty/null");
            Log.Info("Locatortype is empty/null");
            return null;
        }

        /// <summary>
        /// This is for the remote wire protocol usage.  In general external devices need this when connected through Appium or something.
        /// We will have to play this usage by ear as we setup our mobile environments.
        /// </summary>
        /// <param name="eLocatorType">Locator Identification</param>
        /// <param name="locator"></param>
        /// <returns></returns>
        protected IList<IWebElement> GetElementsWire(LocatorType eLocatorType, string locator)
        {
            if (!(eLocatorType == null))
            {
                switch (eLocatorType)
                {
                    case LocatorType.byid:
                        {
                            return appiumDriver.FindElementsById(locator);
                        }
                    case LocatorType.byclassname:
                        {
                            return appiumDriver.FindElementsByClassName(locator);
                        }
                    case LocatorType.bycss:
                        {
                            return appiumDriver.FindElementsByCssSelector(locator);
                        }
                    case LocatorType.link:
                        {
                            return appiumDriver.FindElementsByLinkText(locator);
                        }
                    case LocatorType.byname:
                        {
                            return appiumDriver.FindElementsByName(locator);
                        }
                    case LocatorType.bypartialLink:
                        {
                            return appiumDriver.FindElementsByPartialLinkText(locator);
                        }
                    case LocatorType.bytagname:
                        {
                            return appiumDriver.FindElementsByTagName(locator);
                        }
                    case LocatorType.byxpath:
                        {
                            return appiumDriver.FindElementsByXPath(locator);
                        }
                    case LocatorType.byAccessibilityId:
                        {
                            return appiumDriver.FindElementsByAccessibilityId(locator);
                        }
                    case LocatorType.byAndroidUIAutomator:
                        {
                            return appiumDriver.FindElementsByAndroidUIAutomator(locator);
                        }
                    case LocatorType.byIosUIAutomation:
                        {
                            return appiumDriver.FindElementsByIosUIAutomation(locator);
                        }
                    default:
                        { //TODO: Create common error class and push this through that.
                            throw new Exception("Incorrect UIMap selected for object.  Please correct the UIMap input to have the correct map.");
                        }
                }
            }
            Log.Error("Locatortype is empty/null");
            Log.Info("Locatortype is empty/null");
            return null;
        }

        /// <summary>
        /// Find one element using By class 
        /// </summary>
        /// <param name="by">By class object</param>
        /// <returns>Generic type T object</returns>
        private T GetElementBy<T>(By by)
        {
            return (T)appiumDriver.FindElement(by);
        }

        /// <summary>
        /// Find all elements using By class 
        /// </summary>
        /// <param name="by">By class object</param>
        /// <returns>Generic type T object</returns>
        private IList<T> GetElementsBy<T>(By by)
        {
            return (IList<T>)appiumDriver.FindElements(by);
        }

        /// <summary>
        /// Gets a single web element and assigns it to the public "element" variable for use.
        /// </summary>
        /// <param name="eLocatorType">This is the type of UIMap to be utilized to attach to the web element.</param>
        /// <param name="locator">This is the specific UIMap attach parameter that will be used to attach to the web element.</param>
        /// <param name="waitBeforeGetElement">Wait for element available for operation.</param>
        protected void GetElement(LocatorType eLocatorType, string locator, bool waitBeforeGetElement = true)
        {
            var locatorType = (LocatorType)Enum.Parse(typeof(LocatorType), eLocatorType.ToString());

            if (!((eLocatorType.Equals(LocatorType.id)) || (eLocatorType.Equals(LocatorType.name)) || (eLocatorType.Equals(LocatorType.xpath)) || (eLocatorType.Equals(LocatorType.css))
                || (eLocatorType.Equals(LocatorType.partialLink)) || (eLocatorType.Equals(LocatorType.link)) || (eLocatorType.Equals(LocatorType.tag)) || (eLocatorType.Equals(LocatorType._class))))
            {
                if (String.IsNullOrWhiteSpace(locatorType.ToString()) || (String.IsNullOrWhiteSpace(locator)))
                    throw new InvalidDataException("The UI Locator Type is Empty, Please Specify a UI Locator");
                else if (waitBeforeGetElement)
                    WaitAndGetElement(eLocatorType, locator);
                else
                    element = (AppiumWebElement)GetElementWire(eLocatorType, locator);
            }
            else
            {
                if (String.IsNullOrWhiteSpace(locatorType.ToString()) || (String.IsNullOrWhiteSpace(locator)))
                    throw new InvalidDataException("The UI Locator Type is Empty, Please Specify a UI Locator");
                else if (waitBeforeGetElement)
                    WaitAndGetElement(eLocatorType, locator);
                else
                    element = GetElementBy<AppiumWebElement>(LocateElement(eLocatorType, locator));
            }
        }

        /// <summary>
        /// Gets a list of web elements and assigns it to the public "elements" variable for use.
        /// </summary>
        /// <param name="eLocatorType">This is the type of UIMap to be utilized to attach to the web element.</param>
        /// <param name="locator">This is the specific UIMap attach parameter that will be used to attach to the web element.</param>
        protected void GetMultipleElements(LocatorType eLocatorType, string locator, bool waitBeforeGetElement = true)
        {
            if (!((eLocatorType.Equals(LocatorType.id)) || (eLocatorType.Equals(LocatorType.name)) || (eLocatorType.Equals(LocatorType.xpath)) || (eLocatorType.Equals(LocatorType.css))
                || (eLocatorType.Equals(LocatorType.partialLink)) || (eLocatorType.Equals(LocatorType.link)) || (eLocatorType.Equals(LocatorType.tag)) || (eLocatorType.Equals(LocatorType._class))))
            {
                if (waitBeforeGetElement)
                {
                    WaitAndGetElements(eLocatorType, locator);
                }
                else
                    elements = (IList<IWebElement>)GetElementsWire(eLocatorType, locator);
            }
            else
            {
                if (waitBeforeGetElement)
                {
                    WaitAndGetElements(eLocatorType, locator);
                }
                else
                    elements = GetElementsBy<IWebElement>(LocateElement(eLocatorType, locator));
            }
        }

        /// <summary>
        /// Gets a list of web elements and assigns it to the public "elements" variable for use.
        /// </summary>
        /// <param name="eLocatorType">This is the type of UIMap to be utilized to attach to the web element.</param>
        /// <param name="locator">This is the specific UIMap attach parameter that will be used to attach to the web element.</param>
        protected void GetElements(LocatorType eLocatorType, string locator)
        {
            elements = (IList<IWebElement>)GetElementsWire(eLocatorType, locator);
        }

        /// <summary>
        /// Selects the specific Web Element based on the locator and stores it in the "element" variable in BaseScript for future use.
        /// </summary>
        /// <param name="eLocatorType">String that contains the locator type associated with locator</param>
        /// <param name="locator">String that contains the locator associated with the UI map.</param>
        private void GetUIElement(LocatorType eLocatorType, string locator)
        {
            GetElement(eLocatorType, locator);
        }

        /// <summary>
        /// Selects the multiple web elements based on the locator and stores it in the "element" variable in BaseScript for future use.
        /// </summary>
        /// <param name="eLocatorType">String that contains the locator type associated with locator</param>
        /// <param name="locator">String that contains the locator associated with the UI map.</param>
        private void GetUIElements(LocatorType eLocatorType, string locator)
        {
            GetMultipleElements(eLocatorType, locator);
        }

        #endregion

        #region Actions
        /// <summary>
        /// All actions performed by users on the application will go through this method.
        /// This includes any mouse clicks and keyboard strokes.
        /// </summary>
        /// <param name="eAction">Action to perform on the UI.</param>
        /// <param name="keywordType">Keyword identification type</param>
        /// <param name="locator">locator that is used to locate the UI Map and the Test Data.</param>
        /// <param name="tds"></param>
        /// <param name="sLocatorName"></param>
        /// <param name="text"></param>
        /// <param name="frameSwicthingType">Switching between frames</param>
        /// <param name="ddIndex"></param>
        /// <param name="swipeFromIndex"></param>
        /// <param name="swipeToIndex"></param>
        /// <param name="duration">Wait duration</param>
        /// <param name="fail">Flag to stop all test execution if the specific step fails.</param>
        ///  <returns> Returns true if user action executed successfully otherwise false</returns>
        public bool UserAction(Action eAction, LocatorType eLocatorType, string Locator = "", string sLocatorName = "", string text = "", string frameSwicthingType = "", int ddIndex = -1,
                                int swipeFromIndex = -1, int swipeToIndex = -1, int duration = -1, int xCoordinate = -1, int yCoordinate = -1, bool fail = false)
        {
            var actionType = (Action)Enum.Parse(typeof(Action), eAction.ToString());
            var locatorType = (LocatorType)Enum.Parse(typeof(LocatorType), eLocatorType.ToString());
            CurrentDateTime = DateTime.Now;
            bool bStatus = false;

            try
            {
                if (!(eAction.Equals(Action.GetAlertText) || eAction.Equals(Action.DismissAlert) || eAction.Equals(Action.AcceptAlert) || eAction.Equals(Action.SwitchContext) || (eAction.Equals(Action.SwitchToContext))
                           || eAction.Equals(Action.SwitchToDefaultContext) || eAction.Equals(Action.GetURL) || eAction.Equals(Action.TapOnScreen)))
                {
                    if (eAction.Equals(Action.MultiSelectByText) || eAction.Equals(Action.MultiDeSelectByText) || (eAction.Equals(Action.SwipeByElements)))
                    {
                        if (String.IsNullOrEmpty(locatorType.ToString()) || String.IsNullOrEmpty(Locator))
                            throw new InvalidDataException(@"Attempted to locator/keywordType, but locator/keywordType was null or String.Empty.  
                                Please correct the test and run again.");
                        GetUIElements(eLocatorType, Locator);
                    }
                    else
                    {
                        if (String.IsNullOrEmpty(locatorType.ToString()) || String.IsNullOrEmpty(Locator))
                            throw new InvalidDataException(@"Attempted to locator/keywordType, but locator/keywordType was null or String.Empty.  
                                Please correct the test and run again.");
                        GetUIElement(eLocatorType, Locator);
                    }
                }

                if (text == "")
                { //Performs the standard action with the locator for this step
                    ActionTypeChooser(eAction, sLocatorName, text, frameSwicthingType, ddIndex, swipeFromIndex, swipeToIndex, duration, xCoordinate, yCoordinate, fail);
                }
                else if(!(ddIndex == -1))
                {//Override locator and use text instead for the data
                    ActionTypeChooser(eAction, sLocatorName, text, frameSwicthingType, ddIndex, swipeFromIndex, swipeToIndex, duration, xCoordinate, yCoordinate, fail);
                }
                else if (!(swipeFromIndex == -1 && swipeToIndex == -1))
                {  //takes the indexes for Swiping
                    ActionTypeChooser(eAction, sLocatorName, text, frameSwicthingType, ddIndex, swipeFromIndex, swipeToIndex, duration, xCoordinate, yCoordinate, fail);
                }
                else
                {
                    ActionTypeChooser(eAction, sLocatorName, text, frameSwicthingType, ddIndex, swipeFromIndex, swipeToIndex, duration, xCoordinate, yCoordinate, fail);
                }
                bStatus = true;
            }
            catch (Exception ex)
            {
                //Log.Error(ex.StackTrace);
                Log.Error("Failed to perform " + actionType + "action on " + sLocatorName + " due to some error");
                Log.Error(ex.StackTrace);
                Log.Info(ex.Message);
                bStatus = false;
                throw ex;
            }
            return bStatus;
        }

        /// <summary>
        /// This will perform all actions needed by a test case.
        /// </summary>
        /// <param name="eAction">Action which defines the UserAction to take.</param>
        /// <param name="text">Raw text string which is used to type into an element. This will override the locator Test Data.</param>
        /// <param name="sKeyword">Keyword that is used to locate the test data.</param>
        private void ActionTypeChooser(Action eAction, string sLocatorName, string text, string frameSwicthingType, int index, int swipeFrom, int swipeTo, int duration,
                                       int xCoordinate, int yCoordinate, bool fail = false)
        {
            if (!(eAction == null))
            {
                switch (eAction)
                {
                    case Action.Click:
                        Click(sLocatorName);
                        break;

                    case Action.TypeText:
                        if (String.IsNullOrEmpty(text))
                            throw new InvalidDataException(@"Attempted to write text, but text was null or String.Empty.  
                                Please correct the test and run again.");
                        TypeText(text, sLocatorName);
                        break;

                    case Action.SetValue:
                        if (String.IsNullOrEmpty(text))
                            throw new InvalidDataException(@"Attempted to write text, but text was null or String.Empty.  
                                Please correct the test and run again.");
                        SetValue(sLocatorName, text);
                        break;

                    case Action.AppendText:
                        if (String.IsNullOrEmpty(text))
                            throw new InvalidDataException(@"Attempted to write text, but text was null or String.Empty.  
                                Please correct the test and run again.");
                        AppendText(text, sLocatorName);
                        break;

                    case Action.PrependText:
                        if (String.IsNullOrEmpty(text))
                            throw new InvalidDataException(@"Attempted to write text, but text was null or String.Empty.  
                                Please correct the test and run again.");
                        PrependText(text, sLocatorName);
                        break;
                    case Action.GetText:
                        GetText(sLocatorName);
                        break;

                    case Action.GetAttributeText:
                        GetAttributeText(text, sLocatorName);
                        break;

                    case Action.MouseHover:
                        MouseHover();
                        break;

                    case Action.SelectDropdown:
                        if (int.TryParse(text, out index))
                            DropDown(index, sLocatorName);
                        else
                            if (String.IsNullOrEmpty(text))
                                throw new InvalidDataException(@"Attempted to find text in dropdown, but null or String.Empty was found.  
                                Please correct the test and run again.");
                        DropDown(text, sLocatorName);
                        break;

                    case Action.MultiSelectByText:
                        if (String.IsNullOrEmpty(text))
                            throw new InvalidDataException(@"Attempted to select items, but no items were provided in the test.  
                                Please correct the test and run again.");
                        MultiClickByText(text, sLocatorName);
                        break;

                    case Action.MultiDeSelectByText:
                        if (String.IsNullOrEmpty(text))
                            throw new InvalidDataException(@"Attempted to deselect items, but no items were provided in the test.  
                                Please correct the test and run again.");
                        MultiClickByText(text, sLocatorName, false);
                        break;

                    case Action.NavigateToUrl:
                        if (String.IsNullOrWhiteSpace(text))
                            throw new InvalidDataException(@"Attempted to Navigate to URL, but no URL was provided in the test.  
                                Please correct the test and run again.");
                        NavigateToUrl(FrameGlobals.BaseUrl);
                        break;

                    case Action.ScrollToElement:
                        ScrollToElementLocation(sLocatorName);
                        break;

                    case Action.ScrollUp:
                        ScrollUp();
                        break;

                    case Action.SwipeByElements:
                        SwipeByElements(swipeFrom, swipeTo, duration);
                        break;

                    case Action.SwipeScreenLeft:
                        SwipeScreenLeft(duration);
                        break;

                    case Action.TapOnElement:
                        TapOnElement();
                        break;

                    case Action.TapOnScreen:
                        TapOnScreen(xCoordinate, yCoordinate);
                        break;

                    case Action.SwitchFrame:
                        if (frameSwicthingType.ToLower() == "byname")
                            SwitchToFrameByName(text); //text - iFrame Name
                        else if (frameSwicthingType.ToLower() == "byindex")
                            SwitchToFrameByIndex(index); //index - iFrame Index
                        else if (frameSwicthingType.ToLower() == "byelement")
                            SwitchToFrameByIWebElement(); //By IWebElement
                        break;

                    case Action.SwitchToDefaultContent:
                        SwitchToDefaultContent();
                        break;

                    case Action.AcceptAlert:
                        AcceptAlert();
                        break;

                    case Action.DismissAlert:
                        DismissAlert();
                        break;

                    case Action.GetAlertText:
                        GetAlertText();
                        break;
                    case Action.HighlightElement:
                        HighlightElement();
                        break;

                    case Action.SwitchContext:
                        SwitchToContext(text);
                        break;

                    case Action.SwitchToContext:
                        SwitchToLatestContext();
                        break;

                    case Action.SwitchToDefaultContext:
                        DefaultContext();
                        break;

                    case Action.GetURL:
                        GetURL();
                        break;

                    case Action.LongPress:
                        LongPress();
                        break;

                    default:
                        throw new Exception("Not a valid Action enum value.");
                }
            }
        }

        /// <summary> //INCOMPLETE
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKeyword"></param>
        private void SelectTree(string text, string sKeyword = "")
        {
            //GetUIElement(sKeyword);
            ///////////////////////////////////////////////////////////
            //TODO: Create a common function to select tree elements.//
            ///////////////////////////////////////////////////////////
        }

        /// <summary> //INCOMPLETE
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="sKeyword"></param>
        private void ParseGrid(string text, string sKeyword = "")
        {

            //GetUIElement(sKeyword);
            ///////////////////////////////////////////////////////////
            //TODO: Create a common function to select grid elements.//
            ///////////////////////////////////////////////////////////
        }

        /// <summary>
        /// Select a value from the Drop Down.
        /// </summary>
        /// <param name="sKeyword">Keyword value</param>
        /// <param name="valueToSelect">valueToSelect: Value to be selected in drop down</param>
        private void DropDown(string text, string sLocatorName)
        {
            SelectDropdown(text, sLocatorName);
        }

        /// <summary>
        /// Select a value from the Drop Down using Index.
        /// </summary>
        /// <param name="sKeyword">Keyword value</param>
        /// <param name="sLocatorName"></param>
        /// <param name="indexVal">indexVal: Drop Down Index Value.</param>
        private void DropDown(int index, string sLocatorName)
        {
            SelectDropdownByIndex(index, sLocatorName);
        }
        #endregion
        
        #region Type Text and all Text related operations
        /// <summary>
        /// Enter Text in the Text Area
        /// </summary>
        /// <param name="textValue">text: Enter the value of text to enter.</param>
        protected void TypeText(string textValue, string sLocatorName)
        {
            element.Clear();
            element.SendKeys(textValue);
            Log.Success("Successfully entered text in "+ sLocatorName +"");
        }

        /// <summary>
        /// Set the value passed to the element specified
        /// </summary>
        /// <param name="keywordType">Control Identification</param>
        /// <param name="locator">Control where to enter value</param>
        /// <param name="text">Value to be set</param>
        /// <param name="sLocatorName">Control Name</param>
        /// <param name="duration">Wait for element time</param>
        protected void SetValue(string sLocatorName, string text)
        {
            ((IJavaScriptExecutor)appiumDriver).ExecuteScript("arguments[0].value = arguments[1]", element, "");
            ((IJavaScriptExecutor)appiumDriver).ExecuteScript("arguments[0].value = arguments[1]", element, text);
            Log.Success("The given value sets in the " + sLocatorName + " successfully");
        }

        /// <summary>
        /// Append Text in the Text Area.
        /// </summary>
        /// <param name="textToAppend">textToAppend: Enter the value of text to append in the text area.</param>
        protected void AppendText(string textToAppend, string sLocatorName, bool provideSpace = false)
        {
            string previousText = element.GetAttribute("value");
            element.Clear();
            if (provideSpace == false) { element.SendKeys(previousText + textToAppend); }
            else { element.SendKeys(previousText + " " + textToAppend); }
            Log.Success("Successfully appended text in " + sLocatorName + "");

        }

        /// <summary>
        /// Prepend Text in the Text Area
        /// </summary>
        /// <param name="textToPrepend">textToPrepend: Enter the value of text to prepend in the text area.</param>
        protected void PrependText(string textToPrepend, string sLocatorName, bool provideSpace = false)
        {
            string previousText = element.GetAttribute("value");
            element.Clear();

            if (provideSpace == false) { element.SendKeys(textToPrepend + previousText); }
            else { element.SendKeys(textToPrepend + " " + previousText); }
            Log.Success("Successfully prepended text in " + sLocatorName + "");
        }

        /// <summary>
        /// Provides Type text operation
        /// </summary>
        /// <param name="text">Text to type</param>
        protected void TypeTextUsingAction(string text, string sLocatorName)
        {
            _actionBuilder.SendKeys(text);
            _actionBuilder.Perform();
            Log.Success("Successfully entered text using Actions in " + sLocatorName + "");
        }

        /// <summary>
        /// Navigates to a url
        /// </summary>
        /// <param name="text">URL to navigate</param>
        protected bool NavigateToUrl(string url)
        {
            appiumDriver.Navigate().GoToUrl(url);
            Log.Success("Successfully navigated to " + FrameGlobals.BaseUrl + "");
            if (url.Contains(appiumDriver.Url))
                return true;
            return false;

        }

        /// <summary>
        /// Return text from control
        /// </summary>
        /// <param name="sLocatorName"></param>
        /// <returns></returns>
        protected string GetText(string sLocatorName)
        {
            string returnText = string.Empty;
            returnText = element.Text;
            Log.Success("Successfully returned text# "+ returnText +" from " + sLocatorName + "");
            return returnText;
        }

        /// <summary>
        /// Return text from control by using its attribute value
        /// </summary>
        /// <param name="attributeValue">Element Attribute Value</param>
        /// <param name="sLocatorName">Element Name</param>
        /// <returns></returns>
        protected string GetAttributeText(string attributeValue, string sLocatorName)
        {
            string returnText = string.Empty;
            returnText = element.GetAttribute(attributeValue);
            Log.Success("Successfully returned text from " + sLocatorName + "");
            return returnText;
        }
        #endregion

        #region Other Selenium Actions

        /// <summary>
        /// Perform Click operation
        /// </summary>
        /// <param name="sLocatorName"></param>
        protected void Click(string sLocatorName)
        {
            element.Click();
            Log.Success("Clicked on " + sLocatorName);
        }

        /// <summary>
        /// Switch back to default content/page
        /// </summary>
        protected void SwitchToDefaultContent()
        {
            appiumDriver.SwitchTo().DefaultContent();
            Log.Success("Successfully switched back to default content");
        }

        /// <summary>
        /// Switching between frames by using frame name
        /// <param name="frameName">iframe name</param>
        /// </summary>
        protected void SwitchToFrameByName(string frameName)
        {
            appiumDriver.SwitchTo().Frame(frameName);
            Log.Success("Successfully switched to frame "+ frameName +"");
        }

        /// <summary>
        /// Switching between frames by using frame index
        /// <param name="frameIndex">iframe index</param>
        /// </summary>
        protected void SwitchToFrameByIndex(int frameIndex)
        {
            appiumDriver.SwitchTo().Frame(frameIndex);
            Log.Success("Successfully switched to frame " + frameIndex + "");
        }

        /// <summary>
        /// Switching between frames by using WebElement
        /// </summary>
        protected void SwitchToFrameByIWebElement()
        {
            appiumDriver.SwitchTo().Frame(element);
            Log.Success("Successfully switched to frame by WebElement");
        }

        /// <summary>
        /// Accepts alerts
        /// </summary>
        protected void AcceptAlert()
        {
            appiumDriver.SwitchTo().Alert().Accept();
            Log.Success("Successfully accepted allert messsages");
        }

        /// <summary>
        /// Dismisses Alerts
        /// </summary>
        protected void DismissAlert()
        {
            appiumDriver.SwitchTo().Alert().Dismiss();
            Log.Success("Successfully dismissed allert messsages");
        }

        /// <summary>
        /// Return Alert text
        /// </summary>
        /// <returns></returns>
        protected string GetAlertText()
        {
            string alertText = string.Empty;
            alertText = appiumDriver.SwitchTo().Alert().Text;
            Log.Success("Successfully returned allert text " + alertText + " ");
            return alertText;
        }

        /// <summary>
        /// This function is used to highlight an element in webdriver
        /// </summary>
        /// <param name="driver"> Webdriver instance</param>
        /// <param name="element"> Webelement needs to be highlighted</param>
        /// <example>frameworkCommon.driverHighLight(portDriver, elemTodayCoupons);</example>
        protected void HighlightElement()
        {
            var jsDriver = (IJavaScriptExecutor)appiumDriver;

            try
            {
                string highlightJavascript = @"arguments[0].style.cssText = ""border-width: 2px; border-style: solid; background-color: yellow"";";
                jsDriver.ExecuteScript(highlightJavascript, new object[] { element });
            }
            catch (Exception ex)
            {
                Log.Error("Failed to highlight the required element");
                Log.Info("Exception: " + ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// Returns Page source
        /// </summary>
        /// <returns></returns>
        protected string GetPageSource()
        {
            var sUrl = appiumDriver.PageSource.ToString();
            Log.Success("Successfully returned URL " + sUrl + " of current window");
            return sUrl;
        }

        /// <summary>
        /// Get URL of current window
        /// </summary>
        /// <returns></returns>
        protected void GetURL()
        {
            WebViewURL = appiumDriver.WrappedDriver.Url;
            Log.Success("Successfully retuirned URL of current window");
        }

        #endregion

        #region Contexts functions

        //protected void SwitchToLatestContext()
        //{
        //    Assert.Equals(appiumDriver.GetContexts(), new List<string> { "NATIVE_APP", "WEBVIEW_1" });
        //    appiumDriver.SetContext("WEBVIEW_1");
        //}

        /// <summary>
        /// Get all available Contexts/WebApp or NativeApp
        /// </summary>
        protected List<string> GetAllContexts()
        {
            contexts = appiumDriver.GetContexts();
            return contexts;
        }

        /// <summary>
        /// Switching between Native_App and Hybrid_App
        /// </summary>
        protected void SwitchToContext(string contextType)
        {
            Thread.Sleep(10000);
            GetAllContexts();
            string webviewContext = null;
            for (int i = 0; i < contexts.Count; i++)
            {
                Log.Info(contexts[i]);
                if (contexts[i].Contains("WEBVIEW"))
                {
                    webviewContext = contexts[i];
                }
            }
            if (string.IsNullOrEmpty(webviewContext))
                appiumDriver.SetContext(webviewContext);
            Log.Success("Successfully switched to Context " + webviewContext + "");
        }

        /// <summary>
        /// Used to switch to latest WebView Context, when ever GetContexts() return more than one WebView context
        /// </summary>
        protected void SwitchToLatestContext()
        {
            string newContext = string.Empty;
            int index = 0;
            Thread.Sleep(20000);
            //IEnumerator<string> enumerator = null;
            GetAllContexts();
            List<string> oldContexts = contexts;
            List<string> newContexts = oldContexts;
            while (newContexts.Count == oldContexts.Count)
            {
                Thread.Sleep(10000);
                GetAllContexts();
                newContexts = contexts;
                break;
            }
            //enumerator = newContexts.GetEnumerator();

            do
            {
                //newContext = enumerator.MoveNext().ToString();
                int index1 = 0;
                do
                {
                    Log.Info("Available Contexts " + newContexts[index1] + "");
                    if (oldContexts[0].Contains(newContexts[index1]))
                    {
                        newContext = newContexts[index1];
                        break;
                    }
                    index1++;
                }
                while (index1 < newContexts.Count);

                index++;
            } while (index < oldContexts.Count);
            appiumDriver.SetContext(newContext);
            Log.Success("Successfully switched to Context " + newContext + "");
        }

        /// <summary>
        /// Return Current/Default Context
        /// </summary>
        /// <returns></returns>
        protected string GetCurrentContext()
        {
            currentContext = appiumDriver.GetContext();
            Log.Success("Successfully returned Default/Current Context");
            return currentContext;
        }

        /// <summary>
        /// Switch to default/current context
        /// </summary>
        protected void DefaultContext()
        {
            appiumDriver.SetContext(currentContext);
            Log.Success("Successfully switched to Current/Default " + currentContext + " context");
        }

        #endregion

        #region Multi Click
        /// <summary>
        ///  Executes a click on the current Web Element based on matching test data.
        /// </summary>
        /// <param name="textData">Text data to match</param>
        /// <param name="sLocatorName"></param>
        /// <param name="isSelectOperation">Determines whether to perform select or deselect operation. By default select operation is performed.</param>
        protected void MultiClickByText(string textData, string sLocatorName, bool isSelectOperation = true)
        {
            if (elements != null && !string.IsNullOrEmpty(textData))
            {
                string[] textDataArray = textData.ToLower().Split('|');
                foreach (var elementItem in elements)
                {
                    if (textDataArray.Contains(elementItem.Text.ToLower()))
                    {
                        if (isSelectOperation)
                        {
                            if (!elementItem.Selected)
                                elementItem.Click();
                        }
                        else
                        {
                            if (elementItem.Selected)
                                elementItem.Click();
                        }
                    }
                    else if (!(elementItem.GetAttribute("value") == null))
                    {
                        if (textDataArray.Contains(elementItem.GetAttribute("value").ToLower()))
                        {
                            if (isSelectOperation)
                            {
                                if (!elementItem.Selected)
                                    elementItem.Click();
                            }
                            else
                            {
                                if (elementItem.Selected)
                                    elementItem.Click();
                            }
                        }
                    }
                    else if (!(elementItem.GetAttribute("placeholder") == null))
                    {
                        if (textDataArray.Contains(elementItem.GetAttribute("placeholder").ToLower()))
                        {
                            if (isSelectOperation)
                            {
                                if (!elementItem.Selected)
                                    elementItem.Click();
                            }
                            else
                            {
                                if (elementItem.Selected)
                                    elementItem.Click();
                            }
                        }
                    }
                }
            }
            Log.Success("Successfully clicked on " + sLocatorName + "");
        }
        #endregion

        #region Mouse Operations
        /// <summary>
        /// Move the mouse to a specific element on the screen.
        /// </summary>
        protected void MouseHover()
        {
            _actionBuilder.MoveToElement(element).Perform();
        }
        #endregion

        #region Scrolling, Swiping, MouseMove functions

        /// <summary>
        /// Scroll the screen to a particular element with a driver.
        /// </summary>
        protected void ScrollToElementLocation( string sLocatorName)
        {
            _actionBuilder = new Actions(appiumDriver);
            Point elementLocationPoint = ((ILocatable)element).Coordinates.LocationInViewport;
            int X = elementLocationPoint.X;
            int Y = elementLocationPoint.Y;
            if (X == 0 && Y == 0)
            {
                ((IJavaScriptExecutor)appiumDriver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            else
            {
                _actionBuilder.MoveToElement(element, X, Y);
            }
            Log.Success("Successfully scrolled " + sLocatorName + "");
        }

        /// <summary>
        /// Scroll the screen starting from a particular element with a driver.
        /// </summary>
        /// <param name="we">IWebElement to interact with.</param>
        /// <param name="xOffset">Horizontal movement</param>
        /// <param name="yOffset">Verticle movement</param>
        protected void Scroll(IWebElement webElement, IWebDriver driver, int xOffset, int yOffset)
        {
            Actions actions = new Actions(driver);
            actions.MoveToElement(webElement).ClickAndHold().MoveByOffset(xOffset, yOffset).Build().Perform();
        }

        /// <summary>
        /// Return Elements or Screen Swipe location points
        /// </summary>
        /// <param name="elementFromIndex">Select element or screen point</param>
        /// <param name="elementToIndex">Select element or screen release point</param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private ITouchAction GetSwipeLocationsPoints(int elementFromIndex, int elementToIndex, int duration)
        {
            var start = elements[elementFromIndex].Location;
            var end = elements[elementToIndex].Location;
            ITouchAction touchAction = null;
            if (duration != -1)
            {
                touchAction = new TouchAction(appiumDriver)
                        .Press(start.X, start.Y)
                        .Wait(duration)
                        .MoveTo(end.X, end.Y)
                        .Release();
            }
            else
            {
                touchAction = new TouchAction(appiumDriver)
                        .Press(start.X, start.Y)
                        .Wait(0)
                        .MoveTo(end.X, end.Y)
                        .Release();
            }
            return touchAction;
        }

        /// <summary>
        /// Return single Element or Screen Swipe location points
        /// </summary>
        /// <param name="elementFromIndex">Select element or screen point</param>
        /// <param name="elementToIndex">Select element or screen release point</param>
        /// <param name="duration"></param>
        /// <returns></returns>
        private ITouchAction GetSwipeLocationPoints(int duration)
        {
            var start = element.Location;
            double screenWidth = appiumDriver.Manage().Window.Size.Width;
            double screenHight = appiumDriver.Manage().Window.Size.Height;
            double sWidth = screenWidth / 2;
            double sHight = screenHight / 2;
            ITouchAction touchAction = null;
            if (duration != -1)
            {
                touchAction = new TouchAction(appiumDriver)
                        .Press(0.95, 0.5)
                        .Wait(duration)
                        .MoveTo(0.05, 0.5)
                        .Release();
            }
            else
            {
                touchAction = new TouchAction(appiumDriver)
                        .Press(0.95, 0.5)
                        .Wait(1000)
                        .MoveTo(0.05, 0.5)
                        .Release();
            }
            return touchAction;
        }

        /// <summary>
        /// Swipe Action
        /// </summary>
        /// <param name="elementFromIndex"></param>
        /// <param name="elementToIndex"></param>
        /// <param name="duration"></param>
        private void SwipeByElements(int elementFromIndex, int elementToIndex, int duration)
        {
            var swipe = GetSwipeLocationsPoints(elementFromIndex, elementToIndex, duration);
            swipe.Perform();
            Log.Success("Successfully swiped screen from " + elementFromIndex + " to " + elementToIndex + "");
        }

        /// <summary>
        /// Swipe screen left side
        /// </summary>
        /// <param name="duration"></param>
        protected void SwipeScreenLeft(int duration)
        {
            var swipe = GetSwipeLocationPoints(duration);
            swipe.Perform();
            Log.Success("Successfully swiped screen");
        }

        /// <summary>
        /// Swipe screen right side
        /// </summary>
        /// <param name="duration"></param>
        protected void SwipeScreenRight(int duration)
        {
            touchActions = new TouchAction(appiumDriver);
            if (duration != -1)
            {
                touchActions.Press(0.05, 0.5)
                            .Wait(duration)
                            .MoveTo(0.95, 0.5)
                            .Release();
            }
            else
            {
                touchActions.Press(0.05, 0.5)
                            .Wait(100)
                            .MoveTo(0.95, 0.5)
                            .Release();
            }
        }

        /// <summary>
        /// Scroll Up
        /// </summary>
        protected void ScrollUp()
        {
            IJavaScriptExecutor executor = appiumDriver;
            Dictionary<string, string> scrollObj = new Dictionary<string, string>();
            scrollObj.Add("direction", "up");
            scrollObj.Add("locator", element.GetAttribute("value"));
            executor.ExecuteScript("mobile: scrollTo", scrollObj);
            //appiumDriver.Navigate().GoToUrl("javascript:mobile:swipe, 150);");
            Log.Success("Successfully scrolled up");
        }

        /// <summary>
        /// Scroll Down
        /// </summary>
        protected void ScrollDown()
        {
            IJavaScriptExecutor executor = appiumDriver;
            Dictionary<string, string> scrollObj = new Dictionary<string, string>();
            scrollObj.Add("direction", "down");
            scrollObj.Add("locator", element.GetAttribute("value"));
            executor.ExecuteScript("mobile: scrollTo", scrollObj);
            //appiumDriver.Navigate().GoToUrl("javascript:mobile:swipe, 150);");
            Log.Success("Successfully scrolled up");
        }

        /// <summary>
        /// Tap on element
        /// </summary>
        protected void TapOnElement()
        {
            var tapElement = element.Location;
            Dictionary<string, double> coords = new Dictionary<string, double>();
            coords.Add("X", tapElement.X);
            coords.Add("Y", tapElement.Y);
            appiumDriver.ExecuteScript("mobile: tap", coords);
            Log.Success("Successfully Taped");
        }

        /// <summary>
        /// Tap screen
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        protected void TapOnScreen(int xCoordinate, int yCoordinate)
        {
            IJavaScriptExecutor js = appiumDriver;
            Dictionary<string, double> tapObjs = new Dictionary<string, double>();
            tapObjs.Add("x", xCoordinate);
            tapObjs.Add("y", yCoordinate);
            js.ExecuteScript("mobile: tap", tapObjs);
            Log.Success("Successfully Taped on screen at "+ xCoordinate +" , "+ yCoordinate +" coordinates");
        }

        /// <summary>
        /// Long Press on element for Context Menu to Appear
        /// </summary>
        protected void LongPress()
        {
            double x = element.Location.X;
            double y = element.Location.Y;
            TouchAction touchAction = new TouchAction(appiumDriver);
            touchAction.LongPress(element, x, y).Perform();
            Log.Success("Successfully performed LongPress on "+ element.Text +"");
                                
        }

        /// <summary>
        /// Move to element/focus element
        /// </summary>
        /// <param name="offsetX"></param>
        /// <param name="offsetY"></param>
        protected void MoveToElement(int offsetX, int offsetY)
        {
            double x = element.Location.X;
            double y = element.Location.Y;
            touchActions = new TouchAction(appiumDriver);
            touchActions.MoveTo(element, x, y).Perform();
            Log.Success("Successfully moved to element "+ element.Text +"");
        }

        /// <summary>
        /// Touch on element
        /// </summary>
        protected void TouchActionOnElement(uint duration)
        {
            double x = element.Location.X;
            double y = element.Location.Y;

            touchActions = new TouchAction(appiumDriver);
            touchActions.Press(element, x, y).Wait(duration).Release().Perform();
            Log.Success("Successfully performed touch action on element "+ element.Text +"");
        }

        /// <summary>
        /// Touch on screen
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// </summary>
        protected void TouchActionOnScreen(double x, double y, uint duration)
        {
            touchActions = new TouchAction(appiumDriver);
            touchActions.Press(x, y).Wait(duration).Release().Perform();
            Log.Success("Successfully performed touch action on screen with " + x + " , "+ y +" coordinates");
        }


        #endregion

        #region Other mobile functions
        /// <summary>
        /// Return Current Activity of Android... This will work with Android devices
        /// </summary>
        protected void GetCurrentActivity()
        {
            CurrentActivityAndroid = appiumDriver.GetCurrentActivity();
        }
        #endregion

        #region Drop Down
        /// <summary>
        /// This method will allow the selection of any value from the drowdown using the Value text.
        /// This will select the first value that contains the text included.
        /// </summary>
        /// <param name="value">value: Pass the value to be selected in the dropdown.</param>
        /// <returns>Returns True if dropdown is selected otherwise false</returns>
        protected bool SelectDropdown(string value, string sLocatorName)
        {
            bool selected = false;
            try
            {
                //bool selected = false;
                var selectElement = new SelectElement(element);
                foreach (var Option in selectElement.Options)
                {
                    if (Option.Text.Contains(value))
                    {
                        selectElement.SelectByText(Option.Text);
                        selected = true;
                        Log.Success("Successfully selected value from " + sLocatorName + " dropdown by using text");
                        break;
                    }
                }
                if (selected == false)
                {
                    foreach (var Option in selectElement.Options)
                    {

                        if (Option.TagName.Contains(value))
                        {
                            selectElement.SelectByText(Option.Text);
                            selected = true;
                            Log.Success("Successfully selected value from " + sLocatorName + " dropdown by using text");
                            break;
                        }
                    }
                }
                return selected;
            }

            catch (Exception ex)
            {
                //BaseTest.CaptureScreenshot(_driver);
                BaseTest.Fail(ex.Message);
            }
            return selected;
        }

        /// <summary>
        /// This selects a dropdown by the passed in index.
        /// </summary>
        /// <param name="index">Integer that represents the index of the dropdown item to select.</param>
        /// <param name="sLocatorName"></param>
        /// <returns>Bool value of whether the dropdown was selected successfully or not.</returns>
        protected bool SelectDropdownByIndex(int index, string sLocatorName)
        {
            var selectElement = new SelectElement(element);
            selectElement.SelectByIndex(index);
            Log.Success("Successfully selected value from " + sLocatorName + " dropdown by using index");
            return true;
        }

        /// <summary> 
        /// This method will allow the selection of any value from the drowdown using the Index.
        /// </summary>
        /// <param name="indexVal">Pass the Index value to be selected in the dropdown</param>
        /// <returns>Returns True if dropdown is selected otherwise false</returns>
        protected bool CheckDropdownByText(string text, string sLocatorName)
        {
            bool found = false;
            try
            {
                //bool found = false;
                var selectElement = new SelectElement(element);
                foreach (var option in selectElement.Options)
                {
                    if (option.Text.ToLower() == text.ToLower())
                    {
                        found = true;
                        Log.Success("Successfully foud "+ text +" in dropdown "+ sLocatorName +"");
                        break;
                    }

                    if (option.GetAttribute("value").ToLower() == text.ToLower())
                    {
                        found = true;
                        Log.Success("Successfully foud " + text + " in dropdown " + sLocatorName + "");
                        break;
                    }

                    if (option.TagName.ToLower() == text.ToLower())
                    {
                        found = true;
                        Log.Success("Successfully foud " + text + " in dropdown " + sLocatorName + "");
                        break;
                    }
                }
                return found;
            }

            catch (Exception ex)
            {
                found = false;
                Log.Error("Selected text/option is not present in dropdown "+ sLocatorName +"");
                Log.Error(ex.StackTrace);
            }
            return found;
        }

        /// <summary> 
        /// This method will allow the selection of any value from the drowdown using the Index.
        /// </summary>
        /// <param name="indexVal">Pass the Index value to be selected in the dropdown</param>
        /// <returns>Returns True if dropdown is selected otherwise false</returns>
        protected bool CheckDropdownContainsText(string text, string sLocatorName)
        {
            bool found = false;
            try
            {
                //bool found = false;
                var selectElement = new SelectElement(element);
                foreach (var option in selectElement.Options)
                {
                    if (option.Text.ToLower().Contains(text.ToLower()))
                    {
                        found = true;
                        Log.Success("Successfully selected " + text + " from dropdown " + sLocatorName + "");
                        break;
                    }

                    if (option.GetAttribute("value").ToLower().Contains(text.ToLower()))
                    {
                        found = true;
                        Log.Success("Successfully selected " + text + " from dropdown " + sLocatorName + "");
                        break;
                    }

                    if (option.TagName.ToLower() == text.ToLower())
                    {
                        found = true;
                        Log.Success("Successfully selected " + text + " from dropdown " + sLocatorName + "");
                        break;
                    }
                }
                return found;
            }

            catch (Exception ex)
            {
                found = false;
                Log.Error("Selected text/option is not present in dropdown " + sLocatorName + "");
                Log.Error(ex.StackTrace);
            }
            return found;
        }
        #endregion

        #region Wait functions
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eAppType">Create element on top of App Type</param>
        /// <param name="eWait">Wait type option eWait enum</param>
        /// <param name="sLocatorName">Locator Name</param>
        /// <param name="Keyword">Element locator</param>
        /// <param name="KeywordType">Element Identification</param>
        /// <param name="text">Element Text</param>
        /// <param name="iTime">Timeout</param>
        /// <param name="sKeyword">Keyword</param>
        /// <param name="elementIndex">Element Index</param>
        /// <returns></returns>
        public bool WaitFunctionality(Wait eWait, LocatorType eLocatorType, string Keyword = "", string sLocatorName = "", uint iTime = 0, string text = "", int elementIndex = 0, bool bFail = false)
        {
            var waitType = (Wait)Enum.Parse(typeof(Wait), eWait.ToString());
            CurrentDateTime = DateTime.Now;
            bool bWait = false;
            try
            {
                if (WaitTypeChooser(eWait, eLocatorType, sLocatorName, iTime, Keyword, text, elementIndex))
                {
                    return isWait = true;
                }
                bWait = true;
            }
            catch (Exception ex)
            {
                Log.Error("Failed to perform " + waitType + "action on " + sLocatorName + " due to some error");
                Log.Error(ex.StackTrace);
                Log.Info(ex.Message);
                bWait = false;
                throw ex;
            }
            return bWait; 
        }

        /// <summary>
        /// Function designed to call different "Wait Functionalities" functions based on the enum type passed in.
        /// </summary>
        /// <param name="eAppType">Create element on top of App Type</param>
        /// <param name="eWait">Wait type option eWait enum</param>
        /// <param name="sLocatorName">Locator Name</param>
        /// <param name="Keyword">Element locator</param>
        /// <param name="KeywordType">Element Identification</param>
        /// <param name="text">Element Text</param>
        /// <param name="iTime">Timeout</param>
        /// <param name="sKeyword">Keyword</param>
        /// <param name="elementIndex">Element Index</param>
        /// <returns>Returns true if operation is successfully done otherwise false</returns>
        private bool WaitTypeChooser(Wait eWait, LocatorType eLocatorType, string sLocatorName, uint iTime = 0, string Keyword = "", string text = "", int elementIndex = 0)
        {
            switch (eWait)
            {
                case Wait.TimeWait:
                    return TimeWait(iTime);

                case Wait.WaitForScriptTimeOut:
                    return WaitForScriptTimeOut(iTime);

                case Wait.WaitForElement:
                    return WaitForElement(eLocatorType, Keyword, iTime, sLocatorName);

                case Wait.WaitForSpecificElement:
                    return WaitForSpecificElement(eLocatorType, Keyword, elementIndex, sLocatorName);

                case Wait.WaitForPageLoad:
                    return WaitForPageLoad(iTime);

                case Wait.WaitAndGetElement:
                    return WaitAndGetElement(eLocatorType, Keyword); 

                case Wait.WaitAndGetElements:
                    return WaitAndGetElements(eLocatorType, Keyword);

                case Wait.WaitUntilElementIsClickable:
                    return WaitUntilElementIsClickable(eLocatorType, Keyword, sLocatorName, iTime);
                    
                case Wait.WaitForScreenLoad:
                    return WaitForScreenLoad(eLocatorType, Keyword, iTime, sLocatorName);

                default:
                    throw new Exception("Not a valid Wait enum value.");
            }
        }

        /// <summary>
        /// Implicit Wait for specific duration in Seconds.
        /// </summary>
        /// <param name="iTime">Timeout duration in Seconds.</param>
        /// <returns>Returns true if operation is successfully done otherwise false</returns>
        private bool TimeWait(uint iTime = 0)
        {
            if (iTime == 0)
                iTime = FrameGlobals.TimeOutConfig(FrameGlobals.implicitWait);
            return ImplicitWait(iTime);
        }

        /// <summary>
        /// Set Script Time out.
        /// </summary>
        /// <param name="iTime">Timeout duration in Seconds.</param>
        /// <returns>Returns true if operation is successfully done otherwise false</returns>
        private bool WaitForScriptTimeOut(uint iTime = 0)
        {
            if (iTime == 0)
                iTime = FrameGlobals.TimeOutConfig(FrameGlobals.waitForScript);
            return SetScriptTimeout(iTime);
        }

        /// <summary>
        /// Method to ensure a specific control is available for Action.
        /// </summary>
        /// <param name="eLocatorType">Locator Identification</param>
        /// <param name="Keyword"></param>
        /// <param name="sLocatorName">Element on which performing wait</param>
        /// <param name="iTime">Maximum timeout duration in Seconds.</param>
        /// <returns>Returns true if element is found otherwise false</returns>
        private bool WaitForElement(LocatorType eLocatorType, string Keyword, uint iTime, string sLocatorName)
        {
            if (ExplicitWait(eLocatorType, Keyword, iTime))
            {
                Log.Success("Successfully performed WaitForElement on "+ sLocatorName +" for "+ iTime +" seconds");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Method to ensure that specific element with index is available for action, when we search for multiple elements.
        /// </summary>
        /// <param name="eLocatorType">Locator Indentification</param>
        /// <param name="sKeyword"></param>
        /// <param name="sLocatorName">Locator Name</param>
        /// <param name="elementIndex">Index of element.</param>
        /// <returns>Returns true if element is found otherwise false</returns>
        private bool WaitForSpecificElement(LocatorType eLocatorType, string Keyword, int elementIndex, string sLocatorName)
        {

            if (WaitForMultipleElements(eLocatorType, Keyword, elementIndex))
            {
                Log.Success("Successfully performed WaitForSpecificElement on " + sLocatorName + " seconds");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Implicit Wait for specific duration in Seconds.
        /// </summary>
        /// <param name="iTime">Timeout duration in Seconds.</param>
        /// <returns>Always returns true.</returns>
        protected bool ImplicitWait(uint iTime)
        {
            BaseTest.appiumDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(iTime));
            Log.Success("Successfully ImplicitWaited for " + iTime + " seconds");
            return true;
        }

        /// <summary>
        /// Method to ensure a specific control is available for Action and assign it to element.
        /// This method will wait for a maximun of provided time in seconds, if control is available before that, it will return the desired result. 
        /// </summary>
        /// <param name="sMapType">Map Type.</param>
        /// <param name="sMap">Map Value.</param>
        /// <param name="iTime">Maximum timeout duration in Seconds.</param>
        /// <returns>Returns true or false.</returns>
        protected bool ExplicitWait(LocatorType eLocatorType, string locator, uint iTime)
        {
            bool result = false;
            WebDriverWait wait = new WebDriverWait(BaseTest.appiumDriver, TimeSpan.FromSeconds(iTime));
            var elementInfocus = wait.Until(d =>
            {
                try
                {
                    result = GetElementWire(eLocatorType, locator).Displayed;

                    if (result == true)
                    {
                        element = (AppiumWebElement)GetElementWire(eLocatorType, locator);
                    }
                }
                catch
                {
                    return null;
                }
                return element;
            });
            return result;
        }

        /// <summary>
        /// Wait for the Element to exists before assignment.
        /// </summary>
        /// <param name="eLocatorType">Locator Identification</param>
        /// <param name="locator">Map Value.</param>
        private bool WaitAndGetElement(LocatorType eLocatorType, string locator)
        {
            var locatorType = (LocatorType)Enum.Parse(typeof(LocatorType), eLocatorType.ToString());
            if (String.IsNullOrWhiteSpace(locatorType.ToString()))
                throw new InvalidDataException("The UI Locator Type is Empty, Please Specify a UI Locator");
            else if (String.IsNullOrWhiteSpace(locatorType.ToString()))
                throw new InvalidDataException("The UIMap Value Provided is Empty, Please Provide a UIMap Value");
            else
                return WaitAndReturnElement(eLocatorType, locator);
        }

        /// <summary> 
        /// Wait for element and return once available.
        /// </summary>
        /// <param name="eLocatorType">Map Type.</param>
        /// <param name="locator">Map Value.</param>
        /// <param name="result">True or False after evaluating condition.</param>
        /// <returns>Returns True if element is found otherwise false</returns>
        private bool WaitAndReturnElement(LocatorType eLocatorType, string locator)
        {
            bool result = true;
            WebDriverWait wait = new WebDriverWait(BaseTest.appiumDriver, TimeSpan.FromSeconds(30));
            var elementInfocus = wait.Until(d =>
            {
                try
                {
                    if (!((eLocatorType.Equals(LocatorType.id)) || (eLocatorType.Equals(LocatorType.name)) || (eLocatorType.Equals(LocatorType.xpath)) || (eLocatorType.Equals(LocatorType.css))
                        || (eLocatorType.Equals(LocatorType.partialLink)) || (eLocatorType.Equals(LocatorType.link)) || (eLocatorType.Equals(LocatorType.tag)) || (eLocatorType.Equals(LocatorType._class))))
                    {
                        element = (AppiumWebElement)GetElementWire(eLocatorType, locator);
                    }
                    else
                    {
                        element = GetElementBy<AppiumWebElement>(LocateElement(eLocatorType, locator));
                    }
                    result = element.Displayed;

                    if (result == false)
                    {
                        throw new ElementNotVisibleException();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return element;
            });
            return result;
        }

        /// <summary>
        /// Wait for specific control with index.
        /// This method will wait for a maximun of 10 seconds, if control is available before that, it will return the desired result. 
        /// </summary>
        /// <param name="eLocatorType">Map Type.</param>
        /// <param name="locator">Map Value.</param>
        /// <param name="elementIndex">Provide the index of element.</param>
        /// <returns>Returns True if elements are found otherwise false</returns>
        protected bool WaitForMultipleElements(LocatorType eLocatorType, string locator, int elementIndex)
        {
            bool result = false;
            WebDriverWait wait = new WebDriverWait(BaseTest.appiumDriver, TimeSpan.FromSeconds(30));
            var elementsInfocus = wait.Until(d =>
            {
                try
                {
                    elements = (IList<IWebElement>)GetElementsWire(eLocatorType, locator);
                    if (elements.Count > 0)
                    {
                        result = elements[elementIndex].Displayed;
                    }
                }
                catch { return null; }
                return elements[elementIndex];
            });
            return result;
        }

        /// <summary>
        /// Wait for multiple elements and returns true If all elements are found otherwise false
        /// This method will wait for a maximun of 20 seconds, if element is available before that, it will return the desired result. 
        /// </summary>
        /// <param name="eLocatorType">Keyword Type.</param>
        /// <param name="locator">Keyword Value.</param>
        /// <returns>Returns true If all elements are found otherwise false</returns>
        protected bool WaitAndGetElements(LocatorType eLocatorType, string locator)
        {
            bool bGetElement = false;
            if (!((eLocatorType.Equals(LocatorType.id)) || (eLocatorType.Equals(LocatorType.name)) || (eLocatorType.Equals(LocatorType.xpath)) || (eLocatorType.Equals(LocatorType.css))
                || (eLocatorType.Equals(LocatorType.partialLink)) || (eLocatorType.Equals(LocatorType.link)) || (eLocatorType.Equals(LocatorType.tag)) || (eLocatorType.Equals(LocatorType._class))))
            {
                elements = (IList<IWebElement>)GetElementsWire(eLocatorType, locator);
                foreach (var element in elements)
                {
                    WebDriverWait wait = new WebDriverWait(appiumDriver, TimeSpan.FromSeconds(30));
                    if (!(wait.Until(d => element).Enabled))
                        throw new TimeoutException();
                }
                bGetElement = true;
            }
            else
            {
                elements = GetElementsBy<IWebElement>(LocateElement(eLocatorType, locator)); 
                foreach (var element in elements)
                {
                    WebDriverWait wait = new WebDriverWait(appiumDriver, TimeSpan.FromSeconds(30));
                    if (!(wait.Until(d => element).Enabled))
                        throw new TimeoutException();
                }
                bGetElement = true;
            }
            return bGetElement;
        }

        /// <summary>
        /// Set Script Time out.
        /// </summary>
        /// <param name="iTime">Enter time in seconds</param>
        /// <returns>Always returns true </returns>
        protected bool SetScriptTimeout(uint iTime)
        {
            BaseTest.appiumDriver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(iTime));
            Log.Success("Successfully Script waited for "+ iTime +" seconds");
            return true;
        }

        /// <summary> //INCOMPLETE
        /// Wait till page is loaded.
        /// </summary>
        /// <param name="iTime">Set maximum time to wait in seconds.</param>
        protected bool WaitForPageLoad(uint iTime)
        {
            string state = string.Empty;
            try
            {
                WebDriverWait wait = new WebDriverWait(BaseTest.appiumDriver, TimeSpan.FromSeconds(iTime));
                wait.Until(d =>
                {
                    try
                    {
                        state = ((IJavaScriptExecutor)BaseTest.appiumDriver).ExecuteScript(@"return document.readyState").ToString();
                    }
                    catch (InvalidOperationException)
                    {
                        // Ignore
                    }
                    catch (NoSuchWindowException)
                    {
                        ////////////////////////////////////////////////////////////////////////////////
                        // TODO: need to kick this up a level and implement standard error handling.  //
                        //       if unable to handle it at TestScript level then need to call it here.//
                        ////////////////////////////////////////////////////////////////////////////////
                    }
                    return (state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase));

                });
                Log.Success("Successfully performed WaitForPageLoad");
                return true;
            }
            catch (TimeoutException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                return false;
            }
            catch (NullReferenceException)
            {
                //sometimes Page remains in Interactive mode and never becomes Complete, then we can still try to access the controls
                if (!state.Equals("interactive", StringComparison.InvariantCultureIgnoreCase))
                    throw;
                return false;
            }
            catch (WebDriverException)
            {
                if (BaseTest.appiumDriver.WindowHandles.Count == 1)
                {
                    BaseTest.appiumDriver.SwitchTo().Window(BaseTest.appiumDriver.WindowHandles[0]);
                }
                state = ((IJavaScriptExecutor)BaseTest.appiumDriver).ExecuteScript(@"return document.readyState").ToString();
                if (!(state.Equals("complete", StringComparison.InvariantCultureIgnoreCase) || state.Equals("loaded", StringComparison.InvariantCultureIgnoreCase)))
                    throw;
                return false;
            }
        }

        /// <summary>
        /// Wait until the element is clickable for the time limit specified
        /// </summary>
        /// <param name="eLocatorType">Keyword identification</param>
        /// <param name="locator">Keyword location</param>
        /// <param name="sLocatorName">Keyword/Element Name</param>
        /// <param name="iTime">Max wait time</param>
        /// <returns>Boolean value(True/False)</returns>
        private bool WaitUntilElementIsClickable(LocatorType eLocatorType, string locator, string sLocatorName, uint iTime)
        {
            bool result = true;
            WebDriverWait wait = new WebDriverWait(BaseTest.appiumDriver, TimeSpan.FromSeconds(iTime));
            var elementInfocus = wait.Until(d =>
            {
                try
                {
                    element = (AppiumWebElement)GetElementWire(eLocatorType, locator);
                    result = element.Displayed && element.Enabled;

                    if (result == false)
                    {
                        Log.Error("Element " + sLocatorName + " is not clickable");
                        throw new ElementNotVisibleException();
                    }
                    Log.Success("Element " + sLocatorName + " is ready for clickable");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                return element;
            });
            return result;
        }

        /// <summary>
        /// Wait for screen load
        /// </summary>
        /// <param name="eLocatorType"></param>
        /// <param name="locator"></param>
        /// <param name="iTime"></param>
        /// <param name="sLocatorName"></param>
        private bool WaitForScreenLoad(LocatorType eLocatorType, string locator, uint iTime, string sLocatorName)
        {
            bool bLoad = false;
            int timeout = 0;
            while (timeout < iTime)
            {

                if (!WaitForElement(eLocatorType, locator, iTime, sLocatorName))
                {
                    Log.Error("Screen is already loaded. Wait for screen load failed ");
                    bLoad = false;
                    break;
                }
                else
                {
                    Thread.Sleep(1000);
                    timeout++;
                    Log.Success("Script is waited for screen to be loaded properly");
                    bLoad = true;
                }
            }
            return bLoad;
        }
        #endregion

        #region Assertion
        /// <summary>
        /// Perform an assertion step as defined by passed in parameters.
        /// </summary>
        /// <param name="eCheck">This is the assertion to perform.</param>
        /// <param name="eLocatorType">This is the locator that maps to the UI element and the Test Data</param>
        /// <param name="text">"dynamic" is passed in here to pull from the dynamically mapped elements and test data.  
        /// Any other value will override the locator value.</param>
        /// <param name="bFail">This is a flag which will cause the entire test to fail if the results of this is false.</param>
        /// <param name="dropDownIndex">This is the index to the desired dropdown element to be asserted.</param>
        /// <returns>bool value of whether the assertion was successful or not.</returns>
        public bool Assertion(Check eCheck, LocatorType eLocatorType, string locator, string sLocatorName, string text = "", bool bFail = false, int dropDownIndex = 0)
        {
            var assertionType = (Check)Enum.Parse(typeof(Check), eCheck.ToString());
            CurrentDateTime = DateTime.Now;
            bool bCheck = false;
            try
            {
                if (!(String.IsNullOrEmpty(locator)))
                {
                    GetUIElement(eLocatorType, locator);
                    if (element == null)
                        throw new NoSuchElementException();
                }
                else if (String.IsNullOrEmpty(text))
                {
                    throw new WebDriverException("Keyword is blank.");
                }

                //Assert based on Keyword
                if (text == "")
                {

                    if (AssertionTypeChooser(eCheck, eLocatorType, locator, sLocatorName, text, bFail, dropDownIndex))
                    {
                        bCheck = true;
                    }
                }
                //Assert based on text
                else
                {
                    if (AssertionTypeChooser(eCheck, eLocatorType, locator, sLocatorName, text, bFail, dropDownIndex))
                    {
                        bCheck = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to perform " + assertionType + "action on " + sLocatorName + " due to some error");
                Log.Error(ex.StackTrace);
                Log.Info(ex.Message);
                bCheck = false;
                throw ex;
            }
            return bCheck;
        }

        /// <summary>
        /// INCOMPLETE
        /// </summary>
        /// <param name="dCO"></param>
        /// <returns></returns>
        private bool AssertionTypeChooser(Check eCheck, LocatorType eLocatorType, string locator, string sLocatorName, string text = "", bool bFail = false, int dropDownIndex = 0)
        {
            switch (eCheck)
            {
                case Check.ControlExists:
                    return !(element == null);

                case Check.TextExists:
                    return TextExists(text, sLocatorName);

                case Check.TextContains:
                    return TextContains(text, sLocatorName);

                case Check.Count:
                    return CheckCount(sLocatorName, locator, eLocatorType);//Yet to test

                case Check.CheckBoxChecked:
                    return VerifySelected(sLocatorName);

                case Check.RadioButtonChecked:
                    return VerifySelected(sLocatorName);

                case Check.CheckDropdownByText:
                    return CheckDropdownByText(text, sLocatorName);

                case Check.CheckDropdownContainsText:
                    return CheckDropdownContainsText(text, sLocatorName);

                case Check.MultiSelectByText:
                    return VerifyIfMultiClickByText(text, sLocatorName);//Yet to test

                case Check.MultiDeSelectByText:
                    return VerifyIfMultiClickByText(text, sLocatorName, false);//Yet to test

                case Check.VerifyTitleOfCurrentWindow:
                    return VerifyTitleOfCurrentWindow(text);

                case Check.VerifyURLOfCurrentWindow:
                    return VerifyURLOfCurrentWindow(text);

                case Check.AssertAlertPresent:
                    return AssertAlertPresent();

                case Check.AssertIfCookiePresent:
                    return AssertIfCookiePresent();

                default:
                    throw new Exception("Not a valid assertion enum value.");
                ///////////////////////////////////////////////
                // TODO:Update it with rest of the functions.//
                ///////////////////////////////////////////////
            }
        }

        /// <summary>
        ///  Checks if specific text exists in a page or in a element
        /// </summary>
        /// <param name="dco">DataConfigurations object</param>
        /// <param name="sLocatorName"></param>
        /// <returns>Returns true if text exists otherwise false</returns>
        private bool TextExists(string text, string sLocatorName)
        {
            if (String.IsNullOrEmpty(text))
                return VerifyTextContainsOnPage(text);
            else
                return VerifyTextOnElement(text, sLocatorName);
        }

        /// <summary>
        ///  Checks if text exists in a page or in a element
        /// </summary>
        /// <param name="dco">DataConfigurations object</param>
        /// <returns>Returns true if text exists otherwise false</returns>
        private bool TextContains(string sText, string sLocatorName)
        {
            if (String.IsNullOrEmpty(sText))
                return VerifyTextContainsOnPage(sText);
            else
                return VerifyTextContainsOnElement(sText, sLocatorName);
        }

        /// <summary> 
        /// INCOMPLETE
        /// </summary>
        /// <param name="type"></param>
        /// <param name="text"></param>
        /// <param name="eLocatorType"></param>
        /// <param name="sLocatorName"></param>
        /// <param name="locator"></param>
        /// <returns></returns>
        private bool CheckCount(string sLocatorName, string locator, LocatorType eLocatorType)
        {
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //TODO: Create a common function to count an element based on the text and return the number to be logged.//
            //      This will probably need to be overloaded for various types of count checks.                       //
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (locator != "")
            {
                //GetUIElement(sKeyword, sKeywrodType);
            }
            else
            {
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
                //TODO: Count the specific text on the page...this could be generic objects as well...unsure at this time.//
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////
            }

            return true;
        }
        #endregion

        #region Page and Window Assertions
        /// <summary>
        /// Verify if specific text is present on the page source.
        /// </summary>
        /// <param name="textToValidate">Text to validate.</param>
        /// <returns>Return true or false after evaluating the condition.</returns>
        protected bool VerifyTextContainsOnPage(string textToVerify)
        {
            if (appiumDriver.PageSource.Contains(textToVerify))
            {
                Log.Success("Successfully varified "+ textToVerify +" on page");
                return true;
            }
            Log.Error("" + textToVerify + " is not present on page");
            return false;
        }

        /// <summary>
        /// Verify Title of Current Window.
        /// </summary>
        /// <param name="titleToVerify">Title of window.</param>
        /// <returns>Return true or false after evaluating the condition.</returns>
        protected bool VerifyTitleOfCurrentWindow(string titleToVerify)
        {
            if (appiumDriver.Title.Trim().ToLower() == titleToVerify.ToLower())
            {
                Log.Success("Successfully verified title of current window");
                return true;
            }
            Log.Error("There is no window presnt to verify title of current window");
            return false;
        }

        /// <summary>
        /// Verify URL of Current Window.
        /// </summary>
        /// <param name="urlToVerify">URL of window.</param>
        /// <returns>Return true or false after evaluating the condition.</returns>
        protected bool VerifyURLOfCurrentWindow(string urlToVerify)
        {
            if (appiumDriver.Url.Trim().ToLower() == urlToVerify.ToLower())
            {
                Log.Success("Successfully verified URL of current window");
                return true;
            }
            Log.Error("There is no window presnt to verify URL of current window");
            return false;
        }

        /// <summary> 
        ///  Determines whether alert box is present.
        /// </summary>
        /// <returns>Returns true if alert box is present otherwise false.</returns>
        protected bool AssertAlertPresent()
        {
            try
            {
                appiumDriver.SwitchTo().Alert();
                Log.Error("Successfully determines whether alert box is present");
                return true;
            }
            catch (NoAlertPresentException ex)
            {
                Log.Error("There is no alert present");
                Log.Error(ex.StackTrace);
                return false;
            }
        }

        /// <summary> 
        ///  Determines whether any cookie is available.
        /// </summary>
        /// <returns>Returns true if  any cookie is available otherwise false.</returns>
        protected bool AssertIfCookiePresent()
        {
            return appiumDriver.Manage().Cookies.AllCookies.Count > 0 ? true : false;
        }
        #endregion

        #region Element Assertions
        /// <summary>
        /// Verify Text Value.
        /// </summary>
        /// <param name="text">Text Value.</param>
        /// <param name="sLocatorName"></param>
        /// <returns>Returns true if exact text matches the text of the element.</returns>
        protected bool VerifyTextOnElement(string text, string sLocatorName)
        {
            if ((element.Text.Trim().ToLower()) == (text.Trim().ToLower()))
            {
                Log.Success("Successfully verified " + text + " on element " + sLocatorName + "");
                return true;
            }
            else if (text.Trim().ToLower() == "null")
                return (element.Text == "");
            else if (!(element.GetAttribute("value") == null))
                return (element.GetAttribute("value").Trim().ToLower() == text.Trim().ToLower());
            else
                return false;
        }

        /// <summary>
        /// Verify Text exists within text of an element.
        /// </summary>
        /// <param name="text">Text value.</param>
        /// <returns>Returns true if text is found anywhere in the text of the element.</returns>
        protected bool VerifyTextContainsOnElement(string text, string sLocatorName)
        {
            if ((element.Text.Trim().ToLower()).Contains(text.Trim().ToLower()))
            {
                Log.Success("Successfully verified " + text + " on element " + sLocatorName + "");
                return true;
            }
            else if (!(element.GetAttribute("value") == null))
                return (element.GetAttribute("value").Trim().ToLower().Contains(text.Trim().ToLower()));
            else
                return false;
        }

        /// <summary>
        /// Verify if check box or radio button is selected.
        /// <param name="sLocatorName"></param>
        /// </summary>
        /// <returns>Return true or false after evaluating the condition.</returns>
        protected bool VerifySelected(string sLocatorName)
        {
            if (element.Selected)
            {
                Log.Success("Successfully verified " + sLocatorName +" check box or radio button is selected.");
                return true;
            }
            return false;
        }

        /// <summary> 
        ///  Verify visibility of current element.
        /// </summary>
        /// <returns>Returns true if element is visible otherwise false.</returns>
        protected bool VerifyDisplayed()
        {
            return element.Displayed;
        }

        /// <summary> 
        ///  Determines whether element is editable.
        /// </summary>
        /// <returns>Returns true if element is editable otherwise false.</returns>
        protected bool VerifyEnabled()
        {
            return element.Enabled;
        }

        /// <summary>
        /// Verify if all elements are selected or not based on test data values. 
        /// </summary>
        /// <param name="textToVerify">Text to verify.</param>
        /// <param name="sLocatorName"></param>
        /// <param name="isSelectOperation">Determines whether to verify select or deselect operation. By default select operation is verified.</param>
        /// <returns>Returns true if all elements are selected based on test data values otherwise returns false.</returns>
        protected bool VerifyIfMultiClickByText(string textToVerify, string sLocatorName, bool isSelectOperation = true)
        {
            string[] textDataArray = textToVerify.ToLower().Split('|');
            int clickCount = 0;
            foreach (var elementItem in elements)
            {
                if (textDataArray.Contains(elementItem.Text.ToLower()))
                {
                    if (isSelectOperation)
                    {
                        if (elementItem.Selected)
                            clickCount += 1;
                        Log.Success("Successfully verified all the emenebts are selected or not for "+ sLocatorName +"");
                    }
                    else
                    {
                        if (!elementItem.Selected)
                            clickCount += 1;
                        Log.Success("Successfully verified all the emenebts are selected or not for " + sLocatorName + "");
                    }
                }

                if (textDataArray.Contains(elementItem.GetAttribute("value").ToLower()))
                {
                    if (isSelectOperation)
                    {
                        if (elementItem.Selected)
                            clickCount += 1;
                        Log.Success("Successfully verified all the emenebts are selected or not for " + sLocatorName + "");
                    }
                    else
                    {
                        if (!elementItem.Selected)
                            clickCount += 1;
                        Log.Success("Successfully verified all the emenebts are selected or not for " + sLocatorName + "");
                    }
                }
            }
            return clickCount == textDataArray.Count();
        }
        #endregion
    }
}
