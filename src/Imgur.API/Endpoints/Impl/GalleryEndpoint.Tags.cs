using System;
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
        ///     View tags for a gallery item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<ITagVotes> GetGalleryItemTagsAsync(string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var url = $"gallery/{galleryItemId}/tags";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var tagVotes = await SendRequestAsync<TagVotes>(request).ConfigureAwait(false);
                return tagVotes;
            }
        }

        /// <summary>
        ///     View images for a gallery tag.
        /// </summary>
        /// <param name="tag">The name of the tag.</param>
        /// <param name="sort">The order that the images in the gallery tag should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<ITag> GetGalleryTagAsync(string tag, GalleryTagSortOrder? sort = GalleryTagSortOrder.Viral,
            TimeWindow? window = TimeWindow.Week, int? page = null)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            sort = sort ?? GalleryTagSortOrder.Viral;
            window = window ?? TimeWindow.Week;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();

            var url = $"gallery/t/{tag}/{sortValue}/{windowValue}/{page}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var returnTag = await SendRequestAsync<Tag>(request).ConfigureAwait(false);
                return returnTag;
            }
        }

        /// <summary>
        ///     View a single item in a gallery tag.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="tag">The name of the tag.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IGalleryItem> GetGalleryTagImageAsync(string galleryItemId, string tag)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            var url = $"gallery/t/{tag}/{galleryItemId}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var image = await SendRequestAsync<GalleryImage>(request).ConfigureAwait(false);
                return image;
            }
        }
    }
}