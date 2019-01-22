using NUnit.Framework;

namespace Sharedstreets.Vector.Tile.Tests
{
    public class SharedStreetsTests
    {
        [Test]
        public void GenerateHashTest()
        {
            // arrange
            var input = "haha";

            // act
            var result = SharedStreets.GenerateHash(input);

            // assert
            Assert.IsTrue(result == "4e4d6c332b6fe62a63afe56171fd3725");
        }
    }
}
