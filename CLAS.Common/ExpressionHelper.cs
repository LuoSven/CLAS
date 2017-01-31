using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLAS.Common
{
     public  class ExpressionHelper
    {

         public string ToChinese(string expression)
         {
             if (string.IsNullOrEmpty(expression))
             {
                 return string.Empty;
             }
             var replaceList=new Dictionary<string,string>()
             {
                 {"==", "等于"},
                 {">=", "大于等于"},
                 {"<=", "小于等于"},
                 {">", "大于"},
                 {"<", "小于"}, 
                 {"@keyDownCount", "输入数字数"},
                 {"@price", "当前价格"},
                 {"&&", "并且"},
             };

             foreach (var item in replaceList)
             {
                 expression = expression.Replace(item.Key, item.Value); 
             }

             return expression;

         }
    }
}
