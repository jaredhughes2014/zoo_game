using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Unit tests for the account stsate
    /// </summary>
    [TestFixture(Description = "The AccountState contains the player's ZooGame account")]
    public class AccountStateTests : StateTestFixture<AccountState>
    {
        /// <summary>
        /// Tests that the default values of the account state are set properly
        /// </summary>
        [Test]
        public void TestAccountStateDefaultValues()
        {
            Assert.IsEmpty(State.Alias);
            Assert.IsEmpty(State.Email);
        }

        /// <summary>
        /// Tests that the SetAccountData event properly sets the fields it should set
        /// </summary>
        [Test]
        public void TestSetAccountData()
        {
            var prevAlias = State.Alias;
            var prevEmail = State.Email;

            var alias = prevAlias + "_Test";
            var email = prevEmail + "_Test";

            ExecuteStrictAndUpdate(AccountEvents.SetAccountData, alias, email);

            Assert.AreNotEqual(prevAlias, State.Alias);
            Assert.AreEqual(alias, State.Alias);

            Assert.AreNotEqual(prevEmail, State.Email);
            Assert.AreEqual(email, State.Email);

            var copy = MakeCopy();

            Assert.AreEqual(State.Alias, copy.Alias);
            Assert.AreEqual(State.Email, copy.Email);
        }
    }
}