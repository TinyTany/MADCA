using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;

namespace MADCA.Core.Note.Interface
{
    public enum NoteType
    {
        Unknown,
        Touch,
        Chain, 
        SlideL, 
        SlideR, 
        SnapU, 
        SnapD, 
        HoldBegin,
        HoldRelay,
        HoldEnd
    }
    public interface INote
    {
        NoteType NoteType { get; }
        LanePotision Lane { get; }
        TimingPosition Timing { get; }
        NoteSize NoteSize { get; }
        bool ReSize(NoteSize size);
        bool ReLocate(LanePotision lane, TimingPosition timing);
    }
}
