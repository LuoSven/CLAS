using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CLAS.Web.Core;
using CLAS.Common;
using  System.ComponentModel;
using CLAS.Data.Repositories;
using CLAS.Data.Infrastructure;
using CLAS.Web.Core.Base;
using AutoMapper;
using CLAS.Model.VMs;
using CLAS.Model.DTOs;
using System.Threading.Tasks;
using CLAS.Model.Entities;
using CLAS.Utils;
using CLAS.Model.SMs;

namespace CLAS.Web.Controllers
{
    [Description("用户管理")] 
    public class SystemUserController : BaseController
    {

        private readonly IUserRepo userRepo = new UserRepo(new DatabaseFactory()); 
        [Description("用户列表")]
        [ActionType(RightType.View)]
        public async Task< ActionResult> Index(SystemUserSM sm)
        {
          var Vms= await  userRepo.GetUserList(sm); 
            
          if (Request.IsAjaxRequest())
              return PartialView("_List", Vms);
          return View(Vms);
        }
        [Description("新增用户")]
        [ActionType(RightType.View)]
        public async Task<ActionResult> Add()
        {
            var model = new CL_User(); 
            return View("AddOrEdit", model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(CL_User model)
        {
            if (userRepo.IsUserNameHaved(model.UserName))
            {
                return Json(new { code = 0, message = "用户名已存在，请重新输入" });
            }


            model.Password = DESEncrypt.Encrypt(model.Password); 
            userRepo.Add(model);
            var result = userRepo.SaveChanges();
            if (result > 0)
                return Json(new { code = 1 });
            else
                return Json(new { code = 0, message = "保存失败，请重试" });
        }

        [Description("编辑用户")]
        [ActionType(RightType.Form, "Index")]
        public async Task<ActionResult> Edit(int Id)
        {
            var model = userRepo.GetById(Id);
            model.Password = DESEncrypt.Decrypt(model.Password); 
           return View("AddOrEdit",model);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(CL_User model)
        {
            var entity=userRepo.GetById(model.Id);
            model.Password = DESEncrypt.Encrypt(model.Password);  
           var result= userRepo.SaveChanges();
           if (result > 0)
               return Json(new { code = 1 });
           else
               return Json(new { code = 0,message="保存失败，请重试" });
        }

        [Description("删除用户")]
        [ActionType(RightType.Form, "Index")]
        public async Task<ActionResult> Delete(int Id)
        {
            var model = userRepo.GetById(Id);
            if (model == null)
            {
                return Json(new { code = 0, message = "用户不存在！" }, JsonRequestBehavior.AllowGet);
            }
            userRepo.Delete(model);
            if (userRepo.SaveChanges() > 0)
            {
                Log(model);
                return Json(new { code = 1 }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { code = 0, message = "删除失败，请重试" }, JsonRequestBehavior.AllowGet);
        }


         public async Task<ActionResult> ChangePassword()
        {
            return View();
        }
        [HttpPost]
         public async Task<JsonResult> ChangePassword(string OPassword,string NPassword)
         {
             var result=userRepo.ChangePassword(ViewHelp.GetUserId(), OPassword, NPassword);
             return Json(new { code = result == "" ? 1 : 0,message= result });
         }
 

        [Description("更新系统作业")]
        [ActionType(RightType.Form)]
        public ActionResult UpdateSystemPrograms()
        {
            var programs = ViewHelp.GetAllActionByAssembly(AppDomain.CurrentDomain.BaseDirectory + "\\bin");
            var messageAll = "";
            ISystemProgromRepo systemProgromRepo = new SystemProgromRepo(new DatabaseFactory());
            messageAll += string.Format("共发现{0}个作业\r\n", programs.Count);
            foreach (var item in programs)
            {

                var result = systemProgromRepo.AddOrUpdateProgram(item);
                var message = item.ActionDescription + "(" + item.ControllerName + "/" + item.ActionName + ")";
                switch (result)
                {
                    case 1:
                        message += "新增成功\r\n";
                        break;
                    case 2:
                        message += "新增失败\r\n";
                        break;
                    case 3:
                        message += "更新成功\r\n";
                        break;
                    case 4:
                        message += "更新失败\r\n";
                        break;
                }
                messageAll += message;
            }

            return Json(new { code = 1, message = messageAll }, JsonRequestBehavior.AllowGet);
        }
    }
}
