using NUnit.Framework;
using System.IO;

namespace SharedStreets.Vector.Tile.Tests
{
    public class PbfParsingTests
    {
        [Test]
        public void PbfParsingTest()
        {
            var geometryStream = TileReader.GetGeometryTile();
            var intersectionStream = TileReader.GetIntersectionTile();
            var referenceStream = TileReader.GetReferenceTile();
            var metadataStream = TileReader.GetMetadataTile();

            var geometries = SharedStreetsTileParser.Parse<SharedStreetsGeometry>(geometryStream);
            Assert.IsTrue(geometries.Count == 6202);
            var intersections = SharedStreetsTileParser.Parse<SharedStreetsIntersection>(intersectionStream);
            Assert.IsTrue(intersections.Count == 4031);
            var metadata = SharedStreetsTileParser.Parse<SharedStreetsMetadata>(metadataStream);
            Assert.IsTrue(metadata.Count == 6202);
            var references = SharedStreetsTileParser.Parse<SharedStreetsReference>(referenceStream);
            Assert.IsTrue(references.Count == 8691);
            // round test
            var lonlats = geometries[0].Lonlats;
            Assert.IsTrue(SharedStreets.GeometryId(lonlats) == geometries[0].Id);
        }
    }
}