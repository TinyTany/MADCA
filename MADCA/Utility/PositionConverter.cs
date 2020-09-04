using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;
using System.Drawing;
using MADCA.Core.Score;
using System.IO;

namespace MADCA.Utility
{
    public static class PositionConverter
    {
        /// <summary>
        /// 絶対実座標から仮想座標を計算します
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="p">実座標（左上原点）</param>
        /// <param name="beat">拍数のストライド</param>
        /// <param name="scores"></param>
        /// <param name="position">計算された仮想座標</param>
        /// <returns></returns>
        public static bool ConvertRealToVirtual(IReadOnlyEditorLaneEnvironment env, Point p, uint beat, IReadOnlyList<IReadOnlyScore> scores, out Position position)
        {
            p = new Point(p.X - env.PanelOffset.X, p.Y - env.PanelOffset.Y);
            position = null;
            if (env.AvailableLaneWidth == 0) { return false; }
            var laneLeft = env.SideMargin;
            var laneRight = laneLeft + env.AvailableLaneWidth;
            if (!(laneLeft <= p.X && p.X < laneRight)) { return false; }
            int lanePos = (int)(((p.X - laneLeft) + env.OffsetXRaw) / env.LaneUnitWidth);
            if ((p.X - laneLeft) + env.OffsetXRaw < 0)
            {
                lanePos--;
            }
            var newLanePos = new LanePotision(lanePos);
            var timing = new TimingPosition(env.TimingUnitHeight, (env.PanelSize.Height - p.Y) - (int)env.BottomMargin + env.OffsetY);
            var accum = new TimingPosition(1, 0);
            foreach(var score in scores)
            {
                var tmp = new TimingPosition(score.BeatDen, (int)score.BeatNum);
                if (timing < tmp + accum) { break; }
                accum += tmp;
            }
            var cnt = (int)Math.Floor(((timing - accum) / new TimingPosition(beat, 1)).BarRatio);
            var newTimingPos = new TimingPosition(beat, cnt) + accum;
            position = new Position(newLanePos, newTimingPos);
            return true;
        }

        /// <summary>
        /// 仮想座標から絶対実座標を計算します
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="position">仮想座標</param>
        /// <param name="p">計算された実座標（左上原点）</param>
        /// <returns></returns>
        public static bool ConvertVirtualToReal(IReadOnlyEditorLaneEnvironment env, Position position, out Point p) // TODO: 要検証
        {
            var px = (int)(env.SideMargin + position.Lane.RawLane * env.LaneUnitWidth - env.OffsetXRaw);
            var py = (int)(position.Timing.BarRatio * env.TimingUnitHeight + env.OffsetY);
            px += env.PanelOffset.X;
            py = env.PanelSize.Height - py + env.PanelOffset.Y;
            p = new Point(px, py);
            return true;
        }

        public static bool ConvertVirtualToRealNorm(IReadOnlyEditorLaneEnvironment env, Position position, out Point p)
        {
            var px = (int)(env.SideMargin + position.Lane.RawLane * env.LaneUnitWidth - env.OffsetXRaw);
            var py = (int)(position.Timing.BarRatio * env.TimingUnitHeight - env.OffsetY);
            px += env.PanelOffset.X;
            py = env.PanelSize.Height - py - env.PanelOffset.Y - (int)env.BottomMargin;
            p = new Point(px, py);
            return true;
        }
    }
}
