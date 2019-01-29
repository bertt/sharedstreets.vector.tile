using NUnit.Framework;

namespace SharedStreets.Vector.Tile.Tests
{
    public class PbfWriterTests
    {
        [Test]
        public void ReadAndWriteTests()
        {
            var geometryStream = TileReader.GetGeometryTile();
            var intersectionStream = TileReader.GetIntersectionTile();
            var referenceStream = TileReader.GetReferenceTile();
            var metadataStream = TileReader.GetMetadataTile();

            var geometries = SharedStreetsTileParser.Parse<SharedStreetsGeometry>(geometryStream);
            var intersections = SharedStreetsTileParser.Parse<SharedStreetsIntersection>(intersectionStream);
            var metadata = SharedStreetsTileParser.Parse<SharedStreetsMetadata>(metadataStream);
            var references = SharedStreetsTileParser.Parse<SharedStreetsReference>(referenceStream);

            /// check read/write geometries
            var stream = SharedStreetsTileWriter.Write(geometries);
            Assert.IsTrue(geometryStream.Length == stream.Length);
            stream.Position = 0;
            var newgeometries = SharedStreetsTileParser.Parse<SharedStreetsGeometry>(stream);
            Assert.IsTrue(newgeometries.Count == geometries.Count);

            // check read/write intersections
            stream = SharedStreetsTileWriter.Write(intersections);
            Assert.IsTrue(intersectionStream.Length == stream.Length);
            stream.Position = 0;
            var newintersection = SharedStreetsTileParser.Parse<SharedStreetsIntersection>(stream);
            Assert.IsTrue(newintersection.Count == intersections.Count);

            // check read/write reference
            stream = SharedStreetsTileWriter.Write(references);
            Assert.IsTrue(referenceStream.Length == stream.Length);
            stream.Position = 0;
            var newreference = SharedStreetsTileParser.Parse<SharedStreetsReference>(stream);
            Assert.IsTrue(newreference.Count == references.Count);

            // check read/write metadata
            stream = SharedStreetsTileWriter.Write(metadata);
            Assert.IsTrue(metadataStream.Length == stream.Length);
            stream.Position = 0;
            var newmetadata = SharedStreetsTileParser.Parse<SharedStreetsMetadata>(stream);
            Assert.IsTrue(newmetadata.Count == metadata.Count);


        }

    }
}
