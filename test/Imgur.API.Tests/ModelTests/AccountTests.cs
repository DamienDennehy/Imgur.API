using Imgur.API.Enums;
using Imgur.API.Models.Impl;
using Xunit;

namespace Imgur.API.Tests.ModelTests
{
    public class AccountTests
    {
        [Fact]
        public void Account_NotorietyLevel_Equal()
        {
            var account = new Account {Reputation = -1};
            Assert.Equal(NotorietyLevel.ForeverAlone, account.Notoriety);

            account.Reputation = 0;
            Assert.Equal(NotorietyLevel.Neutral, account.Notoriety);

            account.Reputation = 399;
            Assert.Equal(NotorietyLevel.Neutral, account.Notoriety);

            account.Reputation = 400;
            Assert.Equal(NotorietyLevel.Accepted, account.Notoriety);

            account.Reputation = 999;
            Assert.Equal(NotorietyLevel.Accepted, account.Notoriety);

            account.Reputation = 1000;
            Assert.Equal(NotorietyLevel.Liked, account.Notoriety);

            account.Reputation = 1999;
            Assert.Equal(NotorietyLevel.Liked, account.Notoriety);

            account.Reputation = 2000;
            Assert.Equal(NotorietyLevel.Trusted, account.Notoriety);

            account.Reputation = 3999;
            Assert.Equal(NotorietyLevel.Trusted, account.Notoriety);

            account.Reputation = 4000;
            Assert.Equal(NotorietyLevel.Idolized, account.Notoriety);

            account.Reputation = 7999;
            Assert.Equal(NotorietyLevel.Idolized, account.Notoriety);

            account.Reputation = 8000;
            Assert.Equal(NotorietyLevel.Renowned, account.Notoriety);

            account.Reputation = 19999;
            Assert.Equal(NotorietyLevel.Renowned, account.Notoriety);

            account.Reputation = 20000;
            Assert.Equal(NotorietyLevel.Glorious, account.Notoriety);

            account.Reputation = 99999;
            Assert.Equal(NotorietyLevel.Glorious, account.Notoriety);
        }
    }
}