using System.Collections.Generic;
using System.Threading.Tasks;
using Imgur.API.Models;

namespace Imgur.API.Endpoints
{
    /// <summary>
    ///     Account related actions.
    /// </summary>
    public interface IAccountEndpoint
    {
        /// <summary>
        ///     Request standard user information.
        ///     If you need the username for the account that is logged in, it is returned in the request for an access token.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<IAccount> GetAccountAsync(string username = "me");

        /// <summary>
        ///     Return the images the user has favorited in the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <param name="sortOrder">Indicates the order that a list of items are sorted. Default: Newest.</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetAccountGalleryFavoritesAsync(string username = "me", int? page = null,
            SortOrder? sortOrder = SortOrder.Newest);

        /// <summary>
        ///     Returns the users favorited images, only accessible if you're logged in as the user.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetAccountFavoritesAsync();

        /// <summary>
        ///     Return the images a user has submitted to the gallery.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <param name="page">Set the page number so you don't have to retrieve all the data at once. Default: null.</param>
        /// <returns></returns>
        Task<IEnumerable<IGalleryItem>> GetAccountSubmissionsAsync(string username = "me", int? page = null);

        /// <summary>
        ///     Returns the account settings, only accessible if you're logged in as the user.
        /// </summary>
        /// <returns></returns>
        Task<IAccountSettings> GetAccountSettingsAsync();

        /// <summary>
        ///     Updates the account settings for a given user, the user must be logged in.
        /// </summary>
        /// <param name="bio">The biography of the user, is displayed in the gallery profile page.</param>
        /// <param name="publicImages">Set the users images to private or public by default.</param>
        /// <param name="messagingEnabled">Allows the user to enable / disable private messages.</param>
        /// <param name="albumPrivacy">Sets the default privacy level of albums the users creates.</param>
        /// <param name="acceptedGalleryTerms"> The user agreement to the Imgur Gallery terms.</param>
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
        ///     The totals for a users gallery information.
        /// </summary>
        /// <param name="username">The user account. Default: me</param>
        /// <returns></returns>
        Task<IGalleryProfile> GetGalleryProfileAsync(string username = "me");

        /// <summary>
        /// Checks to see if user has verified their email address.
        /// </summary>
        /// <returns></returns>
        Task<bool> VerifyEmailAsync();

        /// <summary>
        /// Sends an email to the user to verify that their email is valid to upload to gallery. 
        /// Must be logged in as the user to send.
        /// </summary>
        /// <returns></returns>
        Task<bool> SendVerificationEmailAsync();
    }
}