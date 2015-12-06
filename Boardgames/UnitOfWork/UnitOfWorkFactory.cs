using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Reflection;
using System.Xml.Linq;

namespace Boardgames.UnitOfWork
{
    public static class UnitOfWorkFactory
    {
        public static IXmlUnitOfWork CreateUnitOfWork<TEntity>() where TEntity : class, new()
        {
            #region CreateUnitOfWork

            IXmlUnitOfWork unitOfWork;
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyClass = ConfigurationManager.AppSettings["UnitOfWork"];

            Type unitOfWorkType = assembly.GetType(assemblyClass);
            Type unitOfWorkWithGenerictype = unitOfWorkType.MakeGenericType(typeof(TEntity));
            unitOfWork = Activator.CreateInstance(unitOfWorkWithGenerictype) as IXmlUnitOfWork;

            return unitOfWork;

            #endregion
        }
    }
}