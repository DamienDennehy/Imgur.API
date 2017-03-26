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
    ///     Custom Gallery related actions.
    /// </summary>
    public class CustomGalleryEndpoint : EndpointBase, ICustomGalleryEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the CustomGalleryEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public CustomGalleryEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the CustomGalleryEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal CustomGalleryEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal CustomGalleryRequestBuilder RequestBuilder { get; } = new CustomGalleryRequestBuilder();

        /// <summary>
        ///     Add tags to a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tags">The tags that should be added.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> AddCustomGalleryTagsAsync(IEnumerable<string> tags)
        {
            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "g/custom/add_tags";

            using (var request = CustomGalleryRequestBuilder.AddCustomGalleryTagsRequest(url, tags))
            {
                var added = await SendRequestAsync<bool?>(request).ConfigureAwait(false);
                return added ?? true;
            }
        }

        /// <summary>
        ///     Add tags to filter out.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tag">The tag that should be filtered out.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> AddFilteredOutGalleryTagAsync(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "g/block_tag";

            using (var request = CustomGalleryRequestBuilder.AddFilteredOutGalleryTagRequest(url, tag))
            {
                var added = await SendRequestAsync<bool?>(request).ConfigureAwait(false);
                return added ?? true;
            }
        }

        /// <summary>
        ///     View images for current user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<ICustomGallery> GetCustomGalleryAsync(
            CustomGallerySortOrder? sort = CustomGallerySortOrder.Viral, TimeWindow? window = TimeWindow.Week,
            int? page = null)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            sort = sort ?? CustomGallerySortOrder.Viral;
            window = window ?? TimeWindow.Week;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();
            var url = $"g/custom/{sortValue}/{windowValue}/{page}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var gallery = await SendRequestAsync<CustomGallery>(request).ConfigureAwait(false);
                return gallery;
            }
        }

        /// <summary>
        ///     View a single item in a user's custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IGalleryItem> GetCustomGalleryItemAsync(string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"g/custom/{galleryItemId}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var galleryItem = await SendRequestAsync<GalleryItem>(request).ConfigureAwait(false);
                return galleryItem;
            }
        }

        /// <summary>
        ///     Retrieve user's filtered out gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<ICustomGallery> GetFilteredOutGalleryAsync(
            CustomGallerySortOrder? sort = CustomGallerySortOrder.Viral, TimeWindow? window = TimeWindow.Week,
            int? page = null)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            sort = sort ?? CustomGallerySortOrder.Viral;
            window = window ?? TimeWindow.Week;

            var sortValue = $"{sort}".ToLower();
            var windowValue = $"{window}".ToLower();
            var url = $"g/filtered/{sortValue}/{windowValue}/{page}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var gallery = await SendRequestAsync<CustomGallery>(request).ConfigureAwait(false);
                return gallery;
            }
        }

        /// <summary>
        ///     Remove tags from a custom gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tags">The tags that should be removed.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> RemoveCustomGalleryTagsAsync(IEnumerable<string> tags)
        {
            if (tags == null)
                throw new ArgumentNullException(nameof(tags));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "g/custom/remove_tags";

            using (var request = CustomGalleryRequestBuilder.RemoveCustomGalleryTagsRequest(url, tags))
            {
                var removed = await SendRequestAsync<bool?>(request).ConfigureAwait(false);
                return removed ?? true;
            }
        }

        /// <summary>
        ///     Remove a filtered out tag.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="tag">The tag that should be removed.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> RemoveFilteredOutGalleryTagAsync(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new ArgumentNullException(nameof(tag));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "g/unblock_tag";

            using (var request = CustomGalleryRequestBuilder.RemoveFilteredOutGalleryTagRequest(url, tag))
            {
                var removed = await SendRequestAsync<bool?>(request).ConfigureAwait(false);
                return removed ?? true;
            }
        }
    }
}