using MADCA.Core.IO;
using MADCA.Utility;
using System.Collections.Generic;
using JsonObject = System.Collections.Generic.Dictionary<string, dynamic>;

namespace MADCA.Core.Data
{
    public class LanePotision : IExchangeable
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

        public JsonObject Exchange()
        {
            return new JsonObject()
            {
                ["RawLane"] = RawLane
            };
        }

        public void Exchange(JsonObject json)
        {
            RawLane = int.Parse(json["RawLane"]);
        }
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
