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
    public interface ILongNote<TStep> 
        where TStep : IStepNote<TStep>
    {
        bool Put(TStep note);
        bool UnPut(TStep note);
    }
}
