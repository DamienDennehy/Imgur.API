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
    }
}