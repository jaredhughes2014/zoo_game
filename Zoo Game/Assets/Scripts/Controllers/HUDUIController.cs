using UnityEngine;
using Jareel.Unity;
using Zoo.UI;

namespace Zoo.Controllers
{
    /// <summary>
    /// Updates the HUD based on the current state of the system
    /// </summary>
    public class HUDUIController : MonoStateSubscriber<HUDState>
    {
        #region Fields

        // Editor Fields
        [SerializeField] private HUDView m_hud;

        #endregion

        #region Properties
#if UNITY_EDITOR

        /// <summary>
        /// Used to manually access and set the HUD view. Used for testing purposes
        /// </summary>
        public HUDView HUD { get { return m_hud; } set { m_hud = value; } }

#endif
        #endregion

        /// <summary>
        /// Updates the views based on the current state of zoo creation
        /// </summary>
        /// <param name="hud">The current state of the HUD</param>
        protected override void OnStateChanged(HUDState hud)
        {
        }
    }
}
