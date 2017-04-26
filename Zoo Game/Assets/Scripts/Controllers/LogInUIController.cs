using Jareel.Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

using Zoo.UI;

namespace Zoo.Controllers
{
    /// <summary>
    /// The LogInController conveys changes from the state system to a LogInView
    /// </summary>
    public class LogInUIController : MonoStateSubscriber<LogInState>
    {
        #region Fields

        // Editor Fields
        [SerializeField] private LogInView m_logIn;

        #endregion

        #region Testing Properties
#if UNITY_EDITOR

        /// <summary>
        /// Used to manually set the log in view. Used to initialize the view from a unit testing environment
        /// </summary>
        public LogInView LogIn { get { return m_logIn; } set { m_logIn = value; } }

#endif
        #endregion

        #region Setup

        protected override void Start()
        {
            base.Start();

            LogIn.OnEmailTextChanged += UpdateEmailText;
            LogIn.OnPasswordTextChanged += UpdatePasswordText;
            LogIn.OnSubmitClicked += HandleSubmission;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            LogIn.OnEmailTextChanged -= UpdateEmailText;
            LogIn.OnPasswordTextChanged -= UpdatePasswordText;
            LogIn.OnSubmitClicked -= HandleSubmission;
        }

        #endregion

        /// <summary>
        /// Updates the view to reflect the most recent changes to the subscribed states
        /// </summary>
        /// <param name="logIn">The state of the LogIn view</param>
        protected override void OnStateChanged(LogInState logIn)
        {
            if (logIn.Submitted) {
                Events.ExecuteStrict(AccountEvents.SetAccountData, logIn.Email, logIn.Email);
                Events.ExecuteStrict(LogInEvents.Reset);
                SceneManager.LoadScene("MainMenu");
            }
            else {
                LogIn.SetEmailText(logIn.Email);
                LogIn.SetPasswordText(logIn.Password);
            }
        }

        /// <summary>
        /// Relays a change from the log in view's email text input to the state
        /// </summary>
        /// <param name="text">The current email input text</param>
        private void UpdateEmailText(string text)
        {
            Events.ExecuteStrict(LogInEvents.UpdateEmailText, text);
        }

        /// <summary>
        /// Relays a change from the log in view's password text input to the state
        /// </summary>
        /// <param name="text">The current password input text</param>
        private void UpdatePasswordText(string text)
        {
            Events.ExecuteStrict(LogInEvents.UpdatePasswordText, text);
        }

        /// <summary>
        /// Handles the submit event from the UI
        /// </summary>
        private void HandleSubmission()
        {
            Events.ExecuteStrict(LogInEvents.Submit);
        }
    }
}
