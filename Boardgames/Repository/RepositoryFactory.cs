using Boardgames.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Boardgames.Repository
{
    public static class RepositoryFactory
    {
        public static TIRepository CreateRepository<TIRepository, TRepository>(IXmlUnitOfWork unitOfWork)
        {
            #region CreateRepository

            TIRepository repository =
                (TIRepository)Activator.CreateInstance(typeof(TRepository), unitOfWork.GetDocument());
            return repository;

            #endregion
        }
    }
}