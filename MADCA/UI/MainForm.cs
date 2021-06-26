using MADCA.Core.Data;
using MADCA.Core.Graphics;
using MADCA.Core.Note;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.Core.Operation;
using MADCA.Core.Score;
using MADCA.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MADCA.UI
{
    public partial class MainForm : Form
    {
        private readonly string appName = "MADCA";
        public MainForm()
        {
            InitializeComponent();
            Text = appName;
            var pbox = new PictureBox() { Location = new Point(), Size = ClientSize };
            var laneEnv = new EditorLaneEnvironment(new Size(pbox.Width / 2, pbox.Height));
            var previewEnv = new PreviewDisplayEnvironment(new Point(pbox.Width / 2, 0), new Size(pbox.Width / 2, pbox.Height));
            var scoreBook = new ScoreBook();
            for (var i = 0; i < 100; ++i) { scoreBook.AddScoreLast(new Score(4, 4)); }
            pbox.Paint += (s, e) => 
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                LaneDrawer.Draw(e.Graphics, laneEnv, scoreBook.Scores);
                PreviewDrawer.Draw(e.Graphics, previewEnv, scoreBook.Scores);
            };
            {
                {
                }
                else
                {
                }
            };
            {
                {
                }
            };
            {
                {
                    {
                    }
                }
            };
            {
            };
            {
            };
        }

        private static void SetEvent(PictureBox box, IReadOnlyEditorLaneEnvironment laneEnv, IReadOnlyPreviewDisplayEnvironment previewEnv, IReadOnlyList<IReadOnlyScore> scores)
        {
            var note = new Touch(new LanePotision(0), new TimingPosition(1, 0), new NoteSize(8));
            box.MouseMove += (s, e) =>
            {
                var area = laneEnv.GetEditorLaneRegion(e.Location);
                if (area == EditorLaneRegion.Lane)
                {
                    PositionConverter.ConvertRealToVirtual(laneEnv, e.Location, 32, scores, out Position position);
                    note.ReLocate(position.Lane, position.Timing);
                    box.Refresh();
                }
            };
            box.Paint += (s, e) =>
            {
                NoteDrawer.DrawToLane(e.Graphics, laneEnv, note);
                NoteDrawer.DrawToPreview(e.Graphics, previewEnv, note);
            };
        }
    }
}
