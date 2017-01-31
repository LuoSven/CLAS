

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CLAS.Web.Core.Base
{
    [AuthorizeFilterAttribute]
    public class BaseController : Controller
    {
        public string ControlName { get; set; }
        public string ActionName { get; set; }

        public void Log(object entity, string Remark = "")
        {

        }

        protected override void OnException(ExceptionContext filterContext)
        {

            // 标记异常已处理
            filterContext.ExceptionHandled = true;
            // 跳转到错误页
            filterContext.Result = View("Error", filterContext.Exception);

        }

    }
}
