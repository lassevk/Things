using System;

namespace Things
{
    internal class GenericParameterNamingRule : ITypeNamingRule
    {
        public string TryName(Type type, TypeNameOptions options)
        {
            if (!type.IsGenericParameter)
                return null;

            return type.Name;
        }
    }
}