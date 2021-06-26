using MADCA.Core.Data;
using MADCA.Core.Note.Concrete;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using MadcaEnv = MADCA.Core.Data.MadcaEnv;

namespace MADCA.Utility
{
    public static class MyExtension
    {
        public static T Find<T>(this IReadOnlyList<T> list, Predicate<T> p)
            where T : class
        {
            foreach (var item in list)
            {
                if (p.Invoke(item))
                {
                    return item;
                }
            }
            return null;
        }

        public static T FindLast<T>(this IReadOnlyList<T> list, Predicate<T> p)
            where T : class
        {
            foreach (var item in list.Reverse())
            {
                if (p.Invoke(item))
                {
                    return item;
                }
            }
            return null;
        }

        public static bool Contains(this Rectangle rect, Point p, IReadOnlyEditorLaneEnvironment env)
        {
            var tmp = rect;
            if (tmp.Contains(p))
            {
                return true;
            }
            tmp.X -= (int)(env.LaneUnitWidth * MadcaEnv.LaneCount);
            return tmp.Contains(p);
        }

        public static Point GetLeftMiddle(this Rectangle rect)
        {
            return new Point(rect.Left, rect.Top + rect.Height / 2);
        }

        public static Point GetRightMiddle(this Rectangle rect)
        {
            return new Point(rect.Right, rect.Top + rect.Height / 2);
        }

        public static GraphicsPath GetGraphicsPath(this Hold hold, IReadOnlyEditorLaneEnvironment env)
        {
            var ps1 = new List<Point>();
            var ps2 = new List<Point>();
            var beginRect = hold.HoldBegin.GetRectangle(env);
            ps1.Add(beginRect.GetLeftMiddle());
            ps2.Add(beginRect.GetRightMiddle());
            foreach (var note in hold.AllNotes.Where(x => x != hold.HoldBegin).OrderBy(x => x.Timing))
            {
                var rect = note.GetRectangle(env);
                var diff = note.Lane.RawLane - hold.HoldBegin.Lane.RawLane;
                rect.X = beginRect.X + diff * (int)env.LaneUnitWidth;
                ps1.Add(rect.GetLeftMiddle());
                ps2.Add(rect.GetRightMiddle());
            }
            ps2.Reverse();
            ps1.AddRange(ps2);
            var gPath = new GraphicsPath();
            for (int i = 0; i < ps1.Count - 1; ++i)
            {
                gPath.AddLine(ps1[i], ps1[i + 1]);
            }
            return gPath;
        }

        public static bool Contains(this Hold hold, Point p, IReadOnlyEditorLaneEnvironment env)
        {
            var laneWidth = env.LaneUnitWidth * MadcaEnv.LaneCount;
            
            var matToLeft = new Matrix();
            matToLeft.Translate(-laneWidth, 0);
            var matToRight = new Matrix();
            matToRight.Translate(laneWidth, 0);
            var leftTimes = 0;

            using (var path = hold.GetGraphicsPath(env))
            {
                if (path.IsVisible(p)) { return true; }
                while (path.GetBounds().Right > env.LaneRect.Right)
                {
                    path.Transform(matToLeft);
                    if (path.IsVisible(p)) { return true; }
                    leftTimes++;
                }
                var matToReset = new Matrix();
                matToReset.Translate(laneWidth * leftTimes, 0);
                path.Transform(matToReset);

                while (path.GetBounds().Left < env.LaneRect.Left)
                {
                    path.Transform(matToRight);
                    if (path.IsVisible(p)) { return true; }
                }
            }

            return false;
        }
        /// <summary>
        /// Int型の整数を安全にUIntに変換します。
        /// 明示的なキャストは使用しないで下さい
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static uint ToUInt(this int i)
        {
            return i < 0 ? (uint)-i : (uint)i;
        }

        /// <summary>
        /// Long型の整数を安全にUIntに変換します。
        /// 明示的なキャストは使用しないで下さい
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static uint ToUInt(this long i)
        {
            return i < 0 ? (uint)-i : (uint)i;
        }
    }
}
