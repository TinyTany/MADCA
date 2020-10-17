using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;
using MADCA.Core.Note.Concrete;
using MADCA.Core.Note.Interface;
using MADCA.UI;

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

        /// <summary>
        /// nullを返すことがあります
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="timing"></param>
        /// <param name="size"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ShortNote Factory(LanePotision lane, TimingPosition timing, NoteSize size, NoteMode mode)
        {
            switch (mode)
            {
                case NoteMode.Touch:
                    return new Touch(lane, timing, size);
                case NoteMode.Chain:
                    return new Chain(lane, timing, size);
                case NoteMode.SlideL:
                    return new SlideL(lane, timing, size);
                case NoteMode.SlideR:
                    return new SlideR(lane, timing, size);
                case NoteMode.SnapU:
                    return new SnapU(lane, timing, size);
                case NoteMode.SnapD:
                    return new SnapD(lane, timing, size);
                default:
                    Debug.Assert(false);
                    return null;
            }
        }
    }
}
