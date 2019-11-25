﻿using System;
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
        private readonly HoldBegin holdBegin;

        private Hold()
        {
            holdBegin = null;
            stepNotes = new List<HoldStepNote>();
        }

        public Hold(LanePotision lane, TimingPosition timingBegin, TimingPosition timingEnd, NoteSize size)
        {
            System.Diagnostics.Debug.Assert(timingBegin < timingEnd);
            holdBegin = new HoldBegin(lane, timingBegin, size);
            holdBegin.PositionChanging += (begin, beginLane, beginTiming) =>
            {
                if (stepNotes.Where(x => x.Timing <= beginTiming).Any())
                {
                    return false;
                }
                return true;
            };
            stepNotes = new List<HoldStepNote>();
            var result = Put(new HoldRelay(lane, timingEnd, size));
            System.Diagnostics.Debug.Assert(result);
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
            if (timing <= holdBegin.Timing) { return false; }
            if (stepNotes.Where(x => x.Timing == timing).Any()) { return false; }
            return true;
        }
    }
}
