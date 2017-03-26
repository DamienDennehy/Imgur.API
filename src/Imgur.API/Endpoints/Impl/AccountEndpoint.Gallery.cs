using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        internal GalleryRequestBuilder GalleryRequestBuilder { get; } = new GalleryRequestBuilder();

        /// <summary>
        ///     Returns the users favorited images. OAuth authentication required.
        /// </summary>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <param name="sort">The order that the account gallery should be sorted by. Default: Newest</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountFavoritesAsync(int? page = null,
            AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            sort = sort ?? AccountGallerySortOrder.Newest;

            var sortValue = $"{sort}".ToLower();
            var url = $"account/me/favorites/{page}/{sortValue}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var favorites = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return favorites;
            }
        }

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <param name="sort">The order that the account gallery should be sorted by. Default: Newest</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountGalleryFavoritesAsync(string username = "me",
            int? page = null,
            AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            sort = sort ?? AccountGallerySortOrder.Newest;

            var sortValue = $"{sort}".ToLower();
            var url = $"account/{username}/gallery_favorites/{page}/{sortValue}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var favorites = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return favorites;
            }
        }

        /// <summary>
        ///     Return the images a user has submitted to the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountSubmissionsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/{username}/submissions/{page}";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var submissions = await SendRequestAsync<IEnumerable<GalleryItem>>(request).ConfigureAwait(false);
                return submissions;
            }
        }

        /// <summary>
        ///     The totals for a users gallery information.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IGalleryProfile> GetGalleryProfileAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"account/me/gallery_profile";

            using (var request = RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var profile = await SendRequestAsync<GalleryProfile>(request).ConfigureAwait(false);
                return profile;
            }
        }
    }
}