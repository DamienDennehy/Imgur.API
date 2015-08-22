using System.Configuration;

namespace Imgur.API.Tests.Integration
{
    public abstract class TestBase
    {
        public string ClientId => ConfigurationManager.AppSettings["ClientId"];
        public string ClientSecret => ConfigurationManager.AppSettings["ClientSecret"];
        public string MashapeKey => ConfigurationManager.AppSettings["MashapeKey"];
        public string RefreshToken => ConfigurationManager.AppSettings["RefreshToken"];
    }
}