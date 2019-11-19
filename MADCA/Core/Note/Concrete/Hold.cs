using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Note.Interface;

namespace MADCA.Core.Note.Concrete
{
    public sealed class Hold : ILongNote
    {
        public bool Put(in IStepNote note)
        {
            throw new NotImplementedException();
        }

        public bool UnPut(in IStepNote note)
        {
            throw new NotImplementedException();
        }
    }
}
