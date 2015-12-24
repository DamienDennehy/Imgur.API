using System;

namespace Imgur.API
{
    /// <summary>
    ///     The exception that is thrown when an error is found in a response from a Mashape endpoint.
    /// </summary>
    public class MashapeException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the MashapeException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MashapeException(string message)
            : base(message)
        {
        }

        /// <summary>
        ///     Initializes a new instance of the MashapeException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public MashapeException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}