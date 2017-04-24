using Jareel;

namespace Zoo
{
    /// <summary>
    /// Events which can be executed to mutate the HUD state
    /// </summary>
    public enum HUDEvents
    {
        ToggleMovementMode
    }

    /// <summary>
    /// Contains the current state of the HUD
    /// </summary>
    [StateContainer("hud")]
    public class HUDState : State
    {
        #region State Data

        /// <summary>
        /// If true, the motion mode should be set to rotation instead of translation
        /// </summary>
        public bool RotateEnabled { get; set; }

        /// <summary>
        /// If true, the HUD state should be visible
        /// </summary>
        public bool Visible { get; set; }

        #endregion

        /// <summary>
        /// Creates a new HUD state
        /// </summary>
        public HUDState()
        {

        }
    }

    /// <summary>
    /// Controls mutation and cloning of the HUD state
    /// </summary>
    public class HUDController : StateController<HUDState>
    {
        #region Event Listeners

        /// <summary>
        /// Toggles the current movement mode of the HUD
        /// </summary>
        [EventListener(HUDEvents.ToggleMovementMode)]
        private void ToggleMovementMode()
        {

        }

        #endregion

        #region State Adapters

        /// <summary>
        /// Adapts data in the UI state to the HUD state
        /// </summary>
        /// <param name="ui">The current state of the game UI</param>
        [StateAdapter]
        private void AdaptUIState(UIState ui)
        {

        }

        #endregion

        /// <summary>
        /// Creates a deep copy of the HUD state
        /// </summary>
        /// <returns>Deep copy of the HUD state</returns>
        public override HUDState CloneState()
        {
            return new HUDState() {

            };
        }
    }
}