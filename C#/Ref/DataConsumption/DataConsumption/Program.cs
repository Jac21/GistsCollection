using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataConsumption.XML;

namespace DataConsumption
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlParser xmlParser = new XmlParser();
            xmlParser.XmlRead();
            xmlParser.XmlWrite();
            xmlParser.XmlDoc();
            xmlParser.XpathQuery();
        }
    }
}
