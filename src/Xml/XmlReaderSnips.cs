using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CodeSnips
{
    public class XmlReaderSnips
    {
        public static void Run()
        {
            string Xml = @"<OuterElement xmlns=""http://www.w3.org/2000/09/xmldsig#"">
                                <InnerElement>
                                    <StringElement>Here is a string</StringElement>
                                </InnerElement>
                            </OuterElement>";

            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine($"Xml: {Xml}");
            var sr = new StringReader(Xml);
            var reader = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(sr));
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();

            var str = reader.ReadString();
            IsOnEndElement(reader, str, "ReadString");

            sr = new StringReader(Xml);
            reader = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(sr));
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();

            str = reader.ReadElementContentAsString();
            IsOnEndElement(reader, str, "ReadElementContentAsString");

            sr = new StringReader(Xml);
            reader = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(sr));
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            str = reader.ReadElementString();
            IsOnEndElement(reader, str, "ReadElementString");


            sr = new StringReader(Xml);
            reader = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(sr));
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            str = reader.ReadContentAsString();
            reader.ReadEndElement();
            IsOnEndElement(reader, str, "ReadStartElement, ReadContentAsString, ReadEndElement");

            sr = new StringReader(Xml);
            reader = XmlDictionaryReader.CreateDictionaryReader(XmlReader.Create(sr));
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            reader.MoveToContent();
            reader.ReadStartElement();
            str = reader.ReadContentAsString();
            IsOnEndElement(reader, str, "ReadStartElement, ReadContentAsString");

        }

        private static void IsOnEndElement(XmlDictionaryReader reader, string strValue, string callType)
        {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine($"Value: '{strValue}', After: '{callType}',  NodeType: '{reader.NodeType}'");
            Console.WriteLine($"Reader is at: '{reader.Name}', NodeType: '{reader.NodeType}'");
            reader.MoveToContent();
            Console.WriteLine($"MoveToContent(), Reader is at: '{reader.Name}', NodeType: '{reader.NodeType}'");
        }
    }
}
