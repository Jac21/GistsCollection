using DataConsumption.XML;
using DataConsumption.JSON;

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

            JsonParser jsonParser = new JsonParser();
            jsonParser.SerializeObject();
            jsonParser.SerializeList();
            jsonParser.SerializeDictionary();
            jsonParser.SerializeToFile();
        }
    }
}
