using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Boardgames.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}