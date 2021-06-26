using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class SnapD : ShortNote
    {
        public override NoteType NoteType => NoteType.SnapD;

        private SnapD() { }

        public SnapD(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
