using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Account related actions.
    /// </summary>
    public partial class AccountEndpoint : EndpointBase, IAccountEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the AccountEndpoint class.
        /// </summary>
        /// <param name="apiClient"></param>
        public AccountEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the AccountEndpoint class.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="httpClient"></param>
        internal AccountEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal AccountRequestBuilder RequestBuilder { get; } = new AccountRequestBuilder();

        /// <summary>
        ///     Request standard user information.
        ///     If you need the username for the account that is logged in, it is returned in the request for an access token.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAccount> GetAccountAsync(string username = "me")
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"{GetEndpointBaseUrl()}account/{username}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var account = await SendRequestAsync<Account>(request);
                return account;
            }
        }

        /// <summary>
        ///     Returns the account settings, only accessible if you're logged in as the user.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAccountSettings> GetAccountSettingsAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"{GetEndpointBaseUrl()}account/me/settings";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var settings = await SendRequestAsync<AccountSettings>(request);
                return settings;
            }
        }

        /// <summary>
        ///     Updates the account settings for a given user, the user must be logged in.
        /// </summary>
        /// <param name="bio">The biography of the user, is displayed in the gallery profile page.</param>
        /// <param name="publicImages">Set the users images to private or public by default.</param>
        /// <param name="messagingEnabled">Allows the user to enable / disable private messages.</param>
        /// <param name="albumPrivacy">Sets the default privacy level of albums the users creates.</param>
        /// <param name="acceptedGalleryTerms"> The user agreement to the Imgur Gallery terms.</param>
        /// <param name="username">A valid Imgur username (between 4 and 63 alphanumeric characters).</param>
        /// <param name="showMature">Toggle display of mature images in gallery list endpoints.</param>
        /// <param name="newsletterSubscribed">Toggle subscription to email newsletter.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> UpdateAccountSettingsAsync(
            string bio = null,
            bool? publicImages = null,
            bool? messagingEnabled = null,
            AlbumPrivacy? albumPrivacy = null,
            bool? acceptedGalleryTerms = null,
            string username = null,
            bool? showMature = null,
            bool? newsletterSubscribed = null)
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"{GetEndpointBaseUrl()}account/me/settings";

            using (
                var request = RequestBuilder.UpdateAccountSettingsRequest(url, bio, publicImages, messagingEnabled,
                    albumPrivacy, acceptedGalleryTerms, username, showMature, newsletterSubscribed))
            {
                var updated = await SendRequestAsync<bool>(request);
                return updated;
            }
        }

        /// <summary>
        ///     Checks to see if user has verified their email address.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> VerifyEmailAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"{GetEndpointBaseUrl()}account/me/verifyemail";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var verified = await SendRequestAsync<bool>(request);
                return verified;
            }
        }

        /// <summary>
        ///     Sends an email to the user to verify that their email is valid to upload to gallery.
        ///     Must be logged in as the user to send.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> SendVerificationEmailAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"{GetEndpointBaseUrl()}account/me/verifyemail";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var verified = await SendRequestAsync<bool>(request);
                return verified;
            }
        }
    }
}