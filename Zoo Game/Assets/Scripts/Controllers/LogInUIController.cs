﻿using Jareel.Unity;
using UnityEngine;

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

        /// <summary>
        /// Updates the view to reflect the most recent changes to the subscribed states
        /// </summary>
        /// <param name="logIn">The state of the LogIn view</param>
        protected override void OnStateChanged(LogInState logIn)
        {
            LogIn.SetEmailText(logIn.Email);
            LogIn.SetPasswordText(logIn.Password);
        }
    }
}
