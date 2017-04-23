using NUnit.Framework;
using Jareel;

namespace Zoo
{
    /// <summary>
    /// Unit tests for the account stsate
    /// </summary>
    [TestFixture(Description = "The AccountState contains the player's ZooGame account")]
    public class AccountStateTests
    {
        #region Fields

        /// <summary>
        /// The master controller
        /// </summary>
        private ZooMaster m_master;

        /// <summary>
        /// Used to execute updates on the account state
        /// </summary>
        private SequentialExecutor m_executor;

        /// <summary>
        /// Used to get the current state of the master controller
        /// </summary>
        private StateSubscriber<AccountState> m_subscriber;
        private AccountState State { get { return m_subscriber.State1; } }

        #endregion

        #region Setup

        /// <summary>
        /// Initializes the master controller used in each test
        /// </summary>
        [OneTimeSetUp]
        public void InitializeMaster()
        {
            m_master = new ZooMaster();
            m_executor = new SequentialExecutor(m_master);
            m_subscriber = m_master.SubscribeToStates<AccountState>();
        }

        /// <summary>
        /// Executes an event on the tested master controller and forces a state refresh immediately after
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments to supply with the event</param>
        private void ExecuteAndUpdate(object ev, params object[] args)
        {
            m_master.Events.Execute(ev, args);
            m_executor.Execute();
        }

        /// <summary>
        /// Executes an event in strict mode on the tested master controller and forces a state refresh immediately after
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments to supply with the event</param>
        private void ExecuteStrictAndUpdate(object ev, params object[] args)
        {
            m_master.Events.ExecuteStrict(ev, args);
            m_executor.Execute();
        }

        /// <summary>
        /// Generates and returns a distinct copy of the Account State
        /// </summary>
        /// <returns>Distinct copy of the account state</returns>
        private AccountState MakeCopy()
        {
            return m_master.SubscribeToStates<AccountState>().State1;
        }

        #endregion

        #region Tests

        /// <summary>
        /// Tests that the master controller actually creates the account state
        /// </summary>
        [Test]
        public void TestAccountStateIsUsed()
        {
            Assert.NotNull(State, "Account State is not being used in the MasterController");
        }

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
        /// Tests that clones of the AccountState are being executed correctly
        /// </summary>
        [Test]
        public void TestAccountStateCloning()
        {
            var copy = MakeCopy();

            Assert.AreNotSame(copy, State, "AccountState controller is returning direct references instead of clones");
            Assert.AreEqual(copy.Alias, State.Alias, "AccountState clones have different aliases");
            Assert.AreEqual(copy.Email, State.Email, "AccountState clones have different emails");
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