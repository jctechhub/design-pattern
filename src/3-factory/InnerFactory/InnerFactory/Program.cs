using System;

namespace InnerFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var point = Point.Factory.NewCartesianPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
            var origin = Point.Origin2;


            Console.Read();
        }
    }


    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        //Factory Method: be the inner class of the Point class. Similar example is: Task.Factory.
        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }

        //Factory properties
        public static Point Origin => new Point(0,0); //instantiate everytime this is called. 
        public static Point Origin2 = new Point(0, 0);  //instantiate once, and use everywhere. 

    }
}
