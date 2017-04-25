using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Zoo.UI;

namespace Zoo.Controllers.Test
{
    /// <summary>
    /// Tests the behavior of the controller test fixture
    /// </summary>
    public class LogInUITests : ControllerTestFixture<LogInUIController>
    {
        #region View Shortcuts

        /// <summary>
        /// Shortcut to the LogInView attached to the controller
        /// </summary>
        private LogInView LogIn { get { return Controller.LogIn; } }

        #endregion

        #region Setup

        /// <summary>
        /// Overloaded to connect the controller to its controlled view
        /// </summary>
        public override void InitializeController()
        {
            base.InitializeController();

            GenerateLogIn();
        }

        /// <summary>
        /// Generates the LogIn view
        /// </summary>
        private void GenerateLogIn()
        {
            Controller.LogIn = MakeBehaviour<LogInView>();
            LogIn.Email = MakeBehaviour<InputField>();
            LogIn.Password = MakeBehaviour<InputField>();
        }

        #endregion

        #region Test Cases

        /// <summary>
        /// Tests the effect of the update email text event from the controller to the view
        /// </summary>
        [UnityTest]
        public IEnumerator TestUpdateEmailText()
        {
            var previous = LogIn.Email.text;
            var expected = previous + "t";

            yield return ExecuteStrictAndUpdate(LogInEvents.UpdateEmailText, expected);

            Assert.AreNotEqual(previous, LogIn.Email.text);
            Assert.AreEqual(expected, LogIn.Email.text);
        }

        /// <summary>
        /// Tests the effect of the update password event from the controller to the view
        /// </summary>
        [UnityTest]
        public IEnumerator TestUpdatePasswordText()
        {
            var previous = LogIn.Password.text;
            var expected = previous + "t";

            yield return ExecuteStrictAndUpdate(LogInEvents.UpdatePasswordText, expected);

            Assert.AreNotEqual(previous, LogIn.Password.text);
            Assert.AreEqual(expected, LogIn.Password.text);
        }

        /// <summary>
        /// Tests the effect of the submit event on the state system
        /// </summary>
        [UnityTest]
        public IEnumerator TestSubmitUpdatesAccountState()
        {
            var email = LogIn.Email.text + "t";
            var pw = LogIn.Password.text + "t";

            ExecuteStrict(LogInEvents.UpdateEmailText, email);
            ExecuteStrict(LogInEvents.UpdatePasswordText, pw);
            yield return ExecuteStrictAndUpdate(LogInEvents.Submit);

            var account = GetState<AccountState>();

            // TODO for now, the user's email and alias are identical
            Assert.AreEqual(email, account.Email);
            Assert.AreEqual(email, account.Alias);
        }

        /// <summary>
        /// Tests that the effects of the Reset event are carried to the LogIn view
        /// </summary>
        [UnityTest]
        public IEnumerator TestResetClearsInputText()
        {
            var email = LogIn.Email.text + "t";
            var pw = LogIn.Password.text + "t";

            // Simulate each key stroke taking one frame
            yield return ExecuteStrictAndUpdate(LogInEvents.UpdateEmailText, email);
            yield return ExecuteStrictAndUpdate(LogInEvents.UpdatePasswordText, pw);
            yield return ExecuteStrictAndUpdate(LogInEvents.Reset);

            Assert.IsEmpty(LogIn.Email.text);
            Assert.IsEmpty(LogIn.Password.text);
        }

        /// <summary>
        /// Tests that updates to the email text input cause the state to reflect
        /// the most recent text input
        /// </summary>
        [UnityTest]
        public IEnumerator TestEmailTextChangesUpdatesState()
        {
            var previous = LogIn.Email.text;
            var text = previous + "t";

            // The controller should execute an event to update the email text
            LogIn.ProcessEmailChange(text);
            yield return null;

            var state = GetState<LogInState>();
            Assert.AreNotEqual(previous, state.Email);
            Assert.AreEqual(text, state.Email);
        }

        /// <summary>
        /// Tests that updates to the password text input cause the state to reflect
        /// the most recent text input
        /// </summary>
        [UnityTest]
        public IEnumerator TestPasswordTextChangesUpdatesState()
        {
            var previous = LogIn.Password.text;
            var text = previous + "t";

            // The controller should execute an event to update the email text
            LogIn.ProcessPasswordChange(text);
            yield return null;

            var state = GetState<LogInState>();
            Assert.AreNotEqual(previous, state.Password);
            Assert.AreEqual(text, state.Password);
        }

        #endregion
    }
}
