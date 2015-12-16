using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        internal ImageRequestBuilder ImageRequestBuilder { get; } = new ImageRequestBuilder();

        /// <summary>
        ///     Return all of the images associated with the account.
        ///     You can page through the images by setting the page, this defaults to 0.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IImage>> GetImagesAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var url = $"{GetEndpointBaseUrl()}account/{username}/images/{page}";

            using (var request = ImageRequestBuilder.GetImagesRequest(url))
            {
                var images = await SendRequestAsync<IEnumerable<Image>>(request);
                return images;
            }
        }

        /// <summary>
        ///     Return information about a specific image.
        /// </summary>
        /// <param name="id">The image's id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> GetImageAsync(string id, string username = "me")
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));
            
            var url = $"{GetEndpointBaseUrl()}account/{username}/image/{id}";

            using (var request = ImageRequestBuilder.GetImageRequest(url))
            {
                var image = await SendRequestAsync<Image>(request);
                return image;
            }
        }

        /// <summary>
        ///     Returns an array of Image IDs that are associated with the account.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetImageIdsAsync(string username = "me", int? page = null)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var url = $"{GetEndpointBaseUrl()}account/{username}/images/ids/{page}";

            using (var request = ImageRequestBuilder.GetImageIdsRequest(url))
            {
                var images = await SendRequestAsync<IEnumerable<string>>(request);
                return images;
            }
        }

        /// <summary>
        ///     Returns the total number of images associated with the account.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<int> GetImageCountAsync(string username = "me")
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var url = $"{GetEndpointBaseUrl()}account/{username}/images/count";

            using (var request = ImageRequestBuilder.GetImageCountRequest(url))
            {
                var count = await SendRequestAsync<int>(request);
                return count;
            }
        }

        /// <summary>
        ///     Deletes an Image. This requires a delete hash rather than an ID.
        /// </summary>
        /// <param name="deleteHash"></param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(string deleteHash, string username = "me")
        {
            if (string.IsNullOrEmpty(deleteHash))
                throw new ArgumentNullException(nameof(deleteHash));

            if (string.IsNullOrEmpty(username))
                throw new ArgumentNullException(nameof(username));

            if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
                && ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            var url = $"{GetEndpointBaseUrl()}account/{username}/image/{deleteHash}";

            using (var request = ImageRequestBuilder.DeleteImageRequest(url))
            {
                var deleted = await SendRequestAsync<bool>(request);
                return deleted;
            }
        }
    }
}