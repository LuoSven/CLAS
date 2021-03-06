﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Security.Principal;
using CLAS.Common;

namespace CLAS.Web.Core
{
    /// <summary>
    /// 表示Action的权限类型
    /// </summary>
     [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ActionType : System.Attribute
    {
         public ActionType(RightType ActionType, string ParentAction="")
         {
             this.RightType = ActionType;
             this.ParentAction = ParentAction;
         }

         public string ParentAction {get;set;}
         public RightType RightType { get; set; }
            
    }
}