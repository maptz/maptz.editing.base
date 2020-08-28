using System.Runtime.CompilerServices;

namespace Maptz.Editing.Edl
{
    public interface IPatch
    {
        int Source { get; }
        int Target { get; }
    }


    public interface ILevel
    {
        int Db { get; }
    }

    public class Patch : IPatch
    {
        public int Source { get; set; }
        public int Target { get; set; }

        public override string ToString()
        {
            return $"{this.Source} {this.Target}";
        }
    }

    public class Level : ILevel
    {
        public int Db
        {
            get;set;
        }

        public override string ToString()
        {
            return $"{this.Db} Db";
        }
    }

    public class EdlEntry : IEdlEntry
    {
        /* #region Interface: 'Maptz.Avid.Edl.IEdlEntry' Properties */
        /// <summary>
        /// The clip name. 
        /// </summary>
        public string ClipName { get; set; }

        public string Track { get; set; }

        public Level Level { get; set; }

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

        public Patch Patch { get; set; }

        ILevel IEdlEntry.Level => this.Level;

        IPatch IEdlEntry.Patch => this.Patch;


        /* #endregion Interface: 'Maptz.Avid.Edl.IEdlEntry' Properties */
        /* #region Public Methods */
        public override string ToString()
        {
            var patch = this.Patch == null ? string.Empty : $" ({this.Patch.ToString()})";
            return string.Format("{0} {1} - {2}{3}", this.Track, this.RecordIn, this.ClipName, patch);
        }
        /* #endregion Public Methods */
    }
}
