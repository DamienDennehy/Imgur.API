using System.Net.Http;

namespace Imgur.API
{
    internal class ProgressHttpRequestMessage : HttpRequestMessage
    {
        public ProgressHttpRequestMessage(HttpMethod method, string url) :
            base(method, url)
        {

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Content.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
