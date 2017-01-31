using CLAS.Data.Repositories;
using CLAS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using CLAS.Data.Infrastructure;
using CLAS.Common;
using CLAS.Model.VMs;
using CLAS.Model.Entities;
using System.Configuration;
using System.Web.Mvc;
using AutoMapper;
using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Web.Core;

namespace CLAS.Web.Core
{
    public class ViewHelp
    { 
        private static AccountVM GetAccountInfoFromCookie()
        {
            var accountCookie = CookieHelper.GetCookie(StaticKey.CookieAccountKey);
            return new AccountVM(accountCookie);

        }
        public static List<CL_System_Program> GetAllActionByAssembly(string FilePath)
        {
            var result = new List<CL_System_Program>();
            FilePath = FilePath + "\\CLAS.Admin.dll";
            var types = Assembly.LoadFile(FilePath).GetTypes();

            foreach (var type in types)
            {
                if (type.BaseType.Name == "BaseController")//如果是Controller
                { 
                    var ControlDescription = "";
                    object[] Controlattrs = type.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                    if (Controlattrs.Length > 0)
                        ControlDescription = (Controlattrs[0] as System.ComponentModel.DescriptionAttribute).Description;
                   
                        var members = type.GetMethods();
                        foreach (var member in members)
                        {
                            if (member.ReturnType.Name.Contains("ActionResult") || member.ReturnType.Name.Contains("Task"))//如果是Action
                            {
                                ActionType rightType = (ActionType)member.GetCustomAttribute(typeof(ActionType));

                                if (rightType != null)
                                {
                                    var ap = new CL_System_Program();
                                    ap.ParentAction = rightType.ParentAction;
                                    ap.RightType = (int)rightType.RightType;
                                    ap.ActionName = member.Name.ToLower();
                                    ap.ControllerName = member.DeclaringType.Name.Substring(0, member.DeclaringType.Name.Length - 10).ToLower(); // 去掉“Controller”后缀
                                    ap.ControllerDescription = ControlDescription;
                                    object[] attrs = member.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), true);
                                    if (attrs.Length > 0)
                                        ap.ActionDescription = (attrs[0] as System.ComponentModel.DescriptionAttribute).Description;

                                    result.Add(ap);
                                }

                            }
                             
                    }

                }
            }
            return result;
        }
        public static AccountVM UserInfo()
        {
            return GetAccountInfoFromCookie();
        }
        public static int GetUserId()
        {
            return GetAccountInfoFromCookie().UserId;
        }
        
       
  
        public static string GetUserName()
        {
            return GetAccountInfoFromCookie().UserName;
        }
 
    
 
    }
}
