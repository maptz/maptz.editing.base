using Maptz.Editing.Edl;
using Maptz.Timelines;
using System.Collections.Generic;

namespace Maptz.Avid.Edl.Timelines
{




    /// <summary>
    /// Extension methods for IEdlEntry collections. 
    /// </summary>
    public static class EdlEntryExtensions
    {
        #region Public Static Methods

        /// <summary>
        /// Gets a collection of EdlEntryTrimelineSpan instances from a collection of Edl entries. This involves converting the Record TimeCode strings into TimeCode objects. 
        /// </summary>
        /// <param name="edlEntries"></param>
        /// <param name="recordFrameRate"></param>
        /// <returns></returns>
        public static IEnumerable<IEdlEntryTimelineSpan> ToEdlEntryTimelineSpans(this IEnumerable<IEdlEntry> edlEntries, SmpteFrameRate recordFrameRate)
        {
            foreach(var edlEntry in edlEntries)
            {
                yield return EdlEntryTimelineSpan.FromEdlEntry(edlEntry, recordFrameRate);
            }
        }

        /// <summary>
        /// Flattens contiguous spans
        /// </summary>
        /// <param name="edlEntries"></param>
        /// <returns></returns>
        public static IEnumerable<IEdlEntryTimelineSpan> FlattenContiguousSpans(this IEnumerable<IEdlEntryTimelineSpan> edlEntries, SmpteFrameRate recordFrameRate)
        {
            long lastOutFrameNumber = -1;

            EdlEntry currentLine = new EdlEntry();
            var resolvedLines = new List<EdlEntry>();
            foreach (var edlEntry in edlEntries)
            {
                if (edlEntry.Start <= lastOutFrameNumber)
                {
                    //We've got a contiguous clip.
                    currentLine.RecordOut = new TimeCode(edlEntry.End, edlEntry.FrameRate).ToString();
                    currentLine.Notes += edlEntry.Content.ClipName;
                    lastOutFrameNumber = edlEntry.End;
                }
                else
                {
                    currentLine = new EdlEntry();
                    resolvedLines.Add(currentLine);
                    currentLine.RecordIn = new TimeCode(edlEntry.Start, edlEntry.FrameRate).ToString();
                    currentLine.RecordOut = new TimeCode(edlEntry.End, edlEntry.FrameRate).ToString();
                    currentLine.Notes += edlEntry.Content.ClipName;
                    lastOutFrameNumber = edlEntry.End;
                }
            }
            return resolvedLines.ToEdlEntryTimelineSpans(recordFrameRate);
        }
        #endregion Public Static Methods

    }
}