using System.Windows.Forms;
using System.Drawing;
using MADCA.Core.Data;

namespace MADCA.UI
{
    public class PictureBoxHandler
    {
        private readonly PictureBox pictureBox;
        private readonly EditorLaneEnvironment laneEnvironment;
        private readonly PreviewDisplayEnvironment previewEnvironment;

        public Size Size
        {
            set
            {
                pictureBox.Size = value;
                pictureBox.Refresh();
            }
        }

        private PictureBoxHandler() { }

        public PictureBoxHandler(PictureBox box)
        {
            pictureBox = box;
            
        }
    }
}
