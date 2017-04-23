using NUnit.Framework;

namespace Zoo.Test
{
    /// <summary>
    /// Tests the behavior of the MainMenu state system
    /// </summary>
    [TestFixture(Description = "Tests of the MainMenuState")]
    public class MainMenuTest : StateTestFixture<MainMenuState>
    {
        /// <summary>
        /// Tests that the main menu state is initialized with the correct default values
        /// </summary>
        [Test]
        public void TestMainMenuDefaultValues()
        {
            Assert.AreEqual("", State.SelectedMap, "The selected map should be initialized to empty");
            Assert.AreEqual("", State.SelectedPlayMode, "The selected play mode should initialized to empty");
            Assert.AreEqual(State.OpenPanel, MainMenuState.MainMenu, "The open panel should default to the main menu");
            Assert.IsFalse(State.Submitted, "The submitted flag should be initialized to false");
        }

        /// <summary>
        /// Tests the behavior of the open play mode menu event
        /// </summary>
        [Test]
        public void TestOpenPlayModeMenu()
        {
            ExecuteStrictAndUpdate(MainMenuEvents.OpenPlayModeMenu);
            Assert.AreEqual(MainMenuState.PlayModeSelection, State.OpenPanel, "OpenPlayModeMenu is not transitioning to the correct panel");

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel, "Copies do not have the same OpenPanel after the OpenPlayModeMenu event");
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

            Assert.AreEqual(MainMenuState.MainMenu, State.OpenPanel, "ReturnToMain is not transitioning to the correct panel");

            var copy = MakeCopy();
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel, "Copies do not have the same OpenPanel after the ReturnToMain event");
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

            Assert.AreNotEqual(previous, State.SelectedMap, "SelectMap is not changing the selected map");
            Assert.AreEqual(expected, State.SelectedMap, "SelectMap is not changing to the provided value");

            var copy = MakeCopy();
            Assert.AreEqual(State.SelectedMap, copy.SelectedMap, "Copies do not have the same SelectedMap after the SelectMap event");
        }

        /// <summary>
        /// Tests the behavior of the select free play mode event
        /// </summary>
        [Test]
        public void TestSelectFreePlayMode()
        {
            ExecuteStrictAndUpdate(PlayModeEvents.SelectFreePlay);
            Assert.AreEqual(MainMenuState.FreePlay, State.SelectedPlayMode, "SelectFreePlay does not change the state to free play mode");
            Assert.AreEqual(MainMenuState.MapSelection, State.OpenPanel, "SelectFreePlay does not change the open panel to map selection");

            var copy = MakeCopy();
            Assert.AreEqual(State.SelectedPlayMode, copy.SelectedPlayMode, "Copies do not have the same SelectedPlayMode after the SelectFreePlay event");
            Assert.AreEqual(State.SelectedMap, copy.SelectedMap, "Copies do not have the same SelectedMap after the SelectFreePlay event");
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

            Assert.AreEqual("", State.SelectedPlayMode, "UndoPlayModeSelection does not clear the play mode data");
            Assert.AreEqual(MainMenuState.PlayModeSelection, State.OpenPanel, "UndoPlayModeSelection does not change the open panel to play mode selection");

            var copy = MakeCopy();
            Assert.AreEqual(State.SelectedPlayMode, copy.SelectedPlayMode, "Copies do not have the same SelectedPlayMode after the UndoPlayModeSelection event");
            Assert.AreEqual(State.SelectedMap, copy.SelectedMap, "Copies do not have the same SelectedMap after the UndoPlayModeSelection event");
        }

        /// <summary>
        /// Tests the behavior of the submit selections event
        /// </summary>
        [Test]
        public void TestSubmitSelections()
        {
            ExecuteStrictAndUpdate(MainMenuEvents.SubmitSelections);
            Assert.IsTrue(State.Submitted, "SubmitSelections is not setting the submitted flag to true");

            var copy = MakeCopy();
            Assert.AreEqual(State.Submitted, copy.Submitted, "Copies do not have the same submitted flag after SubmitSelections");
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

            Assert.AreEqual("", State.SelectedMap, "The selected map should be reset to empty");
            Assert.AreEqual("", State.SelectedPlayMode, "The selected play mode should reset to empty");
            Assert.AreEqual(State.OpenPanel, MainMenuState.MainMenu, "The open panel should reset to the main menu");
            Assert.IsFalse(State.Submitted, "The submitted flag should be reset to false");

            var copy = MakeCopy();

            Assert.AreEqual(State.SelectedMap, copy.SelectedMap, "Copies do not have the same SelectedMap after the ResetSelections event");
            Assert.AreEqual(State.SelectedPlayMode, copy.SelectedPlayMode, "Copies do not have the same SelectedPlayMode after the ResetSelections event");
            Assert.AreEqual(State.OpenPanel, copy.OpenPanel, "Copies do not have the same OpenPanel after the ResetSelections event");
            Assert.AreEqual(State.Submitted, copy.Submitted, "Copies do not have the same Submitted flag after the ResetSelections event");
        }
    }
}
