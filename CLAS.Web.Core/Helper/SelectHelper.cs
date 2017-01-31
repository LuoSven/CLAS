using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CLAS.Web.Core.Helper
{
     public static class SelectHelper
    {



         public static SelectList GetEnumList(Enum em)
         {
             var Type = em.GetType();
             var list = new List<KeyValuePair<int, string>>();
             foreach (int key in Enum.GetValues(Type))
             {
                 string strName = Enum.ToObject(Type, key).GetEnumDescription();//获取名称
                 list.Add(new KeyValuePair<int, string>(key, strName));//添加到DropDownList控件
             }

            return new SelectList(list, "Key", "Value");
         } 
    }
}
