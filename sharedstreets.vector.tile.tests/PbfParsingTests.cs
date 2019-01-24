using NUnit.Framework;
using System.IO;

namespace Sharedstreets.Vector.Tile.Tests
{
    public class PbfParsingTests
    {
        [Test]
        public void PbfParsingTest()
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

            var geometries = SharedStreetsParser.Parse<SharedStreetsGeometry>(geometryStream);
            Assert.IsTrue(geometries.Count == 6202);
            var intersections = SharedStreetsParser.Parse<SharedStreetsIntersection>(intersectionStream);
            Assert.IsTrue(intersections.Count == 4031);
            var metadata = SharedStreetsParser.Parse<SharedStreetsMetadata>(metadataStream);
            Assert.IsTrue(metadata.Count == 6202);
            var references = SharedStreetsParser.Parse<SharedStreetsReference>(referenceStream);
            Assert.IsTrue(references.Count == 8691);

            // round test
            var lonlats = geometries[0].Lonlats;
            Assert.IsTrue(SharedStreets.GeometryId(lonlats) == geometries[0].Id);
        }
    }
}