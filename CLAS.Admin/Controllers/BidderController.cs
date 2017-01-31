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
    [Description("拍手管理")]
    public class BidderController : BaseController
    {
        private readonly ITacticsRepo tacticsRepo = new TacticsRepo(new DatabaseFactory());
        private readonly IBidderKeyDownRecordRpeo bidderKeyDownRecordRpeo = new BidderKeyDownRecordRpeo(new DatabaseFactory());
        private readonly IBidderScreenCutRpeo bidderScreenCutRpeo = new BidderScreenCutRpeo(new DatabaseFactory());

        [Description("拍手信息")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> Index(TacticsSM sm)
        {
            var vms =  tacticsRepo.GetTacticsVms(sm);

            if (Request.IsAjaxRequest())
                return PartialView("_List", vms);
            return View(vms);
        }

        [Description("新增拍手")]
        [ActionType(RightType.Form)]
        public async Task<ActionResult> Add()
        {
            ViewBag.Id = 0;
            return View("AddOrEdit");
        }



        [Description("编辑拍手")]
        [ActionType(RightType.Form)]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Id = id;
            return View("AddOrEdit");
        }



        [Description("按键记录")]
        [ActionType(RightType.View)]
        public async Task<ActionResult>KeyDownRecord(KeyDownRecordSM sm)
        {

            if (sm.StartTime == null&&sm.EndTime==null)
            {
                var saturday = DateTime.Now.GetLastThirdWeekSaturday();
                sm.StartTime = saturday.UpdateTime("11:28:30");
                sm.EndTime = saturday.UpdateTime("11:30:30");
            }
            ViewBag.start = sm.StartTime;
            ViewBag.end = sm.EndTime;
            var vms = bidderKeyDownRecordRpeo.GetGroupedList(sm);
            if (Request.IsAjaxRequest())
                return PartialView("_KeyDownRecordList", vms);
            return View(vms);
        }




        [Description("截图记录")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> ScreenCutRecord(ScreenCutRecordSM sm)
        {

            if (sm.StartTime == null && sm.EndTime == null)
            {
                var saturday = DateTime.Now.GetLastThirdWeekSaturday();
                sm.StartTime = saturday.UpdateTime("11:28:30");
                sm.EndTime = saturday.UpdateTime("11:30:30");
            }
            ViewBag.start = sm.StartTime;
            ViewBag.end = sm.EndTime;
            var vms = bidderScreenCutRpeo.GetGroupedList(sm);
            if (Request.IsAjaxRequest())
                return PartialView("_KeyDownRecordList", vms);
            return View(vms);
        }
    }
}
