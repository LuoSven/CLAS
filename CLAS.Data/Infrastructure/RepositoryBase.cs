using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data;
using System.Linq.Expressions;
using CLAS.Data;
using CLAS.Model.Entities;
using System.Threading.Tasks;
using System.Data.Entity.Validation;
namespace CLAS.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        private CLASEntities dataContext;
        private readonly DbSet<T> dbset;

        protected RepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbset = DataContext.Set<T>();
        }

        protected IDatabaseFactory DatabaseFactory
        {
            get;
            private set;
        }

        protected CLASEntities DataContext
        {
            get { return dataContext ?? (dataContext = DatabaseFactory.Get()); }
        }
        public virtual void Add(T entity)
        {
            dbset.Add(entity);
            dataContext.SaveChanges();

        }
        //新增方法
        public virtual void AddAll(IEnumerable<T> entities)
        {
            dbset.AddRange(entities);
            dataContext.SaveChanges();

        }
        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }
        //新增方法
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T obj in entities)
            {
                dbset.Attach(obj);
                dataContext.Entry(obj).State = EntityState.Modified;
                dataContext.SaveChanges();

            }
        }



        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);
            dataContext.SaveChanges();

        }
        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbset.Where<T>(where).AsEnumerable();
            dbset.RemoveRange(objects);
            //foreach (T obj in objects)
            //    dbset.Remove(obj);
        }
        //新增方法
        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }


        public virtual void Clear()
        {
            //尚未实现
        }

        public virtual T GetById(long id)
        {
            return dbset.Find(id);
        }
        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return dbset.ToList();
        }
        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).ToList();
        }
        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetAllLazy()
        {
            return dbset;
        }

        public virtual int SaveChanges()
        {
            try
            {
                return dataContext.SaveChanges();
                // 写数据库
            }
            catch (DbEntityValidationException dbEx)
            {
                throw dbEx;
            }

        }


    }
}
