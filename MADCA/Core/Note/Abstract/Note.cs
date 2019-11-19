using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;
using MADCA.Core.Note.Interface;

namespace MADCA.Core.Note.Abstract
{
    public abstract class Note : INote
    {
        public virtual NoteType NoteType => NoteType.Unknown;

        public LanePotision Lane { get; private set; }

        public TimingPosition Timing { get; private set; }

        public NoteSize NoteSize { get; private set; }

        public bool ReLocate(LanePotision lane, TimingPosition timing)
        {
            Lane = new LanePotision(lane);
            Timing = new TimingPosition(timing);
            return true;
        }

        public bool ReSize(NoteSize size)
        {
            NoteSize = new NoteSize(size);
            return true;
        }
    }
}
