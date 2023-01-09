// ------------------------------------------------------------------------------------------------------
// Anonymous
//
// Anonymous methods in C#.
//
// C# 2.0 introduced the ability to put code "inline" when setting an event handler using what they call
// "anonymous methods."
//
// This sample will show this functionality using the shapes classes from the "Events" sample just to
// keep things simple.
//
// PLEASE NOTE: As of C# 3.0, anonymous methods are more or less deprecated and replaced with lambda
//              expressions. They are still supported and still compile but lambda expressions provide
//              a much nicer way to achieve the same results.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Anonymous
{
    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y coordinates of the shape center.
        //
        private int x;
        private int y;

        // The "PropertyChanged" event member.
        //
        public event Action<Shape> PropertyChanged;

        // First public constructor takes no arguments and initializes the fields x and y to zero.
        //
        public Shape()
        {}

        // Second public constructor takes input parameters for x and y and assigns them to the data
        // fields.
        //
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // X coordinate property.
        //
        public int X
        {
            get { return x; }
            set { x = value;
                  OnPropertyChanged(); }
        }

        // Y coordinate property.
        //
        public int Y
        {
            get { return y; }
            set { y = value;
                  OnPropertyChanged(); }
        }

        // Protected method fires the "PropertyChanged" event.
        //
        protected void OnPropertyChanged()
        {
            if (PropertyChanged != null)
                PropertyChanged(this);
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }

        // Virtual "Draw" method.
        //
        public virtual void Draw()
        {
            Console.WriteLine("Center = {0}", this);
        }
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle radius field.
        //
        private int radius;

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        public Circle(int x, int y, int radius) : base(x, y)
        {
            this.radius = radius;
        }

        // Public radius property.
        //
        public int Radius
        {
            get { return radius; }
            set { radius = value;
                  OnPropertyChanged(); }
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", radius);
        }
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Rectangle width/height fields.
        //
        private int width;
        private int height;

        // Rectangle constructor takes x,y coordinates of the center as well as the rectangle width and
        // height.
        //
        public Rectangle(int x, int y, int width, int height) : base(x, y)
        {
            this.width  = width;
            this.height = height;
        }

        // Public width property.
        //
        public int Width
        {
            get { return width; }
            set { width = value;
                  OnPropertyChanged(); }
        }

        // Public height property.
        //
        public int Height
        {
            get { return height; }
            set { height = value;
                  OnPropertyChanged(); }
        }

        // Override the "Draw" method.
        //
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Width = ({0})", width);
            Console.WriteLine("Height = ({0})", height);
        }
    }

    class Program
    {
        static void Main()
        {
            // Create a "Circle" and "Draw" it to the console.
            //
            Circle circle = new Circle(12, 13, 100);
            Console.WriteLine("Initial circle created.");
            circle.Draw();

            // Now add an event handler. Notice several things here:
            //
            // 1. Instead of putting the event handling code into a method, the "delegate" keyword is
            //    used which allows us to put the code "inline." This saves us from having to create a
            //    separate method for a simple event handling method and also saves us from creating a
            //    delegate object.
            //
            // 2. The "delegate" keyword is followed by a parameter list which must exactly match the
            //    parameter list of the delegate type (in this case ... "Action<Shape>").
            //
            circle.PropertyChanged += delegate(Shape s) { s.Draw(); };

            // Now change one of the properties on the circle object and watch the event fire (resulting
            // in drawing the shape data to the console).
            //
            Console.WriteLine("\nChanging the circle radius property.");
            circle.Radius = 75;

            // The next few lines are included only to point out an oddity when working with anonymous
            // methods.
            //
            // Use the "-=" operator to try and remove the event handler from the circle. Unlike regular
            // event handlers, handlers using anonymous methods CANNOT be removed from an object using
            // the "-=" operator. Compiling this code will not, however, generate a compiler error (which
            // has me puzzled). So you might think you're good but notice after our attempt to remove the
            // handler that the circle still prints if any properties are changed.
            //
            circle.PropertyChanged -= delegate(Shape s) { s.Draw(); };
            Console.WriteLine("\nAttempted to remove event handler.");
            Console.WriteLine("Changing the circle radius again.");
            circle.Radius = 50;

            // Another idiosyncrasy of anonymous methods.
            //
            // One exception to the rule that the parameter list must match exactly is that you can omit
            // the parameter list entirely. If your event handling code doesn't need the parameters, you
            // can leave the parameter list off.
            //
            // In this particular sample, create a rectangle object and set an event handler. Leave off
            // the parameter list and instead of using the incoming shape to call the "Draw" method,
            // simply use the local "rect" variable. This shows a feature of anonymous methods in that
            // they have full access to the variables that are in the local scope when the event handler
            // is set.
            //
            Rectangle rect = new Rectangle(10, 10, 600, 450);
            rect.PropertyChanged += delegate { rect.Draw(); };
            Console.WriteLine("\nInitial rectangle created.");
            rect.Draw();

            // Now change the height property on the rectangle and watch the data print to the console.
            //
            Console.WriteLine("\nChanging the rectangle height.");
            rect.Height = 400;

            // Wait for <enter> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
