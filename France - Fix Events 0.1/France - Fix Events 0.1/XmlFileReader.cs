using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace France___Fix_Events_0._1
{
    class XmlFileReader
    {

        public string GetElementContent(string filePath, string element, params string[] moreElements)
        {
            XmlReader reader = XmlReader.Create(filePath);
            reader.ReadToFollowing(element);
            foreach (string item in moreElements)
            {
                reader.ReadToFollowing(item);
            }
            return reader.ReadElementContentAsString();
        }
    }
}
