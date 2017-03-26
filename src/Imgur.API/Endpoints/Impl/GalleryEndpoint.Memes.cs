using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     View images for memes subgallery.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetMemesSubGalleryAsync(
            MemesGallerySortOrder? sort = MemesGallerySortOrder.Viral,
            TimeWindow? window = TimeWindow.Week, int? page = null)
        {
            sort = sort ?? MemesGallerySortOrder.Viral;
            window = window ?? TimeWindow.Week;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();

            var url = $"g/memes/{sortValue}/{windowValue}/{page}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var gallery = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return gallery;
            }
        }

        /// <summary>
        ///     View a single image in the memes gallery.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IGalleryImage> GetMemesSubGalleryImageAsync(string imageId)
        {
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ArgumentNullException(nameof(imageId));

            var url = $"gallery/image/{imageId}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var image = await SendRequestAsync<GalleryImage>(request).ConfigureAwait(false);
                return image;
            }
        }
    }
}