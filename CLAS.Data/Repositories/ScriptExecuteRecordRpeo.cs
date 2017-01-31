using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Model.Entities;
using CLAS.Data.Dapper;
using CLAS.Model;
using CLAS.Model.DTOs;
using CLAS.Model.SMs;
using CLAS.Model.TMs;
using CLAS.Model.VMs;

namespace CLAS.Data.Repositories
{
    public class ScriptExecuteRecordRpeo : RepositoryBase<CL_Script_Execute_Record>, IScriptExecuteRecordRpeo
    {
        public ScriptExecuteRecordRpeo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool AddByDapper(CL_Script_Execute_Record entity)
        {
                var result = DapperHelper.SqlExecute(@"INSERT INTO CL_Script_Execute_Record  VALUES
           (@ScriptId 
           ,@ExecuteTime 
           ,@ActualExecutionStartTime 
           ,@ActualExecutionEndTime 
           ,@Message 
           ,@IsSucceed 
           ,@BidderId 
           ,@ExecuteMSec
           ,@ExecuteConditionExpress)", entity);
                return result > 0;
            
          
        }


        public PagedResult<ScriptExecuteRecordDetailVM> GetList(ScriptSM sm,int page,int pageSize)
        {
            var sql = @"select a.ScriptId,a.BidderId,c.SciptName ScriptName,b.Name BidderName,a.Id,a.ExecuteTime,a.ActualExecutionEndTime,a.ActualExecutionStartTime,a.Message,a.IsSucceed,a.ExecuteMSec,a.ExecuteConditionExpress  from CL_Script_Execute_Record a
join CL_Bidder b on a.BidderId=b.Id
join CL_Script c on a.ScriptId=c.Id 
where 1=1 ";

            if (sm.ScriptId.HasValue)
            {
                sql += " and a.ScriptId =@ScriptId";
            }

            if (sm.BidderId.HasValue)
            {
                sql += " and a.BidderId =@BidderId";
            }
            if (sm.Express.HasValue)
            {
                sql += " and isnull(a.ExecuteConditionExpress,'')<>'' ";
            }
            return DapperHelper.QueryWithPage<ScriptExecuteRecordDetailVM>(sql, sm, "Id" +
                                                                                    " desc",
                page, pageSize);
        }

    }
    public interface IScriptExecuteRecordRpeo : IRepository<CL_Script_Execute_Record>
    {
        bool AddByDapper(CL_Script_Execute_Record entity);

        PagedResult<ScriptExecuteRecordDetailVM> GetList(ScriptSM sm, int page, int pageSize);
    }
}
