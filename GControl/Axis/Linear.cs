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

        public static void CircularCW2D(Axis a1, Axis a2, double c1, double c2, double ep1, double ep2)
        {
            Vector2D sp = new Vector2D(-c1, -c2);
            Vector2D p = sp;
            for (int i = 0; i < 360; i++)
            {
                var newP = p.Rotate(1);
                var moveP = (newP - p) ;
                p = newP;
                a1.AddPlan(new PlanPoint(moveP.X, 100));
                a2.AddPlan(new PlanPoint(moveP.Y, 100));
            }

        }
    }
}
