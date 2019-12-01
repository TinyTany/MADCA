using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Interface;

namespace MADCA.Core.Note.Concrete
{
    public sealed class HoldEnd : HoldStepNote
    {
        public override NoteType NoteType => NoteType.HoldEnd;

        private HoldEnd() { }

        public HoldEnd(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
