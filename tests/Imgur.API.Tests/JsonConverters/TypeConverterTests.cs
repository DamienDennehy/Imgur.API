using System.Collections.Generic;
using Imgur.API.JsonConverters;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.JsonConverters
{
    [TestClass]
    public class TypeConverterTests
    {
        [TestMethod]
        public void TypeConverter_CanConvertComment_IsTrue()
        {
            var converter = new TypeConverter<Comment>();
            var comment = new Comment();
            var canConvert = converter.CanConvert(comment.GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void TypeConverter_CanConvertCommentList_IsTrue()
        {
            var converter = new TypeConverter<IEnumerable<Comment>>();
            var list = new List<Comment> {new Comment(), new Comment()};
            var canConvert = converter.CanConvert(list.GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void TypeConverter_CanConvertImage_IsTrue()
        {
            var converter = new TypeConverter<Image>();
            var image = new Image();
            var canConvert = converter.CanConvert(image.GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void TypeConverter_CanConvertImageList_IsTrue()
        {
            var converter = new TypeConverter<IEnumerable<Image>>();
            var list = new List<Image> {new Image(), new Image()};
            var canConvert = converter.CanConvert(list.GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void TypeConverter_CanConvertInt_IsFalse()
        {
            var converter = new TypeConverter<Image>();
            var list = new List<int> {1, 2, 3};
            Assert.IsFalse(converter.CanConvert(list.GetType()));
        }

        [TestMethod]
        public void TypeConverter_CanConvertString_IsFalse()
        {
            var converter = new TypeConverter<Image>();

            Assert.IsFalse(converter.CanConvert(typeof (string)));
        }
    }
}