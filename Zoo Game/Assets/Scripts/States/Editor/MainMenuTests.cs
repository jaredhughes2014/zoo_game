using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Tests the behavior of the MainMenu state system
    /// </summary>
    [TestFixture(Description = "Tests of the MainMenuState")]
    public class MainMenuTests : StateTestFixture<MainMenuState>
    {
        /// <summary>
        /// Tests that the main menu state is initialized with the correct default values
        /// </summary>
        [Test]
        public void TestMainMenuDefaultValues()
        {
            Assert.IsEmpty(State.SelectedMap);
            Assert.IsEmpty(State.SelectedPlayMode);
            Assert.AreEqual(State.OpenPanel, MainMenuState.MainMenu);
            Assert.IsFalse(State.Submitted);
        }

        /// <summary>
        /// Tests the behavior of the open play mode menu event
        /// </summary>
        [Test]
        public void TestOpenPlayModeMenu()
        {
            ExecuteStrictAndUpdate(MainMenuEvents.OpenPlayModeMenu);
            Assert.AreEqual(MainMenuState.PlayModeSelection, State.OpenPanel);

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel);
        }

        /// <summary>
        /// Tests the behavior of the return to main menu event
        /// </summary>
        [Test]
        public void TestReturnToMain()
        {
            // Make sure to change to a different panel first to make sure this isn't using the default value
            ExecuteStrictAndUpdate(MainMenuEvents.OpenPlayModeMenu);
            ExecuteStrictAndUpdate(MainMenuEvents.ReturnToMain);

            Assert.AreEqual(MainMenuState.MainMenu, State.OpenPanel);

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel);
        }

        /// <summary>
        /// Tests the behavior of the select map event
        /// </summary>
        [Test]
        public void TestSelectMap()
        {
            var previous = State.SelectedMap;
            var expected = previous + "_Test";

            ExecuteStrictAndUpdate(MainMenuEvents.SelectMap, expected);

            Assert.AreNotEqual(previous, State.SelectedMap);
            Assert.AreEqual(expected, State.SelectedMap);

            var copy = MakeCopy();
            Assert.AreEqual(State.SelectedMap, copy.SelectedMap);
        }

        /// <summary>
        /// Tests the behavior of the select free play mode event
        /// </summary>
        [Test]
        public void TestSelectFreePlayMode()
        {
            ExecuteStrictAndUpdate(PlayModeEvents.SelectFreePlay);
            Assert.AreEqual(MainMenuState.FreePlay, State.SelectedPlayMode);
            Assert.AreEqual(MainMenuState.MapSelection, State.OpenPanel);

            var copy = MakeCopy();
            Assert.AreEqual(State.SelectedPlayMode, copy.SelectedPlayMode);
            Assert.AreEqual(State.SelectedMap, copy.SelectedMap);
        }

        /// <summary>
        /// Tests the undo play mode selection event
        /// </summary>
        [Test]
        public void TestUndoPlayModeSelection()
        {
            // Select a play mode first to make sure default state values are not being uesd
            ExecuteStrictAndUpdate(PlayModeEvents.SelectFreePlay);
            ExecuteStrictAndUpdate(PlayModeEvents.UndoPlayModeSelection);

            Assert.AreEqual("", State.SelectedPlayMode);
            Assert.AreEqual(MainMenuState.PlayModeSelection, State.OpenPanel);

            var copy = MakeCopy();
            Assert.AreEqual(State.SelectedPlayMode, copy.SelectedPlayMode);
            Assert.AreEqual(State.SelectedMap, copy.SelectedMap);
        }

        /// <summary>
        /// Tests the behavior of the submit selections event
        /// </summary>
        [Test]
        public void TestSubmitSelections()
        {
            ExecuteStrictAndUpdate(MainMenuEvents.SubmitSelections);
            Assert.IsTrue(State.Submitted);

            var copy = MakeCopy();
            Assert.AreEqual(State.Submitted, copy.Submitted);
        }

        /// <summary>
        /// Tests the behaviors of the reset selections event
        /// </summary>
        [Test]
        public void TestResetSelections()
        {
            // Set up the state with non-default values 
            ExecuteStrictAndUpdate(PlayModeEvents.SelectFreePlay);
            ExecuteStrictAndUpdate(MainMenuEvents.SelectMap, "Test");
            ExecuteStrictAndUpdate(MainMenuEvents.SubmitSelections);

            ExecuteStrictAndUpdate(MainMenuEvents.ResetSelections);

            Assert.AreEqual("", State.SelectedMap);
            Assert.AreEqual("", State.SelectedPlayMode);
            Assert.AreEqual(State.OpenPanel, MainMenuState.MainMenu);
            Assert.IsFalse(State.Submitted);

            var copy = MakeCopy();

            Assert.AreEqual(State.SelectedMap, copy.SelectedMap);
            Assert.AreEqual(State.SelectedPlayMode, copy.SelectedPlayMode);
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel);
            Assert.AreEqual(State.Submitted, copy.Submitted);
        }
    }
}
