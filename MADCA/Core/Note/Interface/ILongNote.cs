using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Note.Interface
{
    public enum LongNoteType
    {
        Unknown,
        Hold
    }
    public interface ILongNote
    {
        bool Put(in IStepNote note);
        bool UnPut(in IStepNote note);
    }
}
