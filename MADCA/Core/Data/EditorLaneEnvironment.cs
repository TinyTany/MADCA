using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using MADCA.Utility;

namespace MADCA.Core.Data
{
    public static class Environment
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
        /// レーン数（不変）
        /// </summary>
        uint LaneCount { get; }
        /// <summary>
        /// 何レーン分をひとまとまりとみなすかの値
        /// </summary>
        uint LaneGroupCount { get; }
        uint SideMargin { get; }
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
        /// このパネル全体のサイズ
        /// </summary>
        Size PanelSize { get; }
        /// <summary>
        /// このパネルの描画オフセット（左上）
        /// </summary>
        Point PanelOffset { get; }
        /// <summary>
        /// レーンを表す矩形（LocationはPanelOffsetを加味した絶対座標）
        /// </summary>
        Rectangle LaneRect { get; }
        uint AvailableLaneWidth { get; }
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
        public uint LaneCount => Environment.LaneCount;

        public uint LaneGroupCount => 5;
        private readonly uint sideMarginMin = 100;
        public uint SideMargin
        {
            get
            {
                var margin = (PanelSize.Width - LaneUnitWidth * LaneCount) / 2;
                if (margin <= sideMarginMin) { return sideMarginMin; }
                return (uint)margin;
            }
        }
        public uint BottomMargin { get; } = 30;
        
        public uint LaneUnitWidth { get; set; } = 10;
        
        public uint TimingUnitHeight { get; set; } = 384;
        
        public Size PanelSize { get; set; }
        public Point PanelOffset { get; set; }
        public Rectangle LaneRect
        {
            get
            {
                int x = (int)SideMargin;
                return new Rectangle(x + PanelOffset.X, PanelOffset.Y, (int)AvailableLaneWidth, (int)AvailableLaneHeight);
            }
        }

        public uint AvailableLaneWidth
        {
            get
            {
                var width = PanelSize.Width - sideMarginMin * 2;
                if (width <= 0) { return 0; }
                if (width >= LaneUnitWidth * LaneCount) { return LaneUnitWidth * LaneCount; }
                return (uint)width;
            }
        }
        public uint AvailableLaneHeight
        {
            get
            {
                var height = PanelSize.Height - BottomMargin;
                if (height <= 0) { return 0; }
                return (uint)height;
            }
        }

        public int OffsetX
        {
            get 
            {
                return MyMath.PositiveMod(OffsetXRaw, (int)(LaneCount * LaneUnitWidth));
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
            PanelSize = size;
            OffsetXRaw = _offsetY = 0;
        }

        public EditorLaneEnvironment(Point p, Size s)
            : this(s)
        {
            PanelOffset = p;
        }

        public EditorLaneRegion GetEditorLaneRegion(Point p)
        {
            if (!(PanelOffset.X <= p.X && p.X <= PanelOffset.X + PanelSize.Width && PanelOffset.Y <= p.Y && p.Y <= PanelOffset.Y + PanelSize.Height))
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
