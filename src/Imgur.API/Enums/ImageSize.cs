namespace Imgur.API.Enums
{
    /// <summary>
    ///     Searchable image sizes.
    /// </summary>
    public enum ImageSize
    {
        /// <summary>
        ///     Small (500 pixels square or less).
        /// </summary>
        Small,

        /// <summary>
        ///     Med (500 to 2,000 pixels square).
        /// </summary>
        Med,

        /// <summary>
        ///     Big (2,000 to 5,000 pixels square).
        /// </summary>
        Big,

        /// <summary>
        ///     Lrg (5,000 to 10,000 pixels square).
        /// </summary>
        Lrg,

        /// <summary>
        ///     Huge (10,000 square pixels and above).
        /// </summary>
        Huge
    }
}