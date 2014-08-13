using System;
using System.Collections.Generic;
using System.Text;

namespace GControl.Axis
{
    class MotionHelper
    {
        public static void Linear2D(Axis a1, double i1, Axis a2, double i2, double v)
        {
            double p = Math.Sqrt(Math.Pow(i1, 2) + Math.Pow(i2, 2));
            double t = p / v;
            int ticksToDest = (int)Math.Round(t * 1000.0 / Ticker.TICKDuration);
            a1.AddPlan(new PlanPoint(i1, ticksToDest));
            a2.AddPlan(new PlanPoint(i2, ticksToDest));
        }
    }
}
