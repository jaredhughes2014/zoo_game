using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Tests the behavior of the zoo creation state system
    /// </summary>
    [TestFixture(Description = "The zoo creation state contains the state of the zoo creation UI")]
    public class ZooCreationTests : StateTestFixture<ZooCreationState>
    {
        /// <summary>
        /// Tests the default values of the HUD state
        /// </summary>
        [Test]
        public void TestDefaultData()
        {
            Assert.AreEqual("", State.NameText, "NameText should be initialized as empty");
            Assert.IsFalse(State.Visible, "The state should not be visible by default");
            Assert.IsFalse(State.Submitted, "The submission flag should be disabled by default");
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
        /// Tests the behavior of the SetNameText event
        /// </summary>
        [Test]
        public void TestSetNameText()
        {
            var original = State.NameText;
            var expected = original + "_Test";

            ExecuteStrictAndUpdate(ZooCreationEvents.SetNameText, expected);

            Assert.AreNotEqual(original, State.NameText, "SetNameText is not changing NameText");
            Assert.AreEqual(expected, State.NameText, "SetNameText is not changing the name text to the provided value");
            Assert.AreEqual(State.NameText, MakeCopy().NameText, "Copies do not have the same name text after a SetNameText event");
        }

        /// <summary>
        /// Tests the behavior of the Submit event
        /// </summary>
        [Test]
        public void TestSubmit()
        {
            ExecuteStrictAndUpdate(ZooCreationEvents.Submit);

            Assert.IsTrue(State.Submitted, "Submit is not enabling the submitted flag");
            Assert.AreEqual(State.Submitted, MakeCopy().Submitted, "Copies do not have the same submitted flag");
        }

        /// <summary>
        /// Tests the behavior of the Reset event
        /// </summary>
        [Test]
        public void TestReset()
        {
            // Use other events to change default values
            ExecuteStrictAndUpdate(ZooCreationEvents.SetNameText, "Test");
            ExecuteStrictAndUpdate(ZooCreationEvents.Submit);
            ExecuteStrictAndUpdate(ZooCreationEvents.Reset);

            Assert.AreEqual("", State.NameText, "NameText should be empty after a reset");
            Assert.IsFalse(State.Submitted, "The submission flag should be disabled after a reset");

            var copy = MakeCopy();
            Assert.AreEqual(State.NameText, copy.NameText, "Copies do not have the same name text after a reset");
            Assert.AreEqual(State.Submitted, copy.Submitted, "Copies do not have the same submission flag after a reset");
        }

        /// <summary>
        /// Tests to make sure the Reset event does not affect the visibility, which should be
        /// controlled by a dependency
        /// </summary>
        [Test]
        public void TestResetDoesNotChangeVisibility()
        {
            var previous = State.Visible;
            ExecuteStrictAndUpdate(ZooCreationEvents.Reset);

            Assert.AreEqual(previous, State.Visible, "Reset is changing the visibility flag when the view should be hidden");

            ExecuteStrictAndUpdate(UIEvents.SetZooCreationOpen);
            previous = State.Visible;
            ExecuteStrictAndUpdate(ZooCreationEvents.Reset);

            Assert.AreEqual(previous, State.Visible, "Reset is changing the visibility flag when the view should be visible");
        }

        /// <summary>
        /// Tests the effect of the SetZooCreationOpen event from the UI event on this state
        /// </summary>
        [Test]
        public void TestSetZooCreationOpen()
        {
            ExecuteStrictAndUpdate(UIEvents.SetZooCreationOpen);
            Assert.True(State.Visible, "SetZooCreationOpen from the UI state is not making Zoo Creation visible");

            ExecuteStrictAndUpdate(UIEvents.SetHUDOpen);
            Assert.False(State.Visible, "Opening a different view in the UI is not making Zoo Creation invisible");
        }
    }
}
