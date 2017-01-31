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
    public class BidderScreenCutRpeo : RepositoryBase<CL_Bidder_ScreenCut>, IBidderScreenCutRpeo
    {
        public BidderScreenCutRpeo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public bool AddByDapper(CL_Bidder_ScreenCut entity)
        {

            var result = DapperHelper.SqlExecute(@"INSERT INTO CL_Bidder_ScreenCut  VALUES
           (@BidderId
           ,@FilePath
           ,@FileName
           ,@UploadTime
           ,@CreateTime)", entity);
                return result > 0;
            
          
        }

        public List<BidderScreenCutGroupDTO> GetList(ScreenCutRecordSM sm)
        {
            var sql = "select * from CL_Bidder_ScreenCut";
            if (!string.IsNullOrEmpty(sm.BidderId))
            {
                sql += " and BidderId=@BidderId";
            }

            if (sm.StartTime.HasValue)
            {
                sql += " and UploadTime>=@StartTime";
            }

            if (sm.EndTime.HasValue)
            {
                sql += " and UploadTime<=@EndTime";
            }
            sql += " order by id ";
            DapperHelper.SqlQuery<CL_Bidder_ScreenCut>(sql);



            return new List<BidderScreenCutGroupDTO>();

        }

    

    }
    public interface IBidderScreenCutRpeo : IRepository<CL_Bidder_ScreenCut>
    {
        bool AddByDapper(CL_Bidder_ScreenCut entity);


        List<BidderScreenCutGroupDTO> GetList(ScreenCutRecordSM sm);
    }
}
