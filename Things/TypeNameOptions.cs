using System;

namespace Things
{
    [Flags]
    public enum TypeNameOptions
    {
        None = 0,

        UseCSharpKeywords = 1,
        IncludeNamespace = 2,
        UseNullableOperator = 4,
        
        Default = UseCSharpKeywords | IncludeNamespace | UseNullableOperator
    }
}