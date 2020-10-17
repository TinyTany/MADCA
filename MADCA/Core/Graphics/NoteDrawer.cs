using MADCA.Core.Data;
using MADCA.Core.Note;
using MADCA.Core.Note.Abstract;
using MADCA.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MADCA.Core.Graphics
{
    public static class NoteDrawer
    {
        public static void DrawToLane(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, NoteBase note)
        {
            // NOTE: お試し実装
            using (var sb = new SolidBrush(NoteGraphicsGenerator.GetColor(note)))
            {
                g.Clip = new Region(env.LaneRect);
                var rect = note.GetRectangle(env);
                g.FillRectangle(sb, rect);
                rect.X -= (int)(env.LaneCount * env.LaneUnitWidth);
                g.FillRectangle(sb, rect);
                g.ResetClip();
            }
        }

        public static void DrawToPreview(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, NoteBase note)
        {
            var diff = note.Timing - env.TimingOffset;
            float r = 1 - (float)(diff / env.TimingLength).BarRatio;
            if (r <= 0 || r > 1) { return; }
            var lane1 = note.Lane.NormalizedLane;
            float startDeg = 270 - 6 * lane1;
            float degSize = 6 * note.NoteSize.Size;
            using(var p = new Pen(NoteGraphicsGenerator.GetColor(note), 5))
            {
                r *= env.Radius;
                g.DrawArc(p, env.CenterPoint.X - r, env.CenterPoint.Y - r, 2 * r, 2 * r, startDeg, -degSize);
            }
        }

        public static void DrawToLane(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, NoteBook noteBook)
        {
            // TODO: Holdの描画処理
            foreach (var note in noteBook.Notes)
            {
                DrawToLane(g, env, note);
            }
        }

        public static void DrawToPreview(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, NoteBook noteBook)
        {
            // TODO: Holdの描画処理
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
                case Note.Interface.NoteType.Touch:
                    return Color.FromArgb(250, 10, 180);
                case Note.Interface.NoteType.Chain:
                    return Color.FromArgb(250, 200, 30);
                case Note.Interface.NoteType.SlideL:
                    return Color.FromArgb(255, 128, 0);
                case Note.Interface.NoteType.SlideR:
                    return Color.FromArgb(25, 200, 25);
                case Note.Interface.NoteType.SnapU:
                    return Color.FromArgb(190, 0, 0);
                case Note.Interface.NoteType.SnapD:
                    return Color.FromArgb(20, 140, 220);
                default:
                    return Color.White;
            }
        }
    }
}
