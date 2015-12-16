namespace Imgur.API.Tests.FakeResponses
{
    public class AlbumEndpointResponses
    {
        public const string GetAlbumResponse =
            "{\"data\":{\"id\":\"5F5Cy\",\"title\":null,\"description\":null,\"datetime\":1446591779,\"cover\":\"79MH23L\",\"cover_width\":609,\"cover_height\":738,\"account_url\":\"sarah\",\"account_id\":9571,\"privacy\":\"public\",\"layout\":\"blog\",\"views\":4,\"link\":\"http://imgur.com/a/5F5Cy\",\"favorite\":false,\"nsfw\":null,\"section\":null,\"images_count\":3,\"images\":[{\"id\":\"79MH23L\",\"title\":null,\"description\":null,\"datetime\":1446591783,\"type\":\"image/png\",\"animated\":false,\"width\":609,\"height\":738,\"size\":451530,\"views\":2846,\"bandwidth\":1285054380,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/79MH23L.png\"},{\"id\":\"CqEsRhO\",\"title\":null,\"description\":null,\"datetime\":1446591788,\"type\":\"image/png\",\"animated\":false,\"width\":552,\"height\":414,\"size\":154397,\"views\":3087,\"bandwidth\":476623539,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/CqEsRhO.png\"},{\"id\":\"zYOxPum\",\"title\":null,\"description\":null,\"datetime\":1446591791,\"type\":\"image/png\",\"animated\":false,\"width\":684,\"height\":444,\"size\":98211,\"views\":7548,\"bandwidth\":741296628,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/zYOxPum.png\"}]},\"success\":true,\"status\":200}";

        public const string GetAlbumImagesResponse =
            "{\"data\":[{\"id\":\"79MH23L\",\"title\":null,\"description\":null,\"datetime\":1446591783,\"type\":\"image/png\",\"animated\":false,\"width\":609,\"height\":738,\"size\":451530,\"views\":2849,\"bandwidth\":1286408970,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/79MH23L.png\"},{\"id\":\"CqEsRhO\",\"title\":null,\"description\":null,\"datetime\":1446591788,\"type\":\"image/png\",\"animated\":false,\"width\":552,\"height\":414,\"size\":154397,\"views\":3089,\"bandwidth\":476932333,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/CqEsRhO.png\"},{\"id\":\"zYOxPum\",\"title\":null,\"description\":null,\"datetime\":1446591791,\"type\":\"image/png\",\"animated\":false,\"width\":684,\"height\":444,\"size\":98211,\"views\":7551,\"bandwidth\":741591261,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/zYOxPum.png\"}],\"success\":true,\"status\":200}";

        public const string GetAlbumImageResponse =
            "{\"data\":{\"id\":\"79MH23L\",\"title\":null,\"description\":null,\"datetime\":1446591783,\"type\":\"image/png\",\"animated\":false,\"width\":609,\"height\":738,\"size\":451530,\"views\":2849,\"bandwidth\":1286408970,\"vote\":null,\"favorite\":false,\"nsfw\":null,\"section\":null,\"account_url\":null,\"account_id\":null,\"comment_preview\":null,\"link\":\"http://i.imgur.com/79MH23L.png\"},\"success\":true,\"status\":200}";

        public const string CreateAlbumResponse =
            "{\"data\":{\"id\":\"3gfxo\",\"deletehash\":\"iIFJnFpVbYOvzvv\"},\"success\":true,\"status\":200}";

        public const string UpdateAlbumResponse =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string DeleteAlbumResponse =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string FavoriteAlbumResponse =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string SetAlbumImagesResponse =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string AddAlbumImagesResponse =
            "{\"data\":true,\"success\":true,\"status\":200}";

        public const string RemoveAlbumImagesResponse =
            "{\"data\":true,\"success\":true,\"status\":200}";
    }
}