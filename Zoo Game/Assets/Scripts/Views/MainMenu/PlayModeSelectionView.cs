using UnityEngine;

namespace Zoo.UI
{
    /// <summary>
    /// Renders the play mode selection view
    /// </summary>
    public class PlayModeSelectionView : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event fired when the user taps the free play button
        /// </summary>
        public delegate void OnTapFreePlayHandler();
        public event OnTapFreePlayHandler OnTapFreePlay;

        #endregion

        #region Input Handlers

        /// <summary>
        /// Executed from the scene to begin a tap free play event
        /// </summary>
        public void TapFreePlay()
        {
            if (OnTapFreePlay != null) OnTapFreePlay();
        }

        #endregion
    }
}