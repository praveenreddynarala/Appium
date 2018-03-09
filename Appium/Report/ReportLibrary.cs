using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Appium;
using Appium;
using Appium.Helpers;

namespace AutonitroFramework.Report
{
    public class ReportLibrary:BaseTest
    {
        public List<string> funcList = new List<string>();
        public static StringBuilder logString = new StringBuilder();

        public static void WriteFixtureDivToRepoert(string fixtureName)
        {

            // DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            //  TestDataPath = currentDirectory.Parent.FullName + "\\TestData\\TestData.xls";
            // resultsFolder = currentDirectory.Parent.FullName;
            // resultsFolder = resultsFolder + "\\_output\\ResultFolder";
            //// string htmlPath = resultsFolder;
            //  System.IO.TextWriter writer = new System.IO.StreamWriter(htmlPath, true);
            System.IO.TextWriter writer = new System.IO.StreamWriter(FrameGlobals.htmlPath, true);
            string suiteDivVar2 = "</div>" +
                "<div id= " + fixtureName + " style=\"margin-left:5%; display:none\" name=\"fixture\">" + "\r\n" +
                "<h1 align=\"center\"></h1><!-- Outer table --><TABLE align = \"center\" style= \"border-color: black; border-style: solid;\"   border=\"1\" width=\"700\" face=\"Trebuchet MS\"><tr><td><h3 align = \"Center\">" + fixtureName + "</h3></td></tr>" + "\r\n" +
                "<div id=\"button\">" + "\r\n" +
                "<form>" + "\r\n" +
                "<center>" + "\r\n" +
                "<input type=\"button\" style=\"width:60px;height:30px;background:#2F4F4F;border:1px solid black\" value=\"Close\" onClick=\"javascript:window.close();\">" + "\r\n" +
                "</center>" + "\r\n" +
                "</form>" + "\r\n" +
                "</div>" + "\r\n";

            writer.WriteLine(suiteDivVar2);
            writer.Flush();
            writer.Close();

        }


        public static void logResult(ResultStatus result, List<string> testSteps, string logs = null, AppiumDriver driver = null, string testCompletionTime=null)
        {
            System.IO.TextWriter writer = null;
            //  DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            //  string  htmlPath = currentDirectory.Parent.FullName + "\\_output\\ResultFolder\\New_Result.html";
            try
            {
                string tcName = Gallio.Framework.TestContext.CurrentContext.Test.Name;
                string updateString = "";
                //string[] testName = tcName.Split('-');
                string ssName = null;
                if (result.ToString().Equals("Fail"))
                {
                    ssName = CaptureScreenshot(driver, tcName);               
                    ssName=ssName.Replace(@"\",@"\\");
                }

                writer = new System.IO.StreamWriter(FrameGlobals.htmlPath, true);
                writer.WriteLine("<TABLE id = \"outer_table\" align = \"Center\" style= \"border-color: black; border-style: solid;\"" +
                          "border=\"1\" width=\"700\" face=\"Trebuchet MS\"><tr><td width = \"80%\">" +
                          "<a href=\"JavaScript:doMenu('" + tcName + "');\" id=\"x" + tcName + "\">[+]</a><b>" + tcName + "</b></td>");

                if (result.ToString().Equals("Pass"))
                {
                    updateString = updateString + "<td bgcolor=\"Green\" width = \"10%\">" + result.ToString() + "</td>" + "<td bgcolor=\"Green\" width = \"10%\">" + testCompletionTime.ToString() + "</td>";
                }
                else
                {
                    updateString = updateString + "<td bgcolor=\"Red\" width = \"10%\"><a href=\"JavaScript:dispLog('" + tcName + "','" + ssName + "','" + logs + "');\">Fail</a></td>" + "<td bgcolor=\"Green\" width = \"10%\">" + testCompletionTime.ToString() + "</td>";
                }

                writer.WriteLine(updateString + "<tr><td>" +
                    "<div id=\"" + tcName + "\" style=\"margin-left:1em; display:none\">" +
                    "<!--Inner table-->" +
                    "<TABLE align = \"Center\" style= \"border-color: black; border-style: solid;\"   border=\"1\" width=\"500\" face=\"Trebuchet MS\">");
                updateString = "";
                int len = testSteps.Count;
                if (result.ToString().Equals("Fail")) { len = len - 1; }
                for (int iCount = 0; iCount < len; iCount++)
                {
                    updateString = updateString + "<tr><td>" + testSteps[iCount] + "</td><td bgcolor=\"Green\" width = \"20%\">Pass</td></tr>";
                }

                if (result.ToString().Equals("Fail"))
                {
                    updateString = updateString + "<tr><td>" + testSteps[testSteps.Count - 1] + "</td><td bgcolor=\"Red\" width = \"20%\"><a href=\"JavaScript:dispLog('" + tcName + "','" + ssName + "','" + logs + "');\">Fail</a></td></tr>";
                }

                writer.WriteLine(updateString + "</TABLE><!--Inner Table-->");
                writer.WriteLine("</div></tr></td></Table>");
                writer.WriteLine("</tr>");
                if (result.ToString().Equals("Fail"))
                {
                    writer.WriteLine("\n<Div id=" + tcName + ">\n<H3>\n<Pre>");
                    writer.WriteLine("**********" + tcName + "**********" + "<BR> \n <font size=\"2\" face=\"arial\" >");
                    writer.WriteLine(logString);
                    writer.WriteLine("\n</font>\n</Pre>\n</H3>\n</Div>");
                }
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                writer.Flush();
                writer.Close();
                //Assert.Fail(ex.Message);
                //Console.WriteLine("Test case end");                
            }

        }
    }
}
