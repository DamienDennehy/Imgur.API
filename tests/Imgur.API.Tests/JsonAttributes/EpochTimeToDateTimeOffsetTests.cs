using System;
using System.IO;
using Imgur.API.JsonAttributes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Imgur.API.Tests.JsonAttributes
{
    [TestClass]
    public class EpochTimeToDateTimeOffsetTests
    {
        [TestMethod]
        public void EpochTimeToDateTimeOffset_CanConvertDateTimeOffset_IsTrue()
        {
            var converter = new EpochTimeToDateTimeOffset();

            Assert.IsTrue(converter.CanConvert(typeof (DateTimeOffset)));
        }

        [TestMethod]
        public void EpochTimeToDateTimeOffset_CanConvertString_IsFalse()
        {
            var converter = new EpochTimeToDateTimeOffset();

            Assert.IsFalse(converter.CanConvert(typeof (string)));
        }

        [TestMethod]
        public void EpochTimeToDateTimeOffset_CanConvertInt_IsFalse()
        {
            var converter = new EpochTimeToDateTimeOffset();

            Assert.IsFalse(converter.CanConvert(typeof (int)));
        }

        [TestMethod]
        public void EpochTimeToDateTimeOffset_ReadJsonInt64_AreEqual()
        {
            var converter = new EpochTimeToDateTimeOffset();

            var reader = new JsonTextReader(new StringReader("1439134235"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void EpochTimeToDateTimeOffset_ReadJsonInt64_AreNotEqual()
        {
            var converter = new EpochTimeToDateTimeOffset();

            var reader = new JsonTextReader(new StringReader("1439134235"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2005, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void EpochTimeToDateTimeOffset_ReadJsonString_ThrowsInvalidCastException()
        {
            var converter = new EpochTimeToDateTimeOffset();

            var reader = new JsonTextReader(new StringReader("'abcdefg'"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof (InvalidCastException))]
        public void EpochTimeToDateTimeOffset_ReadJsonBoolean_ThrowsInvalidCastException()
        {
            var converter = new EpochTimeToDateTimeOffset();

            var reader = new JsonTextReader(new StringReader("true"));
            reader.Read();
            var serializer = new JsonSerializer();

            var expected = new DateTimeOffset(new DateTime(2015, 8, 9, 15, 30, 35, DateTimeKind.Utc));
            var actual = (DateTimeOffset) converter.ReadJson(reader, typeof (DateTimeOffset), null, serializer);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof (NotImplementedException))]
        public void EpochTimeToDateTimeOffset_WriteJson_ThrowsNotImplementedException()
        {
            var converter = new EpochTimeToDateTimeOffset();
            converter.WriteJson(null, null, null);
        }
    }
}