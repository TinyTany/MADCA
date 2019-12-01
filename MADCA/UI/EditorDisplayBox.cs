using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MADCA.UI
{
    public sealed class EditorDisplayBox
    {
        public event Action<PaintEventArgs> Paint;
        public event Action<MouseEventArgs> MouseDown, MouseUp, MouseMove, MouseDrag;
        private bool clicking = false;
        private readonly PictureBox pictureBox;

        private EditorDisplayBox() { }

        public EditorDisplayBox(PictureBox box)
        {
            pictureBox = box;
            pictureBox.Paint += (s, e) => Paint?.Invoke(e);
            pictureBox.MouseDown += (s, e) =>
            {
                clicking = true;
                MouseDown?.Invoke(e);
            };
            pictureBox.MouseMove += (s, e) =>
            {
                if (clicking)
                {
                    MouseDrag?.Invoke(e);
                }
                else
                {
                    MouseMove?.Invoke(e);
                }
            };
            pictureBox.MouseUp += (s, e) =>
            {
                clicking = false;
                MouseUp?.Invoke(e);
            };
        }

        public void Refresh() => pictureBox.Refresh();
    }
}
