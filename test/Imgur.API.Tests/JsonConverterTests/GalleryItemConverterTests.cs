using System;
using System.IO;
using System.Linq;
using Imgur.API.Enums;
using Imgur.API.JsonConverters;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.Mocks;
using Newtonsoft.Json;
using Xunit;

namespace Imgur.API.Tests.JsonConverterTests
{
    public class GalleryItemConverterTests
    {
        [Theory]
        [InlineData(typeof(GalleryAlbum), true)]
        [InlineData(typeof(GalleryImage), true)]
        [InlineData(typeof(GalleryItem), true)]
        [InlineData(typeof(DateTime), false)]
        [InlineData(typeof(string), false)]
        [InlineData(typeof(int), false)]
        [InlineData(typeof(bool), false)]
        [InlineData(typeof(float), false)]
        [InlineData(typeof(Image), false)]
        public void CanConvert(Type type, bool canConvert)
        {
            var converter = new GalleryItemConverter();

            Assert.Equal(converter.CanConvert(type), canConvert);
        }

        [Fact]
        public void GalleryItemConverter_ReadJsonGallery_Null()
        {
            var converter = new GalleryItemConverter();
            var reader = new JsonTextReader(new StringReader(""));
            reader.Read();
            var serializer = new JsonSerializer
            {
                Converters = {converter}
            };

            var actual = (GalleryAlbum) converter.ReadJson(reader, typeof(GalleryItem), null, serializer);
            Assert.Null(actual);
        }

        [Fact]
        public void GalleryItemConverter_ReadJsonGalleryAlbum_Equal()
        {
            var converter = new GalleryItemConverter();
            var reader = new JsonTextReader(new StringReader(MockAccountEndpointResponses.GetGalleryAlbum));
            reader.Read();
            var serializer = new JsonSerializer
            {
                Converters = {converter}
            };

            var actual = (GalleryAlbum) converter.ReadJson(reader, typeof(GalleryItem), null, serializer);
            Assert.NotNull(actual);

            Assert.Equal(null, actual.AccountId);
            Assert.Equal("SpaceCowboy02", actual.AccountUrl);
            Assert.Equal(null, actual.CommentCount);
            Assert.Equal("JsKDPBN", actual.Cover);
            Assert.Equal(240, actual.CoverHeight);
            Assert.Equal(500, actual.CoverWidth);
            Assert.Equal(new DateTimeOffset(new DateTime(2015, 09, 19, 16, 43, 54, DateTimeKind.Utc)),
                actual.DateTime);
            Assert.Equal(null, actual.Description);
            Assert.Equal(119, actual.Downs);
            Assert.Equal(true, actual.Favorite);
            Assert.Equal("LqLmI", actual.Id);
            Assert.Equal(9, actual.ImagesCount);
            Assert.Equal(0, actual.Images.Count());
            Assert.Equal(AlbumLayout.Blog, actual.Layout);
            Assert.Equal("http://imgur.com/a/LqLmI", actual.Link);
            Assert.Equal(null, actual.Nsfw);
            Assert.Equal(null, actual.Privacy);
            Assert.Equal(null, actual.Score);
            Assert.Equal("Game of Expectation and Reality", actual.Title);
            Assert.Equal(null, actual.Topic);
            Assert.Equal(null, actual.TopicId);
            Assert.Equal(4040, actual.Ups);
            Assert.Equal(96803, actual.Views);
            Assert.Equal(VoteOption.Down, actual.Vote);
        }

        [Fact]
        public void GalleryItemConverter_ReadJsonGalleryImage_Equal()
        {
            var converter = new GalleryItemConverter();
            var reader = new JsonTextReader(new StringReader(MockAccountEndpointResponses.GetGalleryImage));
            reader.Read();
            var serializer = new JsonSerializer
            {
                Converters = {converter}
            };

            var actual = (GalleryImage) converter.ReadJson(reader, typeof(GalleryItem), null, serializer);
            Assert.NotNull(actual);

            Assert.Equal(null, actual.AccountId);
            Assert.Equal("YoSoyPablo1", actual.AccountUrl);
            Assert.Equal(false, actual.Animated);
            Assert.Equal(118267772080, actual.Bandwidth);
            Assert.Equal(null, actual.CommentCount);
            Assert.Equal(new DateTimeOffset(new DateTime(2015, 09, 19, 20, 29, 47, DateTimeKind.Utc)),
                actual.DateTime);
            Assert.Equal(null, actual.DeleteHash);
            Assert.Equal(null, actual.Description);
            Assert.Equal(225, actual.Downs);
            Assert.Equal(true, actual.Favorite);
            Assert.Equal(null, actual.Gifv);
            Assert.Equal(720, actual.Height);
            Assert.Equal("http://i.imgur.com/l35eOVB.jpg", actual.Link);
            Assert.Equal(false, actual.Looping);
            Assert.Equal(null, actual.Mp4);
            Assert.Equal(null, actual.Nsfw);
            Assert.Equal(null, actual.Score);
            Assert.Equal(null, actual.Section);
            Assert.Equal(111952, actual.Size);
            Assert.Equal("Someone is happy. New gadgets arrived to Ahmed from Microsoft", actual.Title);
            Assert.Equal(null, actual.TopicId);
            Assert.Equal(null, actual.Topic);
            Assert.Equal("image/jpeg", actual.Type);
            Assert.Equal(3567, actual.Ups);
            Assert.Equal(1056415, actual.Views);
            Assert.Equal(VoteOption.Up, actual.Vote);
            Assert.Equal(960, actual.Width);
        }
    }
}