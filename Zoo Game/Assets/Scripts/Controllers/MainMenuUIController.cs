using Jareel.Unity;
using Zoo.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Zoo.Controllers
{
    /// <summary>
    /// Updates the main menu views to reflect the current state of the system
    /// </summary>
    public class MainMenuUIController : MonoStateSubscriber<MainMenuState>
    {
        #region Fields

        // Editor fields
        [SerializeField] private MainMenuView m_mainMenu;

        [SerializeField] private PlayModeSelectionView m_playModeSelection;

        [SerializeField] private MapSelectionView m_mapSelection;

        #endregion

        #region Properties
#if UNITY_EDITOR

        /// <summary>
        /// Used to access and manually set the main menu view. Used for testing purposes
        /// </summary>
        public MainMenuView MainMenu { get { return m_mainMenu; } set { m_mainMenu = value; } }

        /// <summary>
        /// Used to access and manually set the play mode selection view. Used for testing purposes
        /// </summary>
        public PlayModeSelectionView PlayModeSelection { get { return m_playModeSelection; } set { m_playModeSelection = value; } }

        /// <summary>
        /// Used to access and manually set the map selection view. Used for testing purposes
        /// </summary>
        public MapSelectionView MapSelection { get { return m_mapSelection; } set { m_mapSelection = value; } }

#endif
        #endregion

        #region Setup

        protected override void Start()
        {
            base.Start();

            MainMenu.OnTapPlayMode += RegisterPlayModeTapped;
            PlayModeSelection.OnTapFreePlay += RegisterFreePlayTapped;
            MapSelection.OnTapMap += RegisterMapSelection;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            MainMenu.OnTapPlayMode -= RegisterPlayModeTapped;
            PlayModeSelection.OnTapFreePlay -= RegisterFreePlayTapped;
            MapSelection.OnTapMap -= RegisterMapSelection;
        }

        #endregion

        /// <summary>
        /// Executed every time the state changes to update the views to reflect the state
        /// </summary>
        /// <param name="mainMenu">The state of the main menu</param>
        protected override void OnStateChanged(MainMenuState mainMenu)
        {
            if (mainMenu.Submitted) {
                SceneManager.LoadScene("GameUI");
                SceneManager.LoadScene("GameWorld", LoadSceneMode.Additive);
            }
            else {
                MainMenu.gameObject.SetActive(mainMenu.OpenPanel == MainMenuState.MainMenu);
                MapSelection.gameObject.SetActive(mainMenu.OpenPanel == MainMenuState.MapSelection);
                PlayModeSelection.gameObject.SetActive(mainMenu.OpenPanel == MainMenuState.PlayModeSelection);
            }
        }

        /// <summary>
        /// Listener to the main menu's play mode tap event
        /// </summary>
        private void RegisterPlayModeTapped()
        {
            Events.ExecuteStrict(MainMenuEvents.OpenPlayModeMenu);
        }

        /// <summary>
        /// Listener to the main menu's play mode tap event
        /// </summary>
        private void RegisterFreePlayTapped()
        {
            Events.ExecuteStrict(PlayModeEvents.SelectFreePlay);
        }

        /// <summary>
        /// Registers a map selection made by the user
        /// </summary>
        /// <param name="mapID">The ID of the map</param>
        private void RegisterMapSelection(string mapID)
        {
            Events.ExecuteStrict(MainMenuEvents.SelectMap, mapID);
            Events.ExecuteStrict(MainMenuEvents.SubmitSelections);
        }
    }
}