using CLAS.Data.Infrastructure;
using CLAS.Data.Repositories;
using CLAS.Model.Entities;
using CLAS.Web.Core;
using CLAS.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace CLAS.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult NoRight()
        {
            return View();
        }
    }
}
