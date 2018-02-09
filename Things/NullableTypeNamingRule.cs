using System;
using System.Reflection;

using static Things.ReSharperUtilities;

namespace Things
{
    internal class NullableTypeNamingRule : ITypeNamingRule
    {
        public string TryName(Type type, TypeNameOptions options)
        {
            if ((options & TypeNameOptions.UseNullableOperator) == 0)
                return null;

            var ti = type.GetTypeInfo();
            if (!(ti?.IsGenericType ?? false))
                return null;

            if (ti.GetGenericTypeDefinition() != typeof(Nullable<>))
                return null;

            var underlyingType = ti.GetGenericArguments()[0];
            assume(underlyingType != null);

            return Name.Of(underlyingType, options) + "?";
        }
    }
}