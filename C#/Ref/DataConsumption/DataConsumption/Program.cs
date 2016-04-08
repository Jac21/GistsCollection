using System.Configuration;
using DataConsumption.XML;
using DataConsumption.JSON;
using DataConsumption.LINQ;

namespace DataConsumption
{
    class Program
    {
        static void Main()
        {
            string configString = ConfigurationManager.AppSettings["Configuration"];

            switch (configString.Trim())
            {
                case "XML":
                    // declare and initialize XmlParser class
                    XmlParser xmlParser = new XmlParser();

                    xmlParser.XmlRead();
                    xmlParser.XmlWrite();
                    xmlParser.XmlDoc();
                    xmlParser.XpathQuery();

                    break;
                case "JSON":
                    // declare and initialize JsonParser
                    JsonParser jsonParser = new JsonParser();

                    jsonParser.SerializeObject();
                    jsonParser.SerializeList();
                    jsonParser.SerializeDictionary();
                    jsonParser.SerializeToFile();
                    jsonParser.SerializeDataset();

                    break;
                case "LINQ":
                    // declare and initialize LinqOperations class, as well as XmlParser
                    // to use with the LinqToXml method
                    LinqOperations linqOperations = new LinqOperations();

                    // call to LINQ example operations
                    linqOperations.WhereOperator();
                    linqOperations.OrderByOperator();

                    XmlParser xmlParserToLinq = new XmlParser();
                    linqOperations.LinqToXml(xmlParserToLinq.XmlString);

                    linqOperations.AllOperator();
                    linqOperations.AnyOperator();
                    linqOperations.AverageOperator();
                    linqOperations.CastOperator();

                    break;
            }
            
        }
    }
}
