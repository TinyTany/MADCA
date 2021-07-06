using System;
using MADCA.Core.Data;
using MADCA.Core.Note;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;

namespace MADCA.Core.Operation
{
    // インタフェースにした方がいいかなあ
    public abstract class Operation
    {
        public Action Invoke, Undo;
    }

    public class CompositeOperation : Operation
    {
        private CompositeOperation() { }

        public CompositeOperation(params Operation[] operations)
        {
            Invoke = () =>
            {
                for (var i = 0; i < operations.Length; ++i)
                {
                    operations[i].Invoke();
                }
            };

            Undo = () =>
            {
                for (var i = 0; i < operations.Length; ++i)
                {
                    operations[i].Undo();
                }
            };
        }
    }

    public class AddShortNoteOperation : Operation
    {
        private AddShortNoteOperation() { }

        public AddShortNoteOperation(NoteBook book, Note.Abstract.ShortNote note) 
        {
            Invoke = () => 
            {
                book.PutShortNote(note);
            };

            Undo = () =>
            {
                book.UnPutShortNote(note);
            };
        }
    }

    public class DeleteShortNoteOperation : Operation
    {
        private DeleteShortNoteOperation() { }

        public DeleteShortNoteOperation(NoteBook book, Note.Abstract.ShortNote note)
        {
            Invoke = () =>
            {
                book.UnPutShortNote(note);
            };

            Undo = () =>
            {
                book.PutShortNote(note);
            };
        }
    }

    public class ReLocateNoteOperation : Operation
    {
        private ReLocateNoteOperation() { }

        public ReLocateNoteOperation(NoteBase note, Position before, Position after)
        {
            Invoke = () =>
            {
                note.ReLocate(after.Lane, after.Timing);
            };

            Undo = () =>
            {
                note.ReLocate(before.Lane, before.Timing);
            };
        }
    }

    public class ReSizeNoteOperation : Operation
    {
        private ReSizeNoteOperation() { }

        public ReSizeNoteOperation(NoteBase note, NoteSize before, NoteSize after)
        {
            Invoke = () =>
            {
                note.ReSize(after);
            };

            Undo = () =>
            {
                note.ReSize(before);
            };
        }
    }

    public class AddHoldOperation : Operation
    {
        private AddHoldOperation() { }

        public AddHoldOperation(NoteBook book, Hold hold)
        {
            Invoke = () =>
            {
                book.PutHold(hold);
            };

            Undo = () =>
            {
                book.UnPutHold(hold);
            };
        }
    }

    public class DeleteHoldOperation : Operation
    {
        private DeleteHoldOperation() { }

        public DeleteHoldOperation(NoteBook book, Hold hold)
        {
            Invoke += () =>
            {
                book.UnPutHold(hold);
            };

            Undo += () =>
            {
                book.PutHold(hold);
            };
        }
    }

    public class AddHoldRelayOperation : Operation
    {
        private AddHoldRelayOperation() { }

        public AddHoldRelayOperation(Hold hold, HoldRelay relay)
        {
            Invoke += () =>
            {
                hold.Put(relay);
            };

            Undo += () =>
            {
                hold.UnPut(relay);
            };
        }
    }

    public class DeleteHoldStepOperation : Operation
    {
        private DeleteHoldStepOperation() { }

        public DeleteHoldStepOperation(Hold hold, HoldStepNote relay)
        {
            Invoke += () =>
            {
                hold.UnPut(relay);
            };

            Undo += () =>
            {
                hold.Put(relay);
            };
        }
    }
}
