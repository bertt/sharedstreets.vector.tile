using Google.Protobuf.Collections;
using NUnit.Framework;

namespace Sharedstreets.Vector.Tile.Tests
{
    public class SharedStreetsTests
    {
        [Test]
        public void GenerateHashTest()
        {
            // arrange
            var input = "Intersection -74.00482177734375 40.741641998291016";

            // act
            var result = SharedStreets.GenerateHash(input);

            // assert
            Assert.IsTrue(result == "2a4a9e4ad1923f11ec46224f834d69a2");
        }

        [Test]
        public void RoundTest()
        {
            // arrange
            var input = new RepeatedField<double>() { 110, 45, 115, 50, 120, 55 };

            //act 
            var result = SharedStreets.Round(input);

            //assert
            Assert.IsTrue(result[0] == "110.00000");
        }

        [Test]
        public void GeometryIdTest()
        {
            // arrange
            var input = new RepeatedField<double>() { 110, 45, 115, 50, 120, 55 };

            //act 
            var result = SharedStreets.GeometryId(input);

            //assert
            Assert.IsTrue(result.Equals("723cda09fa38e07e0957ae939eb2684f"));
        }

        [Test]
        public void GeometryMessageTest()
        {
            // arrange
            var input = new RepeatedField<double>() { 110, 45, 115, 50, 120, 55};

            //act 
            var result = SharedStreets.GeometryMessage(input);

            //assert
            Assert.IsTrue(result.Equals("Geometry 110.00000 45.00000 115.00000 50.00000 120.00000 55.00000"));
        }
    }
}
