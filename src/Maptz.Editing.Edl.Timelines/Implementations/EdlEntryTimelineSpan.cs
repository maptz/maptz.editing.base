using Maptz.Editing.Edl;
using Maptz.Timelines;
namespace Maptz.Avid.Edl.Timelines
{


    /// <summary>
    /// An EDL entry interpretted as a ITimelineSpanItem. 
    /// </summary>
    public class EdlEntryTimelineSpan : TimeCodeTimelineContentSpan<IEdlEntry>, IEdlEntryTimelineSpan
    {
        public EdlEntryTimelineSpan(long start, long length, IEdlEntry item, SmpteFrameRate frameRate) : base(start, length, item, frameRate)
        {

        }

        public static EdlEntryTimelineSpan FromEdlEntry(IEdlEntry entry, SmpteFrameRate recordFrameRate)
        {
            var startTimeCode = new TimeCode(entry.RecordIn, recordFrameRate);
            var outTimeCode = new TimeCode(entry.RecordIn, recordFrameRate);

            return new EdlEntryTimelineSpan(startTimeCode.TotalFrames, outTimeCode.TotalFrames - startTimeCode.TotalFrames, entry, recordFrameRate);
            //, IDictionary<IEdlEntry, SmpteFrameRate> sourceFrameRateLookup
        }
    }
}