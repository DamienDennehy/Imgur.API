using System;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     Get additional information about an image in the gallery.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<IGalleryImage> GetGalleryImageAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }
    }
}