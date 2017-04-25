using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;
using UnityEngine.UI;
using Zoo.UI;

namespace Zoo.Controllers.Test
{
    /// <summary>
    /// Tests the interaction between the state system and the HUD ui
    /// </summary>
    public class HUDUITests : ControllerTestFixture<HUDUIController>
    {
        #region View Shortcuts

        /// <summary>
        /// The current state of the HUD
        /// </summary>
        private HUDView HUD { get { return Controller.HUD; } }

        #endregion

        #region Setup

        /// <summary>
        /// Extended to initialize all views that are needed
        /// </summary>
        public override void InitializeController()
        {
            base.InitializeController();

            GenerateHUDView();
        }

        /// <summary>
        /// Generates the HUD view with all of its view components
        /// </summary>
        private void GenerateHUDView()
        {
            Controller.HUD = MakeBehaviour<HUDView>();
        }

        #endregion

        #region Test Cases

        /// <summary>
        /// Tests that the change movement mode function updates the movement mode in the state
        /// </summary>
        [UnityTest]
        public IEnumerator TestChangeMovementMode()
        {
            HUD.ChangeMovementMode();
            yield return null;

            var state = GetState<HUDState>();
            Assert.IsTrue(state.RotateEnabled);

            HUD.ChangeMovementMode();
            yield return null;

            state = GetState<HUDState>();
            Assert.IsFalse(state.RotateEnabled);
        }

        #endregion
    }
}
