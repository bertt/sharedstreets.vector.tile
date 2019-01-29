using BruTile.Predefined;
using Google.Protobuf.Collections;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Sharedstreets.Vector.Tile;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<SharedStreetsGeometry> geometries;
        private List<SharedStreetsIntersection> intersections;
        private List<SharedStreetsReference> references;
        private List<SharedStreetsMetadata> metadata;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            MyMapControl.Map.Layers.Add(new TileLayer(KnownTileSources.Create()));
            var point1 = SphericalMercator.FromLonLat(4.875269, 52.327419);
            var point2 = SphericalMercator.FromLonLat(4.956937, 52.355972);
            MyMapControl.Map.NavigateTo(new BoundingBox(point1.X, point1.Y, point2.X, point2.Y));
            MyMapControl.Map.NavigateTo(MyMapControl.Map.Resolutions[18]);
            MyMapControl.Map.Layers.Add(CreatePointLayer());
            MyMapControl.Map.Layers.Add(CreateLineLayer());

        }

        private void LoadData()
        {
            var amsterdamTile = "./testfixtures/12-2103-1346.";

            var geometryStream = File.OpenRead(amsterdamTile + "geometry.6.pbf");
            var intersectionStream = File.OpenRead(amsterdamTile + "intersection.6.pbf");
            var metadataStream = File.OpenRead(amsterdamTile + "metadata.6.pbf");
            var referenceStream = File.OpenRead(amsterdamTile + "reference.6.pbf");

            geometries = SharedStreetsTileParser.Parse<SharedStreetsGeometry>(geometryStream);
            intersections = SharedStreetsTileParser.Parse<SharedStreetsIntersection>(intersectionStream);
            metadata = SharedStreetsTileParser.Parse<SharedStreetsMetadata>(metadataStream);
            references = SharedStreetsTileParser.Parse<SharedStreetsReference>(referenceStream);
        }

        private MemoryLayer CreateLineLayer()
        {
            var features = geometries.Select(c =>
            {
                var line = GetLine(c.Lonlats);
                var feature = new Feature();
                feature.Geometry = line;
                feature["name"] = c.Id;
                return feature;
            });

            var style = new VectorStyle { Line = new Pen(Color.Red, 4) };

            return new MemoryLayer
            {
                Name = "Lines",
                DataSource = new MemoryProvider(features),
                Style = style
            };

        }

        private LineString GetLine(RepeatedField<double> lonlats)
        {
            var linestring = new LineString();
            for(int i=0;i< lonlats.Count;i++)
            {
                var point = SphericalMercator.FromLonLat(lonlats[i], lonlats[i+1]);
                linestring.Vertices.Add(point);
                i++;
            }

            return linestring;
        }


        private MemoryLayer CreatePointLayer()
        {
            var features = intersections.Select(c =>
            {
                var feature = new Feature();
                var point = SphericalMercator.FromLonLat(c.Lon, c.Lat);
                feature.Geometry = point;
                feature["name"] = c.Id;
                return feature;
            });

            return new MemoryLayer
            {
                Name = "Points",
                DataSource = new MemoryProvider(features),
                Style = new SymbolStyle { Fill = { Color = new Color(255, 215, 0, 200) }, SymbolScale = 0.9 }
            };
        }
    }
}
