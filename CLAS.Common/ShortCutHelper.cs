using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IWshRuntimeLibrary;

namespace CLAS.Common
{
    public  class ShortCutHelper
    {
        public static void CreateShorCut(string shortCutName,string exeName,string shorCutDes)
        {
            var lnkName = @"\" + shortCutName + ".lnk";
             exeName = @"\" + exeName + ".exe";
             var deskTopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + lnkName;
             if (System.IO.File.Exists(deskTopPath))  //
            {
                System.IO.File.Delete(deskTopPath);//删除原来的桌面快捷键方式 
            }
            var shell = new WshShell();
            //快捷键方式创建的位置、名称
            var shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + lnkName);
            shortcut.TargetPath = @Application.StartupPath + exeName; //目标文件
            shortcut.WorkingDirectory = Environment.CurrentDirectory;//该属性指定应用程序的工作目录，当用户没有指定一个具体的目录时，快捷方式的目标应用程序将使用该属性所指定的目录来装载或保存文件。
            shortcut.WindowStyle = 1; //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
            shortcut.Description = shorCutDes; //描述 
            shortcut.Arguments = ""; 
            shortcut.Save(); //必须调用保存快捷才成创建成功
        }
    }
}
