using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        internal GalleryRequestBuilder GalleryRequestBuilder { get; } = new GalleryRequestBuilder();

        /// <summary>
        ///     The totals for a users gallery information.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IGalleryProfile> GetGalleryProfileAsync(string username = "me")
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/gallery_profile";

            using (var request = ImageRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var profile = await SendRequestAsync<GalleryProfile>(request);
                return profile;
            }
        }

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <param name="sort">Indicates the order that a list of items are sorted. Default: Newest.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountGalleryFavoritesAsync(string username = "me",
            int? page = null,
            AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var sortValue = $"{sort ?? AccountGallerySortOrder.Newest}".ToLower();
            var url = $"account/{username}/gallery_favorites/{page}/{sortValue}";

            using (var request = ImageRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var favorites = await SendRequestAsync<IEnumerable<IGalleryItem>>(request);
                return favorites;
            }
        }

        /// <summary>
        ///     Returns the users favorited images. OAuth authentication required.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountFavoritesAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = "account/me/favorites";

            using (var request = ImageRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var favorites = await SendRequestAsync<IEnumerable<IGalleryItem>>(request);
                return favorites;
            }
        }

        /// <summary>
        ///     Return the images a user has submitted to the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountSubmissionsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/submissions/{page}";

            using (var request = ImageRequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var submissions = await SendRequestAsync<IEnumerable<IGalleryItem>>(request);
                return submissions;
            }
        }
    }
}