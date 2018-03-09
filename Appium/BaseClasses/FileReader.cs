using Appium.Helpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appium.BaseClasses
{
    public class FileReader
    {
        /// <summary>
        /// Return the path of the specified file .
        /// If file not present create the file and return the path
        /// </summary>
        /// <param name="folderName">Name of the folder</param>
        /// <returns>Returns complete path for the specified folder</returns>
        public static string GetFilePath(FilePath folderName)
        {
            string val = folderName.ToString();

            try
            {
                var currentDirPath = new DirectoryInfo(Environment.CurrentDirectory);
                string directoryPath = null;
                string filePath = null;
                string resultsFolder = null;
                //resultsFolder = currentDirectory.Parent.FullName;

                switch (folderName)
                {
                    case FilePath.LogReport: filePath = currentDirPath.FullName + "\\LogReportFolder\\FileSample.txt";
                        break;
                    case FilePath.Report:
                        filePath = currentDirPath.FullName + "\\ResultFolder\\New_Result.html";
                        //directoryPath = currentDirPath.FullName + "\\ResultFolder";
                        //if (!Directory.Exists(directoryPath))
                        //{
                        //    Directory.CreateDirectory(directoryPath);
                        //}
                        //filePath = "Results - "+DateTime.Now.Day.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Month.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Year.ToString(CultureInfo.CurrentCulture) + "_" + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Minute.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Hour.ToString(CultureInfo.CurrentCulture) + DateTime.Now.Second.ToString(CultureInfo.CurrentCulture) + ".html";
                        //File.WriteAllText(filePath, @"<html><body></body></html>");
                        //resultsFolder = directoryPath + "\\" + filePath;
                        break;
                    case FilePath.ScreenShot: filePath = currentDirPath.FullName + "\\ScreenshotFolder";
                        break;
                    case FilePath.TestData: filePath = currentDirPath.FullName + "\\TestData\\TestData.xlsx";
                        break;
                }
                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
