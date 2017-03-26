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
        ///     View gallery images for a subreddit.
        /// </summary>
        /// <param name="subreddit">A valid subreddit name. Example: pics, gaming</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetSubredditGalleryAsync(string subreddit,
            SubredditGallerySortOrder? sort = SubredditGallerySortOrder.Time, TimeWindow? window = TimeWindow.Week,
            int? page = null)
        {
            if (string.IsNullOrWhiteSpace(subreddit))
                throw new ArgumentNullException(nameof(subreddit));

            sort = sort ?? SubredditGallerySortOrder.Time;
            window = window ?? TimeWindow.Week;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();

            var url = $"gallery/r/{subreddit}/{sortValue}/{windowValue}/{page}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var gallery = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return gallery;
            }
        }

        /// <summary>
        ///     View a single image in the subreddit.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="subreddit">A valid subreddit name. Example: pics, gaming</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IGalleryImage> GetSubredditImageAsync(string imageId, string subreddit)
        {
            if (string.IsNullOrWhiteSpace(imageId))
                throw new ArgumentNullException(nameof(imageId));

            if (string.IsNullOrWhiteSpace(subreddit))
                throw new ArgumentNullException(nameof(subreddit));

            var url = $"gallery/r/{subreddit}/{imageId}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var image = await SendRequestAsync<GalleryImage>(request).ConfigureAwait(false);
                return image;
            }
        }
    }
}