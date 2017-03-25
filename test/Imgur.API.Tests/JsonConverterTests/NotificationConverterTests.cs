using System;
using System.IO;
using System.Linq;
using Imgur.API.JsonConverters;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.Mocks;
using Newtonsoft.Json;
using Xunit;

namespace Imgur.API.Tests.JsonConverterTests
{
    public class NotificationConverterTests
    {
        [Theory]
        [InlineData(typeof(CommentNotification), true)]
        [InlineData(typeof(MessageNotification), true)]
        [InlineData(typeof(GalleryAlbum), false)]
        [InlineData(typeof(GalleryImage), false)]
        [InlineData(typeof(GalleryItem), false)]
        [InlineData(typeof(Image), false)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(bool), false)]
        [InlineData(typeof(float), false)]
        public void CanConvert(Type type, bool canConvert)
        {
            var converter = new NotificationConverter();

            Assert.Equal(converter.CanConvert(type), canConvert);
        }

        [Fact]
        public void NotificationConverter_ReadJsonCommentNotification_Equal()
        {
            var converter = new NotificationConverter();
            var reader =
                new JsonTextReader(new StringReader(MockAccountEndpointResponses.GetCommentNotification));
            reader.Read();
            var serializer = new JsonSerializer();

            var actual =
                (CommentNotification) converter.ReadJson(reader, typeof(CommentNotification), null, serializer);
            Assert.NotNull(actual);

            Assert.Equal(null, actual.AlbumCover);
            Assert.Equal("jasdev", actual.Author);
            Assert.Equal(3698510, actual.AuthorId);
            Assert.Equal(0, actual.Children.Count());
            Assert.Equal("Reply test", actual.CommentText);
            Assert.Equal(new DateTimeOffset(new DateTime(2014, 07, 22, 23, 12, 54, DateTimeKind.Utc)),
                actual.DateTime);
            Assert.Equal(false, actual.Deleted);
            Assert.Equal(0, actual.Downs);
            Assert.Equal("VK9VqcM", actual.ImageId);
            Assert.Equal(false, actual.OnAlbum);
            Assert.Equal(3615, actual.ParentId);
            Assert.Equal(1, actual.Points);
            Assert.Equal(1, actual.Ups);
            Assert.Equal(3616, actual.Id);
        }

        [Fact]
        public void NotificationConverter_ReadJsonMessageNotification_Equal()
        {
            var converter = new NotificationConverter();
            var reader =
                new JsonTextReader(new StringReader(MockAccountEndpointResponses.GetMessageNotification));
            reader.Read();
            var serializer = new JsonSerializer();

            var actual =
                (MessageNotification) converter.ReadJson(reader, typeof(MessageNotification), null, serializer);
            Assert.NotNull(actual);

            Assert.Equal(76767, actual.Id);
            Assert.Equal("Bob", actual.From);
            Assert.Equal(89898, actual.AccountId);
            Assert.Equal(3434, actual.WithAccount);
            Assert.Equal("Test33", actual.LastMessage);
            Assert.Equal(2, actual.MessageNum);
            Assert.Equal(new DateTimeOffset(new DateTime(2015, 10, 12, 02, 31, 43, DateTimeKind.Utc)),
                actual.DateTime);
        }
    }
}