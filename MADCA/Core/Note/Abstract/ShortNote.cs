using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;

namespace MADCA.Core.Note.Abstract
{
    public abstract class ShortNote : Note
    {
        protected ShortNote() { }

        protected ShortNote(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
