using System;
using System.Collections.Generic;
using System.Text;

namespace GControl.Axis
{
    public class PlanPoint
    {
        public double Destination { get; set; }
        public double Time { get; set; }

        public PlanPoint(double destination, double time)
        {
            Destination = destination;
            Time = time;
        }
    }
}
