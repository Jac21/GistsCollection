using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;

namespace DataConsumption.XML
{
    public class XmlParser
    {
        private static string _xmlString;

        public XmlParser()
        {
            _xmlString = @"<?xml version = ""1.0"" encoding = ""utf-8"" ?>
                            <people>
                                <person firstname = ""john"" lastname = ""doe"">
                                    <contactdetails>
                                        <emailaddress>john@unknown.com</emailaddress>
                                    </contactdetails>
                                </person>
                            </people>";
        }

        /// <summary>
        /// Read in xml string, get attributes based on string args
        /// </summary>
        public void XmlRead()
        {
            using (StringReader stringReader = new StringReader(_xmlString))
            {
                using (
                    XmlReader xmlReader = XmlReader.Create(stringReader,
                        new XmlReaderSettings() {IgnoreWhitespace = true}))
                {
                    xmlReader.MoveToContent();
                    xmlReader.ReadStartElement("people");

                    string firstName = xmlReader.GetAttribute("firstname");
                    string lastName = xmlReader.GetAttribute("lastname");

                    Console.WriteLine("Person: {0} {1}", firstName, lastName);

                    xmlReader.ReadStartElement("person");
                    Console.WriteLine("ContactDetails");

                    xmlReader.ReadStartElement("contactdetails");
                    string emailAddress = xmlReader.ReadString();

                    Console.WriteLine("Email address: {0}", emailAddress);
                }
            }
        }

        /// <summary>
        /// Write xml string using XmlWriter object, creating elements and child attributes
        /// </summary>
        public void XmlWrite()
        {
            StringWriter stream = new StringWriter();

            using (XmlWriter writer = XmlWriter.Create(
                stream,
                new XmlWriterSettings()))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("People");
                writer.WriteStartElement("Person");
                writer.WriteAttributeString("firstName", "John");
                writer.WriteAttributeString("lastName", "Doe");
                writer.WriteStartElement("ContactDetails");
                writer.WriteElementString("EmailAddress", "john@unknown.com");
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.Flush();
            }

            Console.WriteLine(stream.ToString());
        }

        /// <summary>
        /// Use XmlDocument to easily traverse/create small xml files when runtime is not
        /// a concern
        /// </summary>
        public void XmlDoc()
        {
            XmlDocument doc = new XmlDocument();

            doc.LoadXml(_xmlString);
            XmlNodeList nodes = doc.GetElementsByTagName("person");

            // output the names of the people in the document
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes != null)
                {
                    string firstName = node.Attributes["firstname"].Value;
                    string lastName = node.Attributes["lastname"].Value;
                    Console.WriteLine("Name: {0} {1}", firstName, lastName);
                }
            }

            // start creating a new node
            XmlNode newNode = doc.CreateNode(XmlNodeType.Element, "person", "");

            XmlAttribute firstNameAttribute = doc.CreateAttribute("firstname");
            firstNameAttribute.Value = "Foo";

            XmlAttribute lastNameAttribute = doc.CreateAttribute("lastname");
            lastNameAttribute.Value = "Bar";

            if (newNode.Attributes != null)
            {
                newNode.Attributes.Append(firstNameAttribute);
                newNode.Attributes.Append(lastNameAttribute);
            }

            if (doc.DocumentElement != null) doc.DocumentElement.AppendChild(newNode);
            Console.WriteLine("Modified xml...");
            doc.Save(Console.Out);
        }

        /// <summary>
        /// Search XML strings based on XPath
        /// </summary>
        public void XpathQuery()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(_xmlString);

            XPathNavigator nav = doc.CreateNavigator();
            string query = "//people/person[@firstname='john']";
            XPathNodeIterator iterator = nav.Select(query);

            Console.WriteLine(iterator.Count);

            while (iterator.MoveNext())
            {
                string firstname = iterator.Current.GetAttribute("firstname", "");
                string lastname = iterator.Current.GetAttribute("lastName", "");
                Console.WriteLine("Name: {0}, {1}", firstname, lastname);
            }
        }
    }
}
