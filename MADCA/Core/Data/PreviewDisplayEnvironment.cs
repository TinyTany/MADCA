﻿using System;
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
        /// このディスプレイを表す矩形
        /// </summary>
        Rectangle DisplayRegion { get; }
        /// <summary>
        /// 上下左右の余白
        /// </summary>
        int Margin { get; }
        /// <summary>
        /// 譜面レーン領域の円に外接する矩形（正方形）
        /// </summary>
        Rectangle Circle { get; }
        /// <summary>
        /// 譜面レーン領域の円の中心座標
        /// </summary>
        Point CenterPoint { get; }
        /// <summary>
        /// 譜面レーン領域の円の半径
        /// </summary>
        int Radius { get; }
        /// <summary>
        /// 一番手前のTiming位置
        /// </summary>
        TimingPosition TimingOffset { get; }
        /// <summary>
        /// 円の半径に対するTimingの長さ
        /// </summary>
        TimingPosition TimingLength { get; }
    }

    public sealed class PreviewDisplayEnvironment : IReadOnlyPreviewDisplayEnvironment
    {
        public Rectangle DisplayRegion { get; set; }

        public int Margin { get; } = 30;

        public Rectangle Circle
        {
            get
            {
                var rect = new Rectangle();
                int min;
                if (DisplayRegion.Width < DisplayRegion.Height)
                {
                    rect.X = Margin;
                    rect.Y = (DisplayRegion.Height - DisplayRegion.Width) / 2 + Margin;
                    min = DisplayRegion.Width;
                }
                else
                {
                    rect.X = (DisplayRegion.Width - DisplayRegion.Height) / 2 + Margin;
                    rect.Y = Margin;
                    min = DisplayRegion.Height;
                }
                rect.X += DisplayRegion.X;
                rect.Y += DisplayRegion.Y;
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

        public PreviewDisplayEnvironment(Point p, Size s)
        {
            DisplayRegion = new Rectangle(p, s);
            // NOTE: 決め打ち
            TimingOffset = new TimingPosition(1, 0);
            TimingLength = new TimingPosition(1, 2);
        }
    }
}
