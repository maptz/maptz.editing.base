namespace Maptz.Editing.Edl
{
    /// <summary>
    /// An entry in an Edl. 
    /// 
    /// NOTE: This used to use a TimeCode object, but no longer does. You cannot rely on knowing the Source timecode frame-rate. 
    /// </summary>
    public interface IEdlEntry
    {
        ILevel Level { get; }
        IPatch Patch { get; }

        string Track { get;  }

        /* #region Public Properties */
        /// <summary>
        /// The clip name. 
        /// </summary>
        string ClipName { get; set; }
        /// <summary>
        /// Notes for the clip 
        /// </summary>
        string Notes { get; set; }
        /// <summary>
        /// The record in TimeCode. 
        /// </summary>
        string RecordIn { get; set; }
        /// <summary>
        /// The record out timecode. 
        /// </summary>
        string RecordOut { get; set; }
        /// <summary>
        /// The source in timecode. 
        /// </summary>
        string SourceIn { get; set; }
        /// <summary>
        /// The source out timecode. 
        /// </summary>
        string SourceOut { get; set; }
        /* #endregion Public Properties */
    }
}