using System;
using Imgur.API.Enums;

namespace Imgur.API.Models
{
    /// <summary>
    ///     This model is used to represent the basic account information.
    /// </summary>
    public interface IAccount : IDataModel
    {
        /// <summary>
        ///     A basic description the user has filled out.
        /// </summary>
        string Bio { get; set; }

        /// <summary>
        ///     Utc timestamp of account creation, converted from epoch time.
        /// </summary>
        DateTimeOffset Created { get; set; }

        /// <summary>
        ///     The account id for the username requested.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        ///     Notoriety level based on the account's reputation.
        /// </summary>
        NotorietyLevel Notoriety { get; }

        /// <summary>
        ///     The reputation for the account, in its numerical format.
        /// </summary>
        float Reputation { get; set; }

        /// <summary>
        ///     The account username, will be the same as requested in the URL.
        /// </summary>
        string Url { get; set; }
    }
}