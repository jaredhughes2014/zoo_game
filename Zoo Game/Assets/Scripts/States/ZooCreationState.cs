using Jareel;

namespace Zoo
{
    /// <summary>
    /// Events which can be executed to mutate the zoo creation state
    /// </summary>
    public enum ZooCreationEvents
    {
        SetNameText,

        Submit,
        Reset,
    }

    /// <summary>
    /// Contains the state of the zoo creation UI
    /// </summary>
    [StateContainer("zooCreation")]
    public class ZooCreationState : State
    {
        #region State Data

        /// <summary>
        /// The currently entered text for the name of the zoo
        /// </summary>
        [StateData] public string NameText { get; set; }

        /// <summary>
        /// If true, the view should be visible
        /// </summary>
        [StateData] public bool Visible { get; set; }

        /// <summary>
        /// If true, the user has submitted their selections
        /// </summary>
        [StateData] public bool Submitted { get; set; }

        #endregion

        /// <summary>
        /// Creates a new ZooCreationState
        /// </summary>
        public ZooCreationState()
        {

        }
    }

    /// <summary>
    /// Controls the cloning and mutation of the zoo creation state
    /// </summary>
    public class ZooCreationController : StateController<ZooCreationState>
    {
        #region State Data

        /// <summary>
        /// Sets the name text of zoo creation
        /// </summary>
        /// <param name="text">The text entered by the user for the name of the created zoo</param>
        [EventListener(ZooCreationEvents.SetNameText)]
        private void SetNameText(string text)
        {

        }

        /// <summary>
        /// Enables the submitted flag
        /// </summary>
        [EventListener(ZooCreationEvents.Submit)]
        private void Submit()
        {

        }

        /// <summary>
        /// Resets the state of zoo creation
        /// </summary>
        [EventListener(ZooCreationEvents.Reset)]
        private void Reset()
        {

        }

        #endregion

        #region State Adapters

        /// <summary>
        /// Adapts the UI state to the zoo creation state
        /// </summary>
        /// <param name="ui">The state of the UI</param>
        [StateAdapter]
        private void AdaptUIState(UIState ui)
        {

        }

        #endregion

        /// <summary>
        /// Creates a deep copy of the zoo creation state
        /// </summary>
        /// <returns>Deep copy of the zoo creation state</returns>
        public override ZooCreationState CloneState()
        {
            return new ZooCreationState() {

            };
        }
    }
}