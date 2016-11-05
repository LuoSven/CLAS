using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// <summary>
    /// 拍手Repo
    /// </summary>
    public class TacticsRepo : RepositoryBase<CL_Tactics>, ITacticsRepo
    {
        public TacticsRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }






        public TacticsTM GetTacticsByActivationCodeAndUpdateTime(string activationCode, DateTime? updateTime)
        {
            var tacticsSql = @"select a.Id,a.SyncStopTimeBegin,a.SyncStopTimeStop from CL_Tactics a
join CL_Bidder b on b.TracticsId=a.Id
where b.ActivationCode=@activationCode";
            if (updateTime.HasValue)
            {
                tacticsSql += @" and( a.ModifyDate>=@updateTime
                                 or a.CreateDate>=@updateTime)";
            }
            //获取策略
            var tactics =
                DapperHelper.SqlQuery<TacticsTM>(tacticsSql, new { updateTime, activationCode }).FirstOrDefault();
            if (tactics == null)
            {
                return null;
            }
            //获取脚本
            var scripts = DapperHelper.SqlQuery<ScriptExecuteDTO>(@"select a.ScriptId ,a.ExecuteTime, b.CheckInstruction, b.ExecuteInstructions,a.ExecuteCondition from  CL_Tactics_Script a
join CL_Script b on a.ScriptId=b.Id
where a.TacticsId=@Id", tactics);
            foreach (var script in scripts)
            {
                tactics.Scripts.Add(new ScriptExecuteTM(){ExecuteCondition = script.ExecuteCondition,ExecuteTime = script.ExecuteTime,Script = new ScriptTM() {Id=script.ScriptId, CheckInstruction = script.CheckInstruction, ExecuteExpressions = script.ExecuteInstructions }});
            }
            return tactics;

        }

        public List<TacticsVM> GetTacticsVms(TacticsSM sm)
        {
            var tacticsSql = @"select a.* from CL_Tactics a
where 1=1";
            if (!string.IsNullOrEmpty(sm.KeyWords))
            {
                tacticsSql += @" and( a.Description like '%'+@KeyWords+'%' or a.TacticsName like '%'+@KeyWords+'%'";
            }
            //获取策略
            var tacticses = DapperHelper.SqlQuery<TacticsVM>(tacticsSql, new { sm }).ToList();
            foreach (var tactics in tacticses)
            {
                //获取脚本
                var scripts = DapperHelper.SqlQuery<ScriptExecuteNameDTO>(@"select b.id ,b.SciptName ,b.CreateDate ,b.ModifyDate ,a.ExecuteTime, b.CheckInstruction, b.ExecuteInstructions,a.ExecuteCondition from  CL_Tactics_Script a
join CL_Script b on a.ScriptId=b.Id
where a.TacticsId=@Id", tactics);
                foreach (var script in scripts)
                {
                    tactics.Scripts.Add(new ScriptExecuteVM() { ExecuteCondition = script.ExecuteCondition, ExecuteTime = script.ExecuteTime, Script = new ScriptVM() { Id = script.ScriptId, SciptName = script.SciptName, CreateDate = script.CreateDate, ModifyDate = script.ModifyDate, CheckInstruction = script.CheckInstruction, ExecuteExpressions = script.ExecuteInstructions } });
                }
            }
          
            return tacticses;
        }
    }
    public interface ITacticsRepo : IRepository<CL_Tactics>
    {
        /// <summary>
        /// 获取策略
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        TacticsTM GetTacticsByActivationCodeAndUpdateTime(string activationCode, DateTime? updateTime);
        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        List<TacticsVM> GetTacticsVms(TacticsSM sm);
    }
}
