namespace Imgur.API.Enums
{
    /// <summary>
    ///     The album's privacy setting.
    /// </summary>
    public enum AlbumPrivacy
    {
        /// <summary>
        ///     Anyone can see this album.
        /// </summary>
        Public,

        /// <summary>
        ///     This album is hidden from the public albums view, but will still be accessible from via the direct URL.
        /// </summary>
        Secret,

        /// <summary>
        ///     Only the creator can see or access this album.
        /// </summary>
        Hidden
    }
}