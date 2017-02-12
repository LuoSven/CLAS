using CLAS.Model.TMs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using CLAS.Common;
using CLAS.Common.Json;
using CLAS.Data.Infrastructure;
using CLAS.Data.Repositories;
using CLAS.Model.Entities;
using CLAS.Model.VMs;
using CLAS.Utils;

namespace CLAS.API.Controllers
{
    public class UploadController : ApiController
    {
        private readonly IBidderRepo bidderRepo = new BidderRepo(new DatabaseFactory());
        private readonly IBidderScreenCutRpeo bidderScreenCutRpeo = new BidderScreenCutRpeo(new DatabaseFactory()); 

         
        /// <summary>
        /// 图片上传  [FromBody]string token
        /// </summary>
        /// <returns></returns>
        [HttpPost] 
        public void ImgUpload(string s)
        { 
            if (string.IsNullOrEmpty(s))
                throw new HttpResponseException(HttpStatusCode.NotFound);

            var tm = DESEncrypt.DecryptModel<ScreenCutTM>(s);

            var bidderId = bidderRepo.GetIdByActivationCode(tm.ActivationCode);
            if (!bidderId.HasValue)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            // 文件保存目录路径 
            var saveTempPath = "/UploadFiles/" + bidderId+"/" ;
            var dirTempPath = HttpContext.Current.Server.MapPath(saveTempPath);
            if (!Directory.Exists(dirTempPath))
            {
                Directory.CreateDirectory(dirTempPath);
            }


            var context = (HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
            var request = context.Request;//定义传统request对象
            var imgFile = request.Files[0];

            var ext = imgFile.FileName.Split('.')[1];
            var fileName = string.Format("{0}_{1}.{2}", bidderId, DateTime.Now.ToString("yyyyMMddHHmmssfff"), ext);
            var filePath = saveTempPath + fileName;
            imgFile.SaveAs(dirTempPath + fileName);
            var host = Request.RequestUri.Host.ToLower();
            if (host != "www.clas.com")
            {
                host = host + ":8333";
            } 
            var screenCut = new CL_Bidder_ScreenCut()
            {
                BidderId = bidderId.Value,
                FileName = fileName,
                FilePath = filePath,
                UploadTime = tm.UploadTime,
                CreateTime = DateTime.Now,
                Url = "http://"+host + filePath
            };
            bidderScreenCutRpeo.AddByDapper(screenCut);




        }
         
    }
}
