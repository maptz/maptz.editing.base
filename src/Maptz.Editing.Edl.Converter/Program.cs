using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Maptz.Editing.Edl.Converter
{
    public class JsonRoot
    {
        public IEnumerable<JsonTrack> VideoTracks { get; set; }
        public IEnumerable<JsonTrack> AudioTracks { get; set; }
    }
    public class JsonTrack
    {
        public string Name { get; set; }
        public IEnumerable<IEdlEntry> Entries { get; set; }
    }

    class EDLToJsonConverter
    {
        public static string Convert(IEnumerable<IEdlEntry> edlEntries)
        {
            var grp = edlEntries.GroupBy(p => p.Track);
            var root = new JsonRoot
            {
                VideoTracks = grp.Where(p => p.Key.StartsWith("V")).Select(p => new JsonTrack
                {
                    Name = p.Key,
                    Entries = p.ToArray()
                }).ToArray(),
                AudioTracks = grp.Where(p => p.Key.StartsWith("A")).Select(p => new JsonTrack
                {
                    Name = p.Key,
                    Entries = p.ToArray()
                }).ToArray(),
            };

            var serializerSettings = new JsonSerializerSettings();
            serializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var json = JsonConvert.SerializeObject(root, Formatting.Indented, serializerSettings);
            return json;

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var testFile = @"F:\PROJECT FILES - JW\EDL PREMIERE IMPORT TEST\PULLS - IV - DARRYL COLLIS - 200 years, COVID.01.edl";
            ReadEdl(testFile).GetAwaiter().GetResult();
        }

        static async Task ReadEdl(string filePath)
        {
            var txt = File.ReadAllText(filePath);
            var edlDeserializer = new CMX3600Deserializer();
            var edlEntries = edlDeserializer.Read(txt);
            var json = EDLToJsonConverter.Convert(edlEntries);
            var outputFilePath = Path.Combine(new FileInfo(filePath).Directory.FullName, Path.GetFileNameWithoutExtension(filePath) + ".json");
            File.WriteAllText(outputFilePath, json);

        }
    }
}
