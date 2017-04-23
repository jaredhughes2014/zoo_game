using NUnit.Framework;
using Jareel;

namespace Zoo
{
    /// <summary>
    /// Base class for a Jareel State test fixture. This base class provides easy access to
    /// the tested state and simplifies the simulation of Jareel functionality without using
    /// mocking
    /// </summary>
    public class StateTestFixture<S> where S : State, new()
    {
        #region Fields

        /// <summary>
        /// The master controller
        /// </summary>
        protected ZooMaster Master { get; private set; }

        /// <summary>
        /// Used to execute updates on the master controller
        /// </summary>
        private SequentialExecutor m_executor;

        /// <summary>
        /// Used to get the current state of the master controller
        /// </summary>
        private StateSubscriber<S> m_subscriber;
        protected S State { get { return m_subscriber.State1; } }

        #endregion

        #region Setup

        /// <summary>
        /// Initializes the master controller used in each test
        /// </summary>
        [OneTimeSetUp]
        public void InitializeMaster()
        {
            Master = new ZooMaster();
            m_executor = new SequentialExecutor(Master);
            m_subscriber = Master.SubscribeToStates<S>();
        }

        /// <summary>
        /// Executes an event on the tested master controller and forces a state refresh immediately after
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments to supply with the event</param>
        protected void ExecuteAndUpdate(object ev, params object[] args)
        {
            Master.Events.Execute(ev, args);
            m_executor.Execute();
        }

        /// <summary>
        /// Executes an event in strict mode on the tested master controller and forces a state refresh immediately after
        /// </summary>
        /// <param name="ev">The event to execute</param>
        /// <param name="args">The arguments to supply with the event</param>
        protected void ExecuteStrictAndUpdate(object ev, params object[] args)
        {
            Master.Events.ExecuteStrict(ev, args);
            m_executor.Execute();
        }

        /// <summary>
        /// Generates and returns a distinct copy of the tested State
        /// </summary>
        /// <returns>Copy of the tested state</returns>
        protected AccountState MakeCopy()
        {
            return Master.SubscribeToStates<AccountState>().State1;
        }

        #endregion

        #region Tests

        /// <summary>
        /// Tests that the master controller actually creates the state. This test will always function
        /// the same way and thus cannot be inherited
        /// </summary>
        [Test]
        public void TestStateIsUsed()
        {
            Assert.NotNull(State, "Account State is not being used in the MasterController");
        }

        /// <summary>
        /// Tests that clones of the tested State do not refer to the same object
        /// </summary>
        [Test]
        public void TestStateClonesAreClones()
        {
            Assert.AreNotSame(MakeCopy(), State, string.Format("Controller of {0} is returning direct references instead of clones", typeof(S).Name));
        }

        #endregion
    }
}