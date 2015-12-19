namespace Imgur.API.Enums
{
    /// <summary>
    ///     A reason why a comment is inappropriate.
    /// </summary>
    public enum ReportComment
    {
        /// <summary>
        ///     Doesn't belong on Imgur.
        /// </summary>
        DoesNotBelong = 1,

        /// <summary>
        ///     Spam.
        /// </summary>
        Spam = 2,

        /// <summary>
        ///     Abusive.
        /// </summary>
        Abusive = 3,

        /// <summary>
        ///     Mature content not marked as mature.
        /// </summary>
        MatureContentNotMarked = 4,

        /// <summary>
        ///     Pornography.
        /// </summary>
        Pornography = 5
    }
}