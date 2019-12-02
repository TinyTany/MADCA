using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.MaterialBox
{
    public sealed class MaterialBook
    {
        private readonly List<NoteBox> notes;
        private readonly List<ScoreBox> scores;

        public MaterialBook()
        {
            notes = new List<NoteBox>();
            scores = new List<ScoreBox>();
        }
    }
}
