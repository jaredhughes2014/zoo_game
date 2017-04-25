
using UnityEngine.UI;
using NUnit.Framework;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Tests the behavior of the log in view
    /// </summary>
    public class LogInViewTests : ViewTestFixture<LogInView>
    {
        /// <summary>
        /// Extended to initialize all UI component on the view
        /// </summary>
        public override void InitializeView()
        {
            base.InitializeView();

            View.Email = MakeBehaviour<InputField>();
            View.Password = MakeBehaviour<InputField>();
        }

        #region Test Cases

        /// <summary>
        /// Tests the behavior of the SetEmailText function
        /// </summary>
        [Test]
        public void TestSetEmailText()
        {
            var previous = View.Email.text;
            var expected = previous + "_Test";

            View.SetEmailText(expected);

            Assert.AreNotEqual(previous, View.Email.text);
            Assert.AreEqual(expected, View.Email.text);
        }

        /// <summary>
        /// Tests the behavior of the OnEmailTextChanged event
        /// </summary>
        [Test]
        public void TestOnEmailTextChangedEvent()
        {
            var previous = View.Email.text;
            var expected = previous + "t";

            bool success = false;

            View.OnEmailTextChanged += (text) => {
                success = text == expected;
            };
            View.SetEmailText(expected);

            Assert.True(success);
        }

        /// <summary>
        /// Tests the behavior of the SetPasswordText function
        /// </summary>
        [Test]
        public void TestSetPasswordText()
        {
            var previous = View.Password.text;
            var expected = previous + "_Test";

            View.SetPasswordText(expected);

            Assert.AreNotEqual(previous, View.Password.text);
            Assert.AreEqual(expected, View.Password.text);
        }

        /// <summary>
        /// Tests the behavior of the OnPasswordTextUpdated event
        /// </summary>
        [Test]
        public void TestOnPasswordTextChangedEvent()
        {
            var previous = View.Password.text;
            var expected = previous + "t";

            bool success = false;

            View.OnPasswordTextChanged += (text) => {
                success = text == expected;
            };
            View.SetPasswordText(expected);

            Assert.True(success);
        }

        /// <summary>
        /// Tests that the OnSubmitClicked event is properly executed
        /// </summary>
        [Test]
        public void TestOnSubmitClickedEvent()
        {
            bool success = false;

            View.OnSubmitClicked += () => {
                success = true;
            };
            View.Submit();

            Assert.IsTrue(success);
        }

        #endregion
    }
}
