using MADCA.Core.Note.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;

namespace MADCA.Core.Note.Abstract
{
    public abstract class HoldStepNote : Note//, IStepNote
    {
        public event Func<HoldStepNote, LanePotision, TimingPosition, bool> PositionChanging;
    }
}
