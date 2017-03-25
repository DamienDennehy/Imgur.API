using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Xunit;

namespace Imgur.API.Tests.JsonConverterTests
{
    public class TypeConverterTests
    {
        public static IEnumerable<object[]> TypeConverterListBool => new[]
        {
            new object[] {new List<bool> {false, true}, true},
            new object[] {true, false},
            new object[] {"dfiop", false},
            new object[] {1234, false}
        };

        [Theory]
        [InlineData(true, true)]
        [InlineData("zyus", false)]
        [InlineData("123", false)]
        public void TypeConverterBool_CanConvert(object value, bool isConvertable)
        {
            var converter = new TypeConverter<bool>();
            var canConvert = converter.CanConvert(value.GetType());
            Assert.Equal(canConvert, isConvertable);
        }

        [Theory]
        [InlineData(true, false)]
        [InlineData("zyus", false)]
        [InlineData(123, true)]
        public void TypeConverterInt_CanConvert(object value, bool isConvertable)
        {
            var converter = new TypeConverter<int>();
            var canConvert = converter.CanConvert(value.GetType());
            Assert.Equal(canConvert, isConvertable);
        }

        [Theory, MemberData("TypeConverterListBool")]
        public void TypeConverterListBool_CanConvert(object value, bool isConvertable)
        {
            var converter = new TypeConverter<IEnumerable<bool>>();
            var list = new List<bool> {false, true};
            var canConvert = converter.CanConvert(list.GetType());
            Assert.True(canConvert);
        }
    }
}