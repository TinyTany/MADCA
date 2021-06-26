using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class SlideL : ShortNote
    {
        public override NoteType NoteType => NoteType.SlideL;

        private SlideL() { }

        public SlideL(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
