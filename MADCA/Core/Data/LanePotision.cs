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
}
