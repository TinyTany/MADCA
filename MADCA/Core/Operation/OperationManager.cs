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

        public event Action<bool> UndoStatusChanged, RedoStatusChanged;
        private bool undoStatus, redoStatus;

        public OperationManager()
        {
            stackUndo = new Stack<Operation>();
            stackRedo = new Stack<Operation>();
            undoStatus = redoStatus = false;
        }

        private void AddOperation(Operation op)
        {
            stackUndo.Push(op);
            stackRedo.Clear();
            if (!undoStatus)
            {
                undoStatus = true;
                UndoStatusChanged?.Invoke(undoStatus);
            }
            if (redoStatus)
            {
                redoStatus = false;
                RedoStatusChanged?.Invoke(redoStatus);
            }
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
                if (undoStatus && !stackUndo.Any())
                {
                    undoStatus = false;
                    UndoStatusChanged?.Invoke(undoStatus);
                }
                if (!redoStatus)
                {
                    redoStatus = true;
                    RedoStatusChanged?.Invoke(redoStatus);
                }
            }
        }

        public void Redo()
        {
            if (stackRedo.Any())
            {
                var op = stackRedo.Pop();
                op.Invoke();
                stackUndo.Push(op);
                if (!undoStatus)
                {
                    undoStatus = true;
                    UndoStatusChanged?.Invoke(undoStatus);
                }
                if (redoStatus && !stackRedo.Any())
                {
                    redoStatus = false;
                    RedoStatusChanged?.Invoke(redoStatus);
                }
            }
        }
    }
}
