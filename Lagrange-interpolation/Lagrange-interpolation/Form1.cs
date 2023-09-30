using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lagrange_interpolation
{
    public partial class Form1 : Form
    {
        // Drawing classes for the 2 picture boxes
        static Graphics g1;
        static Graphics g2;
        // This contains the points (x,y) the user adds by clicking on the canvas and their t values
        List<Point> points;
        List<double> tValues;

        // Save the index of the grabbed point of the upper canvas, and the index of the grabbed t value of the bottom canvas
        int pointGrabbed;
        int tGrabbed;

        // bool value for either using a chord T value or a custom set
        private bool shouldUseChord;


        public Form1()
        {
            InitializeComponent();
            points = new List<Point>();
            tValues = new List<double>();
            pointGrabbed = -1; 
            tGrabbed = -1;
            shouldUseChord = true;
        }

        // We'll call the drawing funcitons in the canvas_Paint event method
        // To use this method, we'll have to call the canvas_Invalidate() method, which clears the canvas
        // then the canvas_Paint will redraw everything

        // Here we'll draw the polygon first, and then the input points
        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            g1 = e.Graphics;
            drawPolygon(g1);

            for (int i = 0; i < points.Count; i++)
            {
                g1.DrawPoint(new Point(points[i].X - 3, points[i].Y - 3));
            }
        }

        // In this event listener method, we'll differenciate between left and right mouse buttons
        // On the left click, we'll first check if there is already a point on the cursor's location (e.X, e.Y)
        // - If the value of the point index is (-1), then the palce where the mouse currently is is clear, so we can add
        // a new point to the list, turn on the chord t values option then invalidate to redraw the polygon
        // - If the value of the point index refers to an existing point, then we'll save it's index so we'll know which point 
        // we are supposed to move with the canvas_MouseMove event listener method

        // comment: we maximize the number of points to 9, because after 10 points the drawing funciton does not work
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


        // If we release the left mouse button, we'll set the index of the grabbed point to -1, so we no longer move any points on the canvas
        // If it's the right button, it means we would want to remove a point from the list, so we check if there is a point on the cursor
        // If there is, we remove the point and it's t_value then invalidate the canvas to calculate the new polygon
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

        // If we have an index of a grabbed point, we change it's coordinate to the current x and y coordinates of the mouse's current location
        // then invalidate the canvas to recalculate the t values and the polygon
        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (pointGrabbed != -1)
            {
                points[pointGrabbed] = new Point(e.X, e.Y);
                canvas.Invalidate();
            }
        }

        // In this funciton we calculate the T values based on the sum of the distances between each point
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

            // Here the following happens:
            /*
             * - We create 2 vectors for the Gaussian Elimination: one for the t and x, and one for the t and y coordinates of the input points
             *      so it looks something like that for the xMatrix: 
             *                                                                          [
             *      a0 + a1*t1 + a2*t1^2 + an*t1^n... = x1                                 [1, t1,   t1^2,   ..., t1^n   | x1   ]
             *      a0 + a1*t2 + a2*t2^2 + an*t2^n... = x1                                 [1, t2,   t2^2,   ..., t2^n   | x2   ]
             *                                                                             .
             *                                                                             .
             *                                                                             .
             *      a0 + a1*tn-1 + a2*tn-1^2 + an*tn-1^n... = xn-1                         [1, tn-1, tn-1^2, ..., tn-1^n | xn-1 ]
             *      a0 + a1*tn + a2*tn^2 + an*tn-1^n... = xn                               [1, tn,   tn^2,   ..., tn^n   | xn   ]
             *                                                                          ]
             * Then we calculate the values for (a0, a1, ..., an) with the Gaussian Eliminations by passing the t values and their x
             * Same for the y values
             * Because the Gaussian Elimination contains the values of the x and y coefficiencies at the last index of each row,
             * we create an array for both of them and do a for loop
             */
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


                // We start from
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
