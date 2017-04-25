
using NUnit.Framework;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Tests the behavior of the main menu view
    /// </summary>
    public class MainMenuTests : ViewTestFixture<MainMenuView>
    {
        /// <summary>
        /// Tests the execution of the tap play mode event
        /// </summary>
        [Test]
        public void TestTapPlayModeEvent()
        {
            bool success = false;

            View.OnTapPlayMode += () => {
                success = true;
            };
            View.TapPlayMode();

            Assert.IsTrue(success);
        }
    }
}
