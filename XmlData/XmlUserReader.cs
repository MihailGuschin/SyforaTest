using System.Xml.Linq;

namespace XmlData
{
    public class XmlUserReader
    {
        private const string FilePath = "users.xml";

        public XDocument LoadDocument()
        {
            if (!File.Exists(FilePath))
            {
                var doc = new XDocument(new XElement("Users"));
                doc.Save(FilePath);
                return doc;
            }
            return XDocument.Load(FilePath);
        }

        public void SaveDocument(XDocument doc)
        {
            doc.Save(FilePath);
        }
    }
}
