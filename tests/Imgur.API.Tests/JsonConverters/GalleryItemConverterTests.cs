using System;
using System.IO;
using System.Linq;
using Imgur.API.JsonConverters;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.FakeResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Imgur.API.Tests.JsonConverters
{
    [TestClass]
    public class GalleryItemConverterTests
    {
        [TestMethod]
        public void GalleryItem_CanConvert_IsFalse()
        {
            var converter = new GalleryItemConverter();
            var canConvert = converter.CanConvert(new Image().GetType());
            Assert.IsFalse(canConvert);
        }

        [TestMethod]
        public void GalleryItem_CanConvertGalleryAlbum_IsTrue()
        {
            var converter = new GalleryItemConverter();
            var canConvert = converter.CanConvert(new GalleryAlbum().GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void GalleryItem_CanConvertGalleryImage_IsTrue()
        {
            var converter = new GalleryItemConverter();
            var canConvert = converter.CanConvert(new GalleryImage().GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void GalleryItemConverter_ReadJsonGalleryAlbum_AreEqual()
        {
            var converter = new GalleryItemConverter();
            var reader = new JsonTextReader(new StringReader(AccountEndpointResponses.Imgur.GetGalleryAlbum));
            reader.Read();
            var serializer = new JsonSerializer
            {
                Converters = {converter}
            };

            var actual = (GalleryAlbum) converter.ReadJson(reader, typeof (GalleryItem), null, serializer);
            Assert.IsNotNull(actual);

            Assert.AreEqual(null, actual.AccountId);
            Assert.AreEqual("SpaceCowboy02", actual.AccountUrl);
            Assert.AreEqual(null, actual.CommentCount);
            Assert.AreEqual(null, actual.CommentPreview);
            Assert.AreEqual("JsKDPBN", actual.Cover);
            Assert.AreEqual(240, actual.CoverHeight);
            Assert.AreEqual(500, actual.CoverWidth);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 09, 19, 16, 43, 54, DateTimeKind.Utc)),
                actual.DateTime);
            Assert.AreEqual(null, actual.Description);
            Assert.AreEqual(119, actual.Downs);
            Assert.AreEqual(true, actual.Favorite);
            Assert.AreEqual("LqLmI", actual.Id);
            Assert.AreEqual(9, actual.ImageCount);
            Assert.AreEqual(0, actual.Images.Count());
            Assert.AreEqual(null, actual.Layout);
            Assert.AreEqual("http://imgur.com/a/LqLmI", actual.Link);
            Assert.AreEqual(null, actual.Nsfw);
            Assert.AreEqual(null, actual.Privacy);
            Assert.AreEqual(null, actual.Score);
            Assert.AreEqual("Game of Expectation and Reality", actual.Title);
            Assert.AreEqual(null, actual.Topic);
            Assert.AreEqual(null, actual.TopicId);
            Assert.AreEqual(4040, actual.Ups);
            Assert.AreEqual(96803, actual.Views);
            Assert.AreEqual(null, actual.Vote);
        }

        [TestMethod]
        public void GalleryItemConverter_ReadJsonGalleryImage_AreEqual()
        {
            var converter = new GalleryItemConverter();
            var reader = new JsonTextReader(new StringReader(AccountEndpointResponses.Imgur.GetGalleryImage));
            reader.Read();
            var serializer = new JsonSerializer
            {
                Converters = {converter}
            };

            var actual = (GalleryImage) converter.ReadJson(reader, typeof (GalleryItem), null, serializer);
            Assert.IsNotNull(actual);

            Assert.AreEqual(null, actual.AccountId);
            Assert.AreEqual("YoSoyPablo1", actual.AccountUrl);
            Assert.AreEqual(false, actual.Animated);
            Assert.AreEqual(118267772080, actual.Bandwidth);
            Assert.AreEqual(null, actual.CommentCount);
            Assert.AreEqual(null, actual.CommentPreview);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 09, 19, 20, 29, 47, DateTimeKind.Utc)),
                actual.DateTime);
            Assert.AreEqual(null, actual.DeleteHash);
            Assert.AreEqual(null, actual.Description);
            Assert.AreEqual(225, actual.Downs);
            Assert.AreEqual(true, actual.Favorite);
            Assert.AreEqual(null, actual.Gifv);
            Assert.AreEqual(720, actual.Height);
            Assert.AreEqual("http://i.imgur.com/l35eOVB.jpg", actual.Link);
            Assert.AreEqual(false, actual.Looping);
            Assert.AreEqual(null, actual.Mp4);
            Assert.AreEqual(null, actual.Nsfw);
            Assert.AreEqual(null, actual.Score);
            Assert.AreEqual(null, actual.Section);
            Assert.AreEqual(111952, actual.Size);
            Assert.AreEqual("Someone is happy. New gadgets arrived to Ahmed from Microsoft", actual.Title);
            Assert.AreEqual(null, actual.TopicId);
            Assert.AreEqual(null, actual.Topic);
            Assert.AreEqual("image/jpeg", actual.Type);
            Assert.AreEqual(3567, actual.Ups);
            Assert.AreEqual(1056415, actual.Views);
            Assert.AreEqual(null, actual.Vote);
            Assert.AreEqual(null, actual.Webm);
            Assert.AreEqual(960, actual.Width);
        }
    }
}