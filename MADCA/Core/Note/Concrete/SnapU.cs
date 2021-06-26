using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class SnapU : ShortNote
    {
        public override NoteType NoteType => NoteType.SnapU;

        private SnapU() { }

        public SnapU(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
