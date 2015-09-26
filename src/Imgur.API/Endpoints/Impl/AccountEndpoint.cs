using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Exceptions;
using Imgur.API.Helpers;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Account related actions.
    /// </summary>
    public class AccountEndpoint : EndpointBase, IAccountEndpoint
    {
        private const string GetAccountUrl = "account/{0}";
        private const string GetAccountGalleryFavoritesUrl = "account/{0}/gallery_favorites/{1}/{2}";
        private const string GetAccountFavoritesUrl = "account/{0}/favorites";
        private const string GetAccountSubmissionsUrl = "account/{0}/submissions/{1}";

        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient"></param>
        public AccountEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Request standard user information.
        ///     If you need the username for the account that is logged in, it is returned in the request for an access token.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAccount> GetAccountAsync(string username = "me")
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountUrl);
            endpointUrl = string.Format(endpointUrl, username);
            var account = await MakeEndpointRequestAsync<Account>(HttpMethod.Get, endpointUrl, null);
            return account;
        }

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <param name="sortOrder">Indicates the order that a list of items are sorted. Default: Newest.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountGalleryFavoritesAsync(string username = "me",
            int? page = null,
            SortOrder? sortOrder = SortOrder.Newest)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountGalleryFavoritesUrl);
            endpointUrl = string.Format(endpointUrl, username, page, sortOrder);
            var favorites = await MakeEndpointRequestAsync<IEnumerable<object>>(HttpMethod.Get, endpointUrl, null);
            var imageHelper = new ImageHelper();
            return imageHelper.ConvertToGalleryItems(favorites);
        }

        /// <summary>
        ///     Returns the users favorited images, only accessible if you're logged in as the user.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountFavoritesAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountFavoritesUrl);
            endpointUrl = string.Format(endpointUrl, "me");
            var favorites = await MakeEndpointRequestAsync<IEnumerable<object>>(HttpMethod.Get, endpointUrl, null);
            var imageHelper = new ImageHelper();
            return imageHelper.ConvertToGalleryItems(favorites);
        }

        /// <summary>
        ///     Return the images a user has submitted to the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountSubmissionsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountSubmissionsUrl);
            endpointUrl = string.Format(endpointUrl, username, page);
            var submissions = await MakeEndpointRequestAsync<IEnumerable<object>>(HttpMethod.Get, endpointUrl, null);
            var imageHelper = new ImageHelper();
            return imageHelper.ConvertToGalleryItems(submissions);
        }
    }
}