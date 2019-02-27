
using Maptz.Editing.Edl;

namespace Maptz.Avid.Edl.Timelines
{
    /// <summary>
    /// A span between two timecodes on a timeline, representing an EDL entry. 
    /// </summary>
    public interface IEdlEntryTimelineSpan : ITimeCodeTimelineContentSpan<IEdlEntry>
    {
        
    }
}