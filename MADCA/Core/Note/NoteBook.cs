using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using MADCA.Core.IO;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.Core.Operation;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.Note
{
    public sealed class NoteBook : IExchangeable
    {
        private List<ShortNote> notes;
        private List<Hold> holds;

        // NOTE: 一応外部からノーツたちを見られるようにする（が，リストへの要素追加削除はされたくないという気持ち）
        public IReadOnlyList<ShortNote> Notes => notes;
        public IReadOnlyList<Hold> Holds => holds;

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

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["Notes"] = notes.Select(n => n.Exchange()).ToList(),
                ["Holds"] = holds.Select(h => h.Exchange()).ToList()
            };
        }

        public void Exchange(JsonObject json)
        {
            notes.Clear();
            holds.Clear();
            foreach(var n in json["Notes"])
            {
                var tmp = ShortNote.Factory(n);
                notes.Add(tmp);
            }
            foreach (var h in json["Holds"])
            {
                var tmp = new Hold(h);
                holds.Add(tmp);
            }
        }
    }
}
