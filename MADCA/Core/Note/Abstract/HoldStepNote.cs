using System;
using MADCA.Core.Data;

namespace MADCA.Core.Note.Abstract
{
    public abstract class HoldStepNote : NoteBase, IStepNote<HoldStepNote>
    {
        public event Func<HoldStepNote, LanePotision, TimingPosition, bool> PositionChanging;

        protected HoldStepNote() { }

        protected HoldStepNote(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }

        public override bool ReLocate(LanePotision lane, TimingPosition timing)
        {
            if (PositionChanging is null) { return false; }
            if (!PositionChanging.Invoke(this, lane, timing))
            {
                timing = Timing;
            }
            return base.ReLocate(lane, timing);
        }

        public void RemoveAllEvents()
        {
            PositionChanging = null;
        }
    }
}
