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
            using(var penMain = new Pen(Color.White))
            using(var penSub = new Pen(Color.FromArgb(130, Color.White)))
            using (Font myFont = new Font("MS UI Gothic", 10, FontStyle.Regular))
            {
                g.FillRectangle(backBrush,env.DisplayRegion);
                g.FillEllipse(gameDisplayBrush, env.Circle);
                g.DrawEllipse(penMain, env.Circle);

                var circleCenter = new Point(env.Circle.X + env.Radius, env.Circle.Y + env.Radius);
                var vanishingTiming = env.TimingOffset + env.TimingLength;
                foreach (var score in scores.Where(x => env.TimingOffset < x.TimingEnd && x.TimingBegin < vanishingTiming))
                {
                    // 主線小節番号の描画
                    var r = (int)(((vanishingTiming - score.TimingBegin) / env.TimingLength).BarRatio * env.Radius);
                    var c = new Rectangle(circleCenter.X - r, circleCenter.Y - r, 2 * r, 2 * r);
                    if (0 < r && r < env.Radius)
                    {
                        g.DrawEllipse(penMain, c);
                        // TODO: 表示/非表示を切り替えられるようにしたら良いかもね
                        g.DrawString(scores.IndexOf(score).ToString().PadLeft(3, '0'), myFont, Brushes.White, new Point(c.Left + c.Width / 2 - 14, c.Bottom - 6));
                    }

                    // 副線の描画
                    // TODO: 表示/非表示を切り替えられるようにしたら良いかもね
                    for (var cnt = 1; cnt < score.BeatNum; ++cnt)
                    {
                        r = (int)(((vanishingTiming - score.TimingBegin - new TimingPosition(score.BeatDen, cnt)) / env.TimingLength).BarRatio * env.Radius);
                        if (!(0 < r && r < env.Radius))
                        {
                            continue;
                        }
                        c = new Rectangle(circleCenter.X - r, circleCenter.Y - r, 2 * r, 2 * r);
                        g.DrawEllipse(penSub, c);
                    }
                }
            }
        }
    }
}
