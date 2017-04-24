using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Tests the behavior of the HUD state system
    /// </summary>
    [TestFixture(Description = "The HUD state controls the display of the HUD")]
    public class HUDTests : StateTestFixture<HUDState>
    {
        /// <summary>
        /// Tests the default values of the HUD state
        /// </summary>
        [Test]
        public void TestDefaultData()
        {
            Assert.IsFalse(State.Visible, "State should default to not visible");
            Assert.IsFalse(State.RotateEnabled, "State should default to rotate disabled");
        }

        /// <summary>
        /// Tests that all dependencies are being used
        /// </summary>
        [Test]
        public void TestDependenciesUsed()
        {
            Assert.NotNull(GetState<UIState>(), "UI State, which is a dependency, is not being used");
        }

        /// <summary>
        /// Tests the behavior of the ToggleMovementMode event
        /// </summary>
        [Test]
        public void TestToggleMovementMode()
        {
            var start = State.RotateEnabled;

            ExecuteAndUpdate(HUDEvents.ToggleMovementMode);
            Assert.AreNotEqual(start, State.RotateEnabled, "ToggleMovementMode is not toggling the movement mode");
            Assert.AreEqual(State.RotateEnabled, MakeCopy().RotateEnabled, "Copies do not have the same rotate enabled after a ToggleMovementMode event");

            ExecuteAndUpdate(HUDEvents.ToggleMovementMode);
            Assert.AreEqual(start, State.RotateEnabled, "ToggleMovementMode does not end at its starting value after two executions");
        }

        /// <summary>
        /// Tests the effect of the SetHUDOpen event from the UI event on this state
        /// </summary>
        [Test]
        public void TestSetHUDOpen()
        {
            ExecuteAndUpdate(UIEvents.SetHUDOpen);
            Assert.True(State.Visible, "SetHUDOpen from the UI state is not making the HUD visible");

            ExecuteAndUpdate(UIEvents.SetZooCreationOpen);
            Assert.False(State.Visible, "Opening a different view in the UI is not making the HUD invisible");
        }
    }
}
