using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     View images for memes subgallery.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetMemesSubGalleryAsync(MemesGallerySortOrder? sort, Window? window,
            int? page = null)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     View a single image in the memes gallery.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <returns></returns>
        public async Task<IGalleryImage> GetMemesSubGalleryImageAsync(string imageId)
        {
            throw new NotImplementedException();
        }
    }
}