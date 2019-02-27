using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Maptz.Editing.Edl
{
    /// <summary>
    /// A deserializer for GVG edls. 
    /// </summary>
    public class GVGEdlReaderService : IEdlDeserializer
    {
        /// <summary>
        /// Read a set of Edl entries from a GVG EDL string. 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public IEnumerable<IEdlEntry> Read(string str)
        {
            var cmxLines = new List<IEdlEntry>();
            var lines = str.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
            EdlEntry currentLine = null;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].TrimEnd();
                var isMatch = Regex.IsMatch(line, "^[0-9]+\\s*");
                if (isMatch)
                {
                    currentLine = new EdlEntry();
                    cmxLines.Add(currentLine);


                    var sourceIn = line.Substring(line.Length - 11 * 4 - 3, 11);
                    var sourceOut = line.Substring(line.Length - 11 * 3 - 2, 11);
                    var recordIn = line.Substring(line.Length - 11 * 2 - 1, 11);
                    var recordOut = line.Substring(line.Length - 11, 11);

                    //NB source framerate might not equal record framerate
                    currentLine.SourceIn = sourceIn;
                    currentLine.SourceOut = sourceOut;

                    //currentLine.SourceIn = new TimeCode(sourceIn, framerate);
                    //currentLine.SourceOut = new TimeCode(sourceOut, framerate);
                    currentLine.RecordIn = recordIn;
                    currentLine.RecordOut = recordOut;
                }
                else
                {
                    if (currentLine != null)
                    {
                        var clipNamePrefix = "* FROM CLIP NAME: ";
                        if (line.StartsWith(clipNamePrefix))
                        {
                            currentLine.ClipName = line.Substring(clipNamePrefix.Length);
                        }
                        currentLine.Notes += line;
                    }
                }

            }

            return cmxLines;
        }
    }
}