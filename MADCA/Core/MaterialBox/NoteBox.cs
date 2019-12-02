using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Note.Abstract;
using System.Drawing;

namespace MADCA.Core.MaterialBox
{
    /// <summary>
    /// ノーツをGUIで操作・表示するためのクラス
    /// </summary>
    public sealed class NoteBox : RectangleBox<ShortNote>
    {
        private NoteBox() { }

        public NoteBox(ShortNote note, RectangleF region)
        {
            Note = note;
            Region = region;
        }
    }
}
