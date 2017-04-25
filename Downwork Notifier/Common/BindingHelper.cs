using Downwork_Notifier.Common.Converters;
using Downwork_Notifier.ViewModels.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Downwork_Notifier.Common
{
    public class BindingHelper : BindableBase
    {
        #region Const
        public const string CT_BOOL_VISIBILITY = "__boolVisibilityConverter__";
        public const string CT_EMPTY_VISIBILITY = "__nullVisibilityConverter__";
        public const string CT_NOTEMPTY_VISIBILITY = "~__nullVisibilityConverter__";
        public const string CT_INVERT_BOOL = "__invertBoolConverter__";
        public const string CT_ARRAY_STRING = "__enumerableStringConverter__";
        #endregion Const

        #region Singleton implementation
        private static BindingHelper _instance = null;
        private BindingHelper()
        {
            RegisterDefaultConverters();
            Enums = new EnumResolver(() => OnPropertyChanged(nameof(Enums)));
            DynamicBindings = new DPResolver(() => OnPropertyChanged(nameof(DynamicBindings)));
        }

        public static BindingHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new BindingHelper();
                }
                return _instance;
            }
        }
        #endregion Singleton implementation

        #region Converters
        public Dictionary<String, IValueConverter> Converters { get; } = new Dictionary<string, IValueConverter>();
        public void RegisterDefaultConverters()
        {
            Converters.Add(CT_INVERT_BOOL, new GenericConverter<bool, bool>(b => !b, b => !b));
            Converters.Add(CT_BOOL_VISIBILITY,
                new GenericConverter<bool, Visibility>(
                    b => (b ? Visibility.Visible : Visibility.Collapsed),
                    v => (v == Visibility.Visible ? true : false)
                )
            );
            Converters.Add(CT_EMPTY_VISIBILITY, new GenericConverter<Object, Visibility>((o) =>
                {
                    if (o == null)
                    {
                        return Visibility.Visible;
                    }
                    if (typeof(System.Collections.ICollection).IsAssignableFrom(o.GetType()))
                    {
                        return ((System.Collections.ICollection)o).Count == 0 ? Visibility.Visible : Visibility.Collapsed;
                    }
                    
                    return Visibility.Collapsed;
                })
            );
            Converters.Add(CT_NOTEMPTY_VISIBILITY, new GenericConverter<Object, Visibility>((o) =>
                {
                    if (o == null)
                    {
                        return Visibility.Collapsed;
                    }
                    if (typeof(System.Collections.ICollection).IsAssignableFrom(o.GetType()))
                    {
                        return ((System.Collections.ICollection)o).Count > 0 ? Visibility.Visible : Visibility.Collapsed;
                    }

                    return Visibility.Visible;
                })
            );
            Converters.Add(CT_ARRAY_STRING, new GenericConverter<IEnumerable<Object>, String>((e) =>
                {
                    if (e?.Any() ?? false)
                    {
                        return String.Join("; ", e);
                    }
                    return String.Empty;
                })
            );
        }

        public bool RegisterConverter(String name, IValueConverter converter, Boolean @override = false)
        {
            if (!Converters.ContainsKey(name))
            {
                Converters.Add(name, converter);
                return true;
            }
            else if(@override)
            {
                Converters[name] = converter;
                return true;
            }

            return false;
        }
        #endregion Converters

        #region Enums
        public EnumResolver Enums { get; private set; }
        public class EnumResolver
        {
            private Action _resolverChanged;
            private Dictionary<Type, Object> _innerCache = new Dictionary<Type, object>();

            public EnumResolver(Action resolverChanged)
            {
                _resolverChanged = resolverChanged;
            }

            public Object this[String enumTypeName]
            {
                get
                {
                    Type enumType = ResolveType(enumTypeName);
                    if (enumType == null)
                    {
                        return null;
                    }
                    if (!_innerCache.ContainsKey(enumType))
                    {
                        List<Tuple<Enum, String>> tuples = new List<Tuple<Enum, string>>();
                        foreach (Enum e in Enum.GetValues(enumType))
                        {
                            var key = $"ENUM_{enumType.Name}_{e}";
                            var res = Properties.Resources.ResourceManager.GetString(key);
                            if (String.IsNullOrWhiteSpace(res))
                            {
                                tuples.Add(new Tuple<Enum, string>(e, e.ToString()));
                            }
                            else
                            {
                                tuples.Add(new Tuple<Enum, string>(e, res));
                            }
                        }
                        _innerCache.Add(enumType, tuples);
                    }
                    return _innerCache[enumType];
                }
                set
                {
                    bool fireRefresh = false;
                    Type enumType = ResolveType(enumTypeName);
                    if (enumType != null)
                    {
                        if (_innerCache.ContainsKey(enumType))
                        {
                            if (_innerCache[enumType] != value)
                            {
                                _innerCache[enumType] = value;
                                fireRefresh = true;
                            }
                            if (value == null)
                            {
                                _innerCache.Remove(enumType);
                                fireRefresh = true;
                            }
                        }
                        else if (enumType.IsEnum)
                        {
                            _innerCache.Add(enumType, value);
                            fireRefresh = true;
                        }
                        if (fireRefresh && _resolverChanged != null)
                        {
                            _resolverChanged();
                        }
                    }
                }
            }

            private Type ResolveType(String typeName)
            {
                var enumType = Type.GetType(typeName);
                if (enumType == null || !enumType.IsEnum)
                {
                    var assemblies = AppDomain.CurrentDomain.GetAssemblies();
                    foreach (var assembly in assemblies)
                    {
                        enumType = assembly.GetType(typeName);
                        if (enumType != null && enumType.IsEnum)
                        {
                            break;
                        }
                        enumType = null;
                    }
                }
                return enumType;
            }
        }
        #endregion Enums

        #region DP resolver
        public DPResolver DynamicBindings { get; private set; }
        public class DPResolver : DependencyObject
        {
            private Action _resolverChanged;
            private Dictionary<String, DependencyProperty> _innerCache = new Dictionary<string, DependencyProperty>();

            public DPResolver(Action resolverChanged)
            {
                _resolverChanged = resolverChanged;
            }
            public Object this[String dpName]
            {
                get
                {
                    if (_innerCache.ContainsKey(dpName))
                    {
                        return GetValue(_innerCache[dpName]);
                    }
                    return null;
                }
                set
                {
                    if (_innerCache.ContainsKey(dpName) && value != this[dpName])
                    {
                        SetValue(_innerCache[dpName], value);
                        _resolverChanged();
                    }
                }
            }

            public void RegisterBinding(String dpName, Type endType, Binding binding)
            {
                if (String.IsNullOrWhiteSpace(dpName))
                {
                    throw new ArgumentNullException(nameof(dpName));
                }
                if (binding == null)
                {
                    throw new ArgumentNullException(nameof(binding));
                }

                DependencyProperty dp;
                if (_innerCache.ContainsKey(dpName))
                {
                    dp = _innerCache[dpName];
                }
                else
                {
                    dp = DependencyProperty.Register(dpName, endType, typeof(DPResolver), new PropertyMetadata((d,e) => _resolverChanged()));
                    _innerCache.Add(dpName, dp);
                }

                if (dp != null)
                {
                    var be = BindingOperations.SetBinding(this, dp, binding);
                    _resolverChanged();
                }
            }
        }
        #endregion DP resolver

        #region Commands
        public Dictionary<String, System.Windows.Input.RoutedCommand> Commands { get; } = new Dictionary<string, System.Windows.Input.RoutedCommand>();
        #endregion Commands
    }
}
