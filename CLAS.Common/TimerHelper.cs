using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Runtime;
using Newtonsoft.Json;
using CLAS.Common.Json;

namespace CLAS.Common
{
    public class TimerHelper
    {
     
            /// <summary>  
            /// 获取标准北京时间，读取http://www.beijing-time.org/time.asp  
            /// </summary>  
            /// <returns>返回网络时间</returns>  
            public static DateTime? GetBeijingTime()
            {

                DateTime dt;
                WebRequest wrt = null;
                WebResponse wrp = null;
                try
                {
                    wrt = WebRequest.Create("http://api.k780.com:88/?app=life.time&appkey=22190&sign=c221789b89df02693f3bc730387c2601&format=json");
                    wrp = wrt.GetResponse();

                    var json = string.Empty;
                    using (Stream stream = wrp.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                        {
                            json = sr.ReadToEnd();
                        }
                    }
                    var model=JsonHelper.ParseFromJson<TimeCheck>(json);
                    //string[] tempArray = json.Split(';');
                    //for (int i = 0; i < tempArray.Length; i++)
                    //{
                    //    tempArray[i] = tempArray[i].Replace("\r\n", "");
                    //}

                    //string year = tempArray[1].Split('=')[1];
                    //string month = tempArray[2].Split('=')[1];
                    //string day = tempArray[3].Split('=')[1];
                    //string hour = tempArray[5].Split('=')[1];
                    //string minite = tempArray[6].Split('=')[1];
                    //string second = tempArray[7].Split('=')[1];
                    DateTime.TryParse(model.result.datetime_1, out dt);
                }
                catch (WebException)
                {
                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (wrp != null)
                        wrp.Close();
                    if (wrt != null)
                        wrt.Abort();
                }
                return dt;

            }

         
                //设置系统时间的API函数
                [DllImport("kernel32.dll")]
                private static extern bool SetLocalTime(ref SYSTEMTIME time);

                [StructLayout(LayoutKind.Sequential)]
                private struct SYSTEMTIME
                {
                    public short year;
                    public short month;
                    public short dayOfWeek;
                    public short day;
                    public short hour;
                    public short minute;
                    public short second;
                    public short milliseconds;
                }

                /// <summary>
                /// 设置系统时间
                /// </summary>
                /// <param name="dt">需要设置的时间</param>
                /// <returns>返回系统时间设置状态，true为成功，false为失败</returns>
                public static bool SetDate(DateTime dt)
                {
                    SYSTEMTIME st;

                    st.year = (short)dt.Year;
                    st.month = (short)dt.Month;
                    st.dayOfWeek = (short)dt.DayOfWeek;
                    st.day = (short)dt.Day;
                    st.hour = (short)dt.Hour;
                    st.minute = (short)dt.Minute;
                    st.second = (short)dt.Second;
                    st.milliseconds = (short)dt.Millisecond;
                    bool rt = SetLocalTime(ref st);
                    return rt;
                } 

    }

    public class TimeCheck
    {
        public int success { get; set; }
        public TimeResult result { get; set; }
    }
    public class TimeResult
    {
        public int timestamp { get; set; }
        public string datetime_1 { get; set; }
        public string datetime_2 { get; set; }
        public int week_1 { get; set; }
        public string week_2 { get; set; }
        public string week_3 { get; set; }
        public string week_4 { get; set; }
    }
}
