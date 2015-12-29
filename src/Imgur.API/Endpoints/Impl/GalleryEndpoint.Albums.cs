using System;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     Get additional information about an album in the gallery.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<IGalleryAlbum> GetGalleryAlbumAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }
    }
}