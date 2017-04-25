
using NUnit.Framework;

namespace Zoo.UI.Test
{
    /// <summary>
    /// Tests the behavior of the map selection view
    /// </summary>
    public class MapSelectionTests : ViewTestFixture<MapSelectionView>
    {
        /// <summary>
        /// Tests the execution of the tap map event
        /// </summary>
        [Test]
        public void TestTapMapEvent()
        {
            bool success = false;
            string expected = "Test";

            View.OnTapMap += (mapID) => {
                success = expected == mapID;
            };

            View.TapMap(expected);
            Assert.IsTrue(success);
        }
    }
}
