using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Rdl.Reader
{
    public class rdlReader
    {
        public static rdlReport Get(string filename)
        {
            var doc = XDocument.Load(filename);
            if (doc.Root == null)
                throw new NotSupportedException("Invalid document");

            using (var stream = new FileStream(filename, FileMode.Open))
            {
                switch (doc.Root.Name.NamespaceName)
                {
                    case "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition":
                        var xs2016 = new XmlSerializer(typeof(rdlReport2016));
                        return (rdlReport2016)xs2016.Deserialize(stream);
                    case "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition":
                        var xs2010 = new XmlSerializer(typeof(rdlReport2010));
                        return (rdlReport2010)xs2010.Deserialize(stream);
                    case "http://schemas.microsoft.com/sqlserver/reporting/2008/01/reportdefinition":
                        var xs2008 = new XmlSerializer(typeof(rdlReport2008));
                        return (rdlReport2008)xs2008.Deserialize(stream);
                    case "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition":
                        var xs2005 = new XmlSerializer(typeof(rdlReport2010));
                        return (rdlReport2010)xs2005.Deserialize(stream);
                    case "":
                        var xsBasic = new XmlSerializer(typeof(rdlReportBasic));
                        return (rdlReportBasic)xsBasic.Deserialize(stream);
                    default:
                        throw new NotSupportedException("Unsupported xmlns namespace (version): " + doc.Root.Name.NamespaceName);
                }
            }
        }

        public static Dictionary<string, string> GetDataSets(rdlReport doc)
        {
            var dataSets = new Dictionary<string, string>();

            if (doc.DataSets != null && doc.DataSets.DataSets != null)
            {
                foreach (var node in doc.DataSets.DataSets)
                {
                    if (node == null)
                        continue;
                    if (string.IsNullOrEmpty(node.Name))
                        continue;
                    if (node.Query == null || string.IsNullOrEmpty(node.Query.CommandText))
                        continue;

                    var name = node.Name;
                    var query = node.Query.CommandText;

                    dataSets.Add(name, query);
                }
            }

            return dataSets;
        }

        public static IEnumerable<rdlReportParameter> GetParameters(rdlReport doc)
        {
            if (doc == null)
                return new rdlReportParameter[0];
            if (doc.ReportParameters == null)
                return new rdlReportParameter[0];
            if (doc.ReportParameters.ReportParameter == null)
                return new rdlReportParameter[0];

            return doc.ReportParameters.ReportParameter;
        }
    }
}
