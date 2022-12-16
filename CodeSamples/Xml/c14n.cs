using System.Text;
using System.Xml;
using static System.Net.WebUtility;

namespace CodeSnips.Xml
{
    public class C14n
    {
        public static void Run()
        {
            MemoryStream memoryStream = new();
            MemoryStream c14MemoryStream = new();
            var writer = XmlDictionaryWriter.CreateTextWriter(memoryStream, Encoding.UTF8, false);
            //string gro = "Großbauteile";
            writer.StartCanonicalization(c14MemoryStream, false, null);
            writer.WriteStartElement("With@");
            writer.WriteString(@"\r\nHere is a Großbauteile string\r\n");
            writer.WriteEndElement();
            writer.WriteStartElement("WithOut");
            writer.WriteString("\r\nHere is a Großbauteile string\r\n");
            writer.WriteEndElement();
            writer.WriteStartElement("Großbauteile");
            writer.WriteString("Großbauteile");
            writer.WriteEndElement();
            writer.WriteStartElement("HtmlEncode Großbauteile");
            writer.WriteString(HtmlEncode("Großbauteile"));
            writer.WriteEndElement();

            writer.EndCanonicalization();
            writer.Flush();
            string cbytes = Encoding.UTF8.GetString(c14MemoryStream.ToArray());
            Console.WriteLine($"c14n, Xml: {cbytes}");
            Console.WriteLine("HtmlEncode(Hi: &#xD): " + HtmlEncode("Hi: &#xD;"));
            Console.WriteLine("Uri.EscapeDataString(Hi: &#xD): " + Uri.EscapeDataString("Hi: &#xD;"));
            Console.WriteLine("Uri.EscapeDataString(Hi: Großbauteile): " + Uri.EscapeDataString("Hi: Großbauteile"));
        }
    }
}
