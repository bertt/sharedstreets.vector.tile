using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.IO;

namespace SharedStreets.Vector.Tile
{
    public static class SharedStreetsTileParser
    {
        public static List<T> Parse<T>(Stream stream)
        {
            var objects = new List<T>();
            bool cont = true;
            do
            {
                try
                {
                    var o = ReadObject<T>(stream);
                    objects.Add((T)Convert.ChangeType(o, typeof(T)));
                }
                // workaround for handling eof https://github.com/protocolbuffers/protobuf/issues/4303
                catch (InvalidProtocolBufferException)
                {
                    cont = false;
                }
            }
            while (cont);

            return objects;
        }

        private static object ReadObject<T>(Stream stream)
        {
            object o = null;
            if (typeof(T).Equals(typeof(SharedStreetsGeometry)))
            {
                o = SharedStreetsGeometry.Parser.ParseDelimitedFrom(stream);
            }
            else if (typeof(T).Equals(typeof(SharedStreetsIntersection)))
            {
                o = SharedStreetsIntersection.Parser.ParseDelimitedFrom(stream);
            }
            else if (typeof(T).Equals(typeof(SharedStreetsMetadata)))
            {
                o = SharedStreetsMetadata.Parser.ParseDelimitedFrom(stream);
            }
            else if (typeof(T).Equals(typeof(SharedStreetsReference)))
            {
                o = SharedStreetsReference.Parser.ParseDelimitedFrom(stream);
            }

            return o;
        }
    }
}
