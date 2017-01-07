using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Gallery related actions.
    /// </summary>
    public interface IGalleryEndpoint
    {
        /// <summary>
        ///     Create a comment for an item. OAuth authentication required.
        /// </summary>
        /// <param name="comment">The text of the comment.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<int> CreateGalleryItemCommentAsync(string comment, string galleryItemId);

        /// <summary>
        ///     Reply to a comment that has been created for an item. OAuth authentication required.
        /// </summary>
        /// <param name="comment">The text of the comment.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="parentId">The comment id that you are replying to.</param>
        /// <returns></returns>
        Task<int> CreateGalleryItemCommentReplyAsync(string comment, string galleryItemId, string parentId);

        /// <summary>
        ///     Get additional information about an album in the gallery.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <returns></returns>
        Task<IGalleryAlbum> GetGalleryAlbumAsync(string albumId);

        /// <summary>
        ///     Returns the images in the gallery.
        /// </summary>
        /// <param name="section">The gallery section. Default: Hot</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <param name="showViral">Show or hide viral images from the 'user' section. Default: true</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetGalleryAsync(GallerySection? section = GallerySection.Hot,
            GallerySortOrder? sort = GallerySortOrder.Viral, TimeWindow? window = TimeWindow.Day, int? page = null,
            bool? showViral = true);

        /// <summary>
        ///     Get additional information about an image in the gallery.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <returns></returns>
        Task<IGalleryImage> GetGalleryImageAsync(string imageId);

        /// <summary>
        ///     Get information about a specific comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<IComment> GetGalleryItemCommentAsync(int commentId, string galleryItemId);

        /// <summary>
        ///     The number of comments on an item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<int> GetGalleryItemCommentCountAsync(string galleryItemId);

        /// <summary>
        ///     List all of the IDs for the comments on an item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetGalleryItemCommentIdsAsync(string galleryItemId);

        /// <summary>
        ///     Get all comments for a gallery item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="sort">The order that comments should be sorted by.</param>
        /// <returns></returns>
        Task<IEnumerable<IComment>> GetGalleryItemCommentsAsync(string galleryItemId,
            CommentSortOrder? sort = CommentSortOrder.Best);

        /// <summary>
        ///     View tags for a gallery item.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<ITagVotes> GetGalleryItemTagsAsync(string galleryItemId);

        /// <summary>
        ///     Get the vote information about an image.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<IVote> GetGalleryItemVotesAsync(string galleryItemId);

        /// <summary>
        ///     View images for a gallery tag.
        /// </summary>
        /// <param name="tag">The name of the tag.</param>
        /// <param name="sort">The order that the images in the gallery tag should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        Task<ITag> GetGalleryTagAsync(string tag, GalleryTagSortOrder? sort = GalleryTagSortOrder.Viral,
            TimeWindow? window = TimeWindow.Week, int? page = null);

        /// <summary>
        ///     View a single image in a gallery tag.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="tag">The name of the tag.</param>
        /// <returns></returns>
        Task<IGalleryItem> GetGalleryTagImageAsync(string galleryItemId, string tag);

        /// <summary>
        ///     View images for memes subgallery.
        /// </summary>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Viral</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetMemesSubGalleryAsync(
            MemesGallerySortOrder? sort = MemesGallerySortOrder.Viral, TimeWindow? window = TimeWindow.Day,
            int? page = null);

        /// <summary>
        ///     View a single image in the memes gallery.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <returns></returns>
        Task<IGalleryImage> GetMemesSubGalleryImageAsync(string imageId);

        /// <summary>
        ///     Returns a random set of gallery images.
        /// </summary>
        /// <param name="page">A page of random gallery images, from 0-50. Pages are regenerated every hour.</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetRandomGalleryAsync(int? page = null);

        /// <summary>
        ///     View gallery images for a subreddit.
        /// </summary>
        /// <param name="subreddit">A valid subreddit name. Example: pics, gaming</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Week</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetSubredditGalleryAsync(string subreddit,
            SubredditGallerySortOrder? sort = SubredditGallerySortOrder.Time, TimeWindow? window = TimeWindow.Week,
            int? page = null);

        /// <summary>
        ///     View a single image in the subreddit.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="subreddit">A valid subreddit name. Example: pics, gaming</param>
        /// <returns></returns>
        Task<IGalleryImage> GetSubredditImageAsync(string imageId, string subreddit);

        /// <summary>
        ///     Share an Album or Image to the Gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="title">The title of the image. This is required.</param>
        /// <param name="topicId">The topic id - not the topic name.</param>
        /// <param name="bypassTerms">
        ///     If the user has not accepted the terms yet, this endpoint will return an error. To by-pass
        ///     the terms in general simply set this value to true.
        /// </param>
        /// <param name="mature">If the post is mature, set this value to true.</param>
        /// <returns></returns>
        Task<bool> PublishToGalleryAsync(string galleryItemId, string title, string topicId = null,
            bool? bypassTerms = null,
            bool? mature = null);

        /// <summary>
        ///     Remove an image from the gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <returns></returns>
        Task<bool> RemoveFromGalleryAsync(string galleryItemId);

        /// <summary>
        ///     Report an item in the gallery. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="reason">A reason why content is inappropriate.</param>
        /// <returns></returns>
        Task<bool> ReportGalleryItemAsync(string galleryItemId, ReportReason reason);

        /// <summary>
        ///     Search the gallery with a given query string.
        /// </summary>
        /// <param name="qAll">Search for all of these words (and).</param>
        /// <param name="qAny">Search for any of these words (or).</param>
        /// <param name="qExactly">Search for exactly this word or phrase.</param>
        /// <param name="qNot">Exclude results matching this word or phrase.</param>
        /// <param name="fileType">Show results for a specific file type.</param>
        /// <param name="imageSize">Show results for a specific image size.</param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> SearchGalleryAdvancedAsync(
            string qAll = null, string qAny = null,
            string qExactly = null, string qNot = null,
            ImageFileType? fileType = null, ImageSize? imageSize = null,
            GallerySortOrder? sort = GallerySortOrder.Time, TimeWindow? window = TimeWindow.All, int? page = null);

        /// <summary>
        ///     Search the gallery with a given query string.
        /// </summary>
        /// <param name="query">
        ///     Query string to search by. This parameter also supports boolean operators (AND, OR, NOT) and
        ///     indices (tag: user: title: ext: subreddit: album: meme:). An example compound query would be 'title: cats AND dogs
        ///     ext: gif'
        /// </param>
        /// <param name="sort">The order that the gallery should be sorted by. Default: Time</param>
        /// <param name="window">The time period that should be used in filtering requests. Default: Day</param>
        /// <param name="page">The data paging number. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> SearchGalleryAsync(string query,
            GallerySortOrder? sort = GallerySortOrder.Time, TimeWindow? window = TimeWindow.All, int? page = null);

        /// <summary>
        ///     Vote for an item. Send the same value again to undo a vote. OAuth authentication required.
        /// </summary>
        /// <param name="galleryItemId">The gallery item id.</param>
        /// <param name="vote">The vote.</param>
        /// <returns></returns>
        Task<bool> VoteGalleryItemAsync(string galleryItemId, VoteOption vote);
    }
}