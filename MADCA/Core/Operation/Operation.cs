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

        public AddShortNoteOperation(Note.Abstract.ShortNote shortNote) 
        {
            Invoke = () => { };
            Undo = () => { };
        }
    }
}
