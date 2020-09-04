using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MADCA.Core.Data
{
    public interface IReadOnlyPreviewDisplayEnvironment
    {
        /// <summary>
        /// 全体の大枠のサイズ
        /// </summary>
        Size DisplaySize { get; }
        Point DisplayOffset { get; }
        int Margin { get; }
        /// <summary>
        /// 譜面レーン領域の円に外接する矩形（正方形）
        /// </summary>
        Rectangle Circle { get; }
        Point CenterPoint { get; }
        int Radius { get; }
        /// <summary>
        /// 一番手前のTiming位置
        /// </summary>
        TimingPosition TimingOffset { get; }
        /// <summary>
        /// 円に対するTimingの長さ
        /// </summary>
        TimingPosition TimingLength { get; }
    }

    public sealed class PreviewDisplayEnvironment : IReadOnlyPreviewDisplayEnvironment
    {
        public Size DisplaySize { get; set; }

        public Point DisplayOffset { get; set; }

        public int Margin { get; } = 30;

        public Rectangle Circle
        {
            get
            {
                var rect = new Rectangle();
                int min;
                if (DisplaySize.Width < DisplaySize.Height)
                {
                    rect.X = Margin;
                    rect.Y = (DisplaySize.Height - DisplaySize.Width) / 2 + Margin;
                    min = DisplaySize.Width;
                }
                else
                {
                    rect.X = (DisplaySize.Width - DisplaySize.Height) / 2 + Margin;
                    rect.Y = Margin;
                    min = DisplaySize.Height;
                }
                rect.X += DisplayOffset.X;
                rect.Y += DisplayOffset.Y;
                var r = min / 2 - Margin;
                if (r < 0) { r = 0; }
                rect.Width = rect.Height = 2 * r;
                return rect;
            }
        }

        public Point CenterPoint => new Point(Circle.X + Radius, Circle.Y + Radius);

        public int Radius
        {
            get
            {
                return Circle.Width / 2;
            }
        }

        public TimingPosition TimingOffset { get; set; }
        public TimingPosition TimingLength { get; set; }

        private PreviewDisplayEnvironment() { }

        public PreviewDisplayEnvironment(Size size)
        {
            DisplaySize = size;
            // NOTE: 決め打ち
            TimingOffset = new TimingPosition(1, 0);
            TimingLength = new TimingPosition(1, 2);
        }

        public PreviewDisplayEnvironment(Point p, Size s)
            : this(s)
        {
            DisplayOffset = p;
        }
    }
}
