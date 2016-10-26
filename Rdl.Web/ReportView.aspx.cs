using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Ajax.Utilities;
using Microsoft.Reporting.WebForms;
using Rdl.Reader;

namespace Rdl.Web
{
    public partial class ReportView : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string filename = null;
                Report.ProcessingMode = ProcessingMode.Local;
                
                var name = Request.QueryString["name"];
                if (!string.IsNullOrEmpty(name))
                {
                    var startingPoint = ConfigurationManager.AppSettings["ReportPath"];
                    filename = startingPoint + name + ".rdl";
                }

                if (filename == null)
                    throw new NotSupportedException("No document given");

                var doc = rdlReader.Get(filename);
                if (doc == null)
                    throw new NotSupportedException("Invalid document");

                var dataSets = rdlReader.GetDataSets(doc);

                try
                {
                    var conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection connection = new SqlConnection(conString);
                    connection.Open();

                    var parameters = new List<ReportParameter>();
                    foreach (var d in dataSets)
                    {
                        var cmd = new SqlCommand(d.Value, connection);

                        foreach (string q in Request.QueryString)
                        {
                            if (q == "id")
                                continue;

                            cmd.Parameters.AddWithValue("@" + q, Request.QueryString[q]);
                        }


                        var dataTable = new DataTable();
                        var da = new SqlDataAdapter(cmd);
                        da.Fill(dataTable);
                        //da.Dispose();

                        var data = new ReportDataSource(d.Key, dataTable);
                        Report.LocalReport.DataSources.Add(data);
                    }

                    foreach (string q in Request.QueryString)
                    {
                        if (q == "id" || q == "name" || q == "render")
                            continue;
                        if (string.IsNullOrEmpty(Request.QueryString[q]))
                            continue;
                        parameters.Add(new ReportParameter(q, Request.QueryString[q]));
                    }

                    connection.Close();

                    Report.DataBind();
                    Report.LocalReport.ReportPath = filename;
                    Report.LocalReport.SetParameters(parameters);

                    if (Request.QueryString["render"] == "PDF")
                    {
                        var stream = Report.LocalReport.Render("PDF");
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = "Application/PDF";
                        Response.BinaryWrite(stream);
                        Response.Flush();
                        Response.End();
                    }
                    
                }
                catch (SqlException ex)
                {
                    var text = "<html><body><h1>" + ex.Message + "</h1></body></html>";   
                    Response.Clear();
                    Response.BinaryWrite(text.ToCharArray().Select( x=>(byte)x ).ToArray() );
                    Response.Flush();
                    Response.End();

                }
            }
        }

        protected void PrevBtn_Click(object sender, EventArgs e)
        {
            if (Report.CurrentPage > 1)
                Report.CurrentPage -= 1;
            SetButtonDisabled();
        }

        protected void NextBtn_Click(object sender, EventArgs e)
        {
            if (Report.CurrentPage < Report.LocalReport.GetTotalPages())
                Report.CurrentPage += 1;
            SetButtonDisabled();
        }

        protected void OnReportLoaded(object sender, EventArgs e)
        {
            SetButtonDisabled();
        }

        protected void SetButtonDisabled()
        {
            //PrevBtn.Enabled = Report.CurrentPage > 1;
            //NextBtn.Enabled = Report.CurrentPage < Report.LocalReport.GetTotalPages();
        }
    }
}