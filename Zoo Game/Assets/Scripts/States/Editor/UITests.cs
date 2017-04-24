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
            Assert.AreEqual("", State.OpenPanel, "The open panel should default to empty");
        }

        /// <summary>
        /// Tests the behavior of the SetHUDOpen event
        /// </summary>
        [Test]
        public void TestSetHUDOpen()
        {
            ExecuteStrictAndUpdate(UIEvents.SetHUDOpen);       
            Assert.AreEqual(UIState.HUD, State.OpenPanel, "SetHUDOpen is not setting the open panel to the HUD");

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel, "Copies do not have the same open panel after the SetHUDOpen event");
        }

        /// <summary>
        /// Tests the behavior of the SetZooCreationOpen event
        /// </summary>
        [Test]
        public void TestSetZooCreationOpen()
        {
            ExecuteStrictAndUpdate(UIEvents.SetZooCreationOpen);
            Assert.AreEqual(UIState.ZooCreation, State.OpenPanel, "SetZooCreationOpen is not setting the open panel to the ZooCreation");

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel, "Copies do not have the same open panel after the SetZooCreationOpen event");
        }

        #endregion
    }
}