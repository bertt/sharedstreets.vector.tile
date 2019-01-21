using System.IO;

namespace Sharedstreets.Vector.Tile
{
    public static class SharedStreetsParser
    {
        public static SharedStreetsGeometry ParseGeometry(Stream stream)
        {
            var sharedStreetsGeometry = SharedStreetsGeometry.Parser.ParseFrom(stream);
            return sharedStreetsGeometry;
        }

        public static SharedStreetsMetadata ParseMetadata(Stream stream)
        {
            var sharedStreetsMetadata = SharedStreetsMetadata.Parser.ParseFrom(stream);
            return sharedStreetsMetadata;
        }

        public static SharedStreetsIntersection ParseIntersection(Stream stream)
        {
            var sharedStreetsIntersection = SharedStreetsIntersection.Parser.ParseFrom(stream);
            return sharedStreetsIntersection;
        }

        public static SharedStreetsReference ParseReference(Stream stream)
        {
            var sharedStreetsReference= SharedStreetsReference.Parser.ParseFrom(stream);
            return sharedStreetsReference;
        }
    }
}
