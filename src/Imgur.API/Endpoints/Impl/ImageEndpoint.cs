using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Image related actions.
    /// </summary>
    public class ImageEndpoint : EndpointBase, IImageEndpoint
    {
        private const string UploadImageUrl = "image";
        private const string GetImageUrl = "image/{0}";
        private const string UpdateImageUrl = "image/{0}";
        private const string DeleteImageUrl = "image/{0}";
        private const string FavoriteImageUrl = "image/{0}/favorite";

        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="apiClient"></param>
        public ImageEndpoint(IApiClient apiClient) : base(apiClient)
        {
        }

        /// <summary>
        ///     Get information about an image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> GetImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetImageUrl);
            endpointUrl = string.Format(endpointUrl, id);
            var image = await MakeEndpointRequestAsync<Image>(HttpMethod.Get, endpointUrl);
            return image;
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> UploadImageBinaryAsync(byte[] image, string album = null, string title = null,
            string description = null)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            IImage returnImage;

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), UploadImageUrl);

            using (var content = new MultipartFormDataContent(DateTime.UtcNow.Ticks.ToString()))
            {
                content.Add(new StringContent("type"), "file");
                content.Add(new ByteArrayContent(image), nameof(image));

                if (!string.IsNullOrWhiteSpace(album))
                    content.Add(new StringContent(album), nameof(album));

                if (!string.IsNullOrWhiteSpace(title))
                    content.Add(new StringContent(title), nameof(title));

                if (!string.IsNullOrWhiteSpace(description))
                    content.Add(new StringContent(description), nameof(description));

                returnImage = await MakeEndpointRequestAsync<Image>(HttpMethod.Post, endpointUrl, content);
            }

            return returnImage;
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> UploadImageStreamAsync(Stream image, string album = null, string title = null,
            string description = null)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            IImage returnImage;

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), UploadImageUrl);

            using (var content = new MultipartFormDataContent(DateTime.UtcNow.Ticks.ToString()))
            {
                content.Add(new StringContent("type"), "file");
                content.Add(new StreamContent(image), nameof(image));

                if (!string.IsNullOrWhiteSpace(album))
                    content.Add(new StringContent(album), nameof(album));

                if (!string.IsNullOrWhiteSpace(title))
                    content.Add(new StringContent(title), nameof(title));

                if (!string.IsNullOrWhiteSpace(description))
                    content.Add(new StringContent(description), nameof(description));

                returnImage = await MakeEndpointRequestAsync<Image>(HttpMethod.Post, endpointUrl, content);
            }

            return returnImage;
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
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<IImage> UploadImageUrlAsync(string image, string album = null, string title = null,
            string description = null)
        {
            if (string.IsNullOrEmpty(image))
                throw new ArgumentNullException(nameof(image));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), UploadImageUrl);

            var parameters = new Dictionary<string, string>
            {
                {"type", "URL"},
                {"image", image}
            };

            if (!string.IsNullOrWhiteSpace(album))
                parameters.Add(nameof(album), album);

            if (!string.IsNullOrWhiteSpace(title))
                parameters.Add(nameof(title), title);

            if (!string.IsNullOrWhiteSpace(description))
                parameters.Add(nameof(description), description);

            var content = new FormUrlEncodedContent(parameters.ToArray());

            IImage returnImage = await MakeEndpointRequestAsync<Image>(HttpMethod.Post, endpointUrl, content);

            return returnImage;
        }

        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> DeleteImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), DeleteImageUrl);
            endpointUrl = string.Format(endpointUrl, id);

            return await MakeEndpointRequestAsync<bool>(HttpMethod.Delete, endpointUrl);
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> UpdateImageAsync(string id, string title = null, string description = null)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), UpdateImageUrl);
            endpointUrl = string.Format(endpointUrl, id);

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(title))
                parameters.Add(nameof(title), title);

            if (!string.IsNullOrWhiteSpace(description))
                parameters.Add(nameof(description), description);

            var content = new FormUrlEncodedContent(parameters.ToArray());

            return await MakeEndpointRequestAsync<bool>(HttpMethod.Post, endpointUrl, content);
        }

        /// <summary>
        ///     Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ImgurException"></exception>
        /// <exception cref="MashapeException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <returns></returns>
        public async Task<bool> FavoriteImageAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), FavoriteImageUrl);
            endpointUrl = string.Format(endpointUrl, id);

            //The structure of the response of favoriting an image
            //varies on if Imgur or Mashape Authentication is used
            if (ApiClient is IImgurClient)
            {
                var imgurResult = await MakeEndpointRequestAsync<string>(HttpMethod.Post, endpointUrl);
                return imgurResult.Equals("favorited", StringComparison.OrdinalIgnoreCase);
            }

            var favorited = false;

            try
            {
                await MakeEndpointRequestAsync<ImgurError>(HttpMethod.Post, endpointUrl);
            }
            catch (ImgurException imgurException)
            {
                favorited = imgurException.Message.Equals("f", StringComparison.OrdinalIgnoreCase);
            }

            return favorited;
        }
    }
}