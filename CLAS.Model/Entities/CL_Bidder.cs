//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CLAS.Model.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class CL_Bidder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OpenId { get; set; }
        public string ActivationCode { get; set; }
        public System.DateTime ExpiredDate { get; set; }
        public string Creater { get; set; }
        public System.DateTime CreateDate { get; set; }
        public string Modifier { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}