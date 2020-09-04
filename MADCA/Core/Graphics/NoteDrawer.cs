using MADCA.Core.Data;
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
        public static void DrawToLane(System.Drawing.Graphics g, IReadOnlyEditorLaneEnvironment env, Note.Abstract.Note note)
        {
            // NOTE: お試し実装
            PositionConverter.ConvertVirtualToRealNorm(env, new Position(note.Lane, note.Timing), out Point loc);
            int width = NoteEnvironment.NoteWidth(note.NoteSize.Size, env);
            int height = NoteEnvironment.NoteHeight;
            using(var sb = new SolidBrush(Color.White))
            {
                g.FillRectangle(sb, new RectangleF(loc.X, loc.Y - 2, width, height));
            }
        }

        public static void DrawToPreview(System.Drawing.Graphics g, IReadOnlyPreviewDisplayEnvironment env, Note.Abstract.Note note)
        {
            var diff = note.Timing - env.TimingOffset;
            float r = 1 - (float)(diff / env.TimingLength).BarRatio;
            if (r <= 0 || r > 1) { return; }
            var lane1 = note.Lane.NormalizedLane;
            float startDeg = 270 - 6 * lane1;
            float degSize = 6 * note.NoteSize.Size;
            using(var p = new Pen(Color.White, 5))
            {
                r *= env.Radius;
                g.DrawArc(p, env.CenterPoint.X - r, env.CenterPoint.Y - r, 2 * r, 2 * r, startDeg, - degSize);
            }
        }
    }

    public static class NoteGraphicsGenerator
    {

    }
}
