using System;
using System.IO;
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
        [ExpectedException(typeof (NotImplementedException))]
        public void EpochTimeConverter_WriteJson_ThrowsNotImplementedException()
        {
            var converter = new EpochTimeConverter();
            converter.WriteJson(null, null, null);
        }
    }
}