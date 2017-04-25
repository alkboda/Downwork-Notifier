using Newtonsoft.Json.Serialization;
using System;
using System.Reflection;

namespace ApiLibrary.ApiEntities.Base
{
    public class ToStringValueProvider : IValueProvider
    {
        private PropertyInfo _memberInfo;
        private readonly IValueProvider _defaultProvider;

        public ToStringValueProvider(MemberInfo member, IValueProvider defaultValueProvider)
        {
            _memberInfo = member as PropertyInfo ?? throw new ArgumentNullException(nameof(member));
            _defaultProvider = defaultValueProvider ?? throw new ArgumentNullException(nameof(defaultValueProvider));
        }

        public object GetValue(object target)
        {
            return _memberInfo.GetValue(target)?.ToString();
        }

        public void SetValue(object target, object value)
        {
            _defaultProvider.SetValue(target, value);
        }
    }
}
