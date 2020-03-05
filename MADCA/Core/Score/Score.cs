using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Score
{
    public sealed class Score
    {
        public event Action<Score> Changed;
        public uint BeatNum { get; private set; }
        public uint BeatDen { get; private set; }

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

        public void OnChanged() => Changed?.Invoke(this);
    }
}
