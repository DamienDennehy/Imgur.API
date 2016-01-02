using Imgur.API.Enums;

namespace Imgur.API.Models.Impl
{
    public partial class Account
    {
        /// <summary>
        ///     Notoriety level based on a user's reputation.
        /// </summary>
        public NotorietyLevel Notoriety
        {
            get
            {
                if (Reputation >= 20000)
                    return NotorietyLevel.Glorious;

                if (Reputation >= 8000 && Reputation <= 19999)
                    return NotorietyLevel.Renowned;

                if (Reputation >= 4000 && Reputation <= 7999)
                    return NotorietyLevel.Idolized;

                if (Reputation >= 2000 && Reputation <= 3999)
                    return NotorietyLevel.Trusted;

                if (Reputation >= 1000 && Reputation <= 1999)
                    return NotorietyLevel.Liked;

                if (Reputation >= 400 && Reputation <= 999)
                    return NotorietyLevel.Accepted;

                if (Reputation >= 0 && Reputation <= 399)
                    return NotorietyLevel.Neutral;

                return NotorietyLevel.ForeverAlone;
            }
        }
    }
}