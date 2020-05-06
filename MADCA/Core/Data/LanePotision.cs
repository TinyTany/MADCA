using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Data
{
    public class LanePotision
    {
        public int RawLane { get; private set; }

        public int NormalizedLane
        {
            get
            {
                // NOTE: 本家の仕様としてレーン数は60個
                // HACK: ハードコーディングしてるけど大丈夫かな
                if (RawLane >= 0)
                {
                    return RawLane % 60;
                }
                return 60 - (-RawLane % 60);
            }
        }

        public LanePotision (int lane)
        {
            RawLane = lane;
        }

        public LanePotision (LanePotision other)
            : this(other.RawLane) { }
    }

    public class Position
    {
        public LanePotision Lane { get; private set; }
        public TimingPosition Timing { get; private set; }
        private Position() { }
        public Position(LanePotision lane, TimingPosition timing)
        {
            Lane = lane;
            Timing = timing;
        }
        public Position(Position p)
        {
            Lane = new LanePotision(p.Lane);
            Timing = new TimingPosition(p.Timing);
        }
    }
}
