using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Helpers;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.EndpointResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NSubstitute;

namespace Imgur.API.Tests.Helpers
{
    [TestClass]
    public class ImageHelperTests
    {
        private ImageHelper ImageHelper { get; } = new ImageHelper();

        [TestMethod]
        public void ConvertToGalleryItems_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var galleryObjects = endpoint.ProcessEndpointResponse<IEnumerable<object>>(AccountEndpointResponses.Imgur.GetAccountGalleryFavoritesResponse);
         
            Assert.AreEqual(galleryObjects.Count(), ImageHelper.ConvertToGalleryItems(galleryObjects).Count());
        }
    }
}
