using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net; 
using System.Threading.Tasks; 
using System.Web.Mvc;
using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Data.Repositories;
using CLAS.Model.SMs;
using CLAS.Model.VMs;
using CLAS.Web.Core;
using CLAS.Web.Core.Base;

namespace CLAS.Admin.Controllers
{
    [Description("牌照管理")]
    public class LicenseController : BaseController
    {
        private readonly ITacticsRepo tacticsRepo = new TacticsRepo(new DatabaseFactory());
        private readonly IBidderRepo bidderRepo = new BidderRepo(new DatabaseFactory());
        private readonly ILicenseRepo licenseRepo = new LicenseRepo(new DatabaseFactory());
        private readonly IBidderKeyDownRecordRpeo bidderKeyDownRecordRpeo = new BidderKeyDownRecordRpeo(new DatabaseFactory());

        [Description("牌照信息")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> Index(LicenseSM sm)
        {
            var vms = licenseRepo.GetList(sm);
            InitSelect();
            if (Request.IsAjaxRequest())
                return PartialView("_List", vms);
            return View(vms);
        }
        [Description("自动生成激活码")]
        [ActionType(RightType.Form)]
        public async Task<ActionResult> AutoCreateActivationCode()
        {
            licenseRepo.AutoCreateActivationCode();
            return Json(new{code=1});
        }

        private void InitSelect()
        {
            var bidderList = bidderRepo.GetList();
            ViewBag.BidderList = new SelectList(bidderList, "id", "name");
            var tacticsList = tacticsRepo.GetList();
            ViewBag.TacticsList = new SelectList(tacticsList, "id", "name");
        }
     
 

    }
}
