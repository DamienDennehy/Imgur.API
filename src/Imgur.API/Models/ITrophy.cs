using System;

namespace Imgur.API.Models
{
    /// <summary>
    ///     An earned Trophy.
    /// </summary>
    public interface ITrophy : IDataModel
    {
        /// <summary>
        ///     The ID of the image or the ID of the comment where the trophy was earned.
        /// </summary>
        string Data { get; set; }

        /// <summary>
        ///     A link to where the trophy was earned.
        /// </summary>
        string DataLink { get; set; }

        /// <summary>
        ///     Utc timestamp of when the trophy was earned, converted from epoch time.
        /// </summary>
        DateTimeOffset DateTime { get; set; }

        /// <summary>
        ///     A description of the trophy and how it was earned.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        ///     The ID of the trophy, this is unique to each trophy.
        /// </summary>
        string Id { get; set; }

        /// <summary>
        ///     URL of the image representing the trophy.
        /// </summary>
        string Image { get; set; }

        /// <summary>
        ///     The name of the trophy.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        ///     Can be thought of as the ID of a trophy type.
        /// </summary>
        string NameClean { get; set; }
    }
}