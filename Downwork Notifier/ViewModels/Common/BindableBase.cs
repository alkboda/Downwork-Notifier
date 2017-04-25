using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;

namespace Downwork_Notifier.ViewModels.Common
{
    abstract public class BindableBase : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private Dictionary<String, HashSet<String>> _errors = new Dictionary<String, HashSet<String>>();

        protected Boolean SetProperty<TValue>(ref TValue storage, TValue value, [CallerMemberName]String propertyName = null)
        {
            if ((storage == null && value == null) || (storage != null && storage.Equals(value)))
            {
                return false;
            }
            else
            {
                storage = value;
                OnPropertyChanged(propertyName);
                return true;
            }
        }

        protected virtual Boolean SetProperty<TModel, TValue>(TModel model, Expression<Func<TModel, TValue>> expr, TValue value, [CallerMemberName]String propertyName = null) where TModel : class
        {
            var body = (MemberExpression)expr.Body;
            var property = (System.Reflection.PropertyInfo)body.Member;
            var storage = (TValue)property.GetValue(model);

            if (!EqualityComparer<TValue>.Default.Equals(storage, value))
            {
                property.SetValue(model, value);
                OnPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        protected Boolean AddPropertyError(String errorText, [CallerMemberName]String propertyName = null)
        {
            if (String.IsNullOrWhiteSpace(propertyName))
            {
                return false;
            }
            else
            {
                if (!_errors.ContainsKey(propertyName))
                {
                    _errors.Add(propertyName, new HashSet<String>());
                }
                if (_errors[propertyName].Add(errorText))
                {
                    OnErrorChanged(propertyName);
                    OnPropertyChanged("HasErrors");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        protected void ClearPropertyErrors([CallerMemberName]String propertyName = null)
        {
            if (!String.IsNullOrWhiteSpace(propertyName) && _errors.ContainsKey(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorChanged(propertyName);
                OnPropertyChanged("HasErrors");
            }
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]String propertyName = "")
        {
            if (!String.IsNullOrWhiteSpace(propertyName) && PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion INotifyPropertyChanged implementation

        #region INotifyDataErrorInfo implementation
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        protected void OnErrorChanged(String propertyName)
        {
            if (!String.IsNullOrWhiteSpace(propertyName) && ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        public virtual System.Collections.IEnumerable GetErrors(string propertyName)
        {
            if (!String.IsNullOrWhiteSpace(propertyName) && _errors.ContainsKey(propertyName))
            {
                return _errors[propertyName];
            }
            else
            {
                return null;
            }
        }

        //[IgnoreDataMemberAttribute]
        public virtual bool HasErrors
        {
            get { return _errors.Count > 0; }
        }
        #endregion INotifyDataErrorInfo implementation

        public T Clone<T>(IEnumerable<Type> knownTypes = null) where T : BindableBase
        {
            DataContractJsonSerializer json;
            if (knownTypes != null && knownTypes.Any())
            {
                json = new DataContractJsonSerializer(typeof(T), knownTypes);
            }
            else
            {
                json = new DataContractJsonSerializer(typeof(T));
            }
            T clone = null;
            using (var ms = new System.IO.MemoryStream())
            {
                json.WriteObject(ms, this);
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                clone = (T)json.ReadObject(ms);
            }
            return clone;
        }
    }
}
