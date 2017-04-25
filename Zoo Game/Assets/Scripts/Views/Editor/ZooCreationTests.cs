using NUnit.Framework;
using UnityEngine.UI;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Tests the behavior of the zoo creation view
    /// </summary>
    public class ZooCreationTests : ViewTestFixture<ZooCreationView>
    {
        /// <summary>
        /// Initializes the child components of the view
        /// </summary>
        public override void InitializeView()
        {
            base.InitializeView();

            View.NameField = MakeBehaviour<InputField>();
        }

        #region Test Cases

        /// <summary>
        /// Tests the behavior of the SetNameText method
        /// </summary>
        [Test]
        public void TestSetNameText()
        {
            var previous = View.NameField.text;
            var expected = previous + "t";

            View.UpdateNameText(expected);

            Assert.AreNotEqual(previous, View.NameField.text);
            Assert.AreEqual(expected, View.NameField.text);
        }

        /// <summary>
        /// Tests the behavior of the OnNameTextSet evenet
        /// </summary>
        [Test]
        public void TestOnNameTextSetEvent()
        {
            var previous = View.NameField.text;
            var expected = previous + "t";

            bool success = false;

            View.OnNameTextChanged += (text) => {
                success = text == expected;
            };
            View.ProcessNameUpdate(expected);

            Assert.IsTrue(success);
        }

        /// <summary>
        /// Tests the behavior of the submit event
        /// </summary>
        [Test]
        public void TestSubmit()
        {
            bool success = false;

            View.OnSubmitTapped += () => {
                success = true;
            };
            View.TapSubmit();

            Assert.IsTrue(success);
        }

        #endregion
    }
}
