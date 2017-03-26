using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Gallery related actions.
    /// </summary>
    public partial class GalleryEndpoint : EndpointBase, IGalleryEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the GalleryEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public GalleryEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the GalleryEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal GalleryEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal GalleryRequestBuilder RequestBuilder { get; } = new GalleryRequestBuilder();

        /// <summary>
        ///     Returns the images in the gallery.
        /// </summary>
        /// <param name="section">The gallery section. Default: Hot</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <param name="showViral">Show or hide viral images from the 'user' section. Default: true</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetGalleryAsync(GallerySection? section = GallerySection.Hot,
            GallerySortOrder? sort = GallerySortOrder.Viral, TimeWindow? window = TimeWindow.Day, int? page = null,
            bool? showViral = true)
        {
            section = section ?? GallerySection.Hot;
            sort = sort ?? GallerySortOrder.Viral;
            window = window ?? TimeWindow.Week;
            showViral = showViral ?? true;

            var sectionValue = $"{section}".ToLower();
            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();
            var showViralValue = $"{showViral}".ToLower();

            var url = $"gallery/{sectionValue}/{sortValue}/{windowValue}/{page}?showViral={showViralValue}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var gallery = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return gallery;
            }
        }

        /// <summary>
        ///     Returns a random set of gallery images.
        /// </summary>
        /// <param name="page">A page of random gallery images, from 0-50. Pages are regenerated every hour.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetRandomGalleryAsync(int? page = null)
        {
            var url = $"gallery/random/random/{page}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var gallery = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return gallery;
            }
        }

        /// <summary>
        ///     Share an Album or Image to the Gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="title">The title of the image. This is required.</param>
        /// <param name="topicId">The topic id - not the topic name.</param>
        /// <param name="bypassTerms">
        ///     If the user has not accepted the terms yet, this endpoint will return an error. To by-pass
        ///     the terms in general simply set this value to true.
        /// </param>
        /// <param name="mature">If the post is mature, set this value to true.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> PublishToGalleryAsync(string galleryItemId, string title, string topicId = null,
            bool? bypassTerms = null,
            bool? mature = null)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentNullException(nameof(title));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"gallery/{galleryItemId}";

            using (var request = GalleryRequestBuilder.PublishToGalleryRequest(url, title, topicId, bypassTerms, mature))
            {
                var published = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return published;
            }
        }

        /// <summary>
        ///     Remove an item from the gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> RemoveFromGalleryAsync(string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"gallery/{galleryItemId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Delete, url))
            {
                var removed = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return removed;
            }
        }

        /// <summary>
        ///     Report an item in the gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="reason">A reason why content is inappropriate.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> ReportGalleryItemAsync(string galleryItemId, ReportReason reason)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"gallery/{galleryItemId}/report";

            using (var request = RequestBuilderBase.ReportItemRequest(url, reason))
            {
                var reported = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return reported;
            }
        }

        /// <summary>
        ///     Search the gallery with a given query string.
        /// </summary>
        /// <param name="qAll">Search for all of these words (and).</param>
        /// <param name="qAny">Search for any of these words (or).</param>
        /// <param name="qExactly">Search for exactly this word or phrase.</param>
        /// <param name="qNot">Exclude results matching this word or phrase.</param>
        /// <param name="fileType">Show results for a specific file type.</param>
        /// <param name="imageSize">Show results for a specific image size.</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> SearchGalleryAdvancedAsync(
            string qAll = null, string qAny = null,
            string qExactly = null, string qNot = null,
            ImageFileType? fileType = null, ImageSize? imageSize = null,
            GallerySortOrder? sort = GallerySortOrder.Time,
            TimeWindow? window = TimeWindow.All, int? page = null)
        {
            if (string.IsNullOrWhiteSpace(qAll) &&
                string.IsNullOrWhiteSpace(qAny) &&
                string.IsNullOrWhiteSpace(qExactly) &&
                string.IsNullOrWhiteSpace(qNot))
                throw new ArgumentNullException(null,
                    "At least one search parameter must be provided (All | Any | Exactly | Not).");

            sort = sort ?? GallerySortOrder.Time;
            window = window ?? TimeWindow.All;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();

            var url = $"gallery/search/{sortValue}/{windowValue}/{page}";
            url = GalleryRequestBuilder.SearchGalleryAdvancedRequest(url, qAll, qAny, qExactly, qNot, fileType,
                imageSize);

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var searchResults = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return searchResults;
            }
        }

        /// <summary>
        ///     Search the gallery with a given query string.
        /// </summary>
        /// <param name="query">
        ///     Query string to search by. This parameter also supports boolean operators (AND, OR, NOT) and
        ///     indices (tag: user: title: ext: subreddit: album: meme:). An example compound query would be 'title: cats AND dogs
        ///     ext: gif'
        /// </param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> SearchGalleryAsync(string query,
            GallerySortOrder? sort = GallerySortOrder.Time,
            TimeWindow? window = TimeWindow.All, int? page = null)
        {
            if (string.IsNullOrWhiteSpace(query))
                throw new ArgumentNullException(nameof(query));

            sort = sort ?? GallerySortOrder.Time;
            window = window ?? TimeWindow.All;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();

            var url = $"gallery/search/{sortValue}/{windowValue}/{page}";
            url = GalleryRequestBuilder.SearchGalleryRequest(url, query);

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var searchResults = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return searchResults;
            }
        }
    }
}