using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Common;
using CLAS.Data.Infrastructure;
using CLAS.Model.Entities;
using CLAS.Data.Dapper;
using CLAS.Model;
using CLAS.Model.VMs;

namespace CLAS.Data.Repositories
{
    /// <summary>
    /// 拍手Repo
    /// </summary>
    public class BidderRepo : RepositoryBase<CL_Bidder>, IBidderRepo
    {
        public BidderRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        /// <summary>
        /// 拍手登陆
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        public   string Login(string activationCode, string systemInfo)
        {
            var now = DateTime.Now;
            var bidder = DapperHelper.SqlQuery<CL_Bidder>("select Id,Name from  CL_Bidder  where ActivationCode=@activationCode ", new { activationCode, systemInfo }).FirstOrDefault();
            if (bidder==null)
            {
                return string.Empty;
            }
            DapperHelper.SqlExecute("update CL_Bidder set SystemInfo =@systemInfo,LastActiveDate=@now  where ActivationCode=@activationCode ", new { activationCode, systemInfo, now });
            DapperHelper.SqlExecute("insert into CL_Bidder_Activite_Record values (@id,@now,@systemInfo)", new { id = bidder.Id, now, systemInfo });
             return bidder.Name;;
        }

        /// <summary>
        /// 拍手登陆
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        public  int? GetIdByActivationCode(string activationCode)
        {
            var result = DapperHelper.SqlQuery<int?>("select id from  CL_Bidder  where ActivationCode=@activationCode ",
                new {activationCode}).FirstOrDefault();
            return result;
        }

        public List<ListItem> GetList()
        {
          return DapperHelper.SqlQuery<ListItem>("select Id id,Name name from CL_Bidder where Id!=1").ToList();
        }

        public void AddSyncRecord(int bidderId, DateTime syncTime,string info)
        {
            DapperHelper.SqlExecute("insert into CL_Sync_Record values(@bidderId,@syncTime,0,@info)",
                new {bidderId, syncTime, info});
        }

        public bool GetIsFor51ById(int id)
        {
            var isFor51 = DapperHelper.SqlQuery<bool?>("select IsFor51   from CL_Bidder where Id=@id",new{id}).FirstOrDefault();
            return isFor51.Be();
        }

        public void UpdateIsFor51(bool isFor51)
        {
            DapperHelper.SqlExecute("update CL_Bidder set  IsFor51=@isFor51",
                new { isFor51,  });
        }
    }
    public interface IBidderRepo : IRepository<CL_Bidder>
    {
        /// <summary>
        /// 拍手登陆
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        string Login(string activationCode, string systemInfo);

        /// <summary>
        /// 拍手登陆
        /// </summary>
        /// <param name="activationCode"></param>
        /// <param name="systemInfo"></param>
        /// <returns></returns>
        int? GetIdByActivationCode(string activationCode);
        /// <summary>
        /// 是否是51模拟版
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool GetIsFor51ById(int id);


        List<ListItem> GetList();


        void AddSyncRecord(int bidderId, DateTime syncTime, string info);

        /// <summary>
        /// 更新是否是51测试版
        /// </summary>
        void UpdateIsFor51(bool isFor51);
    }
}
