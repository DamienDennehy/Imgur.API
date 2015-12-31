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
        /// <param name="imageId">The image id.</param>
        /// <returns></returns>
        public async Task<IGalleryImage> GetGalleryImageAsync(string imageId)
        {
            throw new NotImplementedException();
        }
    }
}