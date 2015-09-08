using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Schema.Common.DataTypes;

namespace TemplateTester.Controllers
{
    public class CodeGenController : Controller
    {
        // GET: CodeGen
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult InsertNonquerySp()
        {
            var json = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/InsertNonquerySp.txt"));
            DbTable model = JsonConvert.DeserializeObject<DbTable>(json);

            return View(model);
        }

      public ActionResult CallInserNonQuerySp()
        {
            var json = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/InsertNonquerySp.txt"));
            DbTable model = JsonConvert.DeserializeObject<DbTable>(json);

            return View(model);
        }

        public ActionResult UpdateSP()
        {
            var json = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/InsertNonquerySp.txt"));
            DbTable model = JsonConvert.DeserializeObject<DbTable>(json);
            return View(model);
        }

  

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
    }
}