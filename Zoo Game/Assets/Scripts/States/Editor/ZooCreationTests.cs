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
            Assert.IsEmpty(State.NameText);
            Assert.IsFalse(State.Visible);
            Assert.IsFalse(State.Submitted);
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
        /// Tests the behavior of the SetNameText event
        /// </summary>
        [Test]
        public void TestSetNameText()
        {
            var original = State.NameText;
            var expected = original + "_Test";

            ExecuteStrictAndUpdate(ZooCreationEvents.SetNameText, expected);

            Assert.AreNotEqual(original, State.NameText);
            Assert.AreEqual(expected, State.NameText);

            var copy = MakeCopy();
            Assert.AreEqual(State.NameText, copy.NameText);
        }

        /// <summary>
        /// Tests the behavior of the Submit event
        /// </summary>
        [Test]
        public void TestSubmit()
        {
            ExecuteStrictAndUpdate(ZooCreationEvents.Submit);

            Assert.IsTrue(State.Submitted);

            var copy = MakeCopy();
            Assert.AreEqual(State.Submitted, copy.Submitted);
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

            Assert.AreEqual("", State.NameText);
            Assert.IsFalse(State.Submitted);

            var copy = MakeCopy();
            Assert.AreEqual(State.NameText, copy.NameText);
            Assert.AreEqual(State.Submitted, copy.Submitted);
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

            Assert.AreEqual(previous, State.Visible);

            ExecuteStrictAndUpdate(UIEvents.SetZooCreationOpen);
            previous = State.Visible;
            ExecuteStrictAndUpdate(ZooCreationEvents.Reset);

            Assert.AreEqual(previous, State.Visible);
        }

        /// <summary>
        /// Tests the effect of the SetZooCreationOpen event from the UI event on this state
        /// </summary>
        [Test]
        public void TestSetZooCreationOpen()
        {
            ExecuteStrictAndUpdate(UIEvents.SetZooCreationOpen);
            Assert.True(State.Visible);

            ExecuteStrictAndUpdate(UIEvents.SetHUDOpen);
            Assert.False(State.Visible);
        }
    }
}
