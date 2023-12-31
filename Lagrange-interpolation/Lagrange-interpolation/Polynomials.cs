﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lagrange_interpolation
{
    public class Polynomial
    {
        public Polynomial() : this(new double[] { .0, 1.0 })
        { }
        // We get the coefficiencies from the Gauissan ELimination and we'll use them to calculate the value (x or y) that the polynom
        // should return for the current t value
        public Polynomial(double[] coefficientArray)
        {
            Coefficients = (double[])coefficientArray.Clone();
        }
        public double[] Coefficients;

        public int Degree
        {
            get { return Coefficients.Length - 1; }
        }

        public double Calculate(double x)
        {
            double res = Coefficients[0];
            double t = x;
            for (int i = 1; i < Coefficients.Length; i++)
            {
                res += Coefficients[i] * t;
                t *= x;
            }
            return res;
        }
    }
}
