using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace Maptz.Editing.Edl
{
    /// <summary>
    /// A deserializer for CMX3600 files. 
    /// </summary>
    public class CMX3600Deserializer : IEdlDeserializer
    {
        /// <summary>
        /// Read a set of EDL entries from an input string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public IEnumerable<IEdlEntry> Read(string str)
        {
            var cmxLines = new List<IEdlEntry>();
            var lines = str.Split(new string[] { Environment.NewLine, "\n" }, StringSplitOptions.None);
            EdlEntry currentLine = null;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var isMatch = Regex.IsMatch(line, "^[0-9]+\\s*");
                if (isMatch)
                {
                    currentLine = new EdlEntry();
                    cmxLines.Add(currentLine);
                    var sourceIn = line.Substring(29, 11);
                    var sourceOut = line.Substring(29 + 11 + 1, 11);
                    var recordIn = line.Substring(29 + (11 + 1) * 2, 11);
                    var recordOut = line.Substring(29 + (11 + 1) * 3, 11);

                    //NB source framerate might not equal record framerate
                    currentLine.SourceIn = sourceIn;
                    currentLine.SourceOut = sourceOut;

                    //currentLine.SourceIn = new TimeCode(sourceIn, framerate);
                    //currentLine.SourceOut = new TimeCode(sourceOut, framerate);
                    currentLine.RecordIn = recordIn;
                    currentLine.RecordOut =recordOut;
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