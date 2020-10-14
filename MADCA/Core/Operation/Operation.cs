using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;
using MADCA.Core.Note;

namespace MADCA.Core.Operation
{
    public abstract class Operation
    {
        public Action Invoke, Undo;
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
}
