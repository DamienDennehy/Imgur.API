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
        /// <param name="progress"></param>
        /// <param name="bufferSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> UploadImageAsync(Stream image,
                                             string album = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             IProgress<int> progress = null,
                                             int? bufferSize = 4096,
                                             CancellationToken cancellationToken = default)
        {
            if (image == null)
            {
                throw new ArgumentNullException(nameof(image));
            }

            return UploadImageInternalAsync(image, album, name, title, description, cancellationToken);
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="album"></param>
        /// <param name="name"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> UploadImageAsync(string imageUrl,
                                             string album = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private async Task<IImage> UploadImageInternalAsync(Stream image,
                                                            string album = null,
                                                            string name = null,
                                                            string title = null,
                                                            string description = null,
                                                            CancellationToken cancellationToken = default)
        {
            const string url = "upload";

            using (var request = ImageRequestBuilder.UploadImageStreamRequest(url,
                                                                              image,
                                                                              album,
                                                                              name,
                                                                              title,
                                                                              description))
            {
                var returnImage = await SendRequestAsync<Image>(request,
                                                                cancellationToken).ConfigureAwait(false);
                return returnImage;
            }
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
        /// <param name="progress"></param>
        /// <param name="bufferSize"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<IImage> UploadVideoAsync(Stream video,
                                             string album = null,
                                             string type = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             bool disableAudio = false,
                                             IProgress<int> progress = null,
                                             int? bufferSize = 4096,
                                             CancellationToken cancellationToken = default)
        {
            if (video == null)
            {
                throw new ArgumentNullException(nameof(video));
            }

            return UploadVideoInternalAsync(video, album, name, title, description, cancellationToken);
        }

        private async Task<IImage> UploadVideoInternalAsync(Stream image,
                                                            string album = null,
                                                            string name = null,
                                                            string title = null,
                                                            string description = null,
                                                            CancellationToken cancellationToken = default)
        {
            const string url = "upload";

            using (var request = ImageRequestBuilder.UploadVideoStreamRequest(url,
                                                                              image,
                                                                              album,
                                                                              name,
                                                                              title,
                                                                              description))
            {
                var returnImage = await SendRequestAsync<Image>(request,
                                                                cancellationToken).ConfigureAwait(false);
                return returnImage;
            }
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
