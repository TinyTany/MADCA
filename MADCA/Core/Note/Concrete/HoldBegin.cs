using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class HoldBegin : HoldStepNote
    {
        public override NoteType NoteType => NoteType.HoldBegin;

        private HoldBegin() { }

        public HoldBegin(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
