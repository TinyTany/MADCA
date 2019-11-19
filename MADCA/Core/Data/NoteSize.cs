using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Data
{
    public class NoteSize : IEquatable<NoteSize>
    {
        // NOTE: 取りうるノーツサイズ
        private const int min = 1, max = 60;

        private int _size;
        public int Size
        { 
            get
            {
                return _size;
            }
            private set
            {
                _size = value;
                if (_size < min) { _size = min; }
                if (_size > max) { _size = max; }
            } 
        }

        public NoteSize(int size)
        {
            Size = size;
        }

        public NoteSize(NoteSize other)
            : this(other.Size) { }

        #region IEquatable実装と演算子オーバーロード
        public override bool Equals(object obj)
        {
            return obj is NoteSize size && Equals(size);
        }

        public bool Equals(NoteSize other)
        {
            return Size == other.Size;
        }

        public override int GetHashCode()
        {
            return -1628373540 + Size.GetHashCode();
        }

        public static bool operator ==(NoteSize left, NoteSize right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(NoteSize left, NoteSize right)
        {
            return !(left == right);
        }
        #endregion
    }
}
