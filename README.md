# sharedstreets-vector-tile

## Protocol buffer generated code

The file 'SharedStreetsProto.cs' is generated by tool 'protoc' based on the 'sharedstreets.proto' definition file.

- sharedstreets.proto file is copied from : https://github.com/sharedstreets/sharedstreets-ref-system/blob/master/proto/sharedstreets.proto

- tool protoc can be obtained from https://github.com/protocolbuffers/protobuf/releases

- Compile command (generates a c# file):

```
$ protoc sharedstreets.proto --csharp_out=.
```

sharedstreets is adapted a little bit to get the parsing going, by removing lines like:

```
_unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
```

## Sample code

```
var amsterdamTile = "./testfixtures/12-2103-1346.";
var geometryStream = File.OpenRead(amsterdamTile + "geometry.6.pbf");
var intersectionStream = File.OpenRead(amsterdamTile + "intersection.6.pbf");
var metadataStream = File.OpenRead(amsterdamTile + "metadata.6.pbf");
var referenceStream = File.OpenRead(amsterdamTile + "reference.6.pbf");

var geometry = SharedStreetsParser.ParseGeometry(geometryStream);
Assert.IsTrue(geometry.Lonlats.Count == 50906);
var intersection = SharedStreetsParser.ParseIntersection(intersectionStream);
Assert.IsTrue(intersection.Id == "7f1e47e658e554625c51c6fc6cdafcda");
var metadata = SharedStreetsParser.ParseMetadata(metadataStream);
Assert.IsTrue(metadata.OsmMetadata.WaySections.Count == 8431);
var reference = SharedStreetsParser.ParseReference(referenceStream);
Assert.IsTrue(reference.LocationReferences.Count == 17382);
```

## Api samples

1] Get by id

Request: HTTP GET https://api.sharedstreets.io/v0.1.0/id/7f1e47e658e554625c51c6fc6cdafcda

Response: 
```
{"type":"INT","tiles":["osm/planet-180430/12-2103-1346.intersection.6.pbf","osm/planet-180730/12-2103-1346.intersection.6.pbf","osm/planet-181126/12-2103-1346.intersection.7.pbf","osm/planet-181029/12-2103-1346.intersection.6.pbf","osm/planet-181029/12-2103-1346.intersection.7.pbf","osm/planet-181029/12-2103-1346.intersection.8.pbf","osm/planet-180625/12-2103-1346.intersection.6.pbf","osm/planet-180528/12-2103-1346.intersection.6.pbf","osm/planet-180528/12-2103-1346.intersection.7.pbf","osm/planet-180827/12-2103-1346.intersection.6.pbf","osm/planet-180827/12-2103-1346.intersection.7.pbf","osm/planet-180924/12-2103-1346.intersection.6.pbf","osm/planet-180924/12-2103-1346.intersection.7.pbf","osm/planet-181126/12-2103-1346.intersection.6.pbf"]}
```

2] Get by bbox

Request: HTTP GET https://api.sharedstreets.io/v0.1.0//geom/within?bounds=5.140647,52.082269,5.156558,52.088242&auth=<auth_key>

Response: GeoJSON

3] Match line

Request: HTTP POST https://api.sharedstreets.io/v0.1.0/match/geoms?&includeStreetnames=true&lengthTolerance=0.1&bearingTolerance=10&searchRadius=15&auth=<auth_key>5&ignoreDirection=true&snapToIntersections=true

body:

```
{"type":"Feature","geometry":{"type":"LineString","coordinates":[[-122.33482897844316,47.60832634703897],[-122.33395994272226,47.60869886212237]]},"properties":{}}
```

Response:
```
{"matched":{"type":"FeatureCollection","features":[{"type":"Feature","properties":{"referenceId":"b60d4a26865ad09424a4879130600bf2","fromIntersectionId":"e9604c0d8549130e5de39c96565d07b7","toIntersectionId":"39b03988800c0804ed8a4f68c414a854","fromStreetnames":["4th Avenue"],"toStreetnames":["5th Avenue"],"roadClass":"Tertiary","direction":"forward","geometryId":"c9ed74564c19b8b1690edd9d3fa009cf","streetname":"University Street","referenceLength":99.77,"section":[0,99.77],"side":"left","originalFeature":{"type":"Feature","geometry":{"type":"LineString","coordinates":[[-122.33482897844316,47.60832634703897],[-122.33395994272226,47.60869886212237]]},"properties":{}}},"geometry":{"type":"LineString","coordinates":[[-122.3348881,47.6082149],[-122.3347427,47.6082751],[-122.3343875,47.6084223],[-122.33422060000001,47.608491400000005],[-122.3338736,47.6086351],[-122.3337566,47.608683600000006]]}}]},"unmatched":{"type":"FeatureCollection","features":[]},"invalid":{"type":"FeatureCollection","features":[]}}
```
