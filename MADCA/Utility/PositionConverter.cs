using System;
using System.Collections.Generic;
using MADCA.Core.Data;
using System.Drawing;
using MADCA.Core.Score;
using MadcaEnv = MADCA.Core.Data.MadcaEnv;

namespace MADCA.Utility
{
    public static class PositionConverter
    {
        /// <summary>
        /// 絶対実座標から仮想座標を計算します
        /// LaneのRangeは(-inf, inf)（RawLaneを計算）
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="p">実座標（左上原点）</param>
        /// <param name="beat">拍数のストライド</param>
        /// <param name="scores"></param>
        /// <param name="position">計算された仮想座標</param>
        /// <returns></returns>
        public static bool ConvertRealToVirtual(IReadOnlyEditorLaneEnvironment env, Point p, uint beat, IReadOnlyList<IReadOnlyScore> scores, out Position position)
        {
            p = new Point(p.X - env.PanelRegion.X, p.Y - env.PanelRegion.Y);
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
            var timing = new TimingPosition(env.TimingUnitHeight.ToUInt(), (env.PanelRegion.Height - p.Y) - (int)env.BottomMargin + env.OffsetY);
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
        /// x座標のRangeは(-inf, inf)
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="position">仮想座標</param>
        /// <returns></returns>
        public static Point ConvertVirtualToReal(IReadOnlyEditorLaneEnvironment env, Position position)
        {
            var px = (int)(env.SideMargin + position.Lane.RawLane * env.LaneUnitWidth - env.OffsetXRaw);
            var py = (int)(position.Timing.BarRatio * env.TimingUnitHeight - env.OffsetY);
            px += env.PanelRegion.X;
            py = env.PanelRegion.Height - py - env.PanelRegion.Y - (int)env.BottomMargin;
            return new Point(px, py);
        }

        /// <summary>
        /// 仮想座標から実座標を計算します
        /// x座標のRangeは[0, A)（Aはレーン数×1レーンの幅）
        /// </summary>
        /// <param name="env">エディタレーン環境</param>
        /// <param name="position">仮想座標</param>
        /// <returns></returns>
        public static Point ConvertVirtualToRealNorm(IReadOnlyEditorLaneEnvironment env, Position position)
        {
            var xtmp = MyMath.PositiveMod(position.Lane.RawLane * env.LaneUnitWidth - env.OffsetXRaw, MadcaEnv.LaneCount * env.LaneUnitWidth);
            var px = (int)(env.SideMargin + (int)xtmp);
            var py = (int)(position.Timing.BarRatio * env.TimingUnitHeight - env.OffsetY);
            px += env.PanelRegion.X;
            py = env.PanelRegion.Height - py - env.PanelRegion.Y - (int)env.BottomMargin;
            return new Point(px, py);
        }
    }
}
