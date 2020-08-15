using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    /// Image Endpoint.
    /// </summary>
    public interface IImageEndpoint
    {
        /// <summary>
        /// Get information about an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IImage> GetImageAsync(string imageId,
                                   CancellationToken cancellationToken = default);


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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        Task<IImage> UploadImageAsync(Stream image,
                                      string album = null,
                                      string name = null,
                                      string title = null,
                                      string description = null,
                                      IProgress<int> progress = null,
                                      int? bufferSize = 4096,
                                      CancellationToken cancellationToken = default);

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
        Task<IImage> UploadImageAsync(string imageUrl,
                                      string album = null,
                                      string name = null,
                                      string title = null,
                                      string description = null,
                                      CancellationToken cancellationToken = default);

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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S107:Methods should not have too many parameters", Justification = "<Pending>")]
        Task<IImage> UploadVideoAsync(Stream video,
                                      string album = null,
                                      string type = null,
                                      string name = null,
                                      string title = null,
                                      string description = null,
                                      bool disableAudio = false,
                                      IProgress<int> progress = null,
                                      int? bufferSize = 4096,
                                      CancellationToken cancellationToken = default);

        /// <summary>
        /// Deletes an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> DeleteImageAsync(string imageId,
                                    CancellationToken cancellationToken = default);

        /// <summary>
        /// Updates the title or description of an image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> UpdateImageAsync(string imageId,
                                    string title = null,
                                    string description = null,
                                    CancellationToken cancellationToken = default);

        /// <summary>
        /// Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="imageId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<string> FavoriteImageAsync(string imageId,
                                        CancellationToken cancellationToken = default);
    }
}
