using CLAS.Model.TMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CLAS.Common;
using CLAS.Common.Json;
using CLAS.Data.Infrastructure;
using CLAS.Data.Repositories;
using CLAS.Model.DTOs;
using CLAS.Model.Entities;
using CLAS.Model.VMs;
using CLAS.Utils;

namespace CLAS.API.Controllers
{
    public class CommandController : ApiController
    {

        private readonly IScriptExecuteRecordRpeo scriptExecuteRecordRpeo = new ScriptExecuteRecordRpeo(new DatabaseFactory());
        private readonly IBidderKeyDownRecordRpeo bidderKeyDownRecordRpeo = new BidderKeyDownRecordRpeo(new DatabaseFactory());
        private readonly IBidderRepo bidderRepo = new BidderRepo(new DatabaseFactory());
        private readonly ITacticsRepo tacticsRepo = new TacticsRepo(new DatabaseFactory());
        private readonly IScriptRepo scriptRepo = new ScriptRepo(new DatabaseFactory());
        [HttpPost]
        public string Sync(string t=null)
        {
            var tm = new ServerReponseTM();
            try
            {
              
                if (string.IsNullOrEmpty(t))
                {
                    t = Request.Content.ReadAsStringAsync().Result;
                }
                var model = DESEncrypt.DecryptModel<ClientRequestTM>(t);
                 tm = new ServerReponseTM()
                {
                    SendTime = DateTime.Now
                };
                //验证失败直接返回空的
                var bidderId = bidderRepo.GetIdByActivationCode(model.ActivationCode);
                if (!bidderId.HasValue)
                {
                    return DESEncrypt.EncryptModel(tm);
                }
                var tactics = tacticsRepo.GetTacticsByActivationCodeAndUpdateTime(model.ActivationCode, model.TacticsLastUpdateTime);
                tm.Tactics = tactics;
                //策略不需要更新,更新脚本
                if (tactics == null && model.ScriptLastUpdateTime.HasValue)
                {
                    tm.IsFor51 = bidderRepo.GetIsFor51ById(bidderId.Value);
                    tm.CommandType = ServerCommandType.SynScript;
                    tm.Scripts = scriptRepo.GetScriptsByActivationCodeAndUpdateTime(model.ActivationCode,
                        model.ScriptLastUpdateTime.Value);
                }
                else if (tactics != null)
                { 
                    tm.CommandType = ServerCommandType.SynTactics;
                }

                //同步执行记录
                if (model.ScriptExecuteRecords != null)
                {

                    foreach (var record in model.ScriptExecuteRecords)
                    {
                        var entity = new CL_Script_Execute_Record
                        {
                            ScriptId = record.ScriptId,
                            BidderId = bidderId.Value,
                            ExecuteTime = record.ExecuteTime,
                            ActualExecutionStartTime = record.ActualExecutionStartTime,
                            ActualExecutionEndTime = record.ActualExecutionEndTime,
                            Message = record.Message,
                            IsSucceed = record.IsSucceed,
                            ExecuteMSec = record.ExecuteMSec,
                            ExecuteConditionExpress = record.ExecuteConditionExpress
                        };
                        scriptExecuteRecordRpeo.AddByDapper(entity);
                    }
                }



                //同步按键
                if (model.KeyDownRecords != null)
                {
                    foreach (var record in model.KeyDownRecords)
                    {
                        var entity = new KeyDownRecordDTO()
                        {
                            Key = record.Key,
                            BidderId = bidderId.Value,
                            KeyDownTime = record.KeyDownTime,
                            CreateTime = DateTime.Now,
                            IsEffictive=record.IsEffictive
                        };
                        bidderKeyDownRecordRpeo.AddByDapper(entity);
                    }
                }

                var syncRecord =string.Format("同步类型：{0},同步的策略数量：{1}，同步的脚本数量：{2}", tm.CommandType.GetEnumDescription(),tm.Tactics==null?"0":"1",tm.Scripts==null?0:tm.Scripts.Count);
                bidderRepo.AddSyncRecord(bidderId.Value, DateTime.Now, syncRecord);
            }
            catch (Exception ex)
            {
                
                throw;
            }
          
           
           
           var  result = DESEncrypt.EncryptModel(tm);
            return result;
        }
        [HttpGet]
        public string Login(string s)
        {
            var result = "false";
            var loginVm = DESEncrypt.DecryptModel < ActivationVM>(s);
           var bidderName= bidderRepo.Login(loginVm.ActivationCode, loginVm.SystemInfo);
            if (!string.IsNullOrEmpty(bidderName))
            {
                var bidderId = bidderRepo.GetIdByActivationCode(loginVm.ActivationCode);
                var isFor51 = bidderRepo.GetIsFor51ById(bidderId.Value);
                result= bidderName+","+(isFor51 ? "1" : "0");
            }
            return DESEncrypt.Encrypt(result);

        }
    }
}
