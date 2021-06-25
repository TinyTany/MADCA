using MADCA.Core.Data;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Utility
{
    static class MyUtil
    {
        /// <summary>
        /// nullを返すことがあります
        /// </summary>
        /// <param name="lane"></param>
        /// <param name="timing"></param>
        /// <param name="size"></param>
        /// <param name="mode"></param>
        /// <returns></returns>
        public static ShortNote NoteFactory(LanePotision lane, TimingPosition timing, NoteSize size, NoteMode mode)
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
                    //Debug.Assert(false);
                    return null;
            }
        }
    }
}
