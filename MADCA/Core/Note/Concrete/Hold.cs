using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Note.Interface;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Data;

namespace MADCA.Core.Note.Concrete
{
    public sealed class Hold : ILongNote<HoldStepNote>
    {
        private readonly List<HoldStepNote> stepNotes;

        public HoldBegin HoldBegin { get; }
        public HoldEnd HoldEnd { get; }

        public IReadOnlyList<HoldStepNote> StepNotes => stepNotes;

        // NOTE: こんな実装で大丈夫なのだろうか…
        public IReadOnlyList<NoteBase> AllNotes
        {
            get
            {
                var notes = new List<NoteBase>() { HoldBegin };
                notes.AddRange(StepNotes);
                notes.Add(HoldEnd);
                return notes;
            }
        }

        private Hold() { }

        public Hold(LanePotision lane, TimingPosition timingBegin, TimingPosition timingEnd, NoteSize size)
        {
            System.Diagnostics.Debug.Assert(timingBegin < timingEnd);
            HoldBegin = new HoldBegin(lane, timingBegin, size);
            HoldBegin.PositionChanging += (begin, beginLane, beginTiming) =>
            {
                if (stepNotes.Where(x => x.Timing <= beginTiming).Any())
                {
                    return false;
                }
                if (!(beginTiming < HoldEnd.Timing))
                {
                    return false;
                }
                return true;
            };
            HoldEnd = new HoldEnd(lane, timingEnd, size);
            HoldEnd.PositionChanging += (end, endLane, endTiming) =>
            {
                if (stepNotes.Where(x => x.Timing >= endTiming).Any())
                {
                    return false;
                }
                if (!(HoldBegin.Timing < endTiming))
                {
                    return false;
                }
                return true;
            };
            stepNotes = new List<HoldStepNote>();
        }

        public bool Put(HoldStepNote note)
        {
            if (note is null) { return false; }
            if (!IsStepNoteTimingValid(note.Timing)) { return false; }
            stepNotes.Add(note);
            note.PositionChanging += (step, lane, timing) =>
            {
                switch (step.NoteType)
                {
                    case NoteType.HoldRelay:
                        {
                            return IsStepNoteTimingValid(timing);
                        }
                    default:
                        return false;
                }
            };
            return true;
        }

        public bool UnPut(HoldStepNote note)
        {
            var result = stepNotes.Remove(note);
            if (result)
            {
                note.RemoveAllEvents();
            }
            return result;
        }

        private bool IsStepNoteTimingValid(TimingPosition timing)
        {
            if (timing is null) { return false; }
            if (timing <= HoldBegin.Timing || timing >= HoldEnd.Timing) { return false; }
            if (stepNotes.Where(x => x.Timing == timing).Any()) { return false; }
            return true;
        }
    }
}
