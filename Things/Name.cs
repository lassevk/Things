using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using JetBrains.Annotations;

using static Things.ReSharperUtilities;

namespace Things
{
    public static class Name
    {
        [NotNull, ItemNotNull]
        private static readonly List<ITypeNamingRule> _NamingRules = new List<ITypeNamingRule>
                                                                     {
                                                                         new CSharpKeywordNamingRule(),
                                                                         new GenericParameterNamingRule(),
                                                                         new NullableTypeNamingRule(),
                                                                         new GenericTypeNamingRule(),
                                                                         new NestedTypeNamingRule(),
                                                                         new BasicNamingRule(),
                                                                     };

        [NotNull]
        public static string Of([NotNull] Type type, TypeNameOptions options = TypeNameOptions.Default)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            foreach (var rule in _NamingRules)
            {
                var name = rule.TryName(type, options);
                if (name != null)
                    return name;
            }

            throw new InvalidOperationException($"Unable to name type '{type}'");
        }

        public static string Of([NotNull] PropertyInfo property, TypeNameOptions options = TypeNameOptions.Default)
        {
            if (property == null)
                throw new ArgumentNullException(nameof(property));

            assume(property.DeclaringType != null);

            return Of(property.DeclaringType, options) + "." + property.Name;
        }

        public static string Of([NotNull] EventInfo @event, TypeNameOptions options = TypeNameOptions.Default)
        {
            if (@event == null)
                throw new ArgumentNullException(nameof(@event));

            assume(@event.DeclaringType != null);

            return Of(@event.DeclaringType, options) + "." + @event.Name;
        }

        public static string Of([NotNull] ConstructorInfo constructor, TypeNameOptions options = TypeNameOptions.Default)
        {
            if (constructor == null)
                throw new ArgumentNullException(nameof(constructor));

            assume(constructor.DeclaringType != null);

            return Of(constructor.DeclaringType, options);
        }

        public static string Of([NotNull] MethodInfo method, TypeNameOptions options = TypeNameOptions.Default)
        {
            if (method == null)
                throw new ArgumentNullException(nameof(method));

            assume(method.DeclaringType != null);

            var result = new StringBuilder();
            result.Append(Of(method.DeclaringType, options)).Append('.').Append(method.Name);

            if (method.IsGenericMethod)
            {
                var arguments = method.GetGenericArguments();
                result.Append('<');
                result.Append(string.Join(", ", arguments.Select(arg => Of(arg ?? throw new InvalidOperationException(), options))));
                result.Append('>');
            }

            return result.ToString();
        }
    }
}