using System;
using System.Diagnostics;

using JetBrains.Annotations;

// ReSharper disable UnusedParameter.Global
// ReSharper disable InconsistentNaming

namespace Things
{
    internal static class ReSharperUtilities
    {
        [ContractAnnotation("expression:false => halt")]
        [Conditional("DEBUG")]
        internal static void assume(bool expression)
        {
        }
    }
}
