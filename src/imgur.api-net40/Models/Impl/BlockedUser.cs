using Newtonsoft.Json;

namespace Imgur.API.Models.Impl
{
    /// <summary>
    ///     A user that has been blocked from messaging.
    /// </summary>
    public class BlockedUser : IBlockedUser
    {
        /// <summary>
        ///     The account id of the user.
        /// </summary>
        [JsonProperty("blocked_id")]
        public virtual int BlockedId { get; set; }

        /// <summary>
        ///     The account username.
        /// </summary>
        [JsonProperty("blocked_url")]
        public virtual string BlockedUrl { get; set; }
    }
}