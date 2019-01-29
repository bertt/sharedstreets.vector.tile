using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharedStreets.Vector.Tile
{
    public static class SharedStreets
    {
        public static string GenerateHash(string input)
        {
            var hash = new StringBuilder();
            var md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public static string GeometryId(RepeatedField<double> coords)
        {
            var message = GeometryMessage(coords);
            return GenerateHash(message);
        }


        public static string GeometryMessage(RepeatedField<double> coords)
        {
            var c = Round(coords);
            var result = $"Geometry {string.Join(" ", c)}";
            return result;
        }

        public static List<string> Round(RepeatedField<double> values)
        {
            var result = new List<string>();
            foreach(var v in values)
            {
                result.Add(v.ToString("F5"));
            }
            return result;
        }
    }
}
