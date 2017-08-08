namespace Imgur.API.Enums
{
    /// <summary>
    ///     The order that the comments should be sorted by.
    /// </summary>
    public enum CommentSortOrder
    {
        /// <summary>
        ///     Sort the comments by the newest comments first.
        /// </summary>
        Newest,

        /// <summary>
        ///     Sort the comments by the oldest comments first.
        /// </summary>
        Oldest,

        /// <summary>
        ///     Sort the comments by the best rated comments first.
        /// </summary>
        Best,

        /// <summary>
        ///     Sort the comments by the worst rated comments first.
        /// </summary>
        Worst
    }
}