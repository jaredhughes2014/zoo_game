
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;

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

        /// <summary>
        /// Tests the behavior of the OnEmailTextChanged event
        /// </summary>
        [Test]
        public void TestOnEmailTextChangedEvent()
        {
            var previous = View.Email.text;
            var expected = previous + "t";

            var text = previous;

            View.OnEmailTextChanged += (t) => {
                Assert.AreNotEqual(previous, t);
                Assert.AreEqual(expected, t);
            };

            View.Email.text = expected;
        }

        /// <summary>
        /// Tests the behavior of the OnPasswordChangedEvent event
        /// </summary>
        [Test]
        public void TestOnPasswordChangedEvent()
        {
            var previous = View.Password.text;
            var expected = previous + "t";

            var text = previous;

            View.OnPasswordTextChanged += (t) => {
                Assert.AreNotEqual(previous, t);
                Assert.AreEqual(expected, t);
            };

            View.Email.text = expected;
        }

        #endregion
    }
}
