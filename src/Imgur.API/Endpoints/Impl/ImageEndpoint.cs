using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Image related actions.
    /// </summary>
    public class ImageEndpoint : EndpointBase, IImageEndpoint
    {
        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        public ImageEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient">The type of client that will be used for authentication.</param>
        /// <param name="httpClient"> The class for sending HTTP requests and receiving HTTP responses from the endpoint methods.</param>
        internal ImageEndpoint(IApiClient apiClient, HttpClient httpClient) : base(apiClient, httpClient)
        {
        }

        internal ImageRequestBuilder RequestBuilder { get; } = new ImageRequestBuilder();

        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"image/{id}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Delete, url))
            {
                var deleted = await SendRequestAsync<bool>(request);
                return deleted;
            }
        }

        /// <summary>
        ///     Favorite an image with the given ID. OAuth authentication required.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> FavoriteImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var url = $"image/{id}/favorite";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Post, url))
            {
                //The structure of the response of favoriting an image
                //varies on if Imgur or Mashape Authentication is used
                if (ApiClient is IImgurClient)
                {
                    var imgurResult = await SendRequestAsync<string>(request);
                    return imgurResult.Equals("favorited", StringComparison.OrdinalIgnoreCase);
                }

                //If Mashape Authentication is used, the favorite is returned as an exception
                try
                {
                    await SendRequestAsync<string>(request);
                }
                catch (ImgurException imgurException)
                {
                    return imgurException.Message.Equals("f", StringComparison.OrdinalIgnoreCase);
                }
            }

            return false;
        }

        /// <summary>
        ///     Get information about an image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IImage> GetImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"image/{id}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var image = await SendRequestAsync<Image>(request);
                return image;
            }
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> UpdateImageAsync(string id, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var url = $"image/{id}";

            using (var request = RequestBuilder.UpdateImageRequest(url, title, description))
            {
                var updated = await SendRequestAsync<bool>(request);
                return updated;
            }
        }

        /// <summary>
        ///     Upload a new image using a binary file.
        /// </summary>
        /// <param name="image">A binary file.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IImage> UploadImageBinaryAsync(byte[] image, string album = null, string title = null,
            string description = null)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var url = "image";

            using (var request = RequestBuilder.UploadImageBinaryRequest(url, image, album, title, description))
            {
                var returnImage = await SendRequestAsync<Image>(request);
                return returnImage;
            }
        }

        /// <summary>
        ///     Upload a new image using a stream.
        /// </summary>
        /// <param name="image">A stream.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IImage> UploadImageStreamAsync(Stream image, string album = null, string title = null,
            string description = null)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            var url = "image";

            using (var request = RequestBuilder.UploadImageStreamRequest(url, image, album, title, description))
            {
                var returnImage = await SendRequestAsync<Image>(request);
                return returnImage;
            }
        }

        /// <summary>
        ///     Upload a new image using a URL.
        /// </summary>
        /// <param name="image">The URL for the image.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IImage> UploadImageUrlAsync(string image, string album = null, string title = null,
            string description = null)
        {
            if (string.IsNullOrEmpty(image))
                throw new ArgumentNullException(nameof(image));

            var url = "image";

            using (var request = RequestBuilder.UploadImageUrlRequest(url, image, album, title, description))
            {
                var returnImage = await SendRequestAsync<Image>(request);
                return returnImage;
            }
        }
    }
}