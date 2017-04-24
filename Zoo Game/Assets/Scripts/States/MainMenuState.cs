using UnityEngine;
using Jareel;

namespace Zoo
{
    /// <summary>
    /// Events which can be executed from the main menu
    /// </summary>
    public enum MainMenuEvents
    {
        OpenPlayModeMenu,
        ReturnToMain,
        
        SelectMap,
        SubmitSelections,
        ResetSelections,
    }

    /// <summary>
    /// Events specific to choosing a play mode
    /// </summary>
    public enum PlayModeEvents
    {
        SelectFreePlay,
        UndoPlayModeSelection,
    }

    /// <summary>
    /// Contains the state of the main menu.
    /// </summary>
    [StateContainer("mainMenu")]
    public class MainMenuState : State
    {
        #region Constants

        // Names of views which exist inside the main menu
        public const string MainMenu = "MainMenu";
        public const string PlayModeSelection = "PlayModeSelection";
        public const string MapSelection = "MapSelection";

        // Names of supported play modes
        public const string FreePlay = "FreePlay";

        #endregion

        #region State Data

        /// <summary>
        /// The name of the panel that is currently open.
        /// </summary>
        public string OpenPanel { get; set; }

        /// <summary>
        /// The play mode selected by the player
        /// </summary>
        public string SelectedPlayMode { get; set; }

        /// <summary>
        /// The ID of the map selected by the user
        /// </summary>
        public string SelectedMap { get; set; }

        /// <summary>
        /// If true, a submission has been received
        /// </summary>
        public bool Submitted { get; set; }

        #endregion

        /// <summary>
        /// Creates a new MainMenuState
        /// </summary>
        public MainMenuState()
        {
            OpenPanel = MainMenu;
            SelectedPlayMode = "";
            SelectedMap = "";
        }
    }

    /// <summary>
    /// Controls mutation and cloning of the MainMenuState
    /// </summary>
    public class MainMenuController : StateController<MainMenuState>
    {
        #region Event Listeners

        /// <summary>
        /// Opens the menu which lets the user select the play mode they want
        /// </summary>
        [EventListener(MainMenuEvents.OpenPlayModeMenu)]
        private void OpenPlayModeMenu()
        {

        }

        /// <summary>
        /// Returns the menu to MainMenu mode
        /// </summary>
        [EventListener(MainMenuEvents.ReturnToMain)]
        private void ReturnToMain()
        {

        }

        /// <summary>
        /// Selects the ID of the map to be used
        /// TODO: This is currently not used. There is only one map while testing
        /// </summary>
        /// <param name="mapID">The ID of the map to use</param>
        [EventListener(MainMenuEvents.SelectMap)]
        private void SelectMap(string mapID)
        {
            Debug.LogWarning("SelectMap in MainMenuState currently has no function");
        }

        /// <summary>
        /// Selects the FreePlay mode of play.
        /// </summary>
        [EventListener(PlayModeEvents.SelectFreePlay)]
        private void SelectFreePlay()
        {

        }

        /// <summary>
        /// Undoes the user's mode of play selection and sets the menu back to play mode selection
        /// </summary>
        [EventListener(PlayModeEvents.UndoPlayModeSelection)]
        private void UndoPlayModeSelection()
        {

        }

        /// <summary>
        /// Sets the submit flag to true
        /// </summary>
        [EventListener(MainMenuEvents.SubmitSelections)]
        private void Submit()
        {

        }

        /// <summary>
        /// Resets all state data to default values
        /// </summary>
        [EventListener(MainMenuEvents.ResetSelections)]
        private void Reset()
        {

        }

        #endregion

        /// <summary>
        /// Creates a deep copy of the MainMenuState
        /// </summary>
        /// <returns>Deep copy of the main menu state</returns>
        public override MainMenuState CloneState()
        {
            return new MainMenuState() {

            };
        }
    }
}