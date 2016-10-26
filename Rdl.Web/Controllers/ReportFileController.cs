using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Rdl.Reader;
using Rdl.Web.Models;

namespace Rdl.Web.Controllers
{
    public class ReportFileController : Controller
    {
        public ActionResult Get(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View(new ReportFileViewModel());
            }
            var startingPoint = ConfigurationManager.AppSettings["ReportPath"];
            var path = startingPoint + name + ".rdl";
            try
            {
                var doc = rdlReader.Get(path);
                var paras = rdlReader.GetParameters(doc);

                var model = new ReportFileViewModel
                {
                    ReportFile = name,
                    Parameters = paras
                };

                return View(model);
            }
            catch (FileNotFoundException e)
            {
                return View(new ReportFileViewModel());
            }
            
        }
    }
}