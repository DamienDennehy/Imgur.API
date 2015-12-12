using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Exceptions;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class AccountEndpoint
    {
        private const string GetImagesUrl = "account/{0}/images/{1}";
        private const string GetImageUrl = "account/{0}/image/{1}";
        private const string GetImageIdsUrl = "account/{0}/images/ids/{1}";
        private const string GetImageCountUrl = "account/{0}/images/count";
        private const string DeleteImageUrl = "account/{0}/image/{1}";

        /// <summary>
        ///     Return all of the images associated with the account.
        ///     You can page through the images by setting the page, this defaults to 0.
        /// </summary>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<IImage>> GetImagesAsync(int? page = null)
        {
            throw new NotImplementedException();
            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetImagesUrl);
            //endpointUrl = string.Format(endpointUrl, "me", page);

            //return await MakeEndpointRequestAsync<IEnumerable<Image>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Return information about a specific image.
        /// </summary>
        /// <param name="id">The album's id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> GetImageAsync(string id, string username = "me")
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(id))
            //    throw new ArgumentNullException(nameof(id));

            //if (string.IsNullOrEmpty(username))
            //    throw new ArgumentNullException(nameof(username));

            //if (username.Equals("me", StringComparison.OrdinalIgnoreCase)
            //    && ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetImageUrl);
            //endpointUrl = string.Format(endpointUrl, username, id);
            //var image = await MakeEndpointRequestAsync<Image>(HttpMethod.Get, endpointUrl);
            //return image;
        }

        /// <summary>
        ///     Returns an array of Image IDs that are associated with the account.
        /// </summary>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetImageIdsAsync(int? page = null)
        {
            throw new NotImplementedException();
            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetImageIdsUrl);
            //endpointUrl = string.Format(endpointUrl, "me", page);

            //return await MakeEndpointRequestAsync<IEnumerable<string>>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Returns the total number of images associated with the account.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<int> GetImageCountAsync()
        {
            throw new NotImplementedException();
            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetImageCountUrl);
            //endpointUrl = string.Format(endpointUrl, "me");

            //return await MakeEndpointRequestAsync<int>(HttpMethod.Get, endpointUrl);
        }

        /// <summary>
        ///     Deletes an Image. This requires a delete hash rather than an ID.
        /// </summary>
        /// <param name="deleteHash"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(string deleteHash)
        {
            throw new NotImplementedException();
            //if (string.IsNullOrEmpty(deleteHash))
            //    throw new ArgumentNullException(nameof(deleteHash));

            //if (ApiClient.OAuth2Token == null)
            //    throw new ArgumentNullException(nameof(ApiClient.OAuth2Token));

            //var endpointUrl = string.Concat(GetEndpointBaseUrl(), DeleteImageUrl);
            //endpointUrl = string.Format(endpointUrl, "me", deleteHash);

            //return await MakeEndpointRequestAsync<bool>(HttpMethod.Delete, endpointUrl);
        }
    }
}