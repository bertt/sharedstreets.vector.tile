using Google.Protobuf;
using System.Collections.Generic;
using System.IO;

namespace SharedStreets.Vector.Tile
{
    public static class SharedStreetsTileWriter
    {
        public static Stream Write<T>(List<T> objects)
        {
            var stream = new MemoryStream();
            var output = new CodedOutputStream(stream);
            var type = typeof(T);
            var typeGeometry = typeof(SharedStreetsGeometry);
            var typeIntersection = typeof(SharedStreetsIntersection);
            var typeReference = typeof(SharedStreetsReference);
            var typeMetadata = typeof(SharedStreetsMetadata);

            var isGeometry = type.Equals(typeGeometry);
            var isIntersection = type.Equals(typeIntersection);
            var isReference = type.Equals(typeReference);
            var isMetadata = type.Equals(typeMetadata);

            foreach (var o in objects)
            {
                if (isGeometry)
                {
                    output.WriteMessage((SharedStreetsGeometry)(object)o);
                }
                else if (isIntersection)
                {
                    output.WriteMessage((SharedStreetsIntersection)(object)o);
                }
                else if (isReference)
                {
                    output.WriteMessage((SharedStreetsReference)(object)o);
                }
                else if (isMetadata)
                {
                    output.WriteMessage((SharedStreetsMetadata)(object)o);
                }

                output.Flush();
            }

            return stream;
        }
    }
}
