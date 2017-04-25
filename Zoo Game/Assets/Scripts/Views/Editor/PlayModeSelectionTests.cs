
using NUnit.Framework;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Tests the behavior of the play mode selection view
    /// </summary>
    public class PlayModeSelectionTests : ViewTestFixture<PlayModeSelectionView>
    {
        /// <summary>
        /// Tests the execution of the tap free play event
        /// </summary>
        [Test]
        public void TestTapFreePlayEvent()
        {
            bool success = false;

            View.OnTapFreePlay += () => {
                success = true;
            };
            View.TapFreePlay();

            Assert.IsTrue(success);
        }
    }
}
