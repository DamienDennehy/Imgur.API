namespace Imgur.API.Enums
{
    /// <summary>
    ///     Indicates the order that a list of items are sorted.
    /// </summary>
    public enum CommentSortOrder
    {
        /// <summary>
        ///     Sort the list by the newest item first.
        /// </summary>
        Newest,

        /// <summary>
        ///     Sort the list by the oldest item first.
        /// </summary>
        Oldest,

        /// <summary>
        ///     Sort the list by the best rated item first.
        /// </summary>
        Best,

        /// <summary>
        ///     Sort the list by the worst rated item first.
        /// </summary>
        Worst
    }
}