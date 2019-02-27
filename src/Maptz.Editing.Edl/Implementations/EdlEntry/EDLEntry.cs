namespace Maptz.Editing.Edl
{

    public class EdlEntry : IEdlEntry
    {
        /* #region Interface: 'Maptz.Avid.Edl.IEdlEntry' Properties */
        /// <summary>
        /// The clip name. 
        /// </summary>
        public string ClipName { get; set; }

        /// <summary>
        /// The notes. 
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The record in timecode. 
        /// </summary>
        public string RecordIn { get; set; }

        /// <summary>
        /// The record out timecode. 
        /// </summary>
        public string RecordOut { get; set; }

        /// <summary>
        /// The source in timecode. 
        /// </summary>
        public string SourceIn { get; set; }

        /// <summary>
        /// The source out timecode. 
        /// </summary>
        public string SourceOut { get; set; }
        /* #endregion Interface: 'Maptz.Avid.Edl.IEdlEntry' Properties */
        /* #region Public Methods */
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.RecordIn, this.ClipName);
        }
        /* #endregion Public Methods */
    }
}
