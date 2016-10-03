using Luadicrous.Framework.Extensions;
using NUnit.Framework;
using System;
using System.Reflection;

namespace Luadicrous.Framework.Tests
{
    [TestFixture]
    public class PropertyInfoExtensionsTests
    {
        public class TestType
        {
            public int field;
            public int FieldProperty
            {
                get { return field; }
                set { field = value; }
            }
        }

        [Test]
        public void PropertyInfoCreateGetDelegateTest([Values(-1, 0, 1)] int fieldValue)
        {
            // Assemble
            Type t = typeof(TestType);
            TestType instance = new TestType
            {
                field = fieldValue
            };
            PropertyInfo property = t.GetProperty(nameof(TestType.FieldProperty));

            // Act
            Func<int> getter = property.CreateGetDelegate<int>(instance);

            // Assert
            Assert.True(getter() == instance.field);
        }

        [Test]
        public void PropertyInfoCreateSetDelegateTest([Values(-1, 0, 1)] int setValue)
        {
            // Assemble
            Type t = typeof(TestType);
            TestType instance = new TestType
            {
                field = 2
            };
            PropertyInfo property = t.GetProperty(nameof(TestType.FieldProperty));

            // Act
            Action<int> setter = property.CreateSetDelegate<int>(instance);
            setter(setValue);

            // Assert
            Assert.True(setValue == instance.field);
        }
    }
}
