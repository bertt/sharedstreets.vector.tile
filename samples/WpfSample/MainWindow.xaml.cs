using System.IO;
using System.Linq;
using System.Windows;
using BruTile.Predefined;
using Mapsui;
using Mapsui.Geometries;
using Mapsui.Layers;
using Mapsui.Projection;
using Mapsui.Providers;
using Mapsui.Styles;
using Sharedstreets.Vector.Tile;

namespace WpfSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MyMapControl.Map.Layers.Add(new TileLayer(KnownTileSources.Create()));
            var point1 = SphericalMercator.FromLonLat(4.875269, 52.327419);
            var point2 = SphericalMercator.FromLonLat(4.956937, 52.355972);
            MyMapControl.Map.NavigateTo(new BoundingBox(point1.X,point1.Y, point2.X, point2.Y));
            MyMapControl.Map.NavigateTo(MyMapControl.Map.Resolutions[18]);
            MyMapControl.Map.Layers.Add(CreatePointLayer());
        }

        private MemoryLayer CreatePointLayer()
        {
            var amsterdamTile = "./testfixtures/12-2103-1346.";

            var geometryStream = File.OpenRead(amsterdamTile + "geometry.6.pbf");
            var intersectionStream = File.OpenRead(amsterdamTile + "intersection.6.pbf");
            var metadataStream = File.OpenRead(amsterdamTile + "metadata.6.pbf");
            var referenceStream = File.OpenRead(amsterdamTile + "reference.6.pbf");

            var geometries = SharedStreetsParser.Parse<SharedStreetsGeometry>(geometryStream);
            var intersections = SharedStreetsParser.Parse<SharedStreetsIntersection>(intersectionStream);
            var metadata = SharedStreetsParser.Parse<SharedStreetsMetadata>(metadataStream);
            var references = SharedStreetsParser.Parse<SharedStreetsReference>(referenceStream);

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
