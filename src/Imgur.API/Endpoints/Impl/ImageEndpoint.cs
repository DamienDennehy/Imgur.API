using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Authentication;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    /// <summary>
    ///     Image related actions.
    /// </summary>
    public class ImageEndpoint : EndpointBase, IImageEndpoint
    {
        private const string GetImageUrl = "image/{0}";
        private const string UploadImageUrl = "image";
        private const string FavoriteImageUrl = "image/{0}/favorite";

        /// <summary>
        ///     Initializes a new instance of the ImageEndpoint class.
        /// </summary>
        /// <param name="authentication"></param>
        public ImageEndpoint(IApiAuthentication authentication) : base(authentication)
        {
        }

        /// <summary>
        ///     Get information about an image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        public async Task<IImage> GetImageAsync(string id)
        {
            if (string.IsNullOrEmpty((id)))
                throw new ArgumentNullException(nameof(id));

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), GetImageUrl);
            endpointUrl = string.Format(endpointUrl, id);
            var image = await MakeEndpointRequestAsync<Image>(HttpMethod.Get, endpointUrl, null);
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
        /// <returns></returns>
        public async Task<IImage> UploadImageBinaryAsync(byte[] image, string album, string title, string description)
        {
            if (image == null)
                throw new ArgumentNullException(nameof(image));

            IImage returnImage;

            var endpointUrl = string.Concat(GetEndpointBaseUrl(), UploadImageUrl);

            using (var content = new MultipartFormDataContent(DateTime.UtcNow.Ticks.ToString()))
            {
                content.Add(new StringContent("type"), "file");
                content.Add(new StreamContent(new MemoryStream(image)), nameof(image));

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
        ///     Upload a new image using base64 data.
        /// </summary>
        /// <param name="image">Base64 data.</param>
        /// <param name="album">
        ///     The id of the album you want to add the image to. For anonymous albums, {album} should be the
        ///     deletehash that is returned at creation.
        /// </param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        public Task<IImage> UploadImageBase64Async(string image, string album, string title, string description)
        {
            if (string.IsNullOrEmpty((image)))
                throw new ArgumentNullException(nameof(image));

            throw new NotImplementedException();
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
        /// <returns></returns>
        public Task<IImage> UploadImageUrlAsync(string image, string album, string title, string description)
        {
            if (string.IsNullOrEmpty((image)))
                throw new ArgumentNullException(nameof(image));

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Deletes an image. For an anonymous image, {id} must be the image's deletehash.
        ///     If the image belongs to your account then passing the ID of the image is sufficient.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        public Task<bool> DeleteImageAsync(string id)
        {
            if (string.IsNullOrEmpty((id)))
                throw new ArgumentNullException(nameof(id));

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates the title or description of an image.
        ///     You can only update an image you own and is associated with your account.
        ///     For an anonymous image, {id} must be the image's deletehash.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <param name="title">The title of the image.</param>
        /// <param name="description">The description of the image.</param>
        /// <returns></returns>
        public Task<bool> UpdateImageAsync(string id, string title, string description)
        {
            if (string.IsNullOrEmpty((id)))
                throw new ArgumentNullException(nameof(id));

            throw new NotImplementedException();
        }

        /// <summary>
        ///     Favorite an image with the given ID. The user is required to be logged in to favorite the image.
        /// </summary>
        /// <param name="id">The image id.</param>
        /// <returns></returns>
        public Task<bool> FavoriteImageAsync(string id)
        {
            if (string.IsNullOrEmpty((id)))
                throw new ArgumentNullException(nameof(id));

            throw new NotImplementedException();
        }
    }
}