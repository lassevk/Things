using System;

using JetBrains.Annotations;

namespace Things
{
    internal interface ITypeNameProvider
    {
        [NotNull]
        string NameOf([NotNull] Type type);
    }
}