
using Tracer;
using Serializer.Dto;
using Serializer.Dto.Extentions;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace Serializer
{
    public static class XmlSerializer
    {
        public static string Serialize(List<ThreadInfo> threads)
        {
            System.Xml.Serialization.XmlSerializer xmlSerializer =
                new System.Xml.Serialization.XmlSerializer(typeof(List<ThreadInfoDto>));

            using (var stringWriter = new StringWriter())
            {
                xmlSerializer.Serialize(stringWriter, threads.ToDto());
                return FormatXml(stringWriter.ToString());
            }
                       
        }
        private static string FormatXml(string xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                Console.WriteLine("uncoorect xml");
                return xml;
            }
        }
    }
}