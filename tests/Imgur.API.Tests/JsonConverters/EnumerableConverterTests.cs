using System;
using System.Collections.Generic;
using System.IO;
using Imgur.API.JsonConverters;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;

namespace Imgur.API.Tests.JsonConverters
{
    [TestClass]
    public class EnumerableConverterTests
    {
        [TestMethod]
        public void EpochTimeConverter_CanConvertComment_IsTrue()
        {
            var converter = new EnumerableConverter<Comment>();
            var list = new List<Comment> { new Comment(), new Comment() };
            var canConvert = converter.CanConvert(list.GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void EpochTimeConverter_CanConvertImage_IsTrue()
        {
            var converter = new EnumerableConverter<Image>();
            var list = new List<Image> { new Image(), new Image() };
            var canConvert = converter.CanConvert(list.GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void EpochTimeConverter_CanConvertString_IsFalse()
        {
            var converter = new EpochTimeConverter();

            Assert.IsFalse(converter.CanConvert(typeof(string)));
        }

        [TestMethod]
        public void EpochTimeConverter_CanConvertInt_IsFalse()
        {
            var converter = new EpochTimeConverter();
            var list = new List<int> { 1, 2, 3 };
            Assert.IsFalse(converter.CanConvert(list.GetType()));
        }
    }
}