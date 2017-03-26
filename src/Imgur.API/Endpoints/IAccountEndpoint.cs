using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Enums;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Account related actions.
    /// </summary>
    public interface IAccountEndpoint : IEndpoint
    {
        /// <summary>
        ///     Delete an Album with a given id. OAuth authentication required.
        /// </summary>
        /// <param name="albumId">The album id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<bool> DeleteAlbumAsync(string albumId, string username = "me");

        /// <summary>
        ///     Delete a comment. OAuth authentication required.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<bool> DeleteCommentAsync(int commentId, string username = "me");

        /// <summary>
        ///     Deletes an Image. OAuth authentication required.
        /// </summary>
        /// <param name="imageId">The image id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<bool> DeleteImageAsync(string imageId, string username = "me");

        /// <summary>
        ///     Request standard user information.
        ///     If you need the username for the account that is logged in, it is returned in the request for an access token.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<IAccount> GetAccountAsync(string username = "me");

        /// <summary>
        ///     Returns the users favorited images. OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetAccountFavoritesAsync(int? page = null, AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest);

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <param name="sort">The order that the account gallery should be sorted by. Default: Newest</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetAccountGalleryFavoritesAsync(string username = "me", int? page = null,
            AccountGallerySortOrder? sort = AccountGallerySortOrder.Newest);

        /// <summary>
        ///     Returns the account settings. OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<IAccountSettings> GetAccountSettingsAsync();

        /// <summary>
        ///     Return the images a user has submitted to the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetAccountSubmissionsAsync(string username = "me", int? page = null);

        /// <summary>
        ///     Get additional information about an album, this works the same as the Album Endpoint.
        /// </summary>
        /// <param name="albumId">The album's id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<IAlbum> GetAlbumAsync(string albumId, string username = "me");

        /// <summary>
        ///     Return the total number of albums associated with the account.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<int> GetAlbumCountAsync(string username = "me");

        /// <summary>
        ///     Return a list of all of the album IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetAlbumIdsAsync(string username = "me", int? page = null);

        /// <summary>
        ///     Get all the albums associated with the account.
        ///     Must be logged in as the user to see secret and hidden albums.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IAlbum>> GetAlbumsAsync(string username = "me", int? page = null);

        /// <summary>
        ///     Return information about a specific comment.
        /// </summary>
        /// <param name="commentId">The comment id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<IComment> GetCommentAsync(int commentId, string username = "me");

        /// <summary>
        ///     Return a count of all of the comments associated with the account.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<int> GetCommentCountAsync(string username = "me");

        /// <summary>
        ///     Return a list of all of the comment IDs.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="sort">The order that the comments should be sorted by. Default: Newest</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once.</param>
        /// <returns></returns>
        Task<IEnumerable<int>> GetCommentIdsAsync(string username = "me",
            CommentSortOrder? sort = CommentSortOrder.Newest, int? page = null);

        /// <summary>
        ///     Return the comments the user has created.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="sort">The order that the comments should be sorted by. Default: Newest</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IComment>> GetCommentsAsync(string username = "me",
            CommentSortOrder? sort = CommentSortOrder.Newest, int? page = null);

        /// <summary>
        ///     The totals for a users gallery information.
        /// </summary>
        /// <returns></returns>
        Task<IGalleryProfile> GetGalleryProfileAsync();

        /// <summary>
        ///     Return information about a specific image.
        /// </summary>
        /// <param name="imageId">The images's id.</param>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<IImage> GetImageAsync(string imageId, string username = "me");

        /// <summary>
        ///     Returns the total number of images associated with the account.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<int> GetImageCountAsync(string username = "me");

        /// <summary>
        ///     Returns a list of Image IDs that are associated with the account. OAuth authentication required.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<string>> GetImageIdsAsync(string username = "me", int? page = null);

        /// <summary>
        ///     Return all of the images associated with the account.
        ///     You can page through the images by setting the page, this defaults to 0.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Allows you to set the page number so you don't have to retrieve all the data at once. Default: null</param>
        /// <returns></returns>
        Task<IEnumerable<IImage>> GetImagesAsync(string username = "me", int? page = null);

        /// <summary>
        ///     Returns all of the notifications for the user.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="newNotifications">false for all notifications, true for only non-viewed notification. Default is true.</param>
        /// <returns></returns>
        Task<INotifications> GetNotificationsAsync(bool newNotifications = true);

        /// <summary>
        ///     Sends an email to the user to verify that their email is valid to upload to gallery.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<bool> SendVerificationEmailAsync();

        /// <summary>
        ///     Updates the account settings for a given user.
        ///     OAuth authentication required.
        /// </summary>
        /// <param name="bio">The biography of the user, is displayed in the gallery profile page.</param>
        /// <param name="publicImages">Set the users images to private or public by default.</param>
        /// <param name="messagingEnabled">Allows the user to enable / disable private messages.</param>
        /// <param name="albumPrivacy">Sets the default privacy level of albums the users creates.</param>
        /// <param name="acceptedGalleryTerms">The user agreement to the Imgur Gallery terms.</param>
        /// <param name="username">A valid Imgur username (between 4 and 63 alphanumeric characters).</param>
        /// <param name="showMature">Toggle display of mature images in gallery list endpoints.</param>
        /// <param name="newsletterSubscribed">Toggle subscription to email newsletter.</param>
        /// <returns></returns>
        Task<bool> UpdateAccountSettingsAsync(
            string bio = null,
            bool? publicImages = null,
            bool? messagingEnabled = null,
            AlbumPrivacy? albumPrivacy = null,
            bool? acceptedGalleryTerms = null,
            string username = null,
            bool? showMature = null,
            bool? newsletterSubscribed = null);

        /// <summary>
        ///     Checks to see if user has verified their email address.
        ///     OAuth authentication required.
        /// </summary>
        /// <returns></returns>
        Task<bool> VerifyEmailAsync();
    }
}