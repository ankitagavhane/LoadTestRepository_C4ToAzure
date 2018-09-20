using System.Xml.Serialization;

namespace Twinfield_NewFramework
{
    [XmlRoot(ElementName = "TableType")]
    public class TableType
    {
        [XmlElement(ElementName = "A_DB")]
        public string A_DB { get; set; }

        [XmlElement(ElementName = "B_DB")]
        public string B_DB { get; set; }

		[XmlElement(ElementName = "C_DB")]
		public string C_DB { get; set; }

		[XmlElement(ElementName = "D_DB")]
		public string D_DB { get; set; }

		[XmlElement(ElementName = "E_DB")]
		public string E_DB { get; set; }
		[XmlElement(ElementName = "AllTenantInputTable")]
		public string AllTenantInputTable { get; set; }

        [XmlAttribute(AttributeName = "xsi", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsi { get; set; }

        [XmlAttribute(AttributeName = "xsd", Namespace = "http://www.w3.org/2000/xmlns/")]
        public string Xsd { get; set; }
    }
}