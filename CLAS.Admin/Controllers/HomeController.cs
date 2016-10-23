using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLAS.Web.Core;
using CLAS.Data.Repositories;
using CLAS.Web.Core.Base;
using CLAS.Data.Infrastructure;
using CLAS.Common;
using CLAS.Model.VMs;
using CLAS.Model.DTOs; 
namespace CLAS.Web.Controllers
{
    public class HomeController : BaseController
    {

        private readonly ISystemProgromRepo systemProgromRepo = new SystemProgromRepo(new DatabaseFactory());
        public ActionResult Index()
        { 
            var memuList = systemProgromRepo.GetMenu(); 
            return View(memuList);
        }
         
         
 
         

    }
}
