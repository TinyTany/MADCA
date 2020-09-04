using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Operation
{
    public class OperationManager
    {
        private readonly Stack<Operation> stackUndo, stackRedo;

        public OperationManager()
        {
            stackUndo = new Stack<Operation>();
            stackRedo = new Stack<Operation>();
        }

        public void AddOperation(Operation op)
        {
            stackUndo.Push(op);
            stackRedo.Clear();
        }

        public void AddAndInvokeOperation(Operation op)
        {
            AddOperation(op);
            op.Invoke();
        }

        public void Undo()
        {
            if (stackUndo.Any())
            {
                var op = stackUndo.Pop();
                op.Undo();
                stackRedo.Push(op);
            }
        }

        public void Redo()
        {
            if (stackRedo.Any())
            {
                var op = stackRedo.Pop();
                op.Invoke();
                stackUndo.Push(op);
            }
        }
    }
}
