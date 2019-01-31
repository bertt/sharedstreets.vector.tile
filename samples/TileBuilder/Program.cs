using OsmSharp;
using OsmSharp.Streams;
using SharedStreets.Vector.Tile;
using System;
using System.IO;
using Tiles.Tools;

namespace TileBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var osmpbf = "./data/utrecht-latest.osm.pbf";
            // https://tiles.sharedstreets.io/12-2106-1351.intersection.pbf
            var intersectionStream = File.OpenRead("./data/12-2106-1351.intersection.pbf");
            var intersections = SharedStreetsTileParser.Parse<SharedStreetsIntersection>(intersectionStream);
            // 5099
            Console.WriteLine("Number of intersections:" + intersections.Count);

            var tile = new Tile(2106, 1351, 12);
            var bounds = tile.Bounds();

            // read osm pbf
            var source = new PBFOsmStreamSource(new FileInfo(osmpbf).OpenRead());
            var filtered = source.FilterBox((float)bounds[0], (float)bounds[3], (float)bounds[2], (float)bounds[1]); // left, top, right, bottom
            var i = 0;

            //grab all way segments for road name A
            //grab all way segments for road named B
            //compare both list of nodes
            //return equal nodes


            // filter intersections, geometries
            // todo: improve perf 
            foreach (var element in filtered)
            {
                if (element.Type == OsmGeoType.Way)
                {
                    var way = (Way)element;
                    var nodes = way.Nodes;
                    foreach (var node in nodes)
                    {
                        var otherway = GetOtherWay(source, way, node);

                        if (otherway != null)
                        {
                            Console.WriteLine(node);
                            i++;
                        }
                    }
                }
            }

            //  533926
            Console.WriteLine("Number of nodes: " + i);
            Console.ReadKey();
        }

        private static Way GetOtherWay(OsmStreamSource source, Way OriginalWay, long searchnode)
        {
            foreach (var element in source)
            {
                if (element.Type == OsmGeoType.Way)
                {
                    var way = (Way)element;

                    if (way.Id != OriginalWay.Id)
                    {
                        var nodes = way.Nodes;
                        foreach (var node in nodes)
                        {
                            if (node == searchnode)
                            {
                                return way;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
