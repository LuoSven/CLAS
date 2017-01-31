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
    public class ScriptRepo : RepositoryBase<CL_Script>, IScriptRepo
    {
        public ScriptRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public List<ScriptExecuteTM> GetScriptsByActivationCodeAndUpdateTime(string activationCode, DateTime updateTime)
        {
            var dic = new List<ScriptExecuteTM>();
            var scripts =
                DapperHelper.SqlQuery<ScriptExecuteDTO>(@"select  a.ExecuteTime, b.CheckInstruction, b.ExecuteInstructions,b.ExecuteCondition from  CL_Tactics_Script a
join CL_Script b on a.ScriptId=b.Id
join CL_Bidder c on a.TacticsId=c.TracticsId
where c.ActivationCode=@activationCode and(b.ModifyDate>=@updateTime or b.CreateDate>=@updateTime)",
                    new {activationCode, updateTime});
            foreach (var script in scripts)
            {
                dic.Add(new ScriptExecuteTM(){ExecuteCondition = script.ExecuteCondition,ExecuteTime = script.ExecuteTime,Script = new ScriptTM() {Id=script.ScriptId, CheckInstruction = script.CheckInstruction, ExecuteExpressions = script.ExecuteInstructions }});
            }
            return dic;
        }

        public List<ListItem> GetScriptListItems()
        {
            return DapperHelper.SqlQuery<ListItem>("select Id id,SciptName name from CL_Script ").ToList();
        }

        public List<CL_Script> GetList(ScriptSM sm)
        {
            var sql = "select * from CL_Script ";

            return DapperHelper.SqlQuery<CL_Script>(sql, sm).ToList();
        }
    }
    public interface IScriptRepo : IRepository<CL_Script>
    {
        /// <summary>
        /// 获取更新过的脚本
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="updateTime"></param>
        /// <returns></returns>
        List<ScriptExecuteTM> GetScriptsByActivationCodeAndUpdateTime(string activationCode, DateTime updateTime);

        /// <summary>
        /// 获取所有脚本的下拉
        /// </summary>
        /// <returns></returns>
        List<ListItem> GetScriptListItems();

        /// <summary>
        /// 获取脚本列表
        /// </summary>
        /// <param name="sm"></param> 
        /// <returns></returns>
        List<CL_Script> GetList(ScriptSM sm);

    }
}
