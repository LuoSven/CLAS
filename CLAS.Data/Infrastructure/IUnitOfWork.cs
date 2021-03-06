﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CLAS.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit(); 


        /// <summary>
        ///     Occurs when the class is Saved
        /// </summary>
        event EventHandler<SavedEventArgs> Saved;

        void OnSaved(Object obj, SaveAction action);
    }
}
