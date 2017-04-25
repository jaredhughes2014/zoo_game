using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Zoo.UI;

namespace Zoo.Controllers.Test
{
    /// <summary>
    /// Tests the interaction between the state system and the zoo creation UI
    /// </summary>
    public class ZooCreationUITests : ControllerTestFixture<ZooCreationUIController>
    {
        #region View Shortcuts

        private ZooCreationView ZooCreation { get { return Controller.ZooCreation; } }

        #endregion

        #region Setup

        /// <summary>
        /// Extended to initialize all views that are needed
        /// </summary>
        public override void InitializeController()
        {
            base.InitializeController();

            GenerateZooCreationView();
        }

        /// <summary>
        /// Generates the ZooCreation view with all of its view components
        /// </summary>
        private void GenerateZooCreationView()
        {
            Controller.ZooCreation = MakeBehaviour<ZooCreationView>();
            ZooCreation.NameField = MakeBehaviour<InputField>();
        }

        #endregion

        #region Test Cases

        /// <summary>
        /// Tests that the SetNameText event updates the text of the name field
        /// </summary>
        [UnityTest]
        public IEnumerator TestSetNameTextUpdatesNameField()
        {
            var previous = ZooCreation.NameField.text;
            var expected = previous + "t";

            yield return ExecuteAndUpdate(ZooCreationEvents.SetNameText, expected);

            Assert.AreNotEqual(previous, ZooCreation.NameField.text);
            Assert.AreEqual(expected, ZooCreation.NameField.text);
        }

        /// <summary>
        /// Tests that updates to the text field also update the state with the change
        /// </summary>
        [UnityTest]
        public IEnumerator TestUpdateNameFieldUpdatesState()
        {
            var previous = ZooCreation.NameField.text;
            var expected = previous + "t";

            ZooCreation.ProcessNameUpdate(expected);
            yield return null;

            var state = GetState<ZooCreationState>();
            Assert.AreNotEqual(previous, state.NameText);
            Assert.AreEqual(expected, state.NameText);
        }

        /// <summary>
        /// Tests that the TapSubmit function enables the submitted flag in the zoo creation state
        /// </summary>
        [UnityTest]
        public IEnumerator TestTapSubmitEnablesSubmission()
        {
            ZooCreation.TapSubmit();

            yield return null;

            var state = GetState<ZooCreationState>();
            Assert.IsTrue(state.Submitted);
        }

        /// <summary>
        /// Tests that the Reset event clears all data from the view
        /// </summary>
        [UnityTest]
        public IEnumerator TestResetClearsView()
        {
            var name = "T";

            // Simulate actual inputs
            yield return ExecuteStrictAndUpdate(ZooCreationEvents.SetNameText, name);
            yield return ExecuteStrictAndUpdate(ZooCreationEvents.Reset);

            Assert.IsEmpty(ZooCreation.NameField.text);
        }

        #endregion
    }
}
