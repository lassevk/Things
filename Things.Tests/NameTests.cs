using System;
using System.Linq;
using System.Reflection;

using NUnit.Framework;

#pragma warning disable 67

// ReSharper disable UnusedMember.Global
// ReSharper disable EventNeverSubscribedTo.Global
// ReSharper disable UnusedTypeParameter
// ReSharper disable PossibleNullReferenceException
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable RedundantArgumentDefaultValue
// ReSharper disable AssignNullToNotNullAttribute

namespace Things.Tests
{
    [TestFixture]
    public class NameTests
    {
        private const TypeNameOptions _CSharpKeywords = TypeNameOptions.UseCSharpKeywords;
        private const TypeNameOptions _FullName = TypeNameOptions.IncludeNamespace;
        private const TypeNameOptions _ShortName = TypeNameOptions.None;

        [Test]
        public void OfType_NullType_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Of((Type)null));
        }

        [Test]
        public void OfEvent_NullEventInfo_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Of((EventInfo)null));
        }

        [Test]
        public void OfProperty_NullPropertyInfo_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Of((PropertyInfo)null));
        }

        [Test]
        public void OfMethod_NullMethodInfo_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Of((MethodInfo)null));
        }

        [Test]
        public void OfConstructor_NullConstructorInfo_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => Name.Of((ConstructorInfo)null));
        }

        [Test]
        [TestCase(typeof(byte), _CSharpKeywords, "byte")]
        [TestCase(typeof(sbyte), _CSharpKeywords, "sbyte")]
        [TestCase(typeof(short), _CSharpKeywords, "short")]
        [TestCase(typeof(ushort), _CSharpKeywords, "ushort")]
        [TestCase(typeof(int), _CSharpKeywords, "int")]
        [TestCase(typeof(uint), _CSharpKeywords, "uint")]
        [TestCase(typeof(long), _CSharpKeywords, "long")]
        [TestCase(typeof(ulong), _CSharpKeywords, "ulong")]
        [TestCase(typeof(bool), _CSharpKeywords, "bool")]
        [TestCase(typeof(string), _CSharpKeywords, "string")]
        [TestCase(typeof(decimal), _CSharpKeywords, "decimal")]
        [TestCase(typeof(char), _CSharpKeywords, "char")]
        [TestCase(typeof(double), _CSharpKeywords, "double")]
        [TestCase(typeof(float), _CSharpKeywords, "float")]
        [TestCase(typeof(object), _CSharpKeywords, "object")]

        [TestCase(typeof(byte), _FullName, "System.Byte")]
        [TestCase(typeof(sbyte), _FullName, "System.SByte")]
        [TestCase(typeof(short), _FullName, "System.Int16")]
        [TestCase(typeof(ushort), _FullName, "System.UInt16")]
        [TestCase(typeof(int), _FullName, "System.Int32")]
        [TestCase(typeof(uint), _FullName, "System.UInt32")]
        [TestCase(typeof(long), _FullName, "System.Int64")]
        [TestCase(typeof(ulong), _FullName, "System.UInt64")]
        [TestCase(typeof(bool), _FullName, "System.Boolean")]
        [TestCase(typeof(string), _FullName, "System.String")]
        [TestCase(typeof(decimal), _FullName, "System.Decimal")]
        [TestCase(typeof(char), _FullName, "System.Char")]
        [TestCase(typeof(double), _FullName, "System.Double")]
        [TestCase(typeof(float), _FullName, "System.Single")]
        [TestCase(typeof(object), _FullName, "System.Object")]

        [TestCase(typeof(byte), _ShortName, "Byte")]
        [TestCase(typeof(sbyte), _ShortName, "SByte")]
        [TestCase(typeof(short), _ShortName, "Int16")]
        [TestCase(typeof(ushort), _ShortName, "UInt16")]
        [TestCase(typeof(int), _ShortName, "Int32")]
        [TestCase(typeof(uint), _ShortName, "UInt32")]
        [TestCase(typeof(long), _ShortName, "Int64")]
        [TestCase(typeof(ulong), _ShortName, "UInt64")]
        [TestCase(typeof(bool), _ShortName, "Boolean")]
        [TestCase(typeof(string), _ShortName, "String")]
        [TestCase(typeof(decimal), _ShortName, "Decimal")]
        [TestCase(typeof(char), _ShortName, "Char")]
        [TestCase(typeof(double), _ShortName, "Double")]
        [TestCase(typeof(float), _ShortName, "Single")]
        [TestCase(typeof(object), _ShortName, "Object")]

        [TestCase(typeof(int?), TypeNameOptions.UseCSharpKeywords | TypeNameOptions.UseNullableOperator, "int?")]
        [TestCase(typeof(int?), TypeNameOptions.UseCSharpKeywords, "Nullable<int>")]
        [TestCase(typeof(int?), TypeNameOptions.None, "Nullable<Int32>")]
        [TestCase(typeof(int?), TypeNameOptions.IncludeNamespace, "System.Nullable<System.Int32>")]
        
        [TestCase(typeof(NestedClass), TypeNameOptions.IncludeNamespace, "Things.Tests.NameTests.NestedClass")]
        [TestCase(typeof(NestedClass), TypeNameOptions.None, "NameTests.NestedClass")]
        [TestCase(typeof(NestedClass.DoubleNestedClass), TypeNameOptions.IncludeNamespace, "Things.Tests.NameTests.NestedClass.DoubleNestedClass")]
        [TestCase(typeof(NestedClass.DoubleNestedClass), TypeNameOptions.None, "NameTests.NestedClass.DoubleNestedClass")]

        [TestCase(typeof(Nullable<>), TypeNameOptions.None, "Nullable<T>")]
        [TestCase(typeof(Nullable<>), TypeNameOptions.IncludeNamespace, "System.Nullable<T>")]

        [TestCase(typeof(GenericOuter<>.GenericInner<>), TypeNameOptions.None, "GenericOuter<T1>.GenericInner<T2>")]
        [TestCase(typeof(GenericOuter<string>.GenericInner<int>), TypeNameOptions.None, "GenericOuter<String>.GenericInner<Int32>")]
        [TestCase(typeof(GenericOuter<>.GenericInner<>.Nested), TypeNameOptions.None, "GenericOuter<T1>.GenericInner<T2>.Nested")]
        public void OfType_TestCases_ReturnsExpectedResults(Type type, TypeNameOptions options, string expected)
        {
            var name = Name.Of(type, options);
            Assert.That(name, Is.EqualTo(expected));
        }

        [Test]
        public void OfProperty_PropertyInNestedGenericOpenClasses_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<>.GenericInner<>.Nested.Generic<>);
            var property = type.GetProperty("Property");
            var name = Name.Of(property, TypeNameOptions.None);

            Assert.That(name, Is.EqualTo("GenericOuter<T1>.GenericInner<T2>.Nested.Generic<T3>.Property"));
        }

        [Test]
        public void OfProperty_PropertyInNestedGenericConstructedClasses_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>);
            var property = type.GetProperty("Property");
            var name = Name.Of(property, TypeNameOptions.None);

            Assert.That(name, Is.EqualTo("GenericOuter<String>.GenericInner<Int32>.Nested.Generic<Boolean>.Property"));
        }

        [Test]
        public void OfMethod_OpenGenericMethodInNestedGenericConstructedClasses_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>);
            var method = type.GetMethods().First(m => m.Name == "GenericMethod");
            var name = Name.Of(method, TypeNameOptions.None);

            Assert.That(name, Is.EqualTo("GenericOuter<String>.GenericInner<Int32>.Nested.Generic<Boolean>.GenericMethod<T4>"));
        }

        [Test]
        public void OfMethod_ClosedGenericMethodInNestedGenericConstructedClasses_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>);
            var method = type.GetMethods().First(m => m.Name == "GenericMethod").MakeGenericMethod(typeof(DateTime));
            var name = Name.Of(method, TypeNameOptions.None);

            Assert.That(name, Is.EqualTo("GenericOuter<String>.GenericInner<Int32>.Nested.Generic<Boolean>.GenericMethod<DateTime>"));
        }

        [Test]
        public void OfMethod_ClosedGenericMethodInNestedGenericConstructedClassesUsingCSharpKeywords_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>);
            var method = type.GetMethods().First(m => m.Name == "GenericMethod").MakeGenericMethod(typeof(DateTime));
            var name = Name.Of(method, TypeNameOptions.UseCSharpKeywords);

            Assert.That(name, Is.EqualTo("GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>.GenericMethod<DateTime>"));
        }

        [Test]
        public void OfEvent_ClosedGenericMethodInNestedGenericConstructedClassesUsingCSharpKeywords_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>);
            var evt = type.GetEvent("TestEvent");
            var name = Name.Of(evt, TypeNameOptions.UseCSharpKeywords);

            Assert.That(name, Is.EqualTo("GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>.TestEvent"));
        }

        [Test]
        public void OfConstructor_ClosedGenericMethodInNestedGenericConstructedClassesUsingCSharpKeywords_ReturnsExpectedResults()
        {
            var type = typeof(GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>);
            var evt = type.GetConstructor(Type.EmptyTypes);
            var name = Name.Of(evt, TypeNameOptions.UseCSharpKeywords);

            Assert.That(name, Is.EqualTo("GenericOuter<string>.GenericInner<int>.Nested.Generic<bool>"));
        }

        public class NestedClass
        {
            public class DoubleNestedClass
            {
            }
        }
    }

    public class GenericOuter<T1>
    {
        public string Property { get; set; }

        public class GenericInner<T2>
        {
            public string Property { get; set; }

            public class Nested
            {
                public string Property { get; set; }

                public class Generic<T3>
                {
                    public string Property { get; set; }

                    public void Method()
                    {
                    }

                    public void GenericMethod<T4>()
                    {
                    }

                    public event EventHandler TestEvent;
                }
            }
        }
    }
}
