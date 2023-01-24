// ------------------------------------------------------------------------------------------------------
// GenericsDelegates
//
// C# generic delegates.
//
// In addition to the generic collections, C# pre-defines several generic delegates that cover most uses
// which means there is rarely a reason to grow your own. We'll bring back some of the event handling for
// shape property changes in order to show uses of just a couple of these delegates.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MoreGenerics
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
        // Declare an event called "PropertyChanged". Instead of using a delegate of our own making or
        // even the "EventHandler" delegate, this time we'll use a pre-defined generic delegate "Action"
        // defined in the System namespace. There are actually several forms of the "Action" delegate
        // pre-defined. All of these delegates return void.
        //
        //     Action        - defines a delegate taking no parameters.
        //     Action<T>     - defines a delegate taking 1 parameter.
        //     Action<T1,T2> - defines a delegate taking 2 parameter.
        //        :
        //        :
        //     Action<T1,T2 ... T16> - taking 16 parameters.
        //
        // There is a similar pre-defined delegate "Func". The difference with the "Func" delegate is
        // that it also returns a value which is also a type parameter. The various forms of the "Func"
        // delegate a very much like the "Action" delegate except that the last type parameter is always
        // the return type.
        //
        //     Func<TResult>       - defines a function taking no parameters.
        //     Func<T,TResult>     - defines a function taking 1 parameter.
        //     Func<T1,T2,TResult> - defines a function taking 2 parameters.
        //        :
        //        :
        //     Func<T1,T2 ... T16,TResult> - taking 16 parameters.
        //
        // In this case, we'll use the form of "Action" that takes 2 parameters. The first parameter will
        // be the "Shape" that the property changed on and the second will be a string representing the
        // name of the property that changed.
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
        // Static event handler will write a message to the console that the property on a shape has
        // changed and will also draw the shape.
        //
        // NOTE: The parameter list of this method has to match the parameter list of our "Action"
        //       delegate.
        //
        private static void NotifyShapeChanged(Shape sender, string propertyName)
        {
            Console.WriteLine("Shape property {0} has changed.", propertyName);
            sender.Draw();
        }

        // Function takes in a shape and returns true if the shape center is at 0,0. This is an example
        // of a "System.Predicate" delegate. The definition of this delegate looks like:
        //
        //     delegate bool Predicate<in T>(T obj)
        //
        // Predicate delegates are used all over the place especially in generic collections for doing
        // searches.
        //
        private static bool ShapeAtOrigin(Shape shape) => (shape.Center.X == 0 && shape.Center.Y == 0);

        static void Main()
        {
            // Create a new circle. Set the event handler for property changes. Make calls to change the
            // circle center and radius and watch the property change event fire writing the shape data
            // to the console.
            //
            Circle c1 = new()
            {
                Center = new Point() { X = 11, Y = 12 },
                Radius = 15
            };
            Console.WriteLine($"Created {c1}");
            c1.PropertyChanged += NotifyShapeChanged;
            c1.Center = new Point() { X = 10, Y = 10 };
            c1.Radius = 23;

            // Create a new rectangle. Set the event handler for property changes. Make calls to change
            // the rectangle width and height and watch the property change event fire writing the shape
            // data to the console.
            //
            Rectangle r1 = new()
            {
                Center = new Point() { X = 20, Y = 25 },
                Width = 100,
                Height = 80
            };
            Console.WriteLine($"\nCreated {r1}");
            r1.PropertyChanged += NotifyShapeChanged;
            r1.Width = 125;
            r1.Height = 85;

            // Create a generic list and initialize it with several shape objects.
            //
            Console.WriteLine("\nCreate a list of shapes.");
            List<Shape> shapeList = new()
            {
                new Shape()
                {
                    Center = new Point() { X = 11, Y = 12 }
                },
                new Circle()
                {
                    Center = new Point() { X = 20, Y = 20 },
                    Radius = 2
                },
                new Rectangle()
                {
                    Height = 20,
                    Width = 30
                },
                new Circle()
                {
                    Center = new Point() { X = 1, Y = 2 },
                    Radius = 5
                },
            };

            // Call the generic list "Find" method. This method takes a "System.Predicate" delegate
            // function as input. The idea is that "Find" will iterate through the list calling the
            // specified function for each item in the list until one of the items returns true. At
            // that point, the function will stop and return that item. If no items return true, then
            // the function will return a null reference.
            // 
            Console.WriteLine("\nLook for a shape with its center at the origin.");
            Shape shape = shapeList.Find(ShapeAtOrigin);
            if (shape != null)
                Console.WriteLine($"Found one! {shape}");
        }
    }
}