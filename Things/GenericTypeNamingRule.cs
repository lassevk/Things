using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Things
{
    internal class GenericTypeNamingRule : ITypeNamingRule
    {
        public string TryName(Type type, TypeNameOptions options)
        {
            var ti = type.GetTypeInfo();
            if (!(ti?.IsGenericType ?? false))
                return null;

            var result = new StringBuilder();
            if ((options & TypeNameOptions.IncludeNamespace) != 0)
                result.Append(type.Namespace).Append('.');

            Type[] genericArguments = ti.GetGenericArguments();

            if (type.IsNested)
            {
                var declaringTypeInfo = type.DeclaringType?.GetTypeInfo();
                if (declaringTypeInfo?.IsGenericType ?? false)
                {
                    string declaringTypeName;
                    if (type.IsConstructedGenericType)
                    {
                        Type constructedDeclaringType = type.DeclaringType.MakeGenericType(genericArguments.Take(declaringTypeInfo.GetGenericArguments().Length).ToArray());
                        declaringTypeName = Name.Of(constructedDeclaringType, options);
                    }
                    else
                        declaringTypeName = Name.Of(type.DeclaringType, options);
                    result.Insert(0, declaringTypeName + ".");

                    genericArguments = genericArguments.Skip(declaringTypeInfo.GetGenericArguments().Length).ToArray();
                }
            }

            if (genericArguments.Length > 0)
            {
                result.Append(type.Name.Substring(0, type.Name.IndexOf('`')));
                result.Append('<');
                result.Append(string.Join(", ", genericArguments.Select(arg => Name.Of(arg ?? throw new InvalidOperationException(), options))));
                result.Append('>');
            }
            else
                result.Append(type.Name);

            return result.ToString();
        }
    }
}