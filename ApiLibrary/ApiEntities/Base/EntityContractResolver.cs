using ApiLibrary.ApiEntities.Attributes;
using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace ApiLibrary.ApiEntities.Base
{
    public class EntityContractResolver: DefaultContractResolver
    {
        private string _mapTo = null;

        public EntityContractResolver(String MappedJsonPropertyName)
        {
            if (String.IsNullOrWhiteSpace(MappedJsonPropertyName))
            {
                throw new ArgumentNullException(nameof(MappedJsonPropertyName));
            }

            _mapTo = MappedJsonPropertyName;
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName == nameof(Answer<IEntity>.Data))
            {
                return _mapTo;
            }
            return base.ResolvePropertyName(propertyName);
        }
    }
    public class EntityContractResolver<TEntity>: DefaultContractResolver where TEntity : class
    {
        private string _mapTo = null;

        public EntityContractResolver()
        {
            Type entityType = typeof(TEntity);
            _mapTo = entityType.Name.ToLowerInvariant();

            bool isCollection = false;
            if (entityType.IsArray)
            {
                entityType = entityType.GetElementType();
                isCollection = true;
            }
            else if (typeof(System.Collections.IEnumerable).GetTypeInfo().IsAssignableFrom(entityType.GetTypeInfo())
                && entityType.GetTypeInfo().IsGenericType && entityType.GenericTypeArguments.Length > 0)
            {
                entityType = entityType.GenericTypeArguments[0];
                isCollection = true;
            }

            var jsonMapAttribute = entityType?.GetTypeInfo().GetCustomAttribute<JsonPropertyMapAttribute>();
            if (!isCollection && !String.IsNullOrWhiteSpace(jsonMapAttribute?.MappedJsonObjectName))
            {
                _mapTo = jsonMapAttribute.MappedJsonObjectName;
            }
            if (isCollection && !String.IsNullOrWhiteSpace(jsonMapAttribute?.MappedJsonArrayName))
            {
                _mapTo = jsonMapAttribute.MappedJsonArrayName;
            }
        }

        protected override string ResolvePropertyName(string propertyName)
        {
            if (propertyName == nameof(Answer<IEntity>.Data))
            {
                return _mapTo;
            }
            return base.ResolvePropertyName(propertyName);
        }
    }
}
