using System.Collections.Generic;
using Imgur.API.Enums;

namespace Imgur.API.Models
{
    /// <summary>
    ///     The account settings.
    /// </summary>
    public interface IAccountSettings : IDataModel
    {
        /// <summary>
        ///     The account username.
        /// </summary>
        string AccountUrl { get; set; }

        /// <summary>
        ///     True if the user has accepted the terms of uploading to the Imgur gallery.
        /// </summary>
        bool AcceptedGalleryTerms { get; set; }

        /// <summary>
        ///     The email addresses that have been activated to allow uploading.
        /// </summary>
        string[] ActiveEmails { get; set; }

        /// <summary>
        ///     Set the album privacy to this privacy setting on creation.
        /// </summary>
        AlbumPrivacy AlbumPrivacy { get; set; }

        /// <summary>
        ///     A list of users that have been blocked from messaging.
        /// </summary>
        IEnumerable<IBlockedUser> BlockedUsers { get; set; }

        /// <summary>
        ///     The users email address.
        /// </summary>
        string Email { get; set; }

        /// <summary>
        ///     The users ability to upload higher quality images, there will be less compression.
        /// </summary>
        bool HighQuality { get; set; }

        /// <summary>
        ///     If the user is accepting incoming messages or not.
        /// </summary>
        bool MessagingEnabled { get; set; }

        /// <summary>
        ///     Automatically allow all images to be publicly accessible.
        /// </summary>
        bool PublicImages { get; set; }

        /// <summary>
        ///     True if the user has opted to have mature images displayed in gallery list endpoints.
        /// </summary>
        bool ShowMature { get; set; }
    }
}