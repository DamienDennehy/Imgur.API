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
                var response = await SendRequestAsync<Image>(request,
                                                             cancellationToken).ConfigureAwait(false);
                return response;
            }
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="image"></param>
        /// <param name="album"></param>
        /// <param name="name">The image filename.</param>
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

            return UploadImageInternalAsync(image, album, name, title, description, progress, bufferSize, cancellationToken);
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <param name="album"></param>
        /// <param name="name">The image filename.</param>
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
            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                throw new ArgumentNullException(nameof(imageUrl));
            }

            return UploadImageInternalAsync(imageUrl: imageUrl, album, name, title, description, cancellationToken);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        private async Task<IImage> UploadImageInternalAsync(Stream image,
                                                            string album = null,
                                                            string name = null,
                                                            string title = null,
                                                            string description = null,
                                                            IProgress<int> progress = null,
                                                            int? bufferSize = 4096,
                                                            CancellationToken cancellationToken = default)
        {
            const string url = "upload";

            using (var request = ImageRequestBuilder.UploadImageStreamRequest(url,
                                                                              image,
                                                                              album,
                                                                              name,
                                                                              title,
                                                                              description,
                                                                              progress,
                                                                              bufferSize))
            {
                var response = await SendRequestAsync<Image>(request,
                                                             cancellationToken).ConfigureAwait(false);
                return response;
            }
        }

        private async Task<IImage> UploadImageInternalAsync(string imageUrl,
                                                            string album = null,
                                                            string name = null,
                                                            string title = null,
                                                            string description = null,
                                                            CancellationToken cancellationToken = default)
        {
            const string url = "image";

            using (var request = ImageRequestBuilder.UploadImageUrlRequest(url,
                                                                           imageUrl,
                                                                           album,
                                                                           name,
                                                                           title,
                                                                           description))
            {
                var response = await SendRequestAsync<Image>(request,
                                                             cancellationToken).ConfigureAwait(false);
                return response;
            }
        }

        /// <summary>
        /// Upload a new image.
        /// </summary>
        /// <param name="video"></param>
        /// <param name="album"></param>
        /// <param name="type"></param>
        /// <param name="name">The image filename.</param>
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

            return UploadVideoInternalAsync(video, album, name, title, description, progress, bufferSize, cancellationToken);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        private async Task<IImage> UploadVideoInternalAsync(Stream image,
                                                            string album = null,
                                                            string name = null,
                                                            string title = null,
                                                            string description = null,
                                                            IProgress<int> progress = null,
                                                            int? bufferSize = 4096,
                                                            CancellationToken cancellationToken = default)
        {
            const string url = "upload";

            using (var request = ImageRequestBuilder.UploadVideoStreamRequest(url,
                                                                              image,
                                                                              album,
                                                                              name,
                                                                              title,
                                                                              description,
                                                                              progress,
                                                                              bufferSize))
            {
                var response = await SendRequestAsync<Image>(request,
                                                             cancellationToken).ConfigureAwait(false);
                return response;
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
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return DeleteImageInternalAsync(imageId);
        }

        private async Task<bool> DeleteImageInternalAsync(string imageId)
        {
            var url = $"image/{imageId}";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Delete, url))
            {
                var response = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return response;
            }
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
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            return UpdateImageInternalAsync(imageId, title, description);
        }

        private async Task<bool> UpdateImageInternalAsync(string imageId,
                                                          string title = null,
                                                          string description = null,
                                                          CancellationToken cancellationToken = default)
        {
            var url = $"image/{imageId}";

            using (var request = ImageRequestBuilder.UpdateImageRequest(url,
                                                                        title,
                                                                        description))
            {
                var updated = await SendRequestAsync<bool>(request,
                                                           cancellationToken).ConfigureAwait(false);
                return updated;
            }
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
            if (string.IsNullOrWhiteSpace(imageId))
            {
                throw new ArgumentNullException(nameof(imageId));
            }

            if (_apiClient.OAuth2Token == null)
            {
                throw new InvalidOperationException(OAuth2RequiredExceptionMessage);
            }

            return FavoriteImageInternalAsync(imageId);
        }

        private async Task<string> FavoriteImageInternalAsync(string imageId,
                                                              CancellationToken cancellationToken = default)
        {
            var url = $"image/{imageId}/favorite";

            using (var request = RequestBuilder.CreateRequest(HttpMethod.Post, url))
            {
                var response = await SendRequestAsync<string>(request,
                                                              cancellationToken).ConfigureAwait(false);
                return response;
            }
        }
    }
}
