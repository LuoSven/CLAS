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
            var tacticsSql = @"select a.AddPrice,a.DownReducePrice,a.DelayTime, d.ExecuteInstructions TimeScript,c.ExecuteInstructions PriceScript,a.PriceScriptId,a.Id,a.SyncStopTimeBegin,a.SyncStopTimeStop from CL_Tactics a
join CL_License e on e.TracticsId =a.Id
join CL_Bidder b on b.Id=e.BidderId 
left join CL_Script c on c.Id=a.PriceScriptId
left join CL_Script d on d.Id=a.TimeScriptId 
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
                tacticsSql += @" and( a.Description like '%'+@KeyWords+'%' or a.TacticsName like '%'+@KeyWords+'%' )";
            }
            if (sm.Id.HasValue)
            {
                tacticsSql += @" and a.Id=@Id";
            }
            //获取策略
            var tacticses = DapperHelper.SqlQuery<TacticsVM>(tacticsSql, sm ).ToList();
            foreach (var tactics in tacticses)
            {
                //获取脚本
                var scripts = DapperHelper.SqlQuery<ScriptExecuteNameDTO>(@"select a.ScriptId,a.Id ,b.SciptName ,b.CreateDate ,b.ModifyDate ,a.Name,a.ExecuteTime, b.CheckInstruction, b.ExecuteInstructions,a.ExecuteCondition from  CL_Tactics_Script a
join CL_Script b on a.ScriptId=b.Id
where a.TacticsId=@Id", tactics);
                foreach (var script in scripts)
                {
                    tactics.Scripts.Add(new ScriptExecuteVM() {Id=script.Id,Name=script.Name, ExecuteCondition = script.ExecuteCondition, ExecuteTime = script.ExecuteTime,ScriptId=script.ScriptId, Script = new ScriptVM() { Id = script.ScriptId, ScriptName = script.SciptName, CreateDate = script.CreateDate, ModifyDate = script.ModifyDate, CheckInstruction = script.CheckInstruction, ExecuteExpressions = script.ExecuteInstructions } });
                }
            }
          
            return tacticses;
        }

        public TacticsVM GetTacticsVm(int id)
        {
            var sm=new TacticsSM()
            {
                Id=id
            };
            var result = GetTacticsVms(sm);
            return result.FirstOrDefault();
        }

        public void SaveTactics(TacticsVM model,string userName)
        {
            var entity = new CL_Tactics();
            var ids = model.Scripts.Select(o => o.Id).ToList();
            var deleted = DataContext.CL_Tactics_Script.Where(o => o.TacticsId == model.Id&&!ids.Contains(o.Id));
            foreach (var item in deleted)
            {
                DataContext.CL_Tactics_Script.Remove(item);
            }

            if (model.Id > 0)
            {
                entity = DataContext.CL_Tactics.Where(o => o.Id == model.Id).FirstOrDefault();
                if (entity == null)
                {
                    return;
                }
                entity.ModifyDate = DateTime.Now;
                entity.Modifier = userName;
            }
            else
            {
                entity.CreateDate = DateTime.Now;
                entity.Creater = userName;
                DataContext.CL_Tactics.Add(entity);
            }

            entity.Description = model.Description;
            entity.TacticsName = model.Description;
            entity.SyncStopTimeBegin = model.SyncStopTimeBegin;
            entity.SyncStopTimeStop = model.SyncStopTimeStop;
            DataContext.SaveChanges();
         
            foreach (var executeScript in model.Scripts)
            {
                SaveExecuteScripts(executeScript, entity.Id);
            }
        }

        private void SaveExecuteScripts(ScriptExecuteVM model,int tacticsId)
        {
            var  entity=new CL_Tactics_Script();
            if (model.Id > 0)
            {
                entity = DataContext.CL_Tactics_Script.Where(o => o.Id == model.Id).FirstOrDefault();
                if (entity == null)
                {
                    return;
                } 
            }
            else
            {
                entity.TacticsId = tacticsId;
                DataContext.CL_Tactics_Script.Add(entity);
            }


            entity.ExecuteTime = model.ExecuteTime;
            entity.ScriptId = model.ScriptId; ;
            entity.ExecuteCondition = model.ExecuteCondition;
            DataContext.SaveChanges();
        }


        public List<ListItem> GetList()
        {
            return DapperHelper.SqlQuery<ListItem>("select Id id,TacticsName name from CL_Tactics where Id!=1").ToList();
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



        /// <summary>
        /// 获取策略列表
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TacticsVM GetTacticsVm(int id);


        void SaveTactics(TacticsVM model,string userName);

        List<ListItem> GetList();
    }
}
