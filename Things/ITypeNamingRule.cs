using System;

using JetBrains.Annotations;

namespace Things
{
    internal interface ITypeNamingRule
    {
        [CanBeNull]
        string TryName([NotNull] Type type, TypeNameOptions options);
    }
}
