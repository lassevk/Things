using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace Things
{
    internal class CSharpKeywordNamingRule : ITypeNamingRule
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
                                                                               [typeof(object)] = "object",
                                                                           };

        public string TryName(Type type, TypeNameOptions options)
        {
            if ((options & TypeNameOptions.UseCSharpKeywords) == 0)
                return null;

            _CSharpKeywords.TryGetValue(type, out string name);
            return name;
        }
    }
}