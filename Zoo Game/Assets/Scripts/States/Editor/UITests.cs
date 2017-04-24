using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Unit tests for the account stsate
    /// </summary>
    [TestFixture(Description = "The UIState contains the state of the UI as a whole")]
    public class UIStateTests : StateTestFixture<UIState>
    {
        #region Tests

        /// <summary>
        /// Tests that the default values of the UI state are set properly
        /// </summary>
        [Test]
        public void TestUIDefaultValues()
        {
            Assert.IsEmpty(State.OpenPanel);
        }

        /// <summary>
        /// Tests the behavior of the SetHUDOpen event
        /// </summary>
        [Test]
        public void TestSetHUDOpen()
        {
            ExecuteStrictAndUpdate(UIEvents.SetHUDOpen);       
            Assert.AreEqual(UIState.HUD, State.OpenPanel);

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel);
        }

        /// <summary>
        /// Tests the behavior of the SetZooCreationOpen event
        /// </summary>
        [Test]
        public void TestSetZooCreationOpen()
        {
            ExecuteStrictAndUpdate(UIEvents.SetZooCreationOpen);
            Assert.AreEqual(UIState.ZooCreation, State.OpenPanel);

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel);
        }

        #endregion
    }
}