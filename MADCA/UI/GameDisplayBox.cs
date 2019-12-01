using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADCA.UI
{
    public sealed class GameDisplayBox
    {
        public event Action<PaintEventArgs> Paint;
        private readonly PictureBox pictureBox;

        private GameDisplayBox() { }

        public GameDisplayBox(PictureBox box)
        {
            pictureBox = box;
            pictureBox.Paint += (s, e) => Paint?.Invoke(e);
        }

        public void Refresh() => pictureBox.Refresh();
    }
}
