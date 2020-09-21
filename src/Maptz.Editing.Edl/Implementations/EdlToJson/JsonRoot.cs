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
    public class JsonRoot
    {
        public IEnumerable<JsonTrack> VideoTracks { get; set; }
        public IEnumerable<JsonTrack> AudioTracks { get; set; }
    }
}