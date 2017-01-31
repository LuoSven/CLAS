using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Common
{
    public class MouseEventHelper
    {
        public static void Init(int screenHeight, int screenWidth)
        {
            MouseEventHelper.screenHeight = screenHeight;
            MouseEventHelper.screenWidth = screenWidth;

        }
        #region 私有属性
        //屏幕高度
        private static int screenHeight;
        //屏幕宽度
        private static int screenWidth;
        //移动鼠标 
        const int MOUSEEVENTF_MOVE = 0x0001;
        //模拟鼠标左键按下 
        const int MOUSEEVENTF_LEFTDOWN = 0x0002;
        //模拟鼠标左键抬起 
        const int MOUSEEVENTF_LEFTUP = 0x0004;
        //模拟鼠标右键按下 
        const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        //模拟鼠标右键抬起 
        const int MOUSEEVENTF_RIGHTUP = 0x0010;
        //模拟鼠标中键按下 
        const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        //模拟鼠标中键抬起 
        const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        //标示是否采用绝对坐标 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000; 
        #endregion

        #region 相对运动
        /// <summary>
        /// 相对移动
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_MOVE, ConventPx(x), ConventPy(y), 0, 0);
        }

        /// <summary>
        /// 相对点击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void ClickTo(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, x, y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }
        #endregion    

        #region 绝对事件
          /// <summary>
        /// 绝对移动
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void MoveToAbsolute(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, ConventPx(x), ConventPy(y), 0, 0);
        }


        /// <summary>
        /// 绝对点击
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public static void ClickToAbsolute(int x, int y)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_ABSOLUTE, ConventPx(x), ConventPy(y), 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP | MOUSEEVENTF_ABSOLUTE, ConventPx(x), ConventPy(y), 0, 0);
        }
        #endregion         

        #region 私有函数
        /// <summary>
        /// 根据分辨率转换坐标
        /// </summary>
        /// <param name="px"></param>
        /// <returns></returns>
        private static int ConventPx(int px)
        {
            return px * 65535 / MouseEventHelper.screenWidth;
        }
        /// <summary>
        /// 根据分辨率转换坐标
        /// </summary>
        /// <param name="py"></param>
        /// <returns></returns>
        private static int ConventPy(int py)
        {

            return py * 65535 / MouseEventHelper.screenHeight;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        private static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        #endregion
       
    }
}
