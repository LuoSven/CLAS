
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CLAS.Common
{
    /// <summary>压缩简历RAR
    /// 
    /// </summary>
    public class CompressFile
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pSource">要压缩的文件夹</param>
        /// <param name="pDestination">压缩后的rar完整名</param>
        public void CreateRar(string pSource, string pDestination)
        {
            try
            {
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = "Winrar.exe";
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.Arguments = " a -r -ep1 " + pDestination + " " + pSource;
                proc.Start();
                proc.WaitForExit();
                if (proc.HasExited)
                {
                    int iExitCode = proc.ExitCode;
                    if (iExitCode == 0)
                    {//压缩成功}
                    }
                    else
                    {
                    }
                }
                proc.Close();
            }
            catch (Exception ex)
            {

            }
        }

 
    }
}
