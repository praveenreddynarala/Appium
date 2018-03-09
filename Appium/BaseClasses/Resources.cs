using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Appium.Helpers
{
    public class Resources
    {
        public Dictionary<string, string> Testdata;

        public Resources()
        {
            XmlDocument currentDocument = new XmlDocument();
            XmlReaderSettings readerSettings = new XmlReaderSettings();
            readerSettings.IgnoreWhitespace = true;
            readerSettings.IgnoreComments = true;
            XmlReader reader = XmlReader.Create(Path.Combine(Directory.GetCurrentDirectory(), "TestData", "Data.Xml"), readerSettings);


            currentDocument.Load(reader); ;
            Testdata = ReadTestData(currentDocument, "Data");           
            currentDocument = null;
        }

        public static Dictionary<string, string> ReadTestData(XmlDocument currentDocument, string rootnodepath)
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            XmlNodeList nodeList = currentDocument.SelectNodes(rootnodepath);
            foreach (XmlNode node in nodeList)
            {
                foreach (XmlNode innerNode in node.ChildNodes)
                {
                    if (innerNode.Attributes[0].Value != null && innerNode.Attributes.Count == 2)
                    {
                        dict.Add(innerNode.Attributes[0].Value, innerNode.Attributes[1].Value);
                    }
                }
            }
            currentDocument = null;
            return dict;
        }
    }
}
