using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Interface;

namespace MADCA.Core.Note.Concrete
{
    public sealed class SlideL : Abstract.Note
    {
        public override NoteType NoteType => NoteType.SlideL;
    }
}
