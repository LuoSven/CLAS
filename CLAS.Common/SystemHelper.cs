using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Common
{
    public class SystemHelper
    {
        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        public string GetSystemType()
        {
            var st = "";
            var mc = new ManagementClass("Win32_ComputerSystem");
            var moc = mc.GetInstances();
            foreach (var mo in moc)
            {
                st = mo["SystemType"].ToString();
            }
            return st;
        }

        
       /// <summary>
       /// 获取物理内存
       /// </summary>
       /// <returns></returns>
        public string GetPhysicalMemory()
        {
            var st = "";
            var mc = new ManagementClass("Win32_ComputerSystem");
            var moc = mc.GetInstances();
           
            foreach (var mo in moc)
            {
                st = mo["TotalPhysicalMemory"].ToString();
            }
            return st;
        }

        public string GetDiskInfo()
        {

            string str = "";

            var query = new SelectQuery("Select * From Win32_LogicalDisk");

            var searcher = new ManagementObjectSearcher(query);

            foreach (ManagementBaseObject d in searcher.Get())
            {

                str = d["Name"] + " " + d["DriveType"] + " " + d["VolumeName"];

                Console.WriteLine("\r\n" + str);

            }




            // get disk size         

            ManagementObject disk = new ManagementObject("win32_logicaldisk.deviceid=\"c:\"");

            disk.Get();

            Console.WriteLine("Logical Disk Size = " + disk["Size"] + " bytes");

            Console.ReadLine(); return str;

        }
    }
}
