namespace Imgur.API.Tests.Mocks
{
    public partial class MockAccountEndpointResponses
    {
        public const string DeleteImage =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string DeleteImageError =
            "{\"data\":{\"error\":\"Unauthorized\",\"request\":\"/3/account/me/image/487153732\",\"method\":\"DELETE\"},\"success\":true,\"status\":200}";

        public const string GetImage =
            "{\"data\":{\"id\":\"hbzm7Ge\",\"title\":\"For three days at Camp Imgur, the Imgur flag flew proudly over our humble redwood camp, greeting Imgurians each morning.\",\"description\":null,\"datetime\":1443651980,\"type\":\"image/gif\",\"animated\":true,\"width\":406,\"height\":720,\"size\":23386145,\"views\":329881,\"bandwidth\":7714644898745,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"gifv\":\"http://i.imgur.com/hbzm7Ge.gifv\",\"webm\":\"http://i.imgur.com/hbzm7Ge.webm\",\"mp4\":\"http://i.imgur.com/hbzm7Ge.mp4\",\"link\":\"http://i.imgur.com/hbzm7Geh.gif\",\"looping\":true},\"success\":true,\"status\":200}";

        public const string GetImageCount =
            "{\"data\":2,\"success\":true,\"status\":200}";

        public const string GetImageIds =
            "{\"data\":[\"BJRWQw5\",\"tMW91OL\"],\"success\":true,\"status\":200}";

        public const string GetImages =
            "{\"data\":[{\"id\":\"BJRWQw5\",\"title\":\"url test title!\",\"description\":\"url test desc!\",\"datetime\":1443895079,\"type\":\"image/gif\",\"animated\":true,\"width\":419,\"height\":217,\"size\":381925,\"views\":4,\"bandwidth\":1527700,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":\"imgurapidotnet\",\"account_id\":24562464,\"comment_preview\":null,\"deletehash\":\"v5j1alwofarZzaW\",\"name\":null,\"gifv\":\"http://i.imgur.com/BJRWQw5.gifv\",\"webm\":\"http://i.imgur.com/BJRWQw5.webm\",\"mp4\":\"http://i.imgur.com/BJRWQw5.mp4\",\"link\":\"http://i.imgur.com/BJRWQw5.gif\",\"looping\":true},{\"id\":\"tMW91OL\",\"title\":null,\"description\":null,\"datetime\":1443578033,\"type\":\"image/png\",\"animated\":false,\"width\":1360,\"height\":768,\"size\":927639,\"views\":4,\"bandwidth\":3710556,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":\"imgurapidotnet\",\"account_id\":24562464,\"comment_preview\":null,\"deletehash\":\"w0hvndmtyBfjE6t\",\"name\":\"img_20150929_190056\",\"link\":\"http://i.imgur.com/tMW91OL.png\"}],\"success\":true,\"status\":200}";
    }
}