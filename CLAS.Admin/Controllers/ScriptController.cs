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
using CLAS.Model.VMs;
using CLAS.Web.Core;
using CLAS.Web.Core.Base;

namespace CLAS.Admin.Controllers
{
    [Description("脚本管理")]
    public class ScriptController : BaseController
    {
        private readonly IScriptExecuteRecordRpeo scriptExecuteRecordRpeo = new ScriptExecuteRecordRpeo(new DatabaseFactory());
        private readonly IScriptRepo scriptRpeo = new ScriptRepo(new DatabaseFactory());



        [Description("脚本执行记录")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> ExecuteRecord(ScriptSM sm,int page=1,int pageSize=20)
        {
            var vms = scriptExecuteRecordRpeo.GetList(sm, page, pageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_ListExecuteRecord", vms);
            return View(vms);
        }


        [Description("脚本管理")]
        [ActionType(RightType.View)]
        public ActionResult Index(ScriptSM sm, int page = 1, int pageSize = 20)
        {
            var vms = scriptExecuteRecordRpeo.GetList(sm, page, pageSize);

            if (Request.IsAjaxRequest())
                return PartialView("_List", vms);
            return View(vms);
        }

    }
}
