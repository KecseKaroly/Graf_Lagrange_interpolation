using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Polynomials;

namespace Lagrange_interpolation
{
    public partial class Form1 : Form
    {
        static Graphics g1;
        static Graphics g2;
        List<Point> points;

        int pointGrabbed;
        int tGrabbed;
        private bool shouldUseChord;

        private List<double> tValues;

        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
            pointGrabbed = -1; tGrabbed = -1;
            shouldUseChord = true;
            tValues = new List<double>();
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g1 = e.Graphics;
            drawPolygon(g1);

            for (int i = 0; i < points.Count; i++)
            {
                g1.DrawPoint(new Point(points[i].X - 3, points[i].Y - 3));
            }
        }
        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    int pointIndex = getPointIndex(e.X, e.Y);

                    if (pointIndex == -1 && points.Count != 9)
                    {
                        points.Add(new Point(e.X, e.Y));
                        shouldUseChord = true;
                        canvas.Invalidate();
                    }
                    else
                    {
                        pointGrabbed = pointIndex;
                    }
                    break;
                case MouseButtons.Right:
                    break;
                default:
                    break;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    pointGrabbed = -1;
                    break;
                case MouseButtons.Right:
                    int pointIndex = getPointIndex(e.X, e.Y);
                    if (pointIndex != -1)
                    {
                        points.RemoveAt(pointIndex);
                        tValues.RemoveAt(pointIndex);
                        canvas.Invalidate();
                    }
                    break;
                default:
                    break;
            }
        }
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (pointGrabbed != -1)
            {
                points[pointGrabbed] = new Point(e.X, e.Y);
                canvas.Invalidate();
            }
        }

        private List<double> Calculate_T_Values()
        {
            List<double> t_values = new List<double>();
            t_values.Add(0);
            for (int i = 1; i < points.Count; i++)
            {
                t_values.Add(t_values.Last() + Math.Sqrt(Math.Pow(points[i].X - points[i - 1].X, 2) + Math.Pow(points[i].Y - points[i - 1].Y, 2)));
            }
            for (int i = 1; i < points.Count; i++)
            {
                t_values[i] /= t_values.Last();
            }
            return t_values;
        }

       
        private int getPointIndex(int x, int y)
        {
            for (int i = 0; i < points.Count; i++)
            {
                if ((points[i].X <= x + 8 && points[i].X >= x - 8) && (points[i].Y <= y + 8 && points[i].Y >= y - 8))
                {
                    return i;
                }
            }
            return -1;
        }

        private void drawPolygon(Graphics g)
        {
            if(shouldUseChord)
            {
                tValues = Calculate_T_Values();
            }
            canvas2.Invalidate();
            if (points.Count > 1)
            {
                double[,] xMatrix = new double[points.Count, points.Count+1];
                double[,] yMatrix = new double[points.Count, points.Count + 1];

                for (int i = 0; i < points.Count; i++)
                {
                    for(int j = 0; j < points.Count; j++)
                    {
                        xMatrix[i, j] = Math.Pow(tValues[i], j);
                        yMatrix[i, j] = Math.Pow(tValues[i], j);
                    }
                    xMatrix[i, points.Count] = points[i].X;
                    yMatrix[i, points.Count] = points[i].Y;
                }
                GaussianElimination(xMatrix);
                GaussianElimination(yMatrix);

                double[] xCoeff = new double[points.Count];
                double[] yCoeff = new double[points.Count];

                for(int i = 0; i < points.Count; i++)
                {
                    xCoeff[i] = xMatrix[i, points.Count];
                    yCoeff[i] = yMatrix[i, points.Count];
                }
                Polynomial xPoly = new Polynomial(xCoeff);
                Polynomial yPoly = new Polynomial(yCoeff);

                double currT = 0;
                double d = 1.0 / 10000;

                double currX = xPoly.Calculate(currT);
                double currY = yPoly.Calculate(currT);
                Point temp = new Point((int)currX, (int)currY);
                while (currT < 1)
                {
                    currT += d;
                    currX = xPoly.Calculate(currT);
                    currY = yPoly.Calculate(currT);
                    Point curr = new Point((int)currX, (int)currY);
                    g.DrawLine(new Pen(Brushes.Black, 2f), temp, curr);
                    temp = curr;
                }

            }
        }

        public static bool GaussianElimination(double[,] M)
        {
            // input checks
            int rowCount = M.GetUpperBound(0) + 1;
            if (M == null || M.Length != rowCount * (rowCount + 1))
                throw new ArgumentException("The algorithm must be provided with a (n x n+1) matrix.");
            if (rowCount < 1)
                throw new ArgumentException("The matrix must at least have one row.");

            // pivoting
            for (int col = 0; col + 1 < rowCount; col++) if (M[col, col] == 0)
                // check for zero coefficients
                {
                    // find non-zero coefficient
                    int swapRow = col + 1;
                    for (; swapRow < rowCount; swapRow++) if (M[swapRow, col] != 0) break;

                    if (M[swapRow, col] != 0) // found a non-zero coefficient?
                    {
                        // yes, then swap it with the above
                        double[] tmp = new double[rowCount + 1];
                        for (int i = 0; i < rowCount + 1; i++)
                        { tmp[i] = M[swapRow, i]; M[swapRow, i] = M[col, i]; M[col, i] = tmp[i]; }
                    }
                    else return false; // no, then the matrix has no unique solution
                }

            // elimination
            for (int sourceRow = 0; sourceRow + 1 < rowCount; sourceRow++)
            {
                for (int destRow = sourceRow + 1; destRow < rowCount; destRow++)
                {
                    double df = M[sourceRow, sourceRow];
                    double sf = M[destRow, sourceRow];
                    for (int i = 0; i < rowCount + 1; i++)
                        M[destRow, i] = M[destRow, i] * df - M[sourceRow, i] * sf;
                }
            }

            // back-insertion
            for (int row = rowCount - 1; row >= 0; row--)
            {
                double f = M[row, row];
                if (f == 0) return false;

                for (int i = 0; i < rowCount + 1; i++) M[row, i] /= f;
                for (int destRow = 0; destRow < row; destRow++)
                { M[destRow, rowCount] -= M[destRow, row] * M[row, rowCount]; M[destRow, row] = 0; }
            }
            return true;
        }

        private void canvas2_MouseMove(object sender, MouseEventArgs e)
        {
            if(tGrabbed != -1)
            {
                double newT = (double)(e.X - canvas2.Left - 43) / (canvas2.Right - 63 - canvas2.Left - 43);
                if(newT > 0 && newT < 1)
                    tValues[tGrabbed] = newT;
                canvas.Invalidate();
            }
        }

        private void canvas2_MouseDown(object sender, MouseEventArgs e)
        {
            if (shouldUseChord)
            {
                shouldUseChord = false;
                custom_rb.Checked = true;
            }
            for (int i = 0; i < tValues.Count; i++)
            {
                if (e.X + 5 >= (canvas2.Right - 63 - 43 - canvas2.Left) *tValues[i] + canvas2.Left + 43
                        && e.X - 5 <= (canvas2.Right - 63 - 43 - canvas2.Left) *tValues[i] + canvas2.Left + 43)
                {
                    tGrabbed = i;
                }
            }

        }
        private void canvas2_MouseUp(object sender, MouseEventArgs e)
        {
            tGrabbed = -1;
        }

        private void canvas2_Paint(object sender, PaintEventArgs e)
        {
            g2 = e.Graphics;
            int halfCanvasHeight = canvas2.Height / 2;

            Point leftSide = new Point(canvas2.Left + 43, halfCanvasHeight);
            Point rightSide = new Point(canvas2.Right - 63, halfCanvasHeight);

            g2.DrawLine(Pens.Black, leftSide, rightSide);
            double len = rightSide.X - leftSide.X;
            int intval = 10;
            double d = len / intval;
            double currentX = leftSide.X;
            do
            {
                g2.DrawLine(
                    new Pen(Brushes.Black, 3), 
                    new Point((int)currentX, halfCanvasHeight+10), 
                    new Point((int)currentX, halfCanvasHeight - 10));
                currentX += d;
            } while (currentX <= rightSide.X);

            AddTValuesToCanvas(g2);
        }
        private void AddTValuesToCanvas(Graphics g)
        {
            int halfCanvasHeight = canvas2.Height / 2;
            double len = (canvas2.Right - 63) - (canvas2.Left + 43);
            for (int i = 0; i < tValues.Count; i++)
            {
                double currX = canvas2.Left + 43 + len * tValues[i];
                g.DrawLine(new Pen(Brushes.GreenYellow, 5f),
                           new Point((int)currX, halfCanvasHeight + 10),
                           new Point((int)currX, halfCanvasHeight - 10));
            }

        }

        private void chord_rb_MouseClick(object sender, MouseEventArgs e)
        {
            shouldUseChord = true;
            canvas.Invalidate();
        }

        private void custom_rb_MouseClick(object sender, MouseEventArgs e)
        {

            shouldUseChord = true;
        }

    }
}
