using System;
using System.Collections.Generic;
using System.Linq;
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
        private const string GetGalleryProfileUrl = "account/{0}/gallery_profile";
        private const string AccountSettingsUrl = "account/{0}/settings";
        private const string VerifyEmailUrl = "account/{0}/verifyemail";
        private const string GetAlbumsUrl = "account/{0}/albums/{1}";
        private const string GetAlbumUrl = "account/{0}/album/{1}";
        private const string GetAlbumIdsUrl = "account/{0}/albums/ids/{1}";
        private const string GetAlbumsCountUrl = "account/{0}/albums/count";
        private const string DeleteAlbumUrl = "account/{0}/album/{1}";
        private const string GetCommentsUrl = "account/{0}/comments/{1}/{2}";

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
            var account = await MakeEndpointRequestAsync<Account>(HttpMethod.Get, endpointUrl);
            return account;
        }

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <param name="gallerySortOrder">Indicates the order that a list of items are sorted. Default: Newest.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IGalleryItem>> GetAccountGalleryFavoritesAsync(string username = "me",
            int? page = null,
            GallerySortOrder? gallerySortOrder = GallerySortOrder.Newest)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountGalleryFavoritesUrl);
            endpointUrl = string.Format(endpointUrl, username, page, gallerySortOrder.ToString().ToLower());
            var favorites = await MakeEndpointRequestAsync<IEnumerable<object>>(HttpMethod.Get, endpointUrl);
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
            var favorites = await MakeEndpointRequestAsync<IEnumerable<object>>(HttpMethod.Get, endpointUrl);
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
            var submissions = await MakeEndpointRequestAsync<IEnumerable<object>>(HttpMethod.Get, endpointUrl);
            var imageHelper = new ImageHelper();
            return imageHelper.ConvertToGalleryItems(submissions);
        }

        /// <summary>
        ///     Returns the account settings, only accessible if you're logged in as the user.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAccountSettings> GetAccountSettingsAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), AccountSettingsUrl);
            endpointUrl = string.Format(endpointUrl, "me");
            var settings = await MakeEndpointRequestAsync<AccountSettings>(HttpMethod.Get, endpointUrl);
            return settings;
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
        /// <exception cref="ArgumentOutOfRangeException"></exception>
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
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), AccountSettingsUrl);
            endpointUrl = string.Format(endpointUrl, "me");

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(bio))
                parameters.Add(nameof(bio), bio);

            if (publicImages != null)
                parameters.Add("public_images", publicImages.Value.ToString().ToLower());

            if (messagingEnabled != null)
                parameters.Add("messaging_enabled", messagingEnabled.Value.ToString().ToLower());

            if (albumPrivacy != null)
                parameters.Add("album_privacy", albumPrivacy.ToString().ToLower());

            if (acceptedGalleryTerms != null)
                parameters.Add("accepted_gallery_terms", acceptedGalleryTerms.Value.ToString().ToLower());

            if (!string.IsNullOrWhiteSpace(username))
                parameters.Add("username", username.ToLower());

            if (showMature != null)
                parameters.Add("show_mature", showMature.Value.ToString().ToLower());

            if (newsletterSubscribed != null)
                parameters.Add("newsletter_subscribed", newsletterSubscribed.Value.ToString().ToLower());

            var content = new FormUrlEncodedContent(parameters.ToArray());

            return await MakeEndpointRequestAsync<bool>(HttpMethod.Post, endpointUrl, content);
        }

        /// <summary>
        ///     The totals for a users gallery information.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IGalleryProfile> GetGalleryProfileAsync(string username = "me")
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetGalleryProfileUrl);
            endpointUrl = string.Format(endpointUrl, username);
            var galleryProfile = await MakeEndpointRequestAsync<GalleryProfile>(HttpMethod.Get, endpointUrl);
            return galleryProfile;
        }

        /// <summary>
        ///     Checks to see if user has verified their email address.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> VerifyEmailAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), VerifyEmailUrl);
            endpointUrl = string.Format(endpointUrl, "me");

            return await MakeEndpointRequestAsync<bool>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Sends an email to the user to verify that their email is valid to upload to gallery.
        ///     Must be logged in as the user to send.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> SendVerificationEmailAsync()
        {
            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), VerifyEmailUrl);
            endpointUrl = string.Format(endpointUrl, "me");

            return await MakeEndpointRequestAsync<bool>(HttpMethod.Post, endpointUrl);
        }

        /// <summary>
        ///     Get all the albums associated with the account.
        ///     Must be logged in as the user to see secret and hidden albums.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IAlbum>> GetAlbumsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumsUrl);
            endpointUrl = string.Format(endpointUrl, username, page);

            return await MakeEndpointRequestAsync<IEnumerable<Album>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Get additional information about an album, this works the same as the Album Endpoint.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IAlbum> GetAlbumAsync(string id, string username = "me")
        {
            if (string.IsNullOrEmpty((id)))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumUrl);
            endpointUrl = string.Format(endpointUrl, username, id);
            var album = await MakeEndpointRequestAsync<Album>(HttpMethod.Get, endpointUrl);
            return album;
        }

        /// <summary>
        ///     Return an array of all of the album IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetAlbumIdsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumIdsUrl);
            endpointUrl = string.Format(endpointUrl, username, page);

            return await MakeEndpointRequestAsync<IEnumerable<string>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Return an array of all of the album IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<int> GetAlbumCountAsync(string username = "me")
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAlbumsCountUrl);
            endpointUrl = string.Format(endpointUrl, username);

            return await MakeEndpointRequestAsync<int>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        /// Delete an Album with a given id.
        /// </summary>
        /// <param name="id">The album id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteAlbumAsync(string id, string username = "me")
        {
            if (string.IsNullOrEmpty((id)))
                throw new ArgumentNullException(nameof(id));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), DeleteAlbumUrl);
            endpointUrl = string.Format(endpointUrl, "me", id);

            return await MakeEndpointRequestAsync<bool>(HttpMethod.Delete, endpointUrl);
        }

        /// <summary>
        /// Return the comments the user has created.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="commentSortOrder">'best', 'worst', 'oldest', or 'newest'. Defaults to 'newest'.</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IComment>> GetCommentsAsync(string username = "me",
            CommentSortOrder commentSortOrder = CommentSortOrder.Newest, int? page = null)
        {
            if (string.IsNullOrEmpty((username)))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetCommentsUrl);
            endpointUrl = string.Format(endpointUrl, username, commentSortOrder.ToString().ToLower(), page);

            return await MakeEndpointRequestAsync<IEnumerable<Comment>>(HttpMethod.Get, endpointUrl);
        }

    }
}