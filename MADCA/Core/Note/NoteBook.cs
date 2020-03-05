﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;

namespace MADCA.Core.Note
{
    public sealed class NoteBook
    {
        private readonly List<ShortNote> notes;
        private readonly List<Hold> holds;

        // TODO: 外部からノーツのコレクションを取れるようにすることが必要（のはず）

        public NoteBook()
        {
            notes = new List<ShortNote>();
            holds = new List<Hold>();
        }

        public bool PutShortNote(ShortNote note)
        {
            if (notes.Contains(note)) { return false; }
            notes.Add(note);
            return true;
        }

        public bool UnPutShortNote(ShortNote note)
        {
            return notes.Remove(note);
        }

        public bool PutHold(Hold hold)
        {
            if (holds.Contains(hold)) { return false; }
            holds.Add(hold);
            return true;
        }

        public bool UnPutHold(Hold hold)
        {
            return holds.Remove(hold);
        }

        public void ClearAllNotes()
        {
            notes.Clear();
            holds.Clear();
        }
    }
}