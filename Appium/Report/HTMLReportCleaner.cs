using Appium.BaseClasses;
using Appium.Helpers;
using AutonitroFramework.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.Report
{
    public class HTMLReportCleaner
    {
        // Dynamically rename the html to be as Assembly name
        public void TSsetUp()
        {
            HTMLvariables htmlVars = new HTMLvariables();
            DirectoryInfo currentDirectory = new DirectoryInfo(Environment.CurrentDirectory);
            System.IO.TextWriter writer = new System.IO.StreamWriter(FileReader.GetFilePath(FilePath.Report));
            writer.Write(htmlVars.buildHTML());
            writer.Flush();
            writer.Close();
            writer.Dispose();
            FrameGlobals.htmlPath = FileReader.GetFilePath(FilePath.Report);
        }
    }
}
