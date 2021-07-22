using MADCA.Core.Data;
using MADCA.Core.IO;
using System.Collections.Generic;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.Score
{
    public interface IReadOnlyScore
    {
        uint BeatNum { get; }
        uint BeatDen { get; }
        TimingPosition TimingBegin { get; }
        TimingPosition TimingEnd { get; }
    }

    public sealed class Score : IReadOnlyScore, IExchangeable
    {
        public uint BeatNum { get; set; }
        public uint BeatDen { get; set; }

        // NOTE: ScoreのTimingの範囲は、[TimingBegin, TimingEnd)
        public TimingPosition TimingBegin { get; set; }
        public TimingPosition TimingEnd => TimingBegin + new TimingPosition(BeatDen, (int)BeatNum);

        private Score() { }

        public Score(uint beatNum, uint beatDen)
        {
            BeatNum = beatNum;
            BeatDen = beatDen;
            if (BeatNum == 0 || BeatDen == 0)
            {
                // NOTE: 4/4拍子をデフォルト値として決め打ち
                BeatNum = BeatDen = 4;
            }
        }

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["BeatNum"] = BeatNum,
                ["BeatDen"] = BeatDen,
                ["TimingBegin"] = TimingBegin.Exchange()
            };
        }

        public void Exchange(JsonObject json)
        {
            BeatNum = uint.Parse(json["BeatNum"]);
            BeatDen = uint.Parse(json["BeatDen"]);
            TimingBegin = new TimingPosition(1, 0);
            TimingBegin.Exchange(json["TimingBegin"]);
        }
    }
}
