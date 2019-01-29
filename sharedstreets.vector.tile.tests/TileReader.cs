using System.IO;

namespace SharedStreets.Vector.Tile.Tests
{
    public static class TileReader
    {
        static string tileprefix = "./testfixtures/12-2103-1346.";

        public static Stream GetGeometryTile()
        {
            return File.OpenRead(tileprefix + "geometry.6.pbf");
        }

        public static Stream GetIntersectionTile()
        {
            return File.OpenRead(tileprefix + "intersection.6.pbf");
        }

        public static Stream GetReferenceTile()
        {
            return File.OpenRead(tileprefix + "reference.6.pbf");
        }

        public static Stream GetMetadataTile()
        {
            return File.OpenRead(tileprefix + "metadata.6.pbf");
        }

    }
}
