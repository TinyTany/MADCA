using MADCA.Core.Data;
using MADCA.Core.Graphics;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.Core.Score;
using MADCA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var laneEnv = new Core.Data.EditorLaneEnvironment(new Size(pbox.Width / 2, pbox.Height));
            var previewEnv = new Core.Data.PreviewDisplayEnvironment(new Point(pbox.Width / 2, 0), new Size(pbox.Width / 2, pbox.Height));
            var scoreBook = new Core.Score.ScoreBook();
            for (var i = 0; i < 100; ++i) { scoreBook.AddScoreLast(new Core.Score.Score(4, 4)); }
            pbox.Paint += (s, e) => 
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
                Core.Graphics.LaneDrawer.Draw(e.Graphics, laneEnv, scoreBook.Scores);
                Core.Graphics.PreviewDrawer.Draw(e.Graphics, previewEnv, scoreBook.Scores);
            };
            pbox.MouseWheel += (s, e) =>
            {
                // NOTE: WHEEL_DELTAは120らしい...
                if (laneEnv.GetEditorLaneRegion(e.Location) == Core.Data.EditorLaneRegion.Lane)
                {
                    laneEnv.OffsetXRaw -= e.Delta / 120 * (int)laneEnv.LaneUnitWidth;
                }
                else
                {
                    laneEnv.OffsetY += e.Delta;
                    previewEnv.TimingOffset = new MADCA.Core.Data.TimingPosition(laneEnv.TimingUnitHeight, laneEnv.OffsetY);
                }
                pbox.Refresh();
            };
            Point? offset = null;
            Point? p = null;
            pbox.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Middle && laneEnv.GetEditorLaneRegion(e.Location) == Core.Data.EditorLaneRegion.Lane)
                {
                    offset = new Point(laneEnv.OffsetXRaw, laneEnv.OffsetY);
                    p = e.Location;
                    pbox.Cursor = Cursors.Cross;
                }
            };
            pbox.MouseMove += (s, e) =>
            {
                if (offset != null && p != null)
                {
                    laneEnv.OffsetXRaw = offset.Value.X - (e.X - p.Value.X);
                    // NOTE: ある程度マウスが移動したときのみ縦方向の更新を行う（これをやらないと描画が不安定になる）
                    var diffY = offset.Value.Y + (e.Y - p.Value.Y) - laneEnv.OffsetY;
                    if (Math.Abs(diffY) > 10)
                    {
                        laneEnv.OffsetY += diffY;
                        previewEnv.TimingOffset = new MADCA.Core.Data.TimingPosition(laneEnv.TimingUnitHeight, laneEnv.OffsetY);
                    }
                    pbox.Refresh();
                }
            };
            pbox.MouseUp += (s, e) =>
            {
                offset = p = null;
                pbox.Cursor = Cursors.Default;
            };
            SizeChanged += (s, e) =>
            {
                pbox.Size = ClientSize;
                var halfSize = new Size(ClientSize.Width / 2, ClientSize.Height);
                laneEnv.PanelSize = previewEnv.DisplaySize = halfSize;
                previewEnv.DisplayOffset = new Point(halfSize.Width, 0);
                pbox.Refresh();
            };
            Controls.Add(pbox);
            MinimumSize = new Size((int)laneEnv.SideMargin * 2, 10);

            SetEvent(pbox, laneEnv, previewEnv, scoreBook.Scores);
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
