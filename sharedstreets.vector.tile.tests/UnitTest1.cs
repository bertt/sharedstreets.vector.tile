using NUnit.Framework;
using Sharedstreets.Vector.Tile;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            Assert.IsTrue(new Class1().Add(5, 6) == 11);
            Assert.Pass();
        }
    }
}