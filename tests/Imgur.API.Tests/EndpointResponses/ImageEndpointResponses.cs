namespace Imgur.API.Tests.EndpointResponses
{
    public class ImageEndpointResponses
    {
        public class Imgur
        {
            public const string GetImageResponse =
                "{\"data\":{\"id\":\"zVpyzhW\",\"title\":\"Look Mom, it's Bambi!\",\"description\":null,\"datetime\":1440259938,\"type\":\"image/gif\",\"animated\":true,\"width\":426,\"height\":240,\"size\":26270273,\"views\":1583864,\"bandwidth\":41608539674872,\"vote\":null,\"favorite\":false,\"nsfw\":false,\"section\":\"Eyebleach\",\"account_url\":\"ForAGoodTimeCall8675309\",\"account_id\":23095506,\"comment_preview\":null,\"gifv\":\"http://i.imgur.com/zVpyzhW.gifv\",\"webm\":\"http://i.imgur.com/zVpyzhW.webm\",\"mp4\":\"http://i.imgur.com/zVpyzhW.mp4\",\"link\":\"http://i.imgur.com/zVpyzhWh.gif\",\"looping\":true},\"success\":true,\"status\":200}";

            public const string UploadImageResponse =
                "{\"data\":{\"id\":\"kiNOcUl\",\"title\":\"Title Test\",\"description\":\"Description Test\",\"datetime\":1440373411,\"type\":\"image/gif\",\"animated\":true,\"width\":290,\"height\":189,\"size\":1038889,\"views\":0,\"bandwidth\":0,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":24234234,\"comment_preview\":null,\"deletehash\":\"nGxOKC9ML6KyTWQ\",\"name\":\"\",\"gifv\":\"http://i.imgur.com/kiNOcUl.gifv\",\"webm\":\"http://i.imgur.com/kiNOcUl.webm\",\"mp4\":\"http://i.imgur.com/kiNOcUl.mp4\",\"link\":\"http://i.imgur.com/kiNOcUl.gif\",\"looping\":true},\"success\":true,\"status\":200}";

            public const string FavoriteImageResponseTrue = "{\"data\":\"favorited\",\"success\":true,\"status\":200}";

            public const string FavoriteImageResponseFalse =
                "{\"data\":\"unfavorited\",\"success\":true,\"status\":200}";

            public const string DeleteAlbumResponse = "{\"data\":true,\"success\":true,\"status\":200}";
        }

        public class Mashape
        {
            public const string FavoriteImageResponseTrue =
                "{\"data\":{\"error\":\"f\",\"request\":\"/3/image/CgBdJN9/favorite\",\"method\":\"POST\"},\"success\":true,\"status\":200}";

            public const string FavoriteImageResponseFalse =
                "{\"data\":{\"error\":\"u\",\"request\":\"/3/image/CgBdJN9/favorite\",\"method\":\"POST\"},\"success\":true,\"status\":200}";
        }
    }
}