using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MADCA.Core.Graphics
{
    public static class LaneDrawer
    {
        private static Color backgroundColor = Color.Black;
        public static void Draw(System.Drawing.Graphics g, RectangleF drawArea, float leftMargin, float rightMargin, float laneUnitWidth)
        {
            System.Diagnostics.Debug.Assert(leftMargin + rightMargin <= drawArea.Width);
            using(var backgroundBrush = new SolidBrush(backgroundColor))
            {
                g.FillRectangle(backgroundBrush, 0, 0, drawArea.Width, drawArea.Height);
            }
            using(var penMain = new Pen(Color.White))
            {
                g.DrawLine(penMain, new PointF(leftMargin, 0), new PointF(leftMargin, drawArea.Height));
                g.DrawLine(penMain, new PointF(drawArea.Width - rightMargin, 0), new PointF(drawArea.Width - rightMargin, drawArea.Height));
            }
        }
    }

    public static class NoteGraphicsGenerator
    {

    }
}
