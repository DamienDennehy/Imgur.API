using System;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints.Impl
{
    public partial class GalleryEndpoint
    {
        /// <summary>
        ///     Get the vote information about an image.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        public async Task<IVote> GetGalleryItemVotesAsync(string galleryItemId)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///     Vote for an item. Send the same value again to undo a vote.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="vote">The vote.</param>
        /// <returns></returns>
        public async Task<bool> VoteGalleryItemAsync(string galleryItemId, VoteOption vote)
        {
            throw new NotImplementedException();
        }
    }
}