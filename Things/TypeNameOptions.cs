using System;

namespace Things
{
    [Flags]
    public enum TypeNameOptions
    {
        None = 0,

        UseCSharpKeywords = 1,
        IncludeNamespace = 2,
        
        Default = UseCSharpKeywords | IncludeNamespace
    }
}