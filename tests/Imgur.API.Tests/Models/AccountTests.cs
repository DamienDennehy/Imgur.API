using Imgur.API.Enums;
using Imgur.API.Models.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Imgur.API.Tests.Models
{
    [TestClass]
    public class AccountTests
    {
        [TestMethod]
        public void Account_NotorietyLevel_AreEqual()
        {
            var account = new Account {Reputation = -1};
            Assert.AreEqual(NotorietyLevel.ForeverAlone, account.Notoriety);

            account.Reputation = 0;
            Assert.AreEqual(NotorietyLevel.Neutral, account.Notoriety);

            account.Reputation = 399;
            Assert.AreEqual(NotorietyLevel.Neutral, account.Notoriety);

            account.Reputation = 400;
            Assert.AreEqual(NotorietyLevel.Accepted, account.Notoriety);

            account.Reputation = 999;
            Assert.AreEqual(NotorietyLevel.Accepted, account.Notoriety);

            account.Reputation = 1000;
            Assert.AreEqual(NotorietyLevel.Liked, account.Notoriety);

            account.Reputation = 1999;
            Assert.AreEqual(NotorietyLevel.Liked, account.Notoriety);

            account.Reputation = 2000;
            Assert.AreEqual(NotorietyLevel.Trusted, account.Notoriety);

            account.Reputation = 3999;
            Assert.AreEqual(NotorietyLevel.Trusted, account.Notoriety);

            account.Reputation = 4000;
            Assert.AreEqual(NotorietyLevel.Idolized, account.Notoriety);

            account.Reputation = 7999;
            Assert.AreEqual(NotorietyLevel.Idolized, account.Notoriety);

            account.Reputation = 8000;
            Assert.AreEqual(NotorietyLevel.Renowned, account.Notoriety);

            account.Reputation = 19999;
            Assert.AreEqual(NotorietyLevel.Renowned, account.Notoriety);

            account.Reputation = 20000;
            Assert.AreEqual(NotorietyLevel.Glorious, account.Notoriety);

            account.Reputation = 99999;
            Assert.AreEqual(NotorietyLevel.Glorious, account.Notoriety);
        }
    }
}