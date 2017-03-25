using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Imgur.API.JsonConverters;
using Imgur.API.Models.Impl;
using Newtonsoft.Json;
using Xunit;

namespace Imgur.API.Tests.JsonConverterTests
{
    public class EpochTimeConverterTests
    {
        public static IEnumerable<object[]> ReadJsonData => new[]
        {
            new object[] {new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc)), "1439134235"}
        };

        public static IEnumerable<object[]> WriteJsonData => new[]
        {
            new object[] {"1439134235", new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc))}
        };

        [Theory]
        [InlineData(typeof(DateTimeOffset), true)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(Image), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(bool), false)]
        [InlineData(typeof(float), false)]
        public void CanConvert(Type type, bool canConvert)
        {
            var converter = new EpochTimeConverter();

            Assert.Equal(converter.CanConvert(type), canConvert);
        }

        [Theory, MemberData("ReadJsonData")]
        public void ReadJson(DateTimeOffset expected, string original)
        {
            var converter = new EpochTimeConverter();

            var reader = new JsonTextReader(new StringReader(original));
            reader.Read();
            var serializer = new JsonSerializer();

            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof(DateTimeOffset), null, serializer);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("'abcdefg'")]
        [InlineData("true")]
        public void ReadJson_ThrowsInvalidCastException(string data)
        {
            var converter = new EpochTimeConverter();

            var reader = new JsonTextReader(new StringReader(data));
            reader.Read();
            var serializer = new JsonSerializer();

            var exception = Record.Exception(() => converter.ReadJson(reader, typeof(DateTimeOffset), null, serializer));
            Assert.NotNull(exception);
            Assert.IsType<InvalidCastException>(exception);
        }

        [Theory, MemberData("WriteJsonData")]
        public void WriteJson(string expected, DateTimeOffset original)
        {
            var converter = new EpochTimeConverter();

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();

            converter.WriteJson(writer, original, serializer);

            var actual = sb.ToString();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("xyz")]
        [InlineData("true")]
        [InlineData("21312312")]
        public void WriteJson_ThrowsInvalidCastException(object data)
        {
            var converter = new EpochTimeConverter();

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();

            var exception = Record.Exception(() => converter.WriteJson(writer, data, serializer));
            Assert.NotNull(exception);
            Assert.IsType<InvalidCastException>(exception);
        }
    }
}