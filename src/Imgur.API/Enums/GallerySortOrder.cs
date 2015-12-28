namespace Imgur.API.Enums
{
    /// <summary>
    ///     The order that the gallery should be sorted by.
    /// </summary>
    public enum GallerySortOrder
    {
        /// <summary>
        ///     Sort the gallery by the most viral item first.
        /// </summary>
        Viral,

        /// <summary>
        ///     Sort the gallery by the most recent item first.
        /// </summary>
        Time,

        /// <summary>
        ///     Sort the gallery by the most top rated item in a period first.
        /// </summary>
        Top,

        /// <summary>
        ///     Sort the gallery by the rising items.
        /// </summary>
        Rising
    }
}