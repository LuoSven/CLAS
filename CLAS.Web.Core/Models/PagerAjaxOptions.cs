using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Ajax;

namespace CLAS.Web.Core
{
    public class PagerAjaxOptions : AjaxOptions
    {
        public string SearchFormId { get; set; }
    }
}
