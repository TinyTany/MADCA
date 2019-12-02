using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Operation
{
    public interface IOperation
    {
        void Invoke();
        void Undo();
    }
}
