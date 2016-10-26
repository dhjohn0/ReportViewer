using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
// ReSharper disable InconsistentNaming

namespace Rdl.Reader
{

    [XmlRoot("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition")]
    public class rdlReport2016 : rdlReport { }
    [XmlRoot("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition")]
    public class rdlReport2010 : rdlReport { }
    [XmlRoot("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition")]
    public class rdlReport2008 : rdlReport { }
    [XmlRoot("Report", Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")]
    public class rdlReport2005 : rdlReport { }
    [XmlRoot("Report", Namespace = "")]
    public class rdlReportBasic : rdlReport { }

    public class rdlReport
    {
        [XmlElement("DataSets")]
        public rdlDataSets DataSets { get; set; }
        [XmlElement("ReportParameters")]
        public rdlReportParameters ReportParameters { get; set; }
    }

    public class rdlDataSets
    {
        [XmlElement("DataSet")]
        public List<rdlDataSet> DataSets { get; set; }
    }

    public class rdlDataSet
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlElement("Query")]
        public rdlQuery Query { get; set; }

        [XmlElement("QueryParameters")]
        public List<rdlQueryParameter> QueryParameters { get; set; } 
    }

    public class rdlQueryParameters
    {
        [XmlElement("QueryParameter")]
        public List<rdlQueryParameter> QueryParameter { get; set; }
    }

    public class rdlQueryParameter
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlElement("Value")]
        public string Value { get; set; }
    }
    public class rdlQuery
    {
        [XmlElement("CommandText")]
        public string CommandText { get; set; }
    }

    public class rdlReportParameters
    {
        [XmlElement("ReportParameter")]
        public List<rdlReportParameter> ReportParameter { get; set; } 
    }

    public class rdlReportParameter
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlElement("DataType")]
        public string DataType { get; set; }
        [XmlElement("AllowBlank")]
        public bool AllowBlank { get; set; }
        [XmlElement("Nullable")]
        public bool Nullable { get; set; }
        [XmlElement("Prompt")]
        public string Prompt { get; set; }
        [XmlElement("DefaultValue")]
        public rdlDefaultValue DefaultValue { get; set; }
    }

    public class rdlDefaultValue
    {
        [XmlElement("Values")]
        public rdlValues Values { get; set; }
    }

    public class rdlValues
    {
        [XmlElement("Value")]
        public List<string> Value { get; set; }
    }
}
