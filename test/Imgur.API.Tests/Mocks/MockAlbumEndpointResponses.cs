namespace Imgur.API.Tests.Mocks
{
    public class MockAlbumEndpointResponses
    {
        public class Imgur
        {
            public const string AddAlbumImages =
                "{\"data\":true,\"success\":true,\"status\":200}";

            public const string CreateAlbum =
                "{\"data\":{\"id\":\"3gfxo\",\"deletehash\":\"iIFJnFpVbYOvzvv\"},\"success\":true,\"status\":200}";

            public const string DeleteAlbum =
                "{\"data\":true,\"success\":true,\"status\":200}";

            public const string FavoriteAlbumFalse =
                "{\"data\":\"unfavorited\",\"success\":true,\"status\":200}";

            public const string FavoriteAlbumTrue =
                "{\"data\":\"favorited\",\"success\":true,\"status\":200}";

            public const string GetAlbum =
                "{\"data\":{\"id\":\"5F5Cy\",\"title\":null,\"description\":null,\"datetime\":1446591779,\"cover\":\"79MH23L\",\"cover_width\":609,\"cover_height\":738,\"account_url\":\"sarah\",\"account_id\":9571,\"privacy\":\"public\",\"layout\":\"blog\",\"views\":19,\"link\":\"http://imgur.com/a/5F5Cy\",\"favorite\":false,\"nsfw\":null,\"section\":null,\"images_count\":3,\"in_gallery\":false,\"images\":[{\"id\":\"79MH23L\",\"title\":null,\"description\":null,\"datetime\":1446591783,\"type\":\"image/png\",\"animated\":false,\"width\":609,\"height\":738,\"size\":451530,\"views\":4046,\"bandwidth\":1826890380,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"in_gallery\":false,\"link\":\"http://i.imgur.com/79MH23L.png\"},{\"id\":\"CqEsRhO\",\"title\":null,\"description\":null,\"datetime\":1446591788,\"type\":\"image/png\",\"animated\":false,\"width\":552,\"height\":414,\"size\":154397,\"views\":4305,\"bandwidth\":664679085,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"in_gallery\":false,\"link\":\"http://i.imgur.com/CqEsRhO.png\"},{\"id\":\"zYOxPum\",\"title\":null,\"description\":null,\"datetime\":1446591791,\"type\":\"image/png\",\"animated\":false,\"width\":684,\"height\":444,\"size\":98211,\"views\":9154,\"bandwidth\":899023494,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"in_gallery\":false,\"link\":\"http://i.imgur.com/zYOxPum.png\"}]},\"success\":true,\"status\":200}";

            public const string GetAlbumImage =
                "{\"data\":{\"id\":\"79MH23L\",\"title\":null,\"description\":null,\"datetime\":1446591783,\"type\":\"image/png\",\"animated\":false,\"width\":609,\"height\":738,\"size\":451530,\"views\":2849,\"bandwidth\":1286408970,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/79MH23L.png\"},\"success\":true,\"status\":200}";

            public const string GetAlbumImages =
                "{\"data\":[{\"id\":\"79MH23L\",\"title\":null,\"description\":null,\"datetime\":1446591783,\"type\":\"image/png\",\"animated\":false,\"width\":609,\"height\":738,\"size\":451530,\"views\":2849,\"bandwidth\":1286408970,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/79MH23L.png\"},{\"id\":\"CqEsRhO\",\"title\":null,\"description\":null,\"datetime\":1446591788,\"type\":\"image/png\",\"animated\":false,\"width\":552,\"height\":414,\"size\":154397,\"views\":3089,\"bandwidth\":476932333,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/CqEsRhO.png\"},{\"id\":\"zYOxPum\",\"title\":null,\"description\":null,\"datetime\":1446591791,\"type\":\"image/png\",\"animated\":false,\"width\":684,\"height\":444,\"size\":98211,\"views\":7551,\"bandwidth\":741591261,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/zYOxPum.png\"}],\"success\":true,\"status\":200}";

            public const string RemoveAlbumImages =
                "{\"data\":true,\"success\":true,\"status\":200}";

            public const string SetAlbumImages =
                "{\"data\":true,\"success\":true,\"status\":200}";

            public const string UpdateAlbum =
                "{\"data\":true,\"success\":true,\"status\":200}";
        }
    }
}