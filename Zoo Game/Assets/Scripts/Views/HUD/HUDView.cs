using UnityEngine;

namespace Zoo.UI
{
    /// <summary>
    /// Renders the HUD
    /// </summary>
    public class HUDView : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event executed when the user gestures to change the movement mode
        /// </summary>
        public delegate void OnChangeMovementModeHandler();
        public event OnChangeMovementModeHandler OnChangeMovementMode;

        #endregion

        #region Input Handlers

        /// <summary>
        /// Executed from the scene to change the movement mode
        /// </summary>
        public void ChangeMovementMode()
        {

        }

        #endregion
    }
}