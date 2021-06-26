using MADCA.Utility;

namespace MADCA.Core.Data
{
    public class LanePotision
    {
        public int RawLane { get; private set; }

        public int NormalizedLane
        {
            get
            {
                return MyMath.PositiveMod(RawLane, (int)MadcaEnv.LaneCount);
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
