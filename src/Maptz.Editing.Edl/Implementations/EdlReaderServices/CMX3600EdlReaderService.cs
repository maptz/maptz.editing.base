using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
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
            EdlEntry currentEdlEntry = null;
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                var isMatch = Regex.IsMatch(line, "^[0-9]+\\s*");
                if (isMatch)
                {
                    currentEdlEntry = new EdlEntry();
                    currentEdlEntry.SequenceFPS = 25.0;
                    currentEdlEntry.SourceFPS = 25.0; //Can be overridden if an M2 field is found.
                    cmxLines.Add(currentEdlEntry);
                    var track = line.Substring(14, 3);
                    currentEdlEntry.Track = track.Trim();

                    var sourceIn = line.Substring(29, 11);
                    var sourceOut = line.Substring(29 + 11 + 1, 11);
                    var recordIn = line.Substring(29 + (11 + 1) * 2, 11);
                    var recordOut = line.Substring(29 + (11 + 1) * 3, 11);

                    //NB source framerate might not equal record framerate
                    currentEdlEntry.SourceIn = sourceIn;
                    currentEdlEntry.SourceOut = sourceOut;

                    //currentLine.SourceIn = new TimeCode(sourceIn, framerate);
                    //currentLine.SourceOut = new TimeCode(sourceOut, framerate);
                    currentEdlEntry.RecordIn = recordIn;
                    currentEdlEntry.RecordOut =recordOut;
                }
                else
                {if (i == 17)
                    {

                    }

                    if (currentEdlEntry != null)
                    {
                        if (line.StartsWith("* A1 VOL"))
                        {

                        }
                        var clipNamePrefix = "* FROM CLIP NAME: ";
                        var volMatch = Regex.Match(line, "^\\*\\s+A[0-9]+\\sVOL\\s+=\\s+(?<level>[\\-\\+][0-9\\.]+)");
                        if (line.StartsWith(clipNamePrefix))
                        {
                            currentEdlEntry.ClipName = line.Substring(clipNamePrefix.Length);
                        }
                        else if (line.StartsWith("* PATCH "))
                        {
                            //*PATCH A004C003: FROM SOURCE 1 TO RECORD 5
                            var regex = new Regex("FROM\\sSOURCE\\s(?<source>[0-9]+)\\sTO\\sRECORD\\s(?<record>[0-9]+)");
                            var match =regex.Match(line);
                            if (!match.Success)
                            {
                                throw new NotSupportedException("Unexpected patch: " + line);
                            }
                            currentEdlEntry.Patch = new Patch
                            {
                                Source = int.Parse(match.Groups["source"].Value),
                                Target = int.Parse(match.Groups["record"].Value),
                            };
                        }
                        else if (line.StartsWith("M2 "))
                        {
                            var fps = double.Parse(line.Substring(20, 5));
                            currentEdlEntry.SourceFPS = fps;
                        }
                        else if (volMatch.Success)
                        {
                            /////A3 VOL =  +8.0 DB  PAN R100 
                            currentEdlEntry.Level = new Level
                            {
                                Db = double.Parse(volMatch.Groups["level"].Value)
                            };
                        }
                        

                        currentEdlEntry.Notes += line;
                    }
                }

            }

            return cmxLines;
        }
    }
}