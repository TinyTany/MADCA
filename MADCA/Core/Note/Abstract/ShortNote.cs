using System;
using System.Diagnostics;
using MADCA.Core.Data;
using MADCA.Core.Note.Concrete;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.Note.Abstract
{
    public abstract class ShortNote : NoteBase
    {
        protected ShortNote() { }

        protected ShortNote(LanePotision lane, TimingPosition timing, NoteSize size)
            : base(lane, timing, size) { }

        /// <summary>
        /// nullを返すことがあります
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="timing"></param>
        /// <param name="size"></param>
        /// <param name="noteType"></param>
        /// <returns></returns>
        public static ShortNote Factory(LanePotision lane, TimingPosition timing, NoteSize size, NoteType noteType)
        {
            switch (noteType)
            {
                case NoteType.Touch:
                    return new Touch(lane, timing, size);
                case NoteType.Chain:
                    return new Chain(lane, timing, size);
                case NoteType.SlideL:
                    return new SlideL(lane, timing, size);
                case NoteType.SlideR:
                    return new SlideR(lane, timing, size);
                case NoteType.SnapU:
                    return new SnapU(lane, timing, size);
                case NoteType.SnapD:
                    return new SnapD(lane, timing, size);
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        public static ShortNote Factory(JsonObject json)
        {
            var noteType = NoteType.Parse(typeof(NoteType), json["NoteType"]);
            switch (noteType)
            {
                case NoteType.Touch:
                    {
                        var note = new Touch(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(1));
                        note.Exchange(json);
                        return note;
                    }
                case NoteType.Chain:
                    {
                        var note = new Chain(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(1));
                        note.Exchange(json);
                        return note;
                    }
                case NoteType.SlideL:
                    {
                        var note = new SlideL(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(1));
                        note.Exchange(json);
                        return note;
                    }
                case NoteType.SlideR:
                    {
                        var note = new SlideR(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(1));
                        note.Exchange(json);
                        return note;
                    }
                case NoteType.SnapU:
                    {
                        var note = new SnapU(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(1));
                        note.Exchange(json);
                        return note;
                    }
                case NoteType.SnapD:
                    {
                        var note = new SnapD(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(1));
                        note.Exchange(json);
                        return note;
                    }
                default:
                    Debug.Assert(false);
                    return null;
            }
        }
    }
}
