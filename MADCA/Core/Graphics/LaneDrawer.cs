using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MADCA.Core.Data;
using MADCA.Utility;
using MadcaEnv = MADCA.Core.Data.MadcaEnv;

namespace MADCA.Core.Graphics
{
    public static class LaneDrawer
    {
        private static Color backgroundColor = Color.Black;
        private static Color laneBackColor = Color.FromArgb(32, 32, 32);
        private static Color timingMainColor = Color.FromArgb(120, 255, 255, 0);
        public static void Draw(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, IReadOnlyList<Score.IReadOnlyScore> scores)
        {
            using(var backgroundBrush = new SolidBrush(backgroundColor))
            using(var laneBackBrush = new SolidBrush(laneBackColor))
            {
                g.FillRectangle(backgroundBrush, env.PanelRegion);
                g.FillRectangle(laneBackBrush, env.LaneRect);
            }
            using(var penBorder = new Pen(Color.White))
            using(var penMain = new Pen(Color.FromArgb(200, Color.White)))
            using(var penSub = new Pen(Color.FromArgb(130, Color.White)))
            using (Font myFont = new Font("MS UI Gothic", 10, FontStyle.Bold))
            using(var penTimingMain = new Pen(timingMainColor))
            {
                // レーン補助線を描画
                var diffX = (int)(env.LaneUnitWidth - env.OffsetX % env.LaneUnitWidth);
                if (diffX == env.LaneUnitWidth) { diffX = 0; }
                var curX = diffX + env.LaneRect.Left;
                var curLane = (diffX + env.OffsetX) / env.LaneUnitWidth;
                for (; curX <= env.LaneRect.Right; curX += (int)env.LaneUnitWidth, ++curLane)
                {
                    curLane %= MadcaEnv.LaneCount;
                    if (curLane % env.LaneGroupCount == 0)
                    {
                        g.DrawLine(penMain, new PointF(curX, env.PanelRegion.Y), new PointF(curX, env.PanelRegion.Y + env.AvailableLaneHeight));
                        var textWidth = System.Windows.Forms.TextRenderer.MeasureText(g, curLane.ToString(), myFont).Width;
                        g.DrawString(curLane.ToString(), myFont, Brushes.White, new PointF(curX - textWidth / 2 + 1.5f, env.PanelRegion.Y + env.AvailableLaneHeight + 5));
                        continue;
                    }
                    g.DrawLine(penSub, new PointF(curX, env.PanelRegion.Y), new PointF(curX, env.PanelRegion.Y + env.AvailableLaneHeight));
                }

                // 小節線などを描画
                if (scores == null) { return; }
                var offsetTimingMin = new TimingPosition(env.TimingUnitHeight.ToUInt(), env.OffsetY);
                var offsetTimingMax = new TimingPosition(env.TimingUnitHeight.ToUInt(), env.OffsetY + env.LaneRect.Height);
                var drawScores = scores.Where(x => !(x.TimingEnd <= offsetTimingMin || offsetTimingMax < x.TimingBegin));
                foreach(var score in drawScores)
                {
                    var y = (int)(env.LaneRect.Bottom - (score.TimingBegin - offsetTimingMin).BarRatio * env.TimingUnitHeight);
                    if (env.LaneRect.Top <= y && y <= env.LaneRect.Bottom)
                    {
                        g.DrawString(scores.IndexOf(score).ToString().PadLeft(3, '0'), myFont, Brushes.White, new Point(env.LaneRect.Left - 28, y - 12));
                        g.DrawLine(penTimingMain, new Point(env.LaneRect.Left, y), new Point(env.LaneRect.Right, y));
                    }
                    for (var cnt = 1; cnt < score.BeatNum; ++cnt)
                    { 
                        y -= (int)(env.TimingUnitHeight / score.BeatDen);
                        if (env.LaneRect.Top <= y && y <= env.LaneRect.Bottom)
                        {
                            g.DrawLine(penSub, new Point(env.LaneRect.Left, y), new Point(env.LaneRect.Right, y));
                        }
                    }
                }
            }
        }
    }
}
