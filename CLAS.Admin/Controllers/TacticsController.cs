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
    [Description("策略管理")] 
    public class TacticsController : BaseController
    {
        private readonly ITacticsRepo tacticsRepo = new TacticsRepo(new DatabaseFactory());
        private readonly IScriptRepo scriptRepo = new ScriptRepo(new DatabaseFactory());

        [Description("策略列表")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> Index(TacticsSM sm)
        {
            var vms =  tacticsRepo.GetTacticsVms(sm);

            if (Request.IsAjaxRequest())
                return PartialView("_List", vms);
            return View(vms);
        }

        [Description("编辑策略")]
        [ActionType(RightType.Form)]
        public async Task<ActionResult> Edit(int id)
        {
            ViewBag.Id = id;
            return View("AddOrEdit");
        }


        /// <summary>
        /// 获取策略的对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult GetTactics(int id)
        {
            var vm = tacticsRepo.GetTacticsVm(id);
            vm = vm ?? new TacticsVM();
            vm.ScriptSelectItems = scriptRepo.GetScriptListItems();
            return Json(vm,JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveTactics(TacticsVM model)
        {
             tacticsRepo.SaveTactics(model,ViewHelp.GetUserName()); 
            return Json(new{code=1});
        }

    }
}
