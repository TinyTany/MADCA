using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class Chain : ShortNote
    {
        public override NoteType NoteType => NoteType.Chain;

        private Chain() { }

        public Chain(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
