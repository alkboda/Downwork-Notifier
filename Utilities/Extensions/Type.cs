using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Utilities.Extensions
{
    public static class TypeEx
    {
        public static Type GetUnderlyingIEnumerableInterface(this Type type)
        {
            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(
                    interfaceType =>
                    {
                        return
                            typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(interfaceType.GetTypeInfo())
                            && interfaceType.IsConstructedGenericType
                            && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                            && interfaceType.GenericTypeArguments.Length == 1;
                    });
            }
            return null;
        }

        public static Type GetUnderlyingIEnumerableInterface<TBase>(this Type type)
        {
            if (typeof(IEnumerable<TBase>).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(
                    interfaceType =>
                    {
                        return
                            typeof(IEnumerable<TBase>).GetTypeInfo().IsAssignableFrom(interfaceType.GetTypeInfo())
                            && interfaceType.IsConstructedGenericType
                            && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                            && typeof(TBase).GetTypeInfo().IsAssignableFrom(interfaceType.GenericTypeArguments[0].GetTypeInfo());
                    });
            }
            return null;
        }

        public static Type GetUnderlyingIEnumerableInterface(this Type type, IEnumerable<Type> baseTypes)
        {
            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(type.GetTypeInfo()))
            {
                return type.GetTypeInfo().ImplementedInterfaces.FirstOrDefault(
                    interfaceType =>
                    {
                        return
                            typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(interfaceType.GetTypeInfo())
                            && interfaceType.IsConstructedGenericType
                            && interfaceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)
                            && baseTypes.Contains(interfaceType.GenericTypeArguments[0]);
                    });
            }
            return null;
        }

        public static void GetTypeParts(this Type type, out string className, out string genericPart, out string wherePart)
        {
            className = type.Name;
            genericPart = String.Empty;
            wherePart = String.Empty;

            if (type.GetTypeInfo().IsGenericType)
            {
                var classGenerics = type.GetTypeInfo().IsGenericTypeDefinition
                    ? type.GetTypeInfo().GenericTypeParameters
                    : type.GetTypeInfo().GenericTypeArguments;
                className = className.Substring(0, className.LastIndexOf('`'));
                genericPart = String.Format("<{0}>", String.Join(",", classGenerics.Select(t => t.Name)));

                foreach (var cgt in classGenerics)
                {
                    // Taking constraints for each generic in class
                    //
                    List<String> wheres = new List<String>();

                    if (cgt.IsGenericParameter)
                    {
                        // Taking from attributes
                        //
                        var attr = cgt.GetTypeInfo().GenericParameterAttributes;
                        if (attr != GenericParameterAttributes.None)
                        {
                            if ((attr & GenericParameterAttributes.NotNullableValueTypeConstraint) > 0)
                            {
                                wheres.Add("struct");
                            }
                            else if ((attr & GenericParameterAttributes.DefaultConstructorConstraint) > 0)
                            {
                                wheres.Add("new()");
                            }
                            if ((attr & GenericParameterAttributes.ReferenceTypeConstraint) > 0)
                            {
                                wheres.Add("class");
                            }
                        }

                        // Taking from classes
                        //
                        var constraints = cgt.GetTypeInfo().GetGenericParameterConstraints();
                        if (constraints != null && constraints.Length > 0)
                        {
                            wheres.AddRange(constraints.Where(t => t != typeof(ValueType)).Select(t => t.Name));
                        }
                    }

                    // Accumulating all info
                    //
                    if (wheres.Count > 0)
                    {
                        var cnstrString = String.Join(", ", wheres);
                        wherePart += $" where {cgt.Name} : {cnstrString}";
                    }
                }
            }
        }

        public static Object CreateDefaultInstance(this Type type)
        {
            var ctor = type.GetTypeInfo().DeclaredConstructors.OrderBy(_ => _.GetParameters().Length).FirstOrDefault();
            if (ctor != null)
            {
                var @params = ctor.GetParameters();
                if (@params.Length == 0)
                {
                    return ctor.Invoke(null);
                }
                else
                {
                    var paramValues = @params.Select(_ => _.ParameterType.CreateDefaultInstance()).ToArray();
                    return ctor.Invoke(paramValues);
                }
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
