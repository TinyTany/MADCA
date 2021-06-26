using MADCA.Core.Data;
using MADCA.Core.Note;
using MADCA.Core.Note.Abstract;
using MADCA.Core.Note.Concrete;
using MADCA.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using MadcaEnv = MADCA.Core.Data.MadcaEnv;

namespace MADCA.Core.Graphics
{
    public static class NoteDrawer
    {
        public static void DrawToLane(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, NoteBase note)
        {
            // NOTE: お試し実装
            using (var sb = new SolidBrush(NoteGraphicsGenerator.GetColor(note)))
            using (var pen = new Pen(Color.White))
            {
                g.Clip = new Region(env.LaneRect);
                var rect = note.GetRectangle(env);
                if (note.NoteType == NoteType.HoldRelay)
                {
                    g.DrawRectangle(pen, rect);
                }
                else
                {
                    g.FillRectangle(sb, rect);
                }
                rect.X -= (int)(MadcaEnv.LaneCount * env.LaneUnitWidth);
                if (note.NoteType == NoteType.HoldRelay)
                {
                    g.DrawRectangle(pen, rect);
                }
                else
                {
                    g.FillRectangle(sb, rect);
                }
                g.ResetClip();
            }
        }

        public static void DrawHoldRegionToLane(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, Hold hold)
        {
            using (var sb = new SolidBrush(Color.FromArgb(200, 200, 175, 90)))
            {
                var laneWidth = env.LaneUnitWidth * env.LaneCount;
                g.Clip = new Region(env.LaneRect);
                var path = hold.GetGraphicsPath(env);
                var matToLeft = new Matrix();
                matToLeft.Translate(-laneWidth, 0);
                var matToRight = new Matrix();
                matToRight.Translate(laneWidth, 0);
                var leftTimes = 0;

                g.FillPath(sb, path);
                while (path.GetBounds().Right > env.LaneRect.Right)
                {
                    path.Transform(matToLeft);
                    g.FillPath(sb, path);
                    leftTimes++;
                }
                var matToReset = new Matrix();
                matToReset.Translate(laneWidth * leftTimes, 0);
                path.Transform(matToReset);
                
                while (path.GetBounds().Left < env.LaneRect.Left)
                {
                    path.Transform(matToRight);
                    g.FillPath(sb, path);
                }
                g.ResetClip();
            }
        }

        public static void DrawToPreview(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, NoteBase note)
        {
            var diff = note.Timing - env.TimingOffset;
            float r = 1 - (float)(diff / env.TimingLength).BarRatio;
            if (r <= 0 || r > 1) { return; }
            var lane1 = note.Lane.NormalizedLane;
            float startDeg = CalcCsDeg(6 * lane1);
            float degSize = CalcCsDegSize(6 * note.NoteSize.Size);
            using(var p = new Pen(NoteGraphicsGenerator.GetColor(note), 5))
            {
                r *= env.Radius;
                g.DrawArc(p, env.CenterPoint.X - r, env.CenterPoint.Y - r, 2 * r, 2 * r, startDeg, degSize);
            }
        }

        private static float CalcCsDeg(float madcaDeg)
        {
            return 270 - madcaDeg;
        }

        private static float CalcCsDegSize(float madcaSize)
        {
            return -madcaSize;
        }

        public static void DrawHoldRegionToPreview(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, Hold hold)
        {
            if (hold.HoldEnd.Timing <= env.TimingOffset) { return; }
            if (hold.HoldBegin.Timing >= env.TimingOffset + env.TimingLength) { return; }
            var notes = hold.AllNotes.OrderBy(x => x.Timing).ToList();
            for (var i = 0; i < notes.Count - 1; ++i)
            {
                var begin = notes[i];
                var end = notes[i + 1];

                if (begin.Timing >= env.TimingOffset + env.TimingLength) { continue; }
                if (end.Timing <= env.TimingOffset) { continue; }
                DrawHoldStep(g, env, begin, end);
            }
        }

        private static void DrawHoldStep(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, NoteBase begin, NoteBase end)
        {
            using(var clipPath = new GraphicsPath())
            {
                clipPath.AddEllipse(env.Circle);
                g.Clip = new Region(clipPath);
            }

            var step = 5;
            // 円の中央が0
            var rbegin = (1 - ((begin.Timing - env.TimingOffset) / env.TimingLength).BarRatio) * env.Radius;
            var rend = (1 - ((end.Timing - env.TimingOffset) / env.TimingLength).BarRatio) * env.Radius;
            var dr = rbegin - rend;
            var dlleft = end.Lane.RawLane - begin.Lane.RawLane;
            var dlright = (end.Lane.RawLane + end.NoteSize.Size) - (begin.Lane.RawLane + begin.NoteSize.Size);
            var psLeft = new List<Point>();
            var psRight = new List<Point>();

            var curR = rbegin - step;
            while (true)
            {
                if (curR <= 0 || curR <= rend) { break; }

                if (dlleft != 0)
                {
                    var radLeft = MyMath.DegToRad(CalcCsDeg((float)(begin.Lane.RawLane * 6 + (rbegin - curR) / dr * dlleft * 6)));
                    psLeft.Add(new Point((int)(env.CenterPoint.X + curR * Math.Cos(radLeft)), (int)(env.CenterPoint.Y + curR * Math.Sin(radLeft))));
                }
                if (dlright != 0)
                {
                    var radRight = MyMath.DegToRad(CalcCsDeg((float)((begin.Lane.RawLane + begin.NoteSize.Size) * 6 + (rbegin - curR) / dr * dlright * 6)));
                    psRight.Add(new Point((int)(env.CenterPoint.X + curR * Math.Cos(radRight)), (int)(env.CenterPoint.Y + curR * Math.Sin(radRight))));
                }
                curR -= step;
            }

            var path = new GraphicsPath();
            for(var i = 0; i < psLeft.Count - 1; ++i)
            {
                path.AddLine(psLeft[i], psLeft[i + 1]);
            }

            try
            {
                path.AddArc(
                (float)(env.CenterPoint.X - rend),
                (float)(env.CenterPoint.Y - rend),
                (float)(2 * rend),
                (float)(2 * rend),
                CalcCsDeg(end.Lane.NormalizedLane * 6),
                CalcCsDegSize(end.NoteSize.Size * 6));
            }
            catch (Exception) { }
            for(var i = psRight.Count - 1; i > 0; --i)
            {
                path.AddLine(psRight[i], psRight[i - 1]);
            }
            try
            {
                path.AddArc(
                (float)(env.CenterPoint.X - rbegin),
                (float)(env.CenterPoint.Y - rbegin),
                (float)(2 * rbegin),
                (float)(2 * rbegin),
                CalcCsDeg((begin.Lane.NormalizedLane + begin.NoteSize.Size) * 6),
                CalcCsDegSize(-begin.NoteSize.Size * 6));
            }
            catch (Exception) { }

            using (var sb = new SolidBrush(Color.FromArgb(200, 200, 175, 90)))
            {
                g.FillPath(sb, path);
            }
            path.Dispose();
            g.ResetClip();
        }

        public static void DrawToLane(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, NoteBook noteBook)
        {
            // HACK: 描画対象にするHoldを画面内にあるもののみに絞ったほうがいいんじゃない？
            foreach (var hold in noteBook.Holds)
            {
                DrawHoldRegionToLane(g, env, hold);
                foreach(var note in hold.AllNotes)
                {
                    DrawToLane(g, env, note);
                }
            }
            foreach (var note in noteBook.Notes)
            {
                DrawToLane(g, env, note);
            }
        }

        public static void DrawToPreview(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, NoteBook noteBook)
        {
            foreach (var hold in noteBook.Holds)
            {
                DrawHoldRegionToPreview(g, env, hold);
                DrawToPreview(g, env, hold.HoldBegin);
                DrawToPreview(g, env, hold.HoldEnd);
            }
            foreach (var note in noteBook.Notes)
            {
                DrawToPreview(g, env, note);
            }
        }
    }

    public static class NoteGraphicsGenerator
    {
        // Touch 赤い部分(200, 10, 180) 青い部分(70, 160, 195)
        // Chain 明るい黄色(250, 200, 30) 暗い黄色(200, 170, 35)
        // SlideL (255, 128, 0)
        // SlideR (25, 200, 25)
        // SnapU (190, 0, 0)
        // SnapD (20, 140, 220)
        // Hold 手前 (200, 200, 130) 奥 (200, 150, 50)
        public static Color GetColor(NoteBase note)
        {
            switch (note.NoteType)
            {
                case NoteType.Touch:
                    return Color.FromArgb(250, 10, 180);
                case NoteType.Chain:
                    return Color.FromArgb(250, 200, 30);
                case NoteType.SlideL:
                    return Color.FromArgb(255, 128, 0);
                case NoteType.SlideR:
                    return Color.FromArgb(25, 200, 25);
                case NoteType.SnapU:
                    return Color.FromArgb(190, 0, 0);
                case NoteType.SnapD:
                    return Color.FromArgb(20, 140, 220);
                case NoteType.HoldBegin:
                    return Color.FromArgb(200, 200, 130);
                case NoteType.HoldEnd:
                    return Color.FromArgb(200, 150, 50);
                default:
                    return Color.White;
            }
        }
    }
}
