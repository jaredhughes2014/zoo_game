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
    public class LogInTests : ControllerTestFixture<LogInController>
    {
        #region Setup

        /// <summary>
        /// Overloaded to connect the controller to its controlled view
        /// </summary>
        public override void InitializeController()
        {
            base.InitializeController();

            Controller.LogIn = MakeBehaviour<LogInView>();
            Controller.LogIn.Email = MakeBehaviour<InputField>();
            Controller.LogIn.Password = MakeBehaviour<InputField>();
        }

        #endregion

        #region Test Cases

        /// <summary>
        /// Tests the effect of the update email text event from the controller to the view
        /// </summary>
        /// <returns>Enumerator used to simulate frames</returns>
        [UnityTest]
        public IEnumerator TestUpdateEmailText()
        {
            var previous = Controller.LogIn.Email.text;
            var expected = previous + "t";

            yield return ExecuteStrictAndUpdate(LogInEvents.UpdateEmailText, expected);

            Assert.AreNotEqual(previous, Controller.LogIn.Email.text);
            Assert.AreEqual(expected, Controller.LogIn.Email.text);
        }

        /// <summary>
        /// Tests the effect of the update password event from the controller to the view
        /// </summary>
        /// <returns>Enumerator used to simulate frames</returns>
        [UnityTest]
        public IEnumerator TestUpdatePasswordText()
        {
            var previous = Controller.LogIn.Password.text;
            var expected = previous + "t";

            yield return ExecuteStrictAndUpdate(LogInEvents.UpdatePasswordText, expected);

            Assert.AreNotEqual(previous, Controller.LogIn.Password.text);
            Assert.AreEqual(expected, Controller.LogIn.Password.text);
        }

        /// <summary>
        /// Tests the effect of the submit event on the state system
        /// </summary>
        /// <returns>Enumerator used to simulate frames</returns>
        [UnityTest]
        public IEnumerator TestSubmitUpdatesAccountState()
        {
            var email = Controller.LogIn.Email.text + "t";
            var pw = Controller.LogIn.Password.text + "t";

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
        /// <returns>Enumerator used to simulate Unity frames</returns>
        [UnityTest]
        public IEnumerator TestResetClearsInputText()
        {
            var email = Controller.LogIn.Email.text + "t";
            var pw = Controller.LogIn.Password.text + "t";

            // Simulate each key stroke taking one frame
            yield return ExecuteStrictAndUpdate(LogInEvents.UpdateEmailText, email);
            yield return ExecuteStrictAndUpdate(LogInEvents.UpdatePasswordText, pw);
            yield return ExecuteStrictAndUpdate(LogInEvents.Reset);

            Assert.IsEmpty(Controller.LogIn.Email.text);
            Assert.IsEmpty(Controller.LogIn.Password.text);
        }

        #endregion
    }
}
