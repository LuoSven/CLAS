using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Data.Infrastructure;
using CLAS.Model.Entities;
using CLAS.Data.Dapper;
using CLAS.Model;
using CLAS.Model.VMs;

namespace CLAS.Data.Repositories
{
    public class SystemProgromRepo : RepositoryBase<CL_System_Program>, ISystemProgromRepo
    {
        public SystemProgromRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public int AddOrUpdateProgram(CL_System_Program program)
        {
            var source =
                DapperHelper.SqlQuery<int?>(
                    "select id from CL_System_Program where ActionName=@ActionName and ControllerName=@ControllerName",
                    program).FirstOrDefault();
            if(!source.HasValue)
            {
                program.ModifeTime = DateTime.Now;
                program.CreateTime = DateTime.Now;
                DapperHelper.SqlExecute(@"INSERT INTO  CL_System_Program
     VALUES
           (@ControllerName 
           ,@ActionName 
           ,@ActionDescription 
           ,@ControllerDescription 
           ,@RightType 
           ,@ParentAction 
           ,@ModifeTime 
           ,@CreateTime )", program);
               DataContext.SaveChanges();  
               return 2;
            }
            else
            {
                program.ModifeTime = DateTime.Now;
                program.Id = source.Value;
              var result=  DapperHelper.SqlExecute(@"UPDATE [CLAS].[dbo].[CL_System_Program]
   SET  [ActionDescription] = @ActionDescription 
      ,[ControllerDescription] = @ControllerDescription 
      ,[RightType] = @RightType 
      ,[ParentAction] = @ParentAction 
      ,[ModifeTime] = @ModifeTime
 WHERE  Id=@Id", program);

               if (result > 1)
                return 3;
                return 4;
            }
            

        }


        public List<MenuVM> GetMenu()
        {
            var menuList = new List<MenuVM>();
            var programList = DapperHelper.SqlQuery<CL_System_Program>(@"select a.ControllerName,a.ControllerDescription,a.ActionName,a.ActionDescription,a.Id from CL_System_Program a   ");

            var controls = programList.Select(o => new Tuple<string, string>(o.ControllerName, o.ControllerDescription)).Distinct();
            foreach (var Control in controls)
            {
                var menu = new MenuVM();
                menu.ProgramId = Control.Item1;
                menu.Name = Control.Item2;
                menu.Items = programList.Where(o => o.ControllerName == Control.Item1).Select(o => new MenuVM()
                {
                    ProgramId = o.ControllerName + "_" + o.ActionName,
                    Url = o.ControllerName + "/" + o.ActionName,
                    Name = o.ActionDescription,
                }).ToList();
                menuList.Add(menu);
            }
            return menuList;
        }

       
        public void DeleteAllProgram()
        {
            DapperHelper.SqlExecute(@"delete from CL_System_Program");
        }
        public  bool IsNeedRight(string ActionName, string ControlName,int SystemTypeId=0)
        {
            var sql="select Id from CL_System_Program where ControllerName=@ControlName and ActionName=@ActionName";
            if(SystemTypeId!=0)
                sql+=" and  SystemType=@SystemType";
            var result = DapperHelper.SqlQuery<int>(sql, new { ControlName = ControlName, ActionName = ActionName, SystemType = SystemTypeId }).Any();
            return result;
        }
    

    }
    public interface ISystemProgromRepo : IRepository<CL_System_Program>
    {

       int AddOrUpdateProgram(CL_System_Program program);

       List<MenuVM> GetMenu();

       bool IsNeedRight(string ActionName, string ControlName,int SystemTypeId=0);
       void DeleteAllProgram();
    }
}
