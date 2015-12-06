using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Boardgames.UnitOfWork
{
    public class XmlUnitOfWork<TEntity> : IUnitOfWork, IXmlUnitOfWork where TEntity : class, new()
    {
        private XDocument _document;
        private string _documentPath;
        private string _directoryPath;
        private string _entityElementName;

        public XmlUnitOfWork()
        {
            #region XmlUnitOfWork

            this._entityElementName = string.Format("{0}s", typeof(TEntity).Name);
            this._directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data");
            this._documentPath = Path.Combine(this._directoryPath, string.Format("{0}.xml", typeof(TEntity).Name));
            
            this.CreateIfNotExists();

            #endregion
        }

        #region Private methods

        private void CreateIfNotExists()
        {
            #region CreateIfNotExists

            if (!Directory.Exists(this._directoryPath))
                Directory.CreateDirectory(this._directoryPath);
            
            if(!File.Exists(this._documentPath))
            {
                _document = new XDocument(
                    new XDeclaration("1.0", "UTF-8", "true"),
                    new XElement(this._entityElementName)
                );
                this.SaveChanges();
                return;
            }

            _document = XDocument.Load(this._documentPath);

            #endregion
        }

        #endregion

        #region Public methos

        public void SaveChanges()
        {
            #region SaveChanges

            this._document.Save(this._documentPath);

            #endregion
        }

        public void Dispose()
        {
            #region Dispose

            this._document = null;

            #endregion
        }

        public XDocument GetDocument()
        {
            #region GetDocument

            return this._document;

            #endregion
        }

        #endregion
    }
}