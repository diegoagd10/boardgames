using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml.Linq;

namespace Boardgames.Repository
{
    public class XmlRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, new()
    {
        private readonly XDocument _document;
        private readonly string _entityElementName;
        private readonly string _primaryKeyElementName;

        public XmlRepository(XDocument document)
        {
            #region XmlRepository

            if (document == null)
                throw new ArgumentException("document");
            this._document = document;
            this._entityElementName = string.Format("{0}s", typeof(TEntity).Name);
            this._primaryKeyElementName = GetPrimaryKeyElementName();

            #endregion
        }

        #region Private Methods

        private string GetPrimaryKeyElementName()
        {
            #region GetPrimaryKeyElementName

            string elementName = string.Empty;
            PropertyInfo propertyKey = GetPrimaryKeyProperty();

            if (propertyKey != null)
                elementName = propertyKey.Name;
            return elementName;

            #endregion
        }

        private PropertyInfo GetPrimaryKeyProperty()
        {
            #region GetPrimaryKeyProperty

            PropertyInfo[] properties = typeof(TEntity).GetProperties();
            return properties.FirstOrDefault(p => p.Name.Contains("ID"));

            #endregion
        }

        private TKey GetPrimaryKeyValue(TEntity entity)
        {
            #region GetPrimaryKeyValue

            PropertyInfo propertyKey = GetPrimaryKeyProperty();
            TKey key = (TKey)Convert.ChangeType(propertyKey.GetValue(entity), typeof(TKey));
            
            return key;

            #endregion
        }

        private TEntity GetEntityByElement(XElement element)
        {
            #region GetEntityByElement

            object value;
            XAttribute attribute;
            TEntity entity = new TEntity();
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach(PropertyInfo property in properties)
            {
                attribute = element.Attribute(property.Name);
                value = Convert.ChangeType(attribute.Value, property.PropertyType);
                property.SetValue(entity, value);
            }
            return entity;

            #endregion
        }

        private XElement GetElementByEntity(TEntity entity)
        {
            #region GetElementByEntity

            XAttribute attribute;
            XElement element = new XElement(typeof(TEntity).Name);
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                attribute = new XAttribute(property.Name, property.GetValue(entity));
                element.Add(attribute);
            }
            return element;

            #endregion
        }

        private XElement GetElementByKey(TKey key)
        {
            #region GetElementByKey

            object objValue;
            string value = string.Empty;
            XElement elementFounded = null;

            foreach (XElement element in this._document.Element(this._entityElementName).Elements())
            {
                value = element.Attribute(this._primaryKeyElementName).Value;
                objValue = Convert.ChangeType(value, typeof(TKey));
                if (objValue.Equals(key))
                {
                    elementFounded = element;
                    break;
                }
            }
            return elementFounded;

            #endregion
        }

        private void UpdateElementByEntity(TEntity entity)
        {
            #region UpdateElementByEntity

            string value = string.Empty;
            TKey key = GetPrimaryKeyValue(entity);
            XElement element = GetElementByKey(key);
            PropertyInfo[] properties = typeof(TEntity).GetProperties();

            foreach (PropertyInfo property in properties)
            {
                element.SetAttributeValue(property.Name, property.GetValue(entity));
            }

            #endregion
        }

        private void DeleteElementByEntity(TEntity entity)
        {
            #region DeleteElementByEntity

            TKey key = GetPrimaryKeyValue(entity);
            XElement element = GetElementByKey(key);

            element.Remove();

            #endregion
        }

        #endregion

        #region Public Methods

        public TEntity GetByKey(TKey key)
        {
            #region GetByKey

            string value = string.Empty;
            XElement element = GetElementByKey(key);
            TEntity entity = element == null ? null : GetEntityByElement(element);
            
            return entity;

            #endregion
        }

        public IEnumerable<TEntity> GetAll()
        {
            #region GetAll

            TEntity entity;
            List<TEntity> entities = new List<TEntity>();

            foreach (XElement element in this._document.Element(this._entityElementName).Elements())
            {
                entity = GetEntityByElement(element);
                entities.Add(entity);
            }
            return entities;

            #endregion
        }

        public IEnumerable<TEntity> GetByFilter(Func<TEntity, bool> filter)
        {
            #region GetByFilter

            return GetAll().Where(filter);

            #endregion
        }

        public void Create(TEntity entity)
        {
            #region Create

            TKey key = GetPrimaryKeyValue(entity);
            XElement element = GetElementByEntity(entity);

            if (GetByKey(key) != null)
                throw new ArgumentException("Primary key violated.");
            this._document.Element(this._entityElementName).Add(element);

            #endregion
        }

        public void Update(TEntity entity)
        {
            #region Update

            TKey key = GetPrimaryKeyValue(entity);

            if (GetByKey(key) == null)
                throw new ArgumentException("Not found.");
            UpdateElementByEntity(entity);
            
            #endregion
        }

        public void Delete(TEntity entity)
        {
            #region Delete

            TKey key = GetPrimaryKeyValue(entity);

            if (GetByKey(key) == null)
                throw new ArgumentException("Not found.");
            DeleteElementByEntity(entity);

            #endregion
        }

        #endregion
    }
}