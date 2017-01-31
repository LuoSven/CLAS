﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using CLAS.Common;

namespace CLAS.Web.Core
{
    /// <summary>
    /// 权限拦截
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class AuthorizeFilterAttribute : ActionFilterAttribute
    {

    }
}