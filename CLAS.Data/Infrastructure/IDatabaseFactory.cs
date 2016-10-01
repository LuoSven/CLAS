using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CLAS.Model.Entities;

namespace CLAS.Data.Infrastructure
{
    public interface IDatabaseFactory : IDisposable
    {
        CLASEntities Get();
    }
}
