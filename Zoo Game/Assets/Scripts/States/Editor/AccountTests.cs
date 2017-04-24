using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Unit tests for the account stsate
    /// </summary>
    [TestFixture(Description = "The AccountState contains the player's ZooGame account")]
    public class AccountStateTests : StateTestFixture<AccountState>
    {
        #region Tests

        /// <summary>
        /// Tests that the default values of the account state are set properly
        /// </summary>
        [Test]
        public void TestAccountStateDefaultValues()
        {
            Assert.NotNull(State.Alias, "The alias should not default to null");
            Assert.NotNull(State.Email, "The email should not default to null");
        }

        /// <summary>
        /// Tests that the SetAccountData event properly sets the fields it should set
        /// </summary>
        [Test]
        public void TestSetAccountData()
        {
            var alias = "TestAlias";
            var email = "test@test.com";

            ExecuteStrictAndUpdate(AccountEvents.SetAccountData, alias, email);

            Assert.AreEqual(alias, State.Alias, "SetAccountData did not update the state's alias");
            Assert.AreEqual(email, State.Email, "SetAccountData did not update the state's email");

            var copy = MakeCopy();

            Assert.AreEqual(State.Alias, copy.Alias, "Copies of the state do not have the same alias after a SetAccountData event");
            Assert.AreEqual(State.Email, copy.Email, "Copies of the state do not have the same email after a SetAccountData event");
        }

        #endregion
    }
}