using UnityEngine;
using UnityEngine.UI;

namespace Zoo.UI
{
    /// <summary>
    /// Renders the view where the user can create a new zoo
    /// </summary>
    public class ZooCreationView : MonoBehaviour
    {
        #region Events

        /// <summary>
        /// Event executed when the user taps the Submit button
        /// </summary>
        public delegate void OnSubmitTappedHandler();
        public event OnSubmitTappedHandler OnSubmitTapped;

        /// <summary>
        /// Event executed when the text of the name input field changes
        /// </summary>
        /// <param name="text">The current text of the name field</param>
        public delegate void OnNameTextChangedHandler(string text);
        public event OnNameTextChangedHandler OnNameTextChanged;

        #endregion

        #region Fields

        /// <summary>
        /// The text field the user uses to input their name
        /// </summary>
        [SerializeField] private InputField m_nameField;

        #endregion

        #region Properties

        /// <summary>
        /// The input field used by this view to set the name of the zoo
        /// </summary>
        public InputField NameField
        {
            get
            {
                return m_nameField;
            }
            set
            {
                m_nameField = value;
            }
        }

        #endregion

        #region Control Handlers

        /// <summary>
        /// Executed to update the text of the name input
        /// </summary>
        /// <param name="text">The text of the name input</param>
        public void UpdateNameText(string text)
        {

        }

        #endregion

        #region Input Handlers

        /// <summary>
        /// Executed from the scene to submit a created zoo
        /// </summary>
        public void TapSubmit()
        {

        }

        #endregion
    }
}