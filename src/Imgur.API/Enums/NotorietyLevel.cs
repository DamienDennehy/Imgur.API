namespace Imgur.API.Enums
{
    /// <summary>
    ///     Notoriety level based on a user's reputation.
    /// </summary>
    public enum NotorietyLevel
    {
        /// <summary>
        ///     Reputation less than 0.
        /// </summary>
        ForeverAlone,

        /// <summary>
        ///     Reputation between 0 and 399
        /// </summary>
        Neutral,

        /// <summary>
        ///     Reputation between 400 and 999.
        /// </summary>
        Accepted,

        /// <summary>
        ///     Reputation between 1,000 and 1,999.
        /// </summary>
        Liked,

        /// <summary>
        ///     Reputation between 2,000 and 3,999.
        /// </summary>
        Trusted,

        /// <summary>
        ///     Reputation between 4,000 and 7,999.
        /// </summary>
        Idolized,

        /// <summary>
        ///     Reputation between 8,000 and 19,999.
        /// </summary>
        Renowned,

        /// <summary>
        ///     Reputation equal or greater than 20,000.
        /// </summary>
        Glorious
    }
}