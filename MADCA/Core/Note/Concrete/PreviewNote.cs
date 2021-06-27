using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;

namespace MADCA.Core.Note.Concrete
{
    public sealed class PreviewNote : ShortNote
    {
        public override NoteType NoteType => NoteType.Preview;
        public NoteType SelectedNote { get; set; } = NoteType.Unknown;

        private PreviewNote() { }

        public PreviewNote(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }
    }
}
