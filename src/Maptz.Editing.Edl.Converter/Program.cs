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
