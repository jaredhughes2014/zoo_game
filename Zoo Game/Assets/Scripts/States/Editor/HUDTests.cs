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
            Assert.IsFalse(State.Visible);
            Assert.IsFalse(State.RotateEnabled);
        }

        /// <summary>
        /// Tests that all dependencies are being used
        /// </summary>
        [Test]
        public void TestDependenciesUsed()
        {
            Assert.NotNull(GetState<UIState>());
        }

        /// <summary>
        /// Tests the behavior of the ToggleMovementMode event
        /// </summary>
        [Test]
        public void TestToggleMovementMode()
        {
            var start = State.RotateEnabled;

            ExecuteAndUpdate(HUDEvents.ToggleMovementMode);

            Assert.AreNotEqual(start, State.RotateEnabled);
            var copy = MakeCopy();
            Assert.AreEqual(State.RotateEnabled, copy.RotateEnabled);

            ExecuteAndUpdate(HUDEvents.ToggleMovementMode);
            Assert.AreEqual(start, State.RotateEnabled);
        }

        /// <summary>
        /// Tests the effect of the SetHUDOpen event from the UI event on this state
        /// </summary>
        [Test]
        public void TestSetHUDOpen()
        {
            ExecuteAndUpdate(UIEvents.SetHUDOpen);
            Assert.True(State.Visible);

            ExecuteAndUpdate(UIEvents.SetZooCreationOpen);
            Assert.False(State.Visible);
        }
    }
}
