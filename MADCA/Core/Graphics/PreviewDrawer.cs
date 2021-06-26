using System.Collections.Generic;
using System.Linq;
using MADCA.Core.Data;
using System.Drawing;
using MADCA.Utility;

namespace MADCA.Core.Graphics
{
    public static class PreviewDrawer
    {
        public static void Draw(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, IReadOnlyList<Score.IReadOnlyScore> scores)
        {
            using(var backBrush = new SolidBrush(Color.Black))
            using(var gameDisplayBrush = new SolidBrush(Color.FromArgb(50, 50, 50)))
            using(var whitePen = new Pen(Color.White))
            using (Font myFont = new Font("MS UI Gothic", 10, FontStyle.Regular))
            {
                g.FillRectangle(backBrush,env.DisplayRegion);
                g.FillEllipse(gameDisplayBrush, env.Circle);
                g.DrawEllipse(whitePen, env.Circle);

                var circleCenter = new Point(env.Circle.X + env.Radius, env.Circle.Y + env.Radius);
                var vanishingTiming = env.TimingOffset + env.TimingLength;
                foreach (var score in scores.Where(x => env.TimingOffset < x.TimingBegin && x.TimingBegin < vanishingTiming))
                {
                    var r = (int)(((vanishingTiming - score.TimingBegin) / env.TimingLength).BarRatio * env.Radius);
                    var c = new Rectangle(circleCenter.X - r, circleCenter.Y - r, 2 * r, 2 * r);
                    g.DrawEllipse(whitePen, c);
                    // TODO: 表示/非表示を切り替えられるようにしたら良いかもね
                    g.DrawString(scores.IndexOf(score).ToString().PadLeft(3, '0'), myFont, Brushes.White, new Point(c.Left + c.Width / 2 - 14, c.Bottom - 6));

                }
            }
        }
    }
}
