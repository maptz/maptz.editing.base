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
        double Db { get; }
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
        public double Db
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
        public double? SourceFPS { get; set; }

        public double? SequenceFPS { get; set; }


        public double? RecordInSeconds
        {
            get => ToSeconds(this.RecordIn, this.SequenceFPS);
        }
        public double? RecordOutSeconds
        {
            get => ToSeconds(this.RecordOut, this.SequenceFPS);
        }

        public double? SourceInSeconds
        {
            get => ToSeconds(this.SourceIn, SourceFPS);
        }
        public double? SourceOutSeconds
        {
            get => ToSeconds(this.SourceOut, SourceFPS);
        }

        public double? ToSeconds(string str, double? fps)
        {
            var fr = GetFrameRate(fps);
            if (fr == SmpteFrameRate.Unknown) return null;
            var tc = new TimeCode(str, fr);
            var t = tc.TotalSecondsPrecision;
            return tc.TotalSeconds;



        }

        public static SmpteFrameRate GetFrameRate(double? fps)
        {
            switch (fps)
            {
                case null: return SmpteFrameRate.Unknown;
                case 23.98: return SmpteFrameRate.Smpte2398;
                case 24: return SmpteFrameRate.Smpte24;
                case 25: return SmpteFrameRate.Smpte25;
                //case 29.97: return SmpteFrameRate.Smpte2997Drop;
                case 29.97: return SmpteFrameRate.Smpte2997NonDrop;
                case 30: return SmpteFrameRate.Smpte30;
                case 50: return SmpteFrameRate.Smpte25;
                case 60: return SmpteFrameRate.Smpte30;

                default: return SmpteFrameRate.Unknown;
            }
        }

        

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
