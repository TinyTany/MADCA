﻿using MADCA.Core.IO;
using System;
using System.Collections.Generic;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.Data
{
    public class NoteSize : IEquatable<NoteSize>, IExchangeable
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

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["Size"] = Size
            };
        }

        public void Exchange(JsonObject json)
        {
            Size = int.Parse(json["Size"]);
        }

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
