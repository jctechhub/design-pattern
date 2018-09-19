using System;
using System.Reflection.Metadata.Ecma335;

namespace main
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine(PointFactory.NewCartesianPoint(2, 3));
            Console.WriteLine(PointFactory.NewPolarPoint(4, 5));

            //issue with this approach, that someone may still create new class point by doing the following: 
            var p = new Point(1, 2);

            Console.Read();
        }
    }

    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    /// <summary>
    /// Separate component that initialises the class in a particular way.
    /// </summary>
    public static class PointFactory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
        }
    }

    public class Point
    {
        private double x, y;

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }
}
