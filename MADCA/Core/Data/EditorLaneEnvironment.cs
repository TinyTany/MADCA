using System.Drawing;
using MADCA.Utility;

namespace MADCA.Core.Data
{
    public static class MadcaEnv
    {
        /// <summary>
        /// レーン数（不変）
        /// </summary>
        public static uint LaneCount => 60;
    }

    public static class NoteEnvironment
    {
        public static int NoteHeight => 5; // 決め打ち
        
        public static int NoteWidth(int size, IReadOnlyEditorLaneEnvironment env)
        {
            return (int)env.LaneUnitWidth * size;
        }
    }

    public interface IReadOnlyEditorLaneEnvironment
    {
        /// <summary>
        /// 何レーン分をひとまとまりとみなすかの値
        /// </summary>
        uint LaneGroupCount { get; }
        /// <summary>
        /// 左右の余白
        /// </summary>
        uint SideMargin { get; }
        /// <summary>
        /// 下の余白
        /// </summary>
        uint BottomMargin { get; }
        /// <summary>
        /// Lane幅1に対応する画面上でのピクセル数
        /// </summary>
        uint LaneUnitWidth { get; }
        /// <summary>
        /// Timing幅1に対応する画面上でのピクセル数
        /// </summary>
        uint TimingUnitHeight { get; }
        /// <summary>
        /// このパネルの描画領域
        /// </summary>
        Rectangle PanelRegion { get; }
        /// <summary>
        /// レーンを表す矩形（Locationは絶対座標）
        /// </summary>
        Rectangle LaneRect { get; }
        /// <summary>
        /// 現在の編集可能レーン領域の幅
        /// </summary>
        uint AvailableLaneWidth { get; }
        /// <summary>
        /// 現在の編集可能レーン領域の高さ
        /// </summary>
        uint AvailableLaneHeight { get; }
        /// <summary>
        /// レーンの左下に対応するX軸方向のオフセット（正規化された値、絶対実座標、左下原点）
        /// </summary>
        int OffsetX { get; }
        /// <summary>
        /// レーンの左下に対応するX軸方向のオフセット（絶対実座標、左下原点）
        /// </summary>
        int OffsetXRaw { get; }
        /// <summary>
        /// レーンの左下に対応するY軸方向のオフセット（絶対実座標、左下原点）
        /// </summary>
        int OffsetY { get; }
        EditorLaneRegion GetEditorLaneRegion(Point p);
    }

    public sealed class EditorLaneEnvironment : IReadOnlyEditorLaneEnvironment
    {
        public uint LaneGroupCount => 5;
        private readonly uint sideMarginMin = 100;
        public uint SideMargin
        {
            get
            {
                var margin = (PanelRegion.Width - LaneUnitWidth * MadcaEnv.LaneCount) / 2;
                if (margin <= sideMarginMin) { return sideMarginMin; }
                return margin.ToUInt();
            }
        }
        public uint BottomMargin { get; } = 30;
        
        public uint LaneUnitWidth { get; set; } = 10;
        
        public uint TimingUnitHeight { get; set; } = 384;
        
        public Rectangle PanelRegion { get; set; }
        public Rectangle LaneRect
        {
            get
            {
                int x = (int)SideMargin;
                return new Rectangle(x + PanelRegion.X, PanelRegion.Y, (int)AvailableLaneWidth, (int)AvailableLaneHeight);
            }
        }

        public uint AvailableLaneWidth
        {
            get
            {
                var width = PanelRegion.Width - sideMarginMin * 2;
                if (width <= 0) { return 0; }
                if (width >= LaneUnitWidth * MadcaEnv.LaneCount) { return LaneUnitWidth * MadcaEnv.LaneCount; }
                return width.ToUInt();
            }
        }
        public uint AvailableLaneHeight
        {
            get
            {
                var height = PanelRegion.Height - BottomMargin;
                if (height <= 0) { return 0; }
                return height.ToUInt();
            }
        }

        public int OffsetX
        {
            get 
            {
                return MyMath.PositiveMod(OffsetXRaw, (int)(MadcaEnv.LaneCount * LaneUnitWidth));
            }
        }

        public int OffsetXRaw { get; set; }
        private int _offsetY;
        public int OffsetY 
        { 
            get { return _offsetY; }
            set 
            {
                _offsetY = value;
                if (_offsetY < 0) { _offsetY = 0; }
            }
        }

        private EditorLaneEnvironment() { }
        
        public EditorLaneEnvironment(Size size)
        {
            PanelRegion = new Rectangle(new Point(), size);
            OffsetXRaw = _offsetY = 0;
        }

        public EditorLaneEnvironment(Point p, Size s)
        {
            PanelRegion = new Rectangle(p, s);
            OffsetXRaw = _offsetY = 0;
        }

        public EditorLaneRegion GetEditorLaneRegion(Point p)
        {
            if (!PanelRegion.Contains(p))
            {
                return EditorLaneRegion.Unknown;
            }
            if (p.X < LaneRect.Left) { return EditorLaneRegion.LeftSide; }
            if (LaneRect.Right < p.X) { return EditorLaneRegion.RightSide; }
            if (LaneRect.Contains(p)) { return EditorLaneRegion.Lane; }
            return EditorLaneRegion.LaneBottom;
        }
    }

    public enum EditorLaneRegion
    {
        Unknown,
        LeftSide,
        RightSide,
        Lane,
        LaneBottom
    }
}
