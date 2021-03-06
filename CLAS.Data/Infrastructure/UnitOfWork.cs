﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CLAS.Model.Entities;

namespace CLAS.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDatabaseFactory databaseFactory;
        private CLASEntities dataContext;

        public event EventHandler<SavedEventArgs> Saved;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this.databaseFactory = databaseFactory;
        }

        protected CLASEntities DataContext
        {
            get { return dataContext ?? (dataContext = databaseFactory.Get()); }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }
         

         /// <summary>
        /// Raises the Saved event.
        /// </summary>
        /// <param name="businessObject">
        /// The business Object.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public void OnSaved(Object savedObject, SaveAction action)
        {
            if (Saved != null)
            {
                Saved(savedObject, new SavedEventArgs(action));
            }
        }
    }
    
}
