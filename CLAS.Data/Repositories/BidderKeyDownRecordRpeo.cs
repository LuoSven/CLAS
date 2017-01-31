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
    public class BidderKeyDownRecordRpeo : RepositoryBase<CL_Bidder_KeyDownRecord>, IBidderKeyDownRecordRpeo
    {
        public BidderKeyDownRecordRpeo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool AddByDapper(KeyDownRecordDTO entity)
        {
            var isAdded =
                DapperHelper.SqlQuery<int>(
                    "select id from CL_Bidder_KeyDownRecord where BidderId=@BidderId and [Key]=@Key and KeyDownTime=@KeyDownTime", entity)
                    .Any();
            if (isAdded)
            {
                return true;
            }
            var result = DapperHelper.SqlExecute(@"INSERT INTO CL_Bidder_KeyDownRecord  VALUES
           (@BidderId
           ,@Key
           ,@KeyDownTime
           ,@CreateTime
           ,@IsEffictive)", entity);
                return result > 0;
            
          
        }

        public List<KeyDownRecordGroupVM> GetGroupedList(KeyDownRecordSM sm)
        {
            var result = new List<KeyDownRecordGroupVM>();
            var sql2 = "select * from CL_Bidder_KeyDownRecord where 1=1";
            if (!string.IsNullOrEmpty(sm.BidderId))
            {
                sql2 += " and BidderId=@BidderId";
            }
             
            if (sm.StartTime.HasValue)
            {
                sql2 += " and KeyDownTime>=@StartTime";
            }

            if (sm.EndTime.HasValue)
            {
                sql2 += " and KeyDownTime<=@EndTime";
            }
            sql2 += " order by id ";
            var list = DapperHelper.SqlQuery<KeyDownRecordGroupDetailVM>(sql2, sm).ToList();
            var bidderIds = list.Select(o => o.BidderId).Distinct();
            foreach (var bidderId in bidderIds)
            {
                var bidderName =
                    DapperHelper.SqlQuery<string>("select Name from CL_Bidder where Id=@bidderId", new {bidderId})
                        .FirstOrDefault();
                var details = list.Where(o => o.BidderId == bidderId).ToList();
                result.Add(new KeyDownRecordGroupVM()
                {
                    BidderId = bidderId,
                    BidderName = bidderName,
                    Details = details
                });
            }
            return result;
        }
    

    }
    public interface IBidderKeyDownRecordRpeo : IRepository<CL_Bidder_KeyDownRecord>
    {
        bool AddByDapper(KeyDownRecordDTO entity);


        /// <summary>
        /// 按拍手分组的按键记录
        /// </summary>
        /// <param name="sm"></param>
        /// <returns></returns>
        List<KeyDownRecordGroupVM> GetGroupedList(KeyDownRecordSM sm);
    }
}
