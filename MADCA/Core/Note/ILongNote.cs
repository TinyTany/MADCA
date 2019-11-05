using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Note
{
    public interface ILongNote
    {
        List<IStepNote> Notes { get; }

        bool Put(IStepNote note);

        bool UnPut(IStepNote note);
    }
}
