using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     Get additional information about an album in the gallery.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IGalleryAlbum> GetGalleryAlbumAsync(string albumId)
        {
            if (string.IsNullOrWhiteSpace(albumId))
                throw new ArgumentNullException(nameof(albumId));

            var url = $"gallery/album/{albumId}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var album = await SendRequestAsync<GalleryAlbum>(request).ConfigureAwait(false);
                return album;
            }
        }
    }
}