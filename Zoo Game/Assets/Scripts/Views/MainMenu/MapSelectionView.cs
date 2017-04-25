using UnityEngine;

namespace Zoo.UI
{
    /// <summary>
    /// Renders the map selection view
    /// </summary>
    public class MapSelectionView : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event fired when the user taps a map selection button
        /// </summary>
        public delegate void OnTapMapHandler(string id);
        public event OnTapMapHandler OnTapMap;

        #endregion

        #region Input Handlers

        /// <summary>
        /// Executed from the scene to begin a tap play mode event
        /// </summary>
        /// <param name="mapID">The ID of the map that was tapped</param>
        public void TapMap(string mapID)
        {

        }

        #endregion
    }
}