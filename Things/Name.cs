using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Things
{
    public static class Name
    {
        [NotNull]
        private static readonly Dictionary<Type, string> _CSharpKeywords = new Dictionary<Type, string>
                                                                           {
                                                                               [typeof(byte)] = "byte",
                                                                               [typeof(sbyte)] = "sbyte",
                                                                               [typeof(short)] = "short",
                                                                               [typeof(ushort)] = "ushort",
                                                                               [typeof(int)] = "int",
                                                                               [typeof(uint)] = "uint",
                                                                               [typeof(long)] = "long",
                                                                               [typeof(ulong)] = "ulong",
                                                                               [typeof(bool)] = "bool",
                                                                               [typeof(string)] = "string",
                                                                               [typeof(decimal)] = "decimal",
                                                                               [typeof(char)] = "char",
                                                                               [typeof(double)] = "double",
                                                                               [typeof(float)] = "float",
                                                                               [typeof(object)] ="object",
                                                                           };

        [NotNull]
        public static string Of([NotNull] Type type, TypeNameOptions options = TypeNameOptions.Default)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if ((options & TypeNameOptions.UseCSharpKeywords) != 0)
            {
                if (_CSharpKeywords.TryGetValue(type, out string csharpKeyword))
                    return csharpKeyword;
            }

            if ((options & TypeNameOptions.IncludeNamespace) != 0)
                return type.Namespace + "." + type.Name;

            return type.Name;
        }
    }
}
