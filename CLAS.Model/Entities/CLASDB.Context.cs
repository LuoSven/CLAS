﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CLASEntities : DbContext
    {
        public CLASEntities()
            : base("name=CLASEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CL_Bidder> CL_Bidder { get; set; }
        public virtual DbSet<CL_Script> CL_Script { get; set; }
        public virtual DbSet<CL_Script_Instruction> CL_Script_Instruction { get; set; }
        public virtual DbSet<CL_Tactics> CL_Tactics { get; set; }
        public virtual DbSet<CL_Tactics_Script> CL_Tactics_Script { get; set; }
        public virtual DbSet<CL_User> CL_User { get; set; }
    }
}
