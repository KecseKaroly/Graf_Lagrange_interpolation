using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lagrange_interpolation
{
    public static class GraphicsExtension
    {
        public static void DrawPoint(this Graphics g, Point p)
        {
            Brush brush = Brushes.Blue;
            g.FillRectangle(brush, p.X, p.Y, 6, 6);
            g.DrawRectangle(Pens.Black, p.X, p.Y, 6, 6);
        }
    }
    
}
