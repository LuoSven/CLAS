using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CLAS.Common;
using CLAS.Web.Core.Base;
using CLAS.Model.VMs;
using CLAS.Data.Repositories;
using CLAS.Utils;
using CLAS.Web.Core;
using CLAS.Data.Infrastructure;

namespace CLAS.Web.Controllers
{
    public class AccountController : BaseController
    {
        //
        // GET: /Account/Login
        private readonly IUserRepo userAccountRepo = new UserRepo(new DatabaseFactory());

        public AccountController()
        {
        }
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            //if(ViewHelp.GetUserId()!=0)
            //    return RedirectToLocal(returnUrl);
            CookieHelper.DeleteCookie(StaticKey.CookieAccountKey);
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AccountLoginVM model, string returnUrl)
        {

            if (ModelState.IsValid)
            {
                var result = userAccountRepo.Login(model); 

                if (string.IsNullOrEmpty(result.Message))
                {
                    var acconutCookieE = result.SetCookie();
                    CookieHelper.WriteCookie(StaticKey.CookieAccountKey, acconutCookieE, false);
                    return RedirectToAction("INDEX", "Home");
                }

                ModelState.AddModelError("", result.Message);
            }

            // 如果我们进行到这一步时某个地方出错，则重新显示表单
            return View(model);
        }

        //
        // POST: /Account/LogOff

        public ActionResult LogOff()
        { 
            CookieHelper.DeleteCookie(StaticKey.CookieAccountKey);
            return RedirectToAction("Login");
        }

  

     
         
 
    }
}
