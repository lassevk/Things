using System;

using NUnit.Framework;

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
        public void OfType_TestCases_ReturnsExpectedResults(Type type, TypeNameOptions options, string expected)
        {
            var name = Name.Of(type, options);
            Assert.That(name, Is.EqualTo(expected));
        }
    }
}
