using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class HoldRelay : HoldStepNote
    {
        public override NoteType NoteType => NoteType.HoldRelay;

        private HoldRelay() { }

        public HoldRelay(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
