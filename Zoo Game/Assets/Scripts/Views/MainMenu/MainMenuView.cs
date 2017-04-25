using UnityEngine;

namespace Zoo.UI
{
    /// <summary>
    /// Renders the primary main menu view
    /// </summary>
    public class MainMenuView : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event fired when the user taps the play mode view
        /// </summary>
        public delegate void OnTapPlayModeHandler();
        public event OnTapPlayModeHandler OnTapPlayMode;

        #endregion

        #region Input Handlers

        /// <summary>
        /// Executed from the scene to begin a tap play mode event
        /// </summary>
        public void TapPlayMode()
        {

        }

        #endregion
    }
}