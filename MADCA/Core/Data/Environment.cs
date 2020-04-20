using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MADCA.Core.Data
{
    public interface IReadOnlyEditorLaneEnvironment
    {
        uint LaneCount { get; }
        uint SideMargin { get; }
        uint LaneUnitWidth { get; }
        uint TimingUnitHeight { get; }
        Size EditorLaneSize { get; }
        uint AvailableLaneWidth { get; }
    }

    public sealed class EditorLaneEnvironment : IReadOnlyEditorLaneEnvironment
    {
        /// <summary>
        /// レーン数（不変）
        /// </summary>
        public uint LaneCount => 60;
        private readonly uint sideMarginMin = 100;
        public uint SideMargin
        {
            get
            {
                var margin = (EditorLaneSize.Width - LaneUnitWidth * LaneCount) / 2;
                if (margin <= sideMarginMin) { return sideMarginMin; }
                return (uint)margin;
            }
        }
        /// <summary>
        /// Lane幅1に対応する画面上でのピクセル数
        /// </summary>
        public uint LaneUnitWidth { get; set; } = 10;
        /// <summary>
        /// Timing幅1に対応する画面上でのピクセル数
        /// </summary>
        public uint TimingUnitHeight { get; set; } = 192;
        public Size EditorLaneSize { get; set; }
        public uint AvailableLaneWidth
        {
            get
            {
                var width = EditorLaneSize.Width - sideMarginMin * 2;
                if (width <= 0) { return 0; }
                if (width >= LaneUnitWidth * LaneCount) { return LaneUnitWidth * LaneCount; }
                return (uint)width;
            }
        }
        
        public EditorLaneEnvironment(Size size)
        {
            EditorLaneSize = size;
        }
    }
}
