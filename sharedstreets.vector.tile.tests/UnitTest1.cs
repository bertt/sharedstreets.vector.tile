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
            // sample amsterdam geometry url: 
            // https://tiles.sharedstreets.io/osm/planet-181224/12-2103-1346.geometry.6.pbf
            // https://tiles.sharedstreets.io/osm/planet-181224/12-2103-1346.intersection.6.pbf
            // https://tiles.sharedstreets.io/osm/planet-181224/12-2103-1346.metadata.6.pbf
            // https://tiles.sharedstreets.io/osm/planet-181224/12-2103-1346.reference.6.pbf
            var amsterdamTile = "./testfixtures/12-2103-1346.";
            var geometryStream = File.OpenRead(amsterdamTile + "geometry.6.pbf");
            var intersectionStream = File.OpenRead(amsterdamTile + "intersection.6.pbf");
            var metadataStream = File.OpenRead(amsterdamTile + "metadata.6.pbf");
            var referenceStream = File.OpenRead(amsterdamTile + "reference.6.pbf");

            var geometry = SharedStreetsParser.ParseGeometry(geometryStream);
            Assert.IsTrue(geometry.Lonlats.Count == 50906);
            var intersection = SharedStreetsParser.ParseIntersection(intersectionStream);
            Assert.IsTrue(intersection.Id == "7f1e47e658e554625c51c6fc6cdafcda");
            var metadata = SharedStreetsParser.ParseMetadata(metadataStream);
            Assert.IsTrue(metadata.OsmMetadata.WaySections.Count == 8431);
            var reference = SharedStreetsParser.ParseReference(referenceStream);
            Assert.IsTrue(reference.LocationReferences.Count == 17382);
        }
    }
}