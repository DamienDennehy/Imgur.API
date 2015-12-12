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
    public class NotificationConverterTests
    {
        [TestMethod]
        public void NotificationConverter_CanConvertComment_IsTrue()
        {
            var converter = new NotificationConverter();
            var canConvert = converter.CanConvert(new Comment().GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void NotificationConverter_CanConvertMessage_IsFalse()
        {
            var converter = new NotificationConverter();
            var canConvert = converter.CanConvert(new Image().GetType());
            Assert.IsFalse(canConvert);
        }

        [TestMethod]
        public void NotificationConverter_CanConvertMessage_IsTrue()
        {
            var converter = new NotificationConverter();
            var canConvert = converter.CanConvert(new Message().GetType());
            Assert.IsTrue(canConvert);
        }

        [TestMethod]
        public void NotificationConverter_ReadJsonCommentNotification_AreEqual()
        {
            var converter = new NotificationConverter();
            var reader = new JsonTextReader(new StringReader(AccountEndpointResponses.Imgur.GetCommentNotification));
            reader.Read();
            var serializer = new JsonSerializer();

            var actual = (Comment) converter.ReadJson(reader, typeof (Comment), null, serializer);
            Assert.IsNotNull(actual);

            Assert.AreEqual(null, actual.AlbumCover);
            Assert.AreEqual("jasdev", actual.Author);
            Assert.AreEqual(3698510, actual.AuthorId);
            Assert.AreEqual(0, actual.Children.Count());
            Assert.AreEqual("Reply test", actual.CommentText);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2014, 07, 22, 23, 12, 54, DateTimeKind.Utc)),
                actual.DateTime);
            Assert.AreEqual(false, actual.Deleted);
            Assert.AreEqual(0, actual.Downs);
            Assert.AreEqual("VK9VqcM", actual.ImageId);
            Assert.AreEqual(false, actual.OnAlbum);
            Assert.AreEqual(3615, actual.ParentId);
            Assert.AreEqual(1, actual.Points);
            Assert.AreEqual(1, actual.Ups);
            Assert.AreEqual(3616, actual.Id);
        }

        [TestMethod]
        public void NotificationConverter_ReadJsonMessageNotification_AreEqual()
        {
            var converter = new NotificationConverter();
            var reader = new JsonTextReader(new StringReader(AccountEndpointResponses.Imgur.GetMessageNotification));
            reader.Read();
            var serializer = new JsonSerializer();

            var actual = (Message) converter.ReadJson(reader, typeof (Message), null, serializer);
            Assert.IsNotNull(actual);

            Assert.AreEqual(76767, actual.Id);
            Assert.AreEqual("Bob", actual.From);
            Assert.AreEqual(89898, actual.AccountId);
            Assert.AreEqual(3434, actual.WithAccountId);
            Assert.AreEqual("Test33", actual.LastMessage);
            Assert.AreEqual(2, actual.MessageNum);
            Assert.AreEqual(new DateTimeOffset(new DateTime(2015, 10, 12, 02, 31, 43, DateTimeKind.Utc)),
                actual.DateTime);
        }
    }
}