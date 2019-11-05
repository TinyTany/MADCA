using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;

namespace MADCA.Core.Note
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
        NoteType NoteType { get; set; }
        LanePotision Lane { get; set; }
        TimingPosition Timing { get; set; }
        NoteSize NoteSize { get; set; }
    }
}
