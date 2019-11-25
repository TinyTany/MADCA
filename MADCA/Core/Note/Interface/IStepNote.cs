using MADCA.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Note.Interface
{
    public interface IStepNote<T> : INote
    {
        event Func<T, LanePotision, TimingPosition, bool> PositionChanging;
    }
}
