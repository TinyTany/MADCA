using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;

namespace MADCA.Core.Score
{
    public interface IReadOnlyScore
    {
        uint BeatNum { get; }
        uint BeatDen { get; }
        TimingPosition TimingBegin { get; }
        TimingPosition TimingEnd { get; }
    }

    public sealed class Score : IReadOnlyScore
    {
        public uint BeatNum { get; set; }
        public uint BeatDen { get; set; }

        // NOTE: ScoreのTimingの範囲は、[TimingBegin, TimingEnd)
        public TimingPosition TimingBegin { get; set; }
        public TimingPosition TimingEnd => TimingBegin + new TimingPosition(BeatDen, (int)BeatNum);

        private Score() { }

        public Score(uint num, uint den)
        {
            BeatNum = num;
            BeatDen = den;
            if (BeatNum == 0 || BeatDen == 0)
            {
                // NOTE: 4/4拍子をデフォルト値として決め打ち
                BeatNum = BeatDen = 4;
            }
        }
    }
}
