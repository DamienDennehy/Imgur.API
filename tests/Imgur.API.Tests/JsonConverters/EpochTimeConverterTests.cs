using System;
using System.IO;
using System.Text;
using Imgur.API.JsonConverters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Imgur.API.Tests.JsonConverters
{
    [TestClass]
    public class EpochTimeConverterTests
    {
        [TestMethod]
        public void EpochTimeConverter_CanConvertDateTimeOffset_IsTrue()
        {
            var converter = new EpochTimeConverter();

            Assert.IsTrue(converter.CanConvert(typeof (DateTimeOffset)));
        }

        [TestMethod]
        public void EpochTimeConverter_CanConvertString_IsFalse()
        {
            var converter = new EpochTimeConverter();

            Assert.IsFalse(converter.CanConvert(typeof (string)));
        }

        [TestMethod]
        public void EpochTimeConverter_CanConvertInt_IsFalse()
        {
            var converter = new EpochTimeConverter();

            Assert.IsFalse(converter.CanConvert(typeof (int)));
        }

        [TestMethod]
        public void EpochTimeConverter_ReadJsonInt64_AreEqual()
        {
            var converter = new EpochTimeConverter();

            var reader = new JsonTextReader(new StringReader("1439134235"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EpochTimeConverter_ReadJsonInt64_AreNotEqual()
        {
            var converter = new EpochTimeConverter();

            var reader = new JsonTextReader(new StringReader("1439134235"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2005, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void EpochTimeConverter_ReadJsonString_ThrowsInvalidCastException()
        {
            var converter = new EpochTimeConverter();

            var reader = new JsonTextReader(new StringReader("'abcdefg'"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void EpochTimeConverter_ReadJsonBoolean_ThrowsInvalidCastException()
        {
            var converter = new EpochTimeConverter();

            var reader = new JsonTextReader(new StringReader("true"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EpochTimeConverter_WriteJsonInt64_AreEqual()
        {
            var converter = new EpochTimeConverter();

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();

            var date = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            converter.WriteJson(writer, date, serializer);

            var actual = sb.ToString();

            Assert.AreEqual("1439134235", actual);
        }

        [TestMethod]
        public void EpochTimeConverter_WriteJsonInt64_AreNotEqual()
        {
            var converter = new EpochTimeConverter();

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();

            var date = new DateTimeOffset(new DateTime(2015, 8, 4, 15, 30, 32, DateTimeKind.Utc));
            converter.WriteJson(writer, date, serializer);

            var actual = sb.ToString();

            Assert.AreNotEqual("1439134235", actual);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void EpochTimeConverter_WriteJsonString_ThrowsInvalidCastException()
        {
            var converter = new EpochTimeConverter();

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();

            converter.WriteJson(writer, "xyz", serializer);

            var actual = sb.ToString();

            Assert.AreNotEqual("1439134235", actual);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void EpochTimeConverter_WriteJsonBoolean_ThrowsInvalidCastException()
        {
            var converter = new EpochTimeConverter();

            var sb = new StringBuilder();
            var stringWriter = new StringWriter(sb);
            var writer = new JsonTextWriter(stringWriter);
            var serializer = new JsonSerializer();

            converter.WriteJson(writer, true, serializer);

            var actual = sb.ToString();

            Assert.AreNotEqual("1439134235", actual);
        }
    }
}