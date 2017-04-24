using Jareel;

namespace Zoo
{
    /// <summary>
    /// Events which can be executed to mutate the UI state
    /// </summary>
    public enum UIEvents
    {
        SetHUDOpen,
        SetZooCreationOpen,
    }

    /// <summary>
    /// The state of the UI
    /// </summary>
    [StateContainer("ui")]
    public class UIState : State
    {
        #region Constants

        // Names of UI panels
        public const string ZooCreation = "ZooCreation";
        public const string HUD = "HUD";

        #endregion

        #region State Data

        /// <summary>
        /// The name of the panel that is currently open
        /// </summary>
        public string OpenPanel { get; set; }

        #endregion

        /// <summary>
        /// Creates a new UI state
        /// </summary>
        public UIState()
        {
            OpenPanel = "";
        }
    }

    /// <summary>
    /// Controls mutation and cloning of the UI state
    /// </summary>
    public class UIController : StateController<UIState>
    {
        #region Event Listeners

        /// <summary>
        /// Sets the open panel to the hud
        /// </summary>
        [EventListener(UIEvents.SetHUDOpen)]
        private void SetHUDOpen()
        {
            State.OpenPanel = UIState.HUD;
        }

        /// <summary>
        /// Sets the open panel to zoo creation
        /// </summary>
        [EventListener(UIEvents.SetZooCreationOpen)]
        private void SetZooCreationOpen()
        {
            State.OpenPanel = UIState.ZooCreation;
        }

        #endregion

        /// <summary>
        /// Creates and returns a deep copy of the UI state
        /// </summary>
        /// <returns>Deep copy of the UI state</returns>
        public override UIState CloneState()
        {
            return new UIState() {
                OpenPanel = State.OpenPanel,
            };
        }
    }
}