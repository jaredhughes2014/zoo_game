using NUnit.Framework;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Tests the behavior of the HUD viwe
    /// </summary>
    public class HUDViewTests : ViewTestFixture<HUDView>
    {
        /// <summary>
        /// Tests the behavior of the OnChangeMovementMode event
        /// </summary>
        [Test]
        public void TestOnChangeMovementMoveEvent()
        {
            bool success = false;
            View.OnChangeMovementMode += () => {
                success = true;
            };

            View.ChangeMovementMode();
            Assert.IsTrue(success);
        }
    }
}
