﻿using UnityEngine;
using UnityEngine.UI;

namespace Zoo.UI
{
    /// <summary>
    /// Renders the log in page.
    /// </summary>
    public class LogInView : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event executed when the submit button is clicked
        /// </summary>
        public delegate void OnSubmitClickedHandler();
        public event OnSubmitClickedHandler OnSubmitClicked;

        /// <summary>
        /// Event executed when the email text input changes
        /// </summary>
        public delegate void OnEmailTextChangedHandler(string text);
        public event OnEmailTextChangedHandler OnEmailTextChanged;

        /// <summary>
        /// Event executed when the password text input changes
        /// </summary>
        public delegate void OnPasswordTextChangedHandler(string text);
        public event OnPasswordTextChangedHandler OnPasswordTextChanged;

        #endregion

        #region Fields

        // View Fields

        [SerializeField]
        private InputField m_emailInput;

        [SerializeField]
        private InputField m_passwordField;

        #endregion

        #region Properties

        /// <summary>
        /// The email input field
        /// </summary>
        public InputField Email { get { return m_emailInput; } set { m_emailInput = value; } }

        /// <summary>
        /// The password input field
        /// </summary>
        public InputField Password { get { return m_passwordField; } set { m_passwordField = value; } }

        #endregion

        #region Controller Updates

        /// <summary>
        /// Sets the email text in this view
        /// </summary>
        /// <param name="text">The text to set</param>
        public void SetEmailText(string text)
        {

        }

        /// <summary>
        /// Sets the password text in this view
        /// </summary>
        /// <param name="text">The text to set</param>
        public void SetPasswordText(string text)
        {

        }

        #endregion

        #region Input Events

        /// <summary>
        /// Executed from the editor to begin a submission
        /// </summary>
        public void Submit()
        {

        }

        /// <summary>
        /// Executed every time the email text field changes
        /// </summary>
        public void ProcessEmailChange()
        {

        }

        /// <summary>
        /// Executed every tim the password text field changes
        /// </summary>
        public void ProcessPasswordChange()
        {

        }

        #endregion
    }
}