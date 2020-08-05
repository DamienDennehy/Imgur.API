using System.Net;

namespace Imgur.API.Models
{
    /// <summary>
    /// A response from the API.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Basic<T> : IBasic<T>
    {
        public virtual T Data { get; set; }

        public virtual int Status { get; set; }

        public virtual bool Success { get; set; }
    }
}
