using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Data.Infrastructure;
using CLAS.Model.Entities;
using CLAS.Data.Dapper;
using CLAS.Model;

namespace CLAS.Data.Repositories
{
    public class ScriptRepo : RepositoryBase<CL_Script>, IScriptRepo
    {
        public ScriptRepo(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }
         

    }
    public interface IScriptRepo : IRepository<CL_Script>
    { 


    }
}
