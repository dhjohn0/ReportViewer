using System.Collections.Generic;
using System.Web;
using Rdl.Reader;

namespace Rdl.Web.Models
{
    public class ReportFileViewModel
    {
        public string ReportFile { get; set; }
        public IEnumerable<rdlReportParameter> Parameters { get; set; }
    }
}