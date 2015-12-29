namespace Imgur.API.Enums
{
    /// <summary>
    ///     The time period that should be used in filtering requests.
    /// </summary>
    public enum TimeWindow
    {
        /// <summary>
        ///     Include the last day only in the request.
        /// </summary>
        Day,

        /// <summary>
        ///     Include the last week only in the request.
        /// </summary>
        Week,

        /// <summary>
        ///     Include the last month only in the request.
        /// </summary>
        Month,

        /// <summary>
        ///     Include the last year only in the request.
        /// </summary>
        Year,

        /// <summary>
        ///     Include all in the request.
        /// </summary>
        All
    }
}