using NUnit.Framework;
using Sharedstreets.Vector.Tile;
using System.IO;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var geometryStream = File.OpenRead("./testfixtures/12-1171-1566.geometry.6.pbf");
            var intersectionStream = File.OpenRead("./testfixtures/12-1171-1566.intersection.6.pbf");
            var metadataStream = File.OpenRead("./testfixtures/12-1171-1566.metadata.6.pbf");
            var referenceStream = File.OpenRead("./testfixtures/12-1171-1566.reference.6.pbf");


            SharedStreetsTileParser.Parse(geometryStream);
            // geometryStream.


        }
    }
}