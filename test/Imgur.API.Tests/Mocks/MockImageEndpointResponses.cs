namespace Imgur.API.Tests.Mocks
{
    public class MockImageEndpointResponses
    {
        public class Imgur
        {
            public const string DeleteImage =
                "{\"data\":true,\"success\":true,\"status\":200}";

            public const string FavoriteImageFalse =
                "{\"data\":\"unfavorited\",\"success\":true,\"status\":200}";

            public const string FavoriteImageTrue =
                "{\"data\":\"favorited\",\"success\":true,\"status\":200}";

            public const string GetImage =
                "{\"data\":{\"id\":\"zVpyzhW\",\"title\":\"Look Mom, it's Bambi!\",\"description\":null,\"datetime\":1440259938,\"type\":\"image/gif\",\"animated\":true,\"width\":426,\"height\":240,\"size\":26270273,\"views\":3185896,\"bandwidth\":83694357669608,\"vote\":\"up\",\"favorite\":false,\"nsfw\":false,\"section\":\"Eyebleach\",\"account_url\":\"ForAGoodTimeCall8675309\",\"in_gallery\":true,\"gifv\":\"http://i.imgur.com/zVpyzhW.gifv\",\"webm\":\"http://i.imgur.com/zVpyzhW.webm\",\"mp4\":\"http://i.imgur.com/zVpyzhW.mp4\",\"mp4_size\":\"595876\",\"webm_size\":\"543019\",\"link\":\"http://i.imgur.com/zVpyzhWh.gif\",\"looping\":true},\"success\":true,\"status\":200}";

            public const string UpdateImage =
                "{\"data\":true,\"success\":true,\"status\":200}";

            public const string UploadImage =
                "{\"data\":{\"id\":\"kiNOcUl\",\"title\":\"Title Test\",\"description\":\"Description Test\",\"datetime\":1440373411,\"type\":\"image/gif\",\"animated\":true,\"width\":290,\"height\":189,\"size\":1038889,\"views\":0,\"bandwidth\":0,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":24234234,\"comment_preview\":null,\"deletehash\":\"nGxOKC9ML6KyTWQ\",\"name\":\"\",\"gifv\":\"http://i.imgur.com/kiNOcUl.gifv\",\"webm\":\"http://i.imgur.com/kiNOcUl.webm\",\"mp4\":\"http://i.imgur.com/kiNOcUl.mp4\",\"link\":\"http://i.imgur.com/kiNOcUl.gif\",\"looping\":true},\"success\":true,\"status\":200}";
        }
    }
}