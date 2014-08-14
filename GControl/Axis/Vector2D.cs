using System;
using System.Collections.Generic;
using System.Text;

namespace GControl.Axis
{
    class Vector2D
    {
        public static Vector2D operator + (Vector2D v1, Vector2D v2)
        {
            return v1.Add(v2);
        }
        public static Vector2D operator - (Vector2D v1, Vector2D v2)
        {
            return v1.Subtract(v2);
        }
        public static Vector2D operator * (Vector2D v, double d)
        {
            return v.Multiply(d);
        }
        
        private double _Y;
        private double _X;

        public double X
        {
            get
            {
                return _X;
            }
            set
            {
                _X = value;
            }
        }
        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }
        public double Length { get { return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2)); } }
        public double Angel { get { return Math.Atan(X / Y); } }

        public Vector2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Vector2D Add(Vector2D vector)
        {
            return new Vector2D(X + vector.X, Y + vector.Y);
        }
        public Vector2D Subtract(Vector2D vector)
        {
            return new Vector2D(X - vector.X, Y - vector.Y);
        }
        public Vector2D Multiply(double scalar)
        {
            return new Vector2D(X * scalar, Y * scalar);
        }
        public Vector2D Normalize()
        {
            var length = Length;
            return new Vector2D(X / length, Y / length);
        }
        public double ScalarMultiply(Vector2D vector)
        {
            return X * vector.X + Y * vector.Y;
        }

        public Vector2D Rotate(double angel)
        {
            double rad = angel * Math.PI / 180.0;
            double s = Math.Sin(rad);
            double c = Math.Cos(rad);

            return new Vector2D(X * c - Y * s, X * s + Y * c);
        }

        public override string ToString()
        {
            return string.Format("({0}; {1})", X, Y);
        }
    }
}
