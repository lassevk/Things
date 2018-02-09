using System;

namespace Things
{
    internal class NestedTypeNamingRule : ITypeNamingRule
    {
        public string TryName(Type type, TypeNameOptions options)
        {
            if (!type.IsNested || type.DeclaringType == null)
                return null;

            var declaringTypeName = Name.Of(type.DeclaringType, options);
            return declaringTypeName + "." + type.Name;
        }
    }
}