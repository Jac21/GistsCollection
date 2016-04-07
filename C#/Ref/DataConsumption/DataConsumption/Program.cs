using System.Configuration;
using DataConsumption.XML;
using DataConsumption.JSON;
using DataConsumption.LINQ;

namespace DataConsumption
{
    class Program
    {
        static void Main(string[] args)
        {
            string configString = ConfigurationManager.AppSettings["Configuration"];

            switch (configString.Trim())
            {
                case "XML":
                    XmlParser xmlParser = new XmlParser();

                    xmlParser.XmlRead();
                    xmlParser.XmlWrite();
                    xmlParser.XmlDoc();
                    xmlParser.XpathQuery();

                    break;
                case "JSON":
                    JsonParser jsonParser = new JsonParser();

                    jsonParser.SerializeObject();
                    jsonParser.SerializeList();
                    jsonParser.SerializeDictionary();
                    jsonParser.SerializeToFile();

                    break;
                case "LINQ":
                    LinqOperations linqOperations = new LinqOperations();

                    linqOperations.WhereOperator();
                    linqOperations.OrderByOperator();

                    XmlParser xmlParserToLinq = new XmlParser();
                    linqOperations.LinqToXml(xmlParserToLinq.XmlString);

                    break;
            }
            
        }
    }
}
