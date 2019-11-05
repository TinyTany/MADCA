using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Data
{
    public class LanePotision
    {
        public int Lane { get; private set; }

        public int NormalizedLane
        {
            get
            {
                if (Lane >= 0)
                {
                    return Lane % 60;
                }
                return 60 - (-Lane % 60);
            }
        }

        public LanePotision (int lane)
        {
            Lane = lane;
        }
    }
}
