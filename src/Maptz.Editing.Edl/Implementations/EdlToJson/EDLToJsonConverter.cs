using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
namespace Maptz.Editing.Edl.Json
{

    public class EDLToJsonConverter
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
}