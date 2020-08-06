namespace Imgur.API.Models
{
    /// <summary>
    /// Error Information.
    /// </summary>
    public class Error : IError
    {
        public virtual int Code { get; set; }
        public virtual string Message { get; set; }
        public virtual string Type { get; set; }
    }
}
