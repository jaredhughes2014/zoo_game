
using Jareel;

namespace Zoo
{
    /// <summary>
    /// Events that can be executed to mutate the log in state
    /// </summary>
    public enum LogInEvents
    {
        UpdateEmailText,
        UpdatePasswordText,

        Submit,
        Reset,
    }

    /// <summary>
    /// Contains the state of the log in UI
    /// </summary>
    [StateContainer("logIn")]
    public class LogInState : State
    {
        #region State Data

        /// <summary>
        /// The player's email address
        /// </summary>
        [StateData] public string Email { get; set; }

        /// <summary>
        /// The player's password
        /// </summary>
        [StateData] public string Password { get; set; }

        /// <summary>
        /// If true, the user has submitted their selections
        /// </summary>
        [StateData] public bool Submitted { get; set; }

        #endregion

        /// <summary>
        /// Sets default values
        /// </summary>
        public LogInState()
        {
            Email = "";
            Password = "";
        }
    }

    /// <summary>
    /// Controls mutation and cloning of the Account state
    /// </summary>
    public class LogInController : StateController<LogInState>
    {
        #region Event Listeners

        /// <summary>
        /// Updates the value of the email text field
        /// </summary>
        /// <param name="text">The current text value the user has entered</param>
        [EventListener(LogInEvents.UpdateEmailText)]
        private void UpdateEmailText(string text)
        {
            State.Email = text;
        }

        /// <summary>
        /// Updates the value of the password text field
        /// TODO This is a security vulnerability
        /// </summary>
        /// <param name="text">The current text value the user has entered</param>
        [EventListener(LogInEvents.UpdatePasswordText)]
        private void UpdatePasswordText(string text)
        {
            State.Password = text;
        }

        /// <summary>
        /// Marks the state as submitted
        /// </summary>
        [EventListener(LogInEvents.Submit)]
        private void Submit()
        {
            State.Submitted = true;
        }

        /// <summary>
        /// Resets all values in the log in state
        /// </summary>
        [EventListener(LogInEvents.Reset)]
        private void Reset()
        {
            State.Email = "";
            State.Password = "";
            State.Submitted = false;
        }

        #endregion

        /// <summary>
        /// Creates a deep copy of the LogIn state
        /// </summary>
        /// <returns>Deep copy of the log in state</returns>
        public override LogInState CloneState()
        {
            return new LogInState() {
                Email = State.Email,
                Password = State.Password,
                Submitted = State.Submitted,
            };
        }
    }

}