using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    public class ImageEndpoint : IImageEndpoint
    {
        public Task<bool> DeleteImageAsync(string imageId,
                                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<string> FavoriteImageAsync(string imageId,
                                               CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IImage> GetImageAsync(string imageId,
                                          CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateImageAsync(string imageId,
                                           string title = null,
                                           string description = null,
                                           CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IImage> UploadImageAsync(Stream image,
                                             string album = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<IImage> UploadImageAsync(string image,
                                             string album = null,
                                             string name = null,
                                             string title = null,
                                             string description = null,
                                             CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

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
    }
}
