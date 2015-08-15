namespace Imgur.API.Models
{
    /// <summary>
    ///     Represent the basic meme metadata.
    /// </summary>
    public interface IMemeMetaData
    {
        /// <summary>
        ///     The name of the meme used.
        /// </summary>
        string MemeName { get; set; }

        /// <summary>
        ///     The top text of the meme.
        /// </summary>
        string TopText { get; set; }

        /// <summary>
        ///     The bottom text of the meme.
        /// </summary>
        string BottomText { get; set; }

        /// <summary>
        ///     The image id of the background image of the meme.
        /// </summary>
        string BgImage { get; set; }
    }
}