using System.Collections.Generic;
using Imgur.API.Enums;
using Imgur.API.JsonConverters;
using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     The account settings.
    /// </summary>
    public class AccountSettings : IAccountSettings
    {
        /// <summary>
        ///     True if the user has accepted the terms of uploading to the Imgur gallery.
        /// </summary>
        [JsonProperty("accepted_gallery_terms")]
        public bool AcceptedGalleryTerms { get; set; }

        /// <summary>
        ///     The email addresses that have been activated to allow uploading.
        /// </summary>
        [JsonProperty("active_emails")]
        public string[] ActiveEmails { get; set; }

        /// <summary>
        ///     Set the album privacy to this privacy setting on creation.
        /// </summary>
        [JsonProperty("album_privacy")]
        public AlbumPrivacy AlbumPrivacy { get; set; }

        /// <summary>
        ///     A list of users that have been blocked from messaging.
        /// </summary>
        [JsonProperty("blocked_users")]
        [JsonConverter(typeof (TypeConverter<IEnumerable<BlockedUser>>))]
        public IEnumerable<IBlockedUser> BlockedUsers { get; set; } = new List<IBlockedUser>();

        /// <summary>
        ///     The users email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     The users ability to upload higher quality images, there will be less compression.
        /// </summary>
        [JsonProperty("high_quality")]
        public bool HighQuality { get; set; }

        /// <summary>
        ///     If the user is accepting incoming messages or not.
        /// </summary>
        [JsonProperty("messaging_enabled")]
        public bool MessagingEnabled { get; set; }

        /// <summary>
        ///     Automatically allow all images to be publicly accessible.
        /// </summary>
        [JsonProperty("public_images")]
        public bool PublicImages { get; set; }

        /// <summary>
        ///     True if the user has opted to have mature images displayed in gallery list endpoints.
        /// </summary>
        [JsonProperty("show_mature")]
        public bool ShowMature { get; set; }
    }
}