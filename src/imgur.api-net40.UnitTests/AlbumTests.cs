using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Imgur.API.Authentication;
using Imgur.API.Endpoints;

namespace Imgur.API.UnitTests
{
    /// <summary>
    /// Summary description for AlbumTests
    /// </summary>
    [TestClass]
    public class AlbumTests
    {
        private IImgurClient _client;
        private IAlbumEndpoint _endpoint;

        public AlbumTests()
        {
            this._client = new Authentication.Impl.ImgurClient("", "");
            this._endpoint = new Endpoints.Impl.AlbumEndpoint(this._client);
        }

        [TestMethod]
        public void GetAlbum()
        {
            // Arrange
            var albumId = "";

            // Act
            var album = this._endpoint.GetAlbum(albumId);

            // Assert
            Assert.IsNotNull(album);
        }
    }
}
