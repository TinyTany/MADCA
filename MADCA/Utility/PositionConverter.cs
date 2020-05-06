using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MADCA.Core.Data;
using System.Drawing;
using MADCA.Core.Score;

namespace MADCA.Utility
{
    public static class PositionConverter
    {
        /// <summary>
        /// 実座標から仮想座標を計算します
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="offset">実座標の零点（左下）に対応する仮想座標</param>
        /// <param name="p">実座標（左上原点）</param>
        /// <param name="beat">拍数のストライド</param>
        /// <param name="scores"></param>
        /// <param name="position">計算された仮想座標</param>
        /// <returns></returns>
        public static bool ConvertRealToVirtual(IReadOnlyEditorLaneEnvironment env, Position offset, Point p, uint beat, IReadOnlyList<Score> scores, out Position position)
        {
            p = new Point(p.X - env.PanelOffset.X, p.Y - env.PanelOffset.Y);
            position = null;
            if (env.AvailableLaneWidth == 0) { return false; }
            var laneLeft = env.SideMargin;
            var laneRight = laneLeft + env.AvailableLaneWidth;
            if (!(laneLeft <= p.X && p.X < laneRight)) { return false; }
            int lanePos = (int)((p.X - laneLeft) / env.LaneUnitWidth);
            var newLanePos = new LanePotision(lanePos + offset.Lane.NormalizedLane);
            var timing = offset.Timing + new TimingPosition(env.TimingUnitHeight, env.PanelSize.Height - p.Y);
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
        /// 仮想座標から実座標を計算します
        /// 実座標のXは正の値で、Yは正または負の値になります
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="offset">実座標の零点（左下）に対応する仮想座標</param>
        /// <param name="position">仮想座標</param>
        /// <param name="p">計算された実座標（左上原点）</param>
        /// <returns></returns>
        public static bool ConvertVirtualToReal(IReadOnlyEditorLaneEnvironment env, Position offset, Position position, out Point p)
        {
            var laneDiff = position.Lane.NormalizedLane - offset.Lane.NormalizedLane;
            if (laneDiff < 0)
            {
                laneDiff = (int)env.LaneCount - laneDiff;
            }
            var px = (int)(env.SideMargin + laneDiff * env.LaneUnitWidth);
            var timingDiff = position.Timing - offset.Timing;
            var py = (int)(timingDiff.BarRatio * env.TimingUnitHeight);
            p = new Point(px, env.PanelSize.Height - py);
            p = new Point(p.X + env.PanelOffset.X, p.Y + env.PanelOffset.Y);
            return true;
        }
    }
}
