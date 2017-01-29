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
    public class LicenseRepo : RepositoryBase<CL_License>, ILicenseRepo
    {
        public LicenseRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }


        public List<LicenseDTO> GetList(LicenseSM sm)
        {
            var sql = @"select a.IDCard,a.Id,b.ActivationCode,a.Name,a.Code,a.Password,a.TracticsId,c.TacticsName,a.BidderId,b.Name BidderName,a.Note,d.SyncTime LastSyncTime,e.LoginDate LastActiveTime,b.IsFor51 from CL_License  a
join CL_Bidder b  on a.BidderId=b.Id
join CL_Tactics c on a.TracticsId=c.Id
left join ( select * from  (select * ,ROW_NUMBER()over(partition by BidderId order by SyncTime desc) rowNumber from CL_Sync_Record) t where rowNumber=1) d on a.BidderId=d.BidderId
left join ( select * from  (select * ,ROW_NUMBER()over(partition by BidderId order by LoginDate desc) rowNumber from CL_Bidder_Activite_Record) t where rowNumber=1) e on a.BidderId=e.BidderId
where 1=1";
            if (!string.IsNullOrEmpty(sm.ActivationCode))
            {
                sql += " and b.ActivationCode like '%'+@ActivationCode+'%'";
            }
            if (!string.IsNullOrEmpty(sm.Name))
            {
                sql += " and a.Name like '%'+@Name+'%'";
            }
            return DapperHelper.SqlQuery<LicenseDTO>(sql, sm).ToList();
        }

        public void AutoCreateActivationCode()
        {
            var list = GetList(new LicenseSM());
            
            foreach (var item in list)
            {
                var code = DateTime.Now.Ticks.ToString();
                code = code.Substring(code.Length - 2);
                item.ActivationCode = item.Code + code;
                DapperHelper.SqlExecute("update CL_Bidder set ActivationCode=@ActivationCode where Id=@BidderId", item);
            }
        }
 
    }
    public interface ILicenseRepo : IRepository<CL_License>
    {
        List<LicenseDTO> GetList(LicenseSM sm);

        void AutoCreateActivationCode();
    }
}
