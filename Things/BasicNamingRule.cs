using System;

namespace Things
{
    internal class BasicNamingRule : ITypeNamingRule
    {
        public string TryName(Type type, TypeNameOptions options)
        {
            if ((options & TypeNameOptions.IncludeNamespace) != 0)
                return type.FullName;

            return type.Name;
        }
    }
}