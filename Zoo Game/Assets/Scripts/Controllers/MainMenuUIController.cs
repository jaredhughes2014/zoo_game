using Jareel.Unity;
using Zoo.UI;
using UnityEngine;
using System;

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

        /// <summary>
        /// Executed every time the state changes to update the views to reflect the state
        /// </summary>
        /// <param name="mainMenu">The state of the main menu</param>
        protected override void OnStateChanged(MainMenuState mainMenu)
        {
        }
    }
}