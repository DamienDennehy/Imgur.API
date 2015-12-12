using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Imgur.API.Authentication.Impl;
using Imgur.API.Endpoints;
using Imgur.API.Endpoints.Impl;
using Imgur.API.Exceptions;
using Imgur.API.Models;
using Imgur.API.Models.Impl;
using Imgur.API.Tests.Fakes.FakeResponses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace Imgur.API.Tests.Endpoints
{
    [TestClass]
    public class AccountEndpointTests
    {
        [TestMethod]
        public void GetAccountAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountAsync("Bob");
            endpoint.Received().GetAccountAsync("Bob");
        }

        [TestMethod]
        public void GetAccountAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountAsync();
            endpoint.Received().GetAccountAsync("me");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountAsync();
        }

        [TestMethod]
        public void GetAccountAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var account = endpoint.ProcessEndpointResponse<Account>(AccountEndpointResponses.Imgur.GetAccountResponse);

            Assert.AreEqual(12456, account.Id);
            Assert.AreEqual("Bob", account.Url);
            Assert.AreEqual(null, account.Bio);
            Assert.AreEqual(4343, account.Reputation);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1229591601), account.Created);
        }

        [TestMethod]
        public void GetAccountGalleryFavoritesAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountGalleryFavoritesAsync("Bob");
            endpoint.Received().GetAccountGalleryFavoritesAsync("Bob");
        }

        [TestMethod]
        public void GetAccountGalleryFavoritesAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountGalleryFavoritesAsync();
            endpoint.Received().GetAccountGalleryFavoritesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountGalleryFavoritesAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountGalleryFavoritesAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountGalleryFavoritesAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException
            ()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountGalleryFavoritesAsync();
        }

        [TestMethod]
        public void GetAccountGalleryFavoritesAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var favorites =
                endpoint.ProcessEndpointResponse<IEnumerable<GalleryItem>>(
                    AccountEndpointResponses.Imgur.GetAccountGalleryFavoritesResponse);

            Assert.IsTrue(favorites.Any());
        }

        [TestMethod]
        public void GetAccountFavoritesAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountFavoritesAsync();
            endpoint.Received().GetAccountFavoritesAsync();
        }

        [TestMethod]
        public void GetAccountFavoritesAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountFavoritesAsync();
            endpoint.Received().GetAccountFavoritesAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountFavoritesAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountFavoritesAsync();
        }

        [TestMethod]
        public void GetAccountFavoritesAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var favorites =
                endpoint.ProcessEndpointResponse<IEnumerable<object>>(
                    AccountEndpointResponses.Imgur.GetAccountFavoritesResponse);

            Assert.IsTrue(favorites.Any());
        }

        [TestMethod]
        public void GetAccountSubmissionsAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountSubmissionsAsync("Bob");
            endpoint.Received().GetAccountSubmissionsAsync("Bob");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountSubmissionsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountSubmissionsAsync();
        }

        [TestMethod]
        public void GetAccountSubmissionsAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountSubmissionsAsync();
            endpoint.Received().GetAccountSubmissionsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountSubmissionsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountSubmissionsAsync(null);
        }

        [TestMethod]
        public void GetAccountSubmissionsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var submissions =
                endpoint.ProcessEndpointResponse<IEnumerable<GalleryItem>>(
                    AccountEndpointResponses.Imgur.GetAccountSubmissionsResponse);

            Assert.IsTrue(submissions.Any());
        }

        [TestMethod]
        public void GetAccountSettingsAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAccountSettingsAsync();
            endpoint.Received().GetAccountSettingsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAccountSettingsAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAccountSettingsAsync();
        }

        [TestMethod]
        public void GetAccountSettingsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var accountSettings =
                endpoint.ProcessEndpointResponse<AccountSettings>(
                    AccountEndpointResponses.Imgur.GetAccountSettingsResponse);

            Assert.AreEqual(true, accountSettings.AcceptedGalleryTerms);
            Assert.AreEqual("ImgurApiTest@noreply.com", accountSettings.ActiveEmails.First());
            Assert.AreEqual(AlbumPrivacy.Secret, accountSettings.AlbumPrivacy);
            Assert.AreEqual(45454554, accountSettings.BlockedUsers.First().BlockedId);
            Assert.AreEqual("Bob", accountSettings.BlockedUsers.First().BlockedUrl);
            Assert.AreEqual("ImgurApiTest@noreply.com", accountSettings.Email);
            Assert.AreEqual(false, accountSettings.HighQuality);
            Assert.AreEqual(true, accountSettings.MessagingEnabled);
            Assert.AreEqual(false, accountSettings.PublicImages);
            Assert.AreEqual(true, accountSettings.ShowMature);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task UpdateAccountSettings_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.UpdateAccountSettingsAsync("1234");
        }

        [TestMethod]
        public void UpdateAccountSettingsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var updated = endpoint.ProcessEndpointResponse<bool>(
                GenericEndpointResponses.Imgur.SuccessfulResponse);

            Assert.IsTrue(updated);
        }

        [TestMethod]
        public void GetGalleryProfileAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetGalleryProfileAsync("Bob");
            endpoint.Received().GetGalleryProfileAsync("Bob");
        }

        [TestMethod]
        public void GetGalleryProfileAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetGalleryProfileAsync();
            endpoint.Received().GetGalleryProfileAsync("me");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryProfileAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetGalleryProfileAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetGalleryProfileAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetGalleryProfileAsync();
        }

        [TestMethod]
        public void GetGalleryProfileAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var profile =
                endpoint.ProcessEndpointResponse<GalleryProfile>(
                    AccountEndpointResponses.Imgur.GetGalleryProfileResponse);

            Assert.AreEqual(1470, profile.TotalGalleryComments);
            Assert.AreEqual(3068, profile.TotalGalleryFavorites);
            Assert.AreEqual(156, profile.TotalGallerySubmissions);

            var trophy = profile.Trophies.First(x => x.Data == "114377540");

            Assert.AreEqual("114377540", trophy.Data);
            Assert.AreEqual("/gallery/RdU6zgv/comment/114377540", trophy.DataLink);
            Assert.AreEqual("4852550", trophy.Id);
            Assert.AreEqual("Comment sparked a large reply thread.", trophy.Description);
            Assert.AreEqual("http://s.imgur.com/images/trophies/e8a901.png", trophy.Image);
            Assert.AreEqual("Conversation Starter", trophy.Name);
            Assert.AreEqual("ReplyThread", trophy.NameClean);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1380321520), trophy.DateTime);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task VerifyEmailAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.VerifyEmailAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void VerifyEmailAsync_UnauthorizedAccess_ThrowsImgurException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            var verified =
                endpoint.ProcessEndpointResponse<bool>(AccountEndpointResponses.Imgur.VerifyEmailErrorResponse);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task SendVerificationEmailAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.SendVerificationEmailAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void SendVerificationEmailAsync_WithErrorResponse_AreEqual()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);

            var sent = endpoint.ProcessEndpointResponse<bool>(AccountEndpointResponses.Imgur.SendEmailErrorResponse);
        }

        [TestMethod]
        public void SendVerificationEmailAsync_WithValidResponse_AreEqual()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);

            var sent = endpoint.ProcessEndpointResponse<bool>(GenericEndpointResponses.Imgur.SuccessfulResponse);

            Assert.IsTrue(sent);
        }

        [TestMethod]
        public void GetAlbumsAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumsAsync("Bob", 7);
            endpoint.Received().GetAlbumsAsync("Bob", 7);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumsAsync();
        }

        [TestMethod]
        public void GetAlbumsAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumsAsync();
            endpoint.Received().GetAlbumsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumsAsync(null);
        }

        [TestMethod]
        public void GetAlbumsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var albums =
                endpoint.ProcessEndpointResponse<IEnumerable<Album>>(
                    AccountEndpointResponses.Imgur.GetAlbumsResponse);

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        public void GetAlbumAsync_WithId_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumAsync("dfOdfL");
            endpoint.Received().GetAlbumAsync("dfOdfL");
        }

        [TestMethod]
        public void GetAlbumAsync_WithIdAndUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumAsync("dfOdfL", "Bob");
            endpoint.Received().GetAlbumAsync("dfOdfL", "Bob");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumAsync_WithDefaultUsername_AndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumAsync("78878");
        }

        [TestMethod]
        public void GetAlbumAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var album =
                endpoint.ProcessEndpointResponse<Album>(
                    AccountEndpointResponses.Imgur.GetAlbumResponse);

            Assert.IsNotNull(album);
        }

        [TestMethod]
        public void GetAlbumIdsAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumIdsAsync("Bob", 7);
            endpoint.Received().GetAlbumIdsAsync("Bob", 7);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumIdsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumIdsAsync();
        }

        [TestMethod]
        public void GetAlbumIdsAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumIdsAsync();
            endpoint.Received().GetAlbumIdsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumIdsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumIdsAsync(null);
        }

        [TestMethod]
        public void GetAlbumIdsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var albums =
                endpoint.ProcessEndpointResponse<IEnumerable<string>>(
                    AccountEndpointResponses.Imgur.GetAlbumIdsResponse);

            Assert.AreEqual(50, albums.Count());
        }

        [TestMethod]
        public void GetAlbumCountAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumCountAsync("Bob");
            endpoint.Received().GetAlbumCountAsync("Bob");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumCountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumCountAsync();
        }

        [TestMethod]
        public void GetAlbumCountAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetAlbumCountAsync();
            endpoint.Received().GetAlbumCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetAlbumCountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetAlbumCountAsync(null);
        }

        [TestMethod]
        public void GetAlbumCountAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var albums =
                endpoint.ProcessEndpointResponse<int>(
                    AccountEndpointResponses.Imgur.GetAlbumCountResponse);

            Assert.AreEqual(105, albums);
        }

        [TestMethod]
        public void DeleteAlbumAsync_WithId_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.DeleteAlbumAsync("12345");
            endpoint.Received().DeleteAlbumAsync("12345");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteAlbumAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteAlbumAsync_WithOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteAlbumAsync("1234");
        }

        [TestMethod]
        public void DeleteAlbumAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var deleted =
                endpoint.ProcessEndpointResponse<bool>(
                    AccountEndpointResponses.Imgur.DeleteAlbumResponse);

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void DeleteAlbumAsync_WithErrorReponse_ThrowsImgurError()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var deleted =
                endpoint.ProcessEndpointResponse<bool>(
                    AccountEndpointResponses.Imgur.DeleteAlbumErrorResponse);

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        public void GetCommentsAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentsAsync("Bob", CommentSortOrder.Best, 7);
            endpoint.Received().GetCommentsAsync("Bob", CommentSortOrder.Best, 7);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentsAsync();
        }

        [TestMethod]
        public void GetCommentsAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentsAsync();
            endpoint.Received().GetCommentsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentsAsync(null);
        }

        [TestMethod]
        public void GetCommentsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var comments =
                endpoint.ProcessEndpointResponse<IEnumerable<Comment>>(
                    AccountEndpointResponses.Imgur.GetCommentsResponse);

            Assert.AreEqual(50, comments.Count());
        }

        [TestMethod]
        public void GetCommentAsync_WithId_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentAsync("dfOdfL");
            endpoint.Received().GetCommentAsync("dfOdfL");
        }

        [TestMethod]
        public void GetCommentAsync_WithIdAndUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentAsync("dfOdfL", "Bob");
            endpoint.Received().GetCommentAsync("dfOdfL", "Bob");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentAsync(null);
        }

        [TestMethod]
        public void GetCommentAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var comment =
                endpoint.ProcessEndpointResponse<Comment>(
                    AccountEndpointResponses.Imgur.GetCommentResponse);

            Assert.IsNotNull(comment);
            Assert.AreEqual("scabab", comment.Author);
            Assert.AreEqual(487008510, comment.Id);
            Assert.AreEqual(null, comment.AlbumCover);
            Assert.AreEqual(4194299, comment.AuthorId);
            Assert.AreEqual(0, comment.Children.Count());
            Assert.AreEqual(
                "gyroscope detectors measure inertia.. the stabilization is done entirely by brushless motors. there are stabilizers which actually use 1/2",
                comment.CommentText);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1443969120), comment.DateTime);
            Assert.AreEqual(false, comment.Deleted);
            Assert.AreEqual(0, comment.Downs);
            Assert.AreEqual("DMcOm2V", comment.ImageId);
            Assert.AreEqual(false, comment.OnAlbum);
            Assert.AreEqual(486983435, comment.ParentId);
            Assert.AreEqual(24, comment.Points);
            Assert.AreEqual(24, comment.Ups);
            Assert.AreEqual(null, comment.Vote);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentIdsAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentIdsAsync();
        }

        [TestMethod]
        public void GetCommentIdsAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentIdsAsync();
            endpoint.Received().GetCommentIdsAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentIdsAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentIdsAsync(null);
        }

        [TestMethod]
        public void GetCommentIdsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var Comments =
                endpoint.ProcessEndpointResponse<IEnumerable<string>>(
                    AccountEndpointResponses.Imgur.GetCommentIdsResponse);

            Assert.AreEqual(50, Comments.Count());
        }

        [TestMethod]
        public void GetCommentCountAsync_WithUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentCountAsync("Bob");
            endpoint.Received().GetCommentCountAsync("Bob");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentCountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentCountAsync();
        }

        [TestMethod]
        public void GetCommentCountAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetCommentCountAsync();
            endpoint.Received().GetCommentCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetCommentCountAsync_WithNullUsername_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetCommentCountAsync(null);
        }

        [TestMethod]
        public void GetCommentCountAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var count =
                endpoint.ProcessEndpointResponse<int>(
                    AccountEndpointResponses.Imgur.GetCommentCountResponse);

            Assert.AreEqual(1500, count);
        }

        [TestMethod]
        public void DeleteCommentAsync_WithId_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.DeleteCommentAsync("12345");
            endpoint.Received().DeleteCommentAsync("12345");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteCommentAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteCommentAsync_WithOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteCommentAsync("1234");
        }

        [TestMethod]
        public void DeleteCommentAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var deleted =
                endpoint.ProcessEndpointResponse<bool>(
                    AccountEndpointResponses.Imgur.DeleteCommentResponse);

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void DeleteCommentAsync_WithErrorReponse_ThrowsImgurError()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var deleted =
                endpoint.ProcessEndpointResponse<bool>(
                    AccountEndpointResponses.Imgur.DeleteCommentErrorResponse);

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        public void GetImagesAsync_WithPage_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetImagesAsync(3);
            endpoint.Received().GetImagesAsync(3);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImagesAsync_OAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImagesAsync();
        }

        [TestMethod]
        public void GetImagesAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetImagesAsync();
            endpoint.Received().GetImagesAsync();
        }

        [TestMethod]
        public void GetImagesAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var images =
                endpoint.ProcessEndpointResponse<IEnumerable<Image>>(
                    AccountEndpointResponses.Imgur.GetImagesResponse);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        public void GetImageAsync_WithId_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetImageAsync("dfOdfL");
            endpoint.Received().GetImageAsync("dfOdfL");
        }

        [TestMethod]
        public void GetImageAsync_WithIdAndUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetImageAsync("dfOdfL", "Bob");
            endpoint.Received().GetImageAsync("dfOdfL", "Bob");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageAsync(null);
        }

        [TestMethod]
        public void GetImageAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var image =
                endpoint.ProcessEndpointResponse<Image>(
                    AccountEndpointResponses.Imgur.GetImageResponse);

            Assert.IsNotNull(image);
            Assert.AreEqual("hbzm7Ge", image.Id);
            Assert.AreEqual(
                "For three days at Camp Imgur, the Imgur flag flew proudly over our humble redwood camp, greeting Imgurians each morning.",
                image.Title);
            Assert.AreEqual(null, image.Description);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1443651980), image.DateTime);
            Assert.AreEqual("image/gif", image.Type);
            Assert.AreEqual(true, image.Animated);
            Assert.AreEqual(406, image.Width);
            Assert.AreEqual(720, image.Height);
            Assert.AreEqual(23386145, image.Size);
            Assert.AreEqual(329881, image.Views);
            Assert.AreEqual(7714644898745, image.Bandwidth);
            Assert.AreEqual(null, image.DeleteHash);
            Assert.AreEqual(null, image.Name);
            Assert.AreEqual(null, image.Section);
            Assert.AreEqual("http://i.imgur.com/hbzm7Geh.gif", image.Link);
            Assert.AreEqual("http://i.imgur.com/hbzm7Ge.gifv", image.Gifv);
            Assert.AreEqual("http://i.imgur.com/hbzm7Ge.mp4", image.Mp4);
            Assert.AreEqual("http://i.imgur.com/hbzm7Ge.webm", image.Webm);
            Assert.AreEqual(true, image.Looping);
            Assert.AreEqual(false, image.Favorite);
            Assert.AreEqual(null, image.Nsfw);
            Assert.AreEqual(null, image.Vote);
            Assert.AreEqual(null, image.AccountUrl);
            Assert.AreEqual(null, image.AccountId);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageIdsAsync_WithOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageIdsAsync();
        }

        [TestMethod]
        public void GetImageIdsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var images =
                endpoint.ProcessEndpointResponse<IEnumerable<string>>(
                    AccountEndpointResponses.Imgur.GetImageIdsResponse);

            Assert.AreEqual(2, images.Count());
        }

        [TestMethod]
        public void GetImageCountAsync_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetImageCountAsync();
            endpoint.Received().GetImageCountAsync();
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetImageCountAsync_WithDefaultUsernameAndOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetImageCountAsync();
        }

        [TestMethod]
        public void GetImageCountAsync_WithDefaultUsername_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetImageCountAsync();
            endpoint.Received().GetImageCountAsync();
        }

        [TestMethod]
        public void GetImageCountAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var count =
                endpoint.ProcessEndpointResponse<int>(
                    AccountEndpointResponses.Imgur.GetImageCountResponse);

            Assert.AreEqual(2, count);
        }

        [TestMethod]
        public void DeleteImageAsync_WithDeleteHash_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.DeleteImageAsync("jhjhkhjhsdfsfs");
            endpoint.Received().DeleteImageAsync("jhjhkhjhsdfsfs");
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithNullId_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteImageAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task DeleteImageAsync_WithOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.DeleteImageAsync("1234");
        }

        [TestMethod]
        public void DeleteImageAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var deleted =
                endpoint.ProcessEndpointResponse<bool>(
                    AccountEndpointResponses.Imgur.DeleteImageResponse);

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ImgurException))]
        public void DeleteImageAsync_WithErrorReponse_ThrowsImgurError()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var deleted =
                endpoint.ProcessEndpointResponse<bool>(
                    AccountEndpointResponses.Imgur.DeleteImageErrorResponse);

            Assert.AreEqual(true, deleted);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public async Task GetNotificationsAsync_WithOAuth2NotSet_ThrowsArgumentNullException()
        {
            var imgurAuth = new ImgurClient("123", "1234");
            var endpoint = new AccountEndpoint(imgurAuth);
            await endpoint.GetNotificationsAsync();
        }

        [TestMethod]
        public void GetNotificationsAsync_WithAllNotifications_ReceivedIsTrue()
        {
            var endpoint = Substitute.For<IAccountEndpoint>();
            endpoint.GetNotificationsAsync(false);
            endpoint.Received().GetNotificationsAsync(false);
        }

        [TestMethod]
        public void GetNotificationsAsync_WithValidReponse_AreEqual()
        {
            var endpoint = Substitute.ForPartsOf<EndpointBase>();
            var notifications =
                endpoint.ProcessEndpointResponse<Notifications>(
                    AccountEndpointResponses.Imgur.GetNotifications);

            var messageNotification = notifications.Messages.FirstOrDefault();
            var message = messageNotification.Content as IMessage;

            Assert.IsNotNull(messageNotification);
            Assert.IsNotNull(message);

            Assert.AreEqual(4523, messageNotification.Id);
            Assert.AreEqual(384077, messageNotification.AccountId);
            Assert.AreEqual(false, messageNotification.Viewed);

            Assert.AreEqual(384077, message.AccountId);
            Assert.AreEqual(620, message.Id);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1406935917), message.DateTime);
            Assert.AreEqual("jasdev", message.From);
            Assert.AreEqual("wow. such message.", message.LastMessage);
            Assert.AreEqual(103, message.MessageNum);
            Assert.AreEqual(3698510, message.WithAccountId);

            var commentNotification = notifications.Replies.FirstOrDefault();
            var comment = commentNotification.Content as IComment;

            Assert.IsNotNull(commentNotification);
            Assert.IsNotNull(comment);

            Assert.AreEqual(4511, commentNotification.Id);
            Assert.AreEqual(384077, commentNotification.AccountId);
            Assert.AreEqual(false, commentNotification.Viewed);

            Assert.AreEqual(3616, comment.Id);
            Assert.AreEqual("vk9vqcm", comment.ImageId);
            Assert.AreEqual("reply test", comment.CommentText);
            Assert.AreEqual("jasdev", comment.Author);
            Assert.AreEqual(3698510, comment.AuthorId);
            Assert.AreEqual(false, comment.OnAlbum);
            Assert.AreEqual(null, comment.AlbumCover);
            Assert.AreEqual(1, comment.Ups);
            Assert.AreEqual(0, comment.Downs);
            Assert.AreEqual(1, comment.Points);
            Assert.AreEqual(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(1406070774), comment.DateTime);
            Assert.AreEqual(3615, comment.ParentId);
            Assert.AreEqual(false, comment.Deleted);
            Assert.AreEqual(null, comment.Vote);
        }
    }
}