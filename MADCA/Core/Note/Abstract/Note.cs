using MADCA.Core.Data;
using System.Drawing;
using MADCA.Utility;

namespace MADCA.Core.Note.Abstract
{
    public abstract class NoteBase : INote
    {
        public virtual NoteType NoteType => NoteType.Unknown;

        public LanePotision Lane { get; private set; }

        public TimingPosition Timing { get; private set; }

        public NoteSize NoteSize { get; private set; }

        protected NoteBase() { }

        protected NoteBase(LanePotision lane, TimingPosition timing, NoteSize size)
        {
            Lane = new LanePotision(lane);
            Timing = new TimingPosition(timing);
            NoteSize = new NoteSize(size);
        }

        public virtual bool ReLocate(LanePotision lane, TimingPosition timing)
        {
            Lane = new LanePotision(lane);
            Timing = new TimingPosition(timing);
            return true;
        }

        public bool ReSize(NoteSize size)
        {
            NoteSize = new NoteSize(size);
            return true;
        }

        public Rectangle GetRectangle(IReadOnlyEditorLaneEnvironment env)
        {
            var loc = PositionConverter.ConvertVirtualToRealNorm(env, new Position(Lane, Timing));
            int width = NoteEnvironment.NoteWidth(NoteSize.Size, env);
            int height = NoteEnvironment.NoteHeight;
            return new Rectangle(loc.X, loc.Y - NoteEnvironment.NoteHeight / 2, width, height);
        }
    }
}
