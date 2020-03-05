using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Data
{
    public sealed class EditorLaneEnvironment
    {
        public static readonly int LaneCount = 60;
        public float LeftMargin { get; private set; }
        public float RightMargin { get; private set; }
        public float LaneUnitWidth { get; private set; } = 10f;
        public float TimingUnitHeight { get; private set; }
    }
}
