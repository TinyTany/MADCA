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
    public sealed class Hold// : ILongNote
    {
        private readonly List<HoldStepNote> stepNotes;

        public Hold()
        {
            stepNotes = new List<HoldStepNote>();
        }

        public bool Put(in HoldStepNote note)
        {
            stepNotes.Add(note);
            note.PositionChanging += (step, lane, timing) =>
            {
                switch (step.NoteType)
                {
                    case NoteType.HoldBegin:
                        {

                        }
                        break;
                    case NoteType.HoldRelay:
                        {

                        }
                        break;
                    case NoteType.HoldEnd:
                        {

                        }
                        break;
                    default:
                        return false;
                }
                return true;
            };
            return true;
        }

        public bool UnPut(in HoldStepNote note)
        {
            throw new NotImplementedException();
        }
    }
}
