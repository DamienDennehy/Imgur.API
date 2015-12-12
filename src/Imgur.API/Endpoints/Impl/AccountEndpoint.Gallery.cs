using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Exceptions;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        private const string GetAccountGalleryFavoritesUrl = "account/{0}/gallery_favorites/{1}/{2}";
        private const string GetAccountFavoritesUrl = "account/{0}/favorites";
        private const string GetAccountSubmissionsUrl = "account/{0}/submissions/{1}";
        private const string GetGalleryProfileUrl = "account/{0}/gallery_profile";

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
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetGalleryProfileUrl);
            //endpointUrl = string.Format(endpointUrl, username);
            //var galleryProfile = await MakeEndpointRequestAsync<GalleryProfile>(HttpMethod.Get, endpointUrl);
            //return galleryProfile;
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
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountGalleryFavoritesUrl);
            //endpointUrl = string.Format(endpointUrl, username, page, gallerySortOrder.ToString().ToLower());
            //var favorites = await MakeEndpointRequestAsync<IEnumerable<GalleryItem>>(HttpMethod.Get, endpointUrl);
            //return favorites;
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
            throw new NotImplementedException();
            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountFavoritesUrl);
            //endpointUrl = string.Format(endpointUrl, "me");
            //var favorites = await MakeEndpointRequestAsync<IEnumerable<GalleryItem>>(HttpMethod.Get, endpointUrl);
            //return favorites;
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
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetAccountSubmissionsUrl);
            //endpointUrl = string.Format(endpointUrl, username, page);
            //var submissions = await MakeEndpointRequestAsync<IEnumerable<GalleryItem>>(HttpMethod.Get, endpointUrl);
            //return submissions;
        }
    }
}