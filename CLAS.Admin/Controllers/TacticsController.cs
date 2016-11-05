using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Data.Repositories;
using CLAS.Model.SMs;
using CLAS.Web.Core;
using CLAS.Web.Core.Base;

namespace CLAS.Admin.Controllers
{
    [Description("策略管理")] 
    public class TacticsController : BaseController
    { 
        private readonly ITacticsRepo tacticsRepo = new TacticsRepo(new DatabaseFactory());

        [Description("策略列表")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> Index(TacticsSM sm)
        {
            var vms =  tacticsRepo.GetTacticsVms(sm);

            if (Request.IsAjaxRequest())
                return PartialView("_List", vms);
            return View(vms);
        }
    }
}
