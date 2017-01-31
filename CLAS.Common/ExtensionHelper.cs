using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.UI;
using Microsoft.JScript;
using Microsoft.JScript.Vsa;

namespace CLAS.Common
{
    public static class ExtensionHelper
    {
        /// <summary>
        /// 获取枚举值的详细文本
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetEnumDescription(this object e)
        {

            //object objDesc = DataCache.GetCache(e.ToString());
            //if (objDesc != null)
            //{
            //    return objDesc.ToString();//获得缓存
            //}
            //获取字段信息
            FieldInfo[] ms = e.GetType().GetFields();

            Type t = e.GetType();
            foreach (FieldInfo f in ms)
            {
                //判断名称是否相等
                if (f.Name != e.ToString()) continue;

                //反射出自定义属性
                foreach (Attribute attr in f.GetCustomAttributes(true))
                {
                    //类型转换找到一个Description，用Description作为成员名称
                    DescriptionAttribute dscript = attr as DescriptionAttribute;
                    if (dscript != null)
                    {
                        //DataCache.SetCache(e.ToString(), dscript.Description);//设置缓存
                        return dscript.Description;
                    }
                }
            }
            //如果没有检测到合适的注释，则用默认名称
            return e.ToString();
        }

        public static List<int> ToInts(this string e,char split=',')
        {
            if (string.IsNullOrEmpty(e))
                return new List<int>();
            var Ids = new List<int>();
            foreach (var id in e.Split(','))
            {
                var tempId = 0;
                int.TryParse(id, out tempId);
                Ids.Add(tempId);
            }
            return Ids;
        }
        public static int ToInt(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return 0;
            var Id = 0;
            int.TryParse(e, out Id);
            return Id;
        }

        public static string GetString(this List<int> e)
        {
            var result = "";
            foreach (var charInt in e)
            {
                result += (char) charInt;
            }
            return result;
        }
        /// <summary>
        /// 存在并且为true=true
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool Be(this bool? e)
        {
            return e.HasValue && e.Value;
        }        
        public static double ToDouble(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return 0;
            double id = 0;
            double.TryParse(e, out id);
            return id;
        }
        /// <summary>
        /// 添加引号
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string AddQuotes(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return "\"\"";
            return "\"" +e+ "\"";
        }

        /// <summary>
        /// 作为Js执行
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static object ExcuteAsJs(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return ""; 
            var ve = VsaEngine.CreateEngine();
            var c = Eval.JScriptEvaluate(e, ve);
            return c;
        }
        /// <summary>
        /// 获取以括号开始结束的多个字段
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static List<string> SplitByBrackets(this string e)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(e))
                return result;
            var pattern = @"\(.*?\)";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(e);
            foreach (Match match in matches)
            {
                result.Add(match.Value.Trim('(', ')'));
            }
            return result;
        }

        /// <summary>
        /// 获取以大括号开始结束的多个字段
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static List<string> SplitByBraces(this string e)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(e))
                return result;
            var pattern = @"\{.*?})";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var matches = regex.Matches(e);
            foreach (Match match in matches)
            {
                result.Add(match.Value.Trim('{', '}'));
            }
            return result;
        }

        /// <summary>
        /// 是否是int类型数据
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool IsInt(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return false;
            var id = 0;
            return int.TryParse(e, out id); 
        }
        public static Decimal ToDecimal(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return 0;
            Decimal Id = 0;
            Decimal.TryParse(e, out Id);
            return Id;
        }
        /// <summary>
        /// 获取最近一次第三周的周六
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static DateTime GetLastThirdWeekSaturday(this DateTime time)
        { 
            var totleCount = 0;
            var firstDayOfMonth = new DateTime(time.Year, time.Month, 1);
            var days = DateTime.DaysInMonth(time.Year, time.Month);
            for (var i = 0; i < days; i++)
            {
                var day = firstDayOfMonth.AddDays(i);
                if (day.DayOfWeek == DayOfWeek.Saturday)
                {
                    totleCount++;
                }

                if (day > DateTime.Now)
                {
                    break;
                }
                if (totleCount == 2)
                {
                    return day;
                }

            }
           
            //本月没到第三个周六，返回带入值的上个月，继续完成

            return time.AddMonths(-1).GetLastThirdWeekSaturday();

        }
        /// <summary>
        /// 修改DateTime的时间
        /// </summary>
        /// <param name="time">22:23:21</param>
        /// <returns></returns>
        public static DateTime UpdateTime(this DateTime e,string time)
        {
            var timeString = e.ToString("yyyy-MM-dd ")+time;
            if (DateTime.TryParse(timeString,out e))
            {
                return e;
            }
            throw new Exception("输入的格式有误");
        }
        public static DateTime ToDateTime(this string e)
        {
            if (string.IsNullOrEmpty(e))
                return new DateTime();
            var date =new DateTime();
            DateTime.TryParse(e, out date);
            return date;
        }
        /// <summary>
        /// 获取月份名称，碰到1月就会返回年，主要是下拉和报表用
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string GetMonthName(this DateTime e)
        {
            var MonthName="";
            if (e.Month != 1)
            {
                MonthName = e.Month.ToString() + "月";
            }
            else
            {
                MonthName = e.Year + "年" + e.Month.ToString() + "月";
            }
            return MonthName;
        }
        public static List<KeyValuePair<int, string>> GetEnumList(this Enum em)
        {
            var Type = em.GetType();
            var list = new List<KeyValuePair<int, string>>();
            foreach (int key in Enum.GetValues(Type))
            {
                string strName = Enum.ToObject(Type, key).GetEnumDescription();//获取名称
                list.Add(new KeyValuePair<int,string>(key,strName));//添加到DropDownList控件
            }
            return list;
        }
        public static List< string> GetNames(this Enum em,string otherValue="")
        {
            var Type = em.GetType();
            var list = new List<string>();
            foreach (int key in Enum.GetValues(Type))
            {
                string strName = Enum.ToObject(Type, key).GetEnumDescription();//获取名称
                list.Add(strName + otherValue);//添加到DropDownList控件
            }
            return list;
        }
        public static List<int> GetIds(this Enum em)
        {
            var Type = em.GetType();
            var list = new List<int>();
            foreach (int key in Enum.GetValues(Type))
            { 
                list.Add(key);//添加到DropDownList控件
            }
            return list;
        }
        public static void BindYear(this DropDownList ddl)
        {
            ddl.Items.Clear();
            int intNowYear = DateTime.Today.Year;
            for (int i = 0; i < 60; i++)
            {
                string tempYear = (intNowYear - i).ToString();
                ddl.Items.Add(new ListItem(tempYear, tempYear));
            }
            ddl.Items.Insert(0, new ListItem("年", "0"));
            ddl.SelectedIndex = 0;
        }

        public static void BindMonth(this DropDownList ddl)
        {
            ddl.Items.Clear();
            for (int i = 0; i < 12; i++)
            {
                string tempMonth = (i + 1).ToString();
                ddl.Items.Add(new ListItem(tempMonth, tempMonth));
            }
            ddl.Items.Insert(0, new ListItem("月", "0"));
            ddl.SelectedIndex = 0;
        }

        public static void BindDay(this DropDownList ddl)
        {
            ddl.Items.Clear();
            for (int i = 0; i < 31; i++)
            {
                string tempDay = (i + 1).ToString();
                ddl.Items.Add(new ListItem(tempDay, tempDay));
            }
            ddl.Items.Insert(0, new ListItem("日", "0"));
            ddl.SelectedIndex = 0;
        }
        /// <summary>
        /// 多于5个字自动省略
        /// </summary>
        /// <param name="e"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string Omit(this string e, int maxLength = 20)
        {
            if (string.IsNullOrWhiteSpace(e))
                return string.Empty;
            var str = e;
            str = str.Length > maxLength ? str.Substring(0, maxLength) + "..." : str;
            return str;
        }

        /// <summary>
        /// 自动格式化
        /// </summary>
        /// <param name="e"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string ToYMdHms(this DateTime? e)
        {
            if (e.HasValue)
            {
                return e.Value.ToString("yyyy/MM/dd HH:mm:ss");
            }
            return string.Empty;
        }

        /// <summary>
        /// 自动格式化
        /// </summary>
        /// <param name="e"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string ToYMdHms(this DateTime e)
        {
          
                return e.ToString("yyyy/MM/dd HH:mm:ss");
        }

        /// <summary>
        /// 替换空值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetIfEmtpy<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return;
            if (dic.ContainsKey(key))
            {
                var ob = dic[key];
                if (ob == null || string.IsNullOrEmpty(ob.ToString()))
                {
                    dic[key] = value;
                }
            }
            else
            {
                dic.Add(key, value);
            }
        }


        /// <summary>
        /// 替换空值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<TKey, TValue>(this Dictionary<TKey, TValue> dic, TKey key, TValue value)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
                return;
            if (dic.ContainsKey(key))
            {
                var ob = dic[key]; 
                    dic[key] = value; 
            }
            else
            {
                dic.Add(key, value);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param> 
        public static void RemovesIfHave<TKey, TValue>(this Dictionary<TKey, TValue> dic, List<TKey> keys)
        {
            foreach (var key in keys)
            {
                if (dic.ContainsKey(key))
                {
                    dic.Remove(key);
                }
            }
        }
        public static int CharToAsc(this char e)
        {
            var character = e.ToString();
            if (character.Length == 1)
            {
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                int intAsciiCode = (int)asciiEncoding.GetBytes(character)[0];
                return (intAsciiCode);
            }
            else
            {
                throw new Exception("Character is not valid.");
            }

        }


        public static string ToString(this DateTime? e)
        {
            if (e.HasValue)
            {
                return e.Value.ToString("yyyy-MM-dd HH:mm:ss");
            }
            return string.Empty;
        }


        public static string ToTime(this DateTime? e)
        {
            if (e.HasValue)
            {
                return e.Value.ToString("HH:mm:ss");
            }
            return string.Empty;
        }

        public static string ToTime(this DateTime e)
        {
          
                return e.ToString("HH:mm:ss");
          
        }
    }
}
