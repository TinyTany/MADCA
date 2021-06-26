using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class Touch : ShortNote
    {
        public override NoteType NoteType => NoteType.Touch;

        private Touch() { }

        public Touch(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
