// ------------------------------------------------------------------------------------------------------
// Anonymous
//
// Anonymous methods in C#.
//
// C# 2 introduced the ability to put code "inline" when setting an event handler using what they call
// "anonymous methods."
//
// This sample will show this functionality using the shapes classes from the "Events" sample just to
// keep things simple.
//
// PLEASE NOTE that as of C# 3, anonymous methods are more or less deprecated and replaced with lambda
// expressions. They are still supported and still compile but lambda expressions provide a much nicer
// way to achieve the same results.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Anonymous
{
    // The "Point" struct.
    //
    public struct Point
    {
        // The X and Y auto-implemented properties of the point coordinates.
        //
        public int X { get; init; }
        public int Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class.
    //
    public class Shape
    {
        // The "PropertyChanged" event.
        //
        public event Action<Shape, string> PropertyChanged;

        // The shape center field.
        //
        private Point center;

        // Center point property.
        //
        public Point Center
        {
            get => center;
            set
            {
                center = value;
                OnPropertyChanged("Center");
            }
        }

        // This method fires the event. Note that event now has the specific signature of a shape
        // reference and a string so we can go ahead and call it without having to create any additional
        // event arguments.
        //
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, name);
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle radius field.
        //
        private int radius;

        // Public radius property.
        //
        public int Radius
        {
            get => radius;
            set
            {
                radius = value;
                OnPropertyChanged("Radius");
            }
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Rectangle width/height fields (again going back to fields).
        //
        private int width;
        private int height;

        // Public width property.
        //
        public int Width
        {
            get => width;
            set
            {
                width = value;
                OnPropertyChanged("Width");
            }
        }

        // Public height property. Again the "set" accessor has been modified to call "OnPropertyChanged"
        // from the base class firing off an event.
        //
        public int Height
        {
            get => height;
            set
            {
                height = value;
                OnPropertyChanged("Height");
            }
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Width = ({Width}), Height = ({Height})";
    }

    class Program
    {
        static void Main()
        {
            // Create a "Circle" and "Draw" it to the console.
            //
            Circle circle = new()
            {
                Center = new Point() { X = 12, Y = 13 },
                Radius = 100
            };
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
            circle.PropertyChanged += delegate(Shape s, string p)
            {
                Console.WriteLine($"Circle property {p} has changed.");
                s.Draw();
            };

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
            circle.PropertyChanged -= delegate(Shape s, string p)
            {
                Console.WriteLine($"Circle property {p} has changed.");
                s.Draw();
            };
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
            Rectangle rect = new()
            {
                Center = new Point() { X = 10, Y = 10 },
                Width = 600,
                Height = 450
            };
            rect.PropertyChanged += delegate
            {
                Console.WriteLine("Rectangle property has changed.");
                rect.Draw();
            };
            Console.WriteLine("\nInitial rectangle created.");
            rect.Draw();

            // Now change the height property on the rectangle and watch the data print to the console.
            //
            Console.WriteLine("\nChanging the rectangle height.");
            rect.Height = 400;
        }
    }
}
