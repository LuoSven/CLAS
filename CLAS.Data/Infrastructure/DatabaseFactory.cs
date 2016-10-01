using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLAS.Model.Entities;

namespace CLAS.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private CLASEntities dataContext;
        public CLASEntities Get()
        {
            return dataContext ?? (dataContext = new CLASEntities());
        }
        protected override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
