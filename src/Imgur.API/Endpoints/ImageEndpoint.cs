using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.RequestBuilders;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Image Endpoint.
    /// </summary>
    public class ImageEndpoint : EndpointBase, IImageEndpoint
    {
        /// <summary>
        /// Declares a new instance of the endpoint.
        /// </summary>
        /// <param name="apiClient"></param>
        /// <param name="httpClient"></param>
        public ImageEndpoint(IApiClient apiClient, HttpClient httpClient) : base(
            apiClient, httpClient)
        {
        }

        /// <summary>
        /// Get information about an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> GetImageAsync(string imageId,
                                          CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return GetImageInternalAsync(imageId, cancellationToken);
        }

        private async Task<IImage> GetImageInternalAsync(string imageId,
                                                         CancellationToken cancellationToken = default)
        {
            var url = $"image/{imageId}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Get, url))
            {
                var image = await SendRequestAsync<Image>(request,
                                                          cancellationToken).ConfigureAwait(false);
                return image;
            }
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="album"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> UploadImageAsync(Stream image,
                                             string album = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="album"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> UploadImageAsync(string image,
                                             string album = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="video"></param>
        /// <param name="album"></param>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="disableAudio"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> UploadVideoAsync(Stream video,
                                             string album = null,
                                             string type = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             bool disableAudio = false,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> DeleteImageAsync(string imageId,
                                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the title or description of an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> UpdateImageAsync(string imageId,
                                           string title = null,
                                           string description = null,
                                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<string> FavoriteImageAsync(string imageId,
                                               CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
