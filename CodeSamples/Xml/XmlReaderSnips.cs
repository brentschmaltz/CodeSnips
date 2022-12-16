//------------------------------------------------------------------------------
//
// Copyright (c) Brent Schmaltz
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using System;
using System.IO;
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
