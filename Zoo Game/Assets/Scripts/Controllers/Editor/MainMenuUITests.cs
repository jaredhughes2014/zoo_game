using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using Zoo.UI;

namespace Zoo.Controllers.Test
{
    /// <summary>
    /// Tests the interaction between the state system and the main menu UI
    /// </summary>
    public class MainMenuUITests : ControllerTestFixture<MainMenuUIController>
    {
        #region View Shortcuts

        /// <summary>
        /// Shortcut to the MainMenuView attached to the controller
        /// </summary>
        private MainMenuView MainMenu { get { return Controller.MainMenu; } }

        /// <summary>
        /// Shortcut to the MapSelectionView attached to the controller
        /// </summary>
        private MapSelectionView MapSelection { get { return Controller.MapSelection; } }

        /// <summary>
        /// Shortcut to the PlayModeSelectionView attached to the controller
        /// </summary>
        private PlayModeSelectionView PlayModeSelection { get { return Controller.PlayModeSelection; } }

        #endregion

        #region Setup

        /// <summary>
        /// Overriden to connect the views to the controller
        /// </summary>
        public override void InitializeController()
        {
            base.InitializeController();

            GenerateMainMenu();
            GenerateMapSelection();
            GeneratePlayModeSelection();
        }

        /// <summary>
        /// Generates a functional version of the main menu view and attaches it to the
        /// controller
        /// </summary>
        private void GenerateMainMenu()
        {
            Controller.MainMenu = MakeBehaviour<MainMenuView>();
        }

        /// <summary>
        /// Generates a functional version of the map selection view and attaches it to the
        /// controller
        /// </summary>
        private void GenerateMapSelection()
        {
            Controller.MapSelection = MakeBehaviour<MapSelectionView>();
        }

        /// <summary>
        /// Generates a functional version of the play mode selection view and attaches it to the
        /// controller
        /// </summary>
        private void GeneratePlayModeSelection()
        {
            Controller.PlayModeSelection = MakeBehaviour<PlayModeSelectionView>();
        }

        #endregion

        #region Test Cases

        /// <summary>
        /// Tests that the main menu is open by default
        /// </summary>
        [UnityTest]
        public IEnumerator TestDefaultMainMenuOpen()
        {
            // At least one frame should execute to receive state changes
            yield return null;

            Assert.IsTrue(MainMenu.gameObject.activeInHierarchy);
            Assert.IsFalse(MapSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(PlayModeSelection.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that the OpenPlayModeSelection event activates the proper view
        /// </summary>
        [UnityTest]
        public IEnumerator TestOpenPlayModeSelection()
        {
            // At least one frame should execute to receive state changes
            yield return ExecuteStrictAndUpdate(MainMenuEvents.OpenPlayModeMenu);

            Assert.IsTrue(PlayModeSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MainMenu.gameObject.activeInHierarchy);
            Assert.IsFalse(MapSelection.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that the SelectFreePlay event leads to the map selection view opening
        /// </summary>
        [UnityTest]
        public IEnumerator TestSelectFreePlayOpensMapSelection()
        {
            // At least one frame should execute to receive state changes
            yield return ExecuteStrictAndUpdate(PlayModeEvents.SelectFreePlay);

            Assert.IsTrue(MapSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(PlayModeSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MainMenu.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that the UndoPlayModeSelection event closes the map selection view and
        /// opens reopens the play mode selection view
        /// </summary>
        [UnityTest]
        public IEnumerator TestUndoPlayModeSelection()
        {
            // At least one frame should execute to receive state changes
            yield return ExecuteStrictAndUpdate(PlayModeEvents.UndoPlayModeSelection);

            Assert.IsTrue(PlayModeSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MapSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MainMenu.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that the ReturnToMainMenu event opens the main menu and closes all other
        /// views
        /// </summary>
        [UnityTest]
        public IEnumerator TestReturnToMainOpensMainMenu()
        {
            // At least one frame should execute to receive state changes
            yield return ExecuteStrictAndUpdate(MainMenuEvents.ReturnToMain);

            Assert.IsTrue(MainMenu.gameObject.activeInHierarchy);
            Assert.IsFalse(PlayModeSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MapSelection.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that tapping the play mode button leads to the play mode menu opening
        /// </summary>
        [UnityTest]
        public IEnumerator TestMainMenuTapPlayMode()
        {
            MainMenu.TapPlayMode();

            yield return null;

            Assert.IsTrue(PlayModeSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MainMenu.gameObject.activeInHierarchy);
            Assert.IsFalse(MapSelection.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that tapping the free play button sets the correct play mode and opens
        /// the map selection view
        /// </summary>
        [UnityTest]
        public IEnumerator TestPlayModeSelectionTapFreePlay()
        {
            PlayModeSelection.TapFreePlay();

            yield return null;

            var state = GetState<MainMenuState>();
            Assert.AreEqual(MainMenuState.FreePlay, state.SelectedPlayMode);

            Assert.IsTrue(MapSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(PlayModeSelection.gameObject.activeInHierarchy);
            Assert.IsFalse(MainMenu.gameObject.activeInHierarchy);
        }

        /// <summary>
        /// Tests that tapping a map in map selection sets the ID in the state
        /// </summary>
        [UnityTest]
        public IEnumerator TestMapSelectionTapMapSetsMapID()
        {
            var expected = "Test";
            MapSelection.TapMap(expected);

            yield return null;

            var state = GetState<MainMenuState>();
            Assert.AreEqual(expected, state.SelectedMap);
        }

        #endregion
    }
}
