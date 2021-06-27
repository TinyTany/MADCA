using MADCA.Core.Data;
using MADCA.Utility;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MADCA.UI
{
    class MadcaDisplay
    {
        public PictureBox PictureBox { get; }
        private readonly EditorLaneEnvironment editorLaneEnvironment;
        public IReadOnlyEditorLaneEnvironment EditorLaneEnvironment => editorLaneEnvironment;
        private readonly PreviewDisplayEnvironment previewDisplayEnvironment;
        public IReadOnlyPreviewDisplayEnvironment PreviewDisplayEnvironment => previewDisplayEnvironment;
        public KeyTokenHolder KeyTokenHolder { get; } = new KeyTokenHolder();

        MadcaDisplay() { }

        public MadcaDisplay(
            PictureBox pBox,
            EditorLaneEnvironment laneEnv,
            PreviewDisplayEnvironment previewEnv
            )
        {
            PictureBox = pBox;
            editorLaneEnvironment = laneEnv;
            previewDisplayEnvironment = previewEnv;
            Initialize();
        }

        public MadcaDisplay(PictureBox pBox)
        {
            PictureBox = pBox;
            editorLaneEnvironment = new EditorLaneEnvironment(new Size(pBox.Width / 2, pBox.Height));
            previewDisplayEnvironment = new PreviewDisplayEnvironment(new Point(pBox.Width / 2, 0), new Size(pBox.Width / 2, pBox.Height));
            Initialize();
        }

        private void Initialize()
        {
            PictureBox.SizeChanged += (s, e) =>
            {
                var halfSize = new Size(PictureBox.Width / 2, PictureBox.Height);
                editorLaneEnvironment.PanelRegion = new Rectangle(new Point(), halfSize);
                previewDisplayEnvironment.DisplayRegion = new Rectangle(new Point(halfSize.Width, 0), halfSize);
            };

            PictureBox.MouseWheel += (s, e) =>
            {
                // NOTE: WHEEL_DELTAは120
                if (EditorLaneEnvironment.GetEditorLaneRegion(e.Location) == EditorLaneRegion.Lane)
                {
                    editorLaneEnvironment.OffsetXRaw -= e.Delta / 120 * (int)editorLaneEnvironment.LaneUnitWidth;
                    return;
                }
                if (EditorLaneEnvironment.PanelRegion.Contains(e.Location))
                {
                    if (Control.ModifierKeys.HasFlag(Keys.Shift))
                    {
                        var prevHeight = editorLaneEnvironment.TimingUnitHeight;
                        editorLaneEnvironment.TimingUnitHeight -= e.Delta / 10;
                        return;
                    }
                    editorLaneEnvironment.OffsetY += e.Delta;
                    previewDisplayEnvironment.TimingOffset = new TimingPosition(editorLaneEnvironment.TimingUnitHeight.ToUInt(), editorLaneEnvironment.OffsetY);
                    return;
                }
                if (PreviewDisplayEnvironment.DisplayRegion.Contains(e.Location))
                {
                    var den = (e.Delta / 10).ToUInt();
                    if (den != 0)
                    {
                        previewDisplayEnvironment.TimingLength -= new TimingPosition((e.Delta / 10).ToUInt(), e.Delta < 0 ? -1 : 1);
                    }
                }
            };

            SetEventToDragLane();
        }

        private void SetEventToDragLane()
        {
            Point? offset = null;
            Point? p = null;
            var key = new KeyToken();
            PictureBox.MouseDown += (s, e) =>
            {
                if (KeyTokenHolder.Locked) { return; }
                if (e.Button == MouseButtons.Middle && EditorLaneEnvironment.GetEditorLaneRegion(e.Location) == EditorLaneRegion.Lane)
                {
                    KeyTokenHolder.Lock(key);
                    offset = new Point(EditorLaneEnvironment.OffsetXRaw, EditorLaneEnvironment.OffsetY);
                    p = e.Location;
                    PictureBox.Cursor = Cursors.Cross;
                }
            };
            PictureBox.MouseMove += (s, e) =>
            {
                if (!KeyTokenHolder.CanUnLock(key)) { return; }
                if (offset != null && p != null)
                {
                    editorLaneEnvironment.OffsetXRaw = offset.Value.X - (e.X - p.Value.X);
                    // NOTE: ある程度マウスが移動したときのみ縦方向の更新を行う（これをやらないと描画が不安定になる）
                    var diffY = offset.Value.Y + (e.Y - p.Value.Y) - EditorLaneEnvironment.OffsetY;
                    if (Math.Abs(diffY) > 10)
                    {
                        editorLaneEnvironment.OffsetY += diffY;
                        previewDisplayEnvironment.TimingOffset = new TimingPosition(EditorLaneEnvironment.TimingUnitHeight.ToUInt(), EditorLaneEnvironment.OffsetY);
                    }
                }
            };
            PictureBox.MouseUp += (s, e) =>
            {
                if (!KeyTokenHolder.UnLock(key)) { return; }
                offset = p = null;
                PictureBox.Cursor = Cursors.Default;
            };
        }
    }

    public class KeyToken
    {
        public KeyToken() { }
    }

    public class KeyTokenHolder
    {
        public KeyToken KeyToken { get; private set; } = null;

        /// <summary>
        /// ロックされていない新しいKeyTokenHolderを作成します
        /// </summary>
        public KeyTokenHolder() { }

        /// <summary>
        /// このKeyTokenHolderがKeyTokenによってロックされているかを調べます
        /// </summary>
        public bool Locked => KeyToken != null;

        /// <summary>
        /// 指定したKeyTokenによって、このKeyTokenHolderをロックします
        /// </summary>
        /// <param name="key">ロックするためのKeyToken</param>
        /// <returns>ロックに成功したかどうか</returns>
        public bool Lock(KeyToken key)
        {
            if (Locked)
            {
                return false;
            }
            KeyToken = key;
            return true;
        }

        /// <summary>
        /// 指定したKeyTokenによって、このKeyTokenHolderのロックを解除できるかを調べます。
        /// 実際にロックを解除することは行いません。
        /// </summary>
        /// <param name="key">KeyTokenHolderのロックを解除できるかを確かめたいKeyToken</param>
        /// <returns>指定したKeyTokenによってロックを解除できるかどうか</returns>
        public bool CanUnLock(KeyToken key)
        {
            if (key == null || KeyToken == null)
            {
                return false;
            }
            return ReferenceEquals(key, KeyToken);
        }

        /// <summary>
        /// 指定したKeyTokenによって、このKeyTokenHolderのロックを解除します
        /// </summary>
        /// <param name="key">ロック解除のためのKeyToken</param>
        /// <returns>ロック解除に成功したかどうか</returns>
        public bool UnLock(KeyToken key)
        {
            if (!CanUnLock(key))
            {
                return false;
            }
            KeyToken = null;
            return true;
        }
    }
}
