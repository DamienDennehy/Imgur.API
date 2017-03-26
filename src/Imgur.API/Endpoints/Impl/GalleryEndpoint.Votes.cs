using System;
using System.Net.Http;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;
using Imgur.API.Models.Impl;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     Get the vote information about an image.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<IVote> GetGalleryItemVotesAsync(string galleryItemId)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            var url = $"gallery/{galleryItemId}/votes";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Get, url))
            {
                var vote = await SendRequestAsync<Vote>(request).ConfigureAwait(false);
                return vote;
            }
        }

        /// <summary>
        ///     Vote for an item. Send the same value again to undo a vote. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="vote">The vote.</param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a null reference is passed to a method that does not accept it as a
        ///     valid argument.
        /// </exception>
        /// <exception cref="ImgurException">Thrown when an error is found in a response from an Imgur endpoint.</exception>
        /// <exception cref="MashapeException">Thrown when an error is found in a response from a Mashape endpoint.</exception>
        /// <returns></returns>
        public async Task<bool> VoteGalleryItemAsync(string galleryItemId, VoteOption vote)
        {
            if (string.IsNullOrWhiteSpace(galleryItemId))
                throw new ArgumentNullException(nameof(galleryItemId));

            if (ApiClient.OAuth2Token == null)
                throw new ArgumentNullException(nameof(ApiClient.OAuth2Token), OAuth2RequiredExceptionMessage);

            var voteValue = $"{vote}".ToLower();
            var url = $"gallery/{galleryItemId}/vote/{voteValue}";

            using (var request = RequestBuilders.RequestBuilderBase.CreateRequest(HttpMethod.Post, url))
            {
                var voted = await SendRequestAsync<bool>(request).ConfigureAwait(false);
                return voted;
            }
        }
    }
}