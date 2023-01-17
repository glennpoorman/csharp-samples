// ------------------------------------------------------------------------------------------------------
// MoreGenerics
//
// In addition to the ability to create generic classes, C# also provides many pre-defined generic
// classes. These are mostly collections but also come in the form of delegates.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MoreGenerics
{
    // The "Point" struct (back to the old non-generic version).
    //
    public struct Point
    {
        // Public constructor takes input parameters for x and y and assigns them to the properties.
        //
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        // The X and Y auto-implemented properties of the shape center.
        //
        public int X { get; }
        public int Y { get; }

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
        // Other delegates in the "System" namespace worth looking into are "Predicate" and "Func" (along
        // with all of their variations).
        //
        // In this case, we'll use the form of "Action" that takes 2 parameters. The first parameter will
        // be the "Shape" that the property changed on and the second will be a string representing the
        // name of the property that changed.
        //
        public event Action<Shape, string> PropertyChanged;

        // First public constructor takes no arguments and initializes the x and y properties to zero.
        //
        public Shape()
        { }

        // Second public constructor takes a "Point" object to represent the shape center.
        //
        public Shape(Point center) => Center = center;

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

        // Virtual "Draw" method. We've simplified this method back to its previous version that took
        // no arguments.
        //
        public virtual void Draw()
        {
            Console.WriteLine(this);
        }
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Circle constructor takes a circle center point as well as the circle radius.
        //
        public Circle(Point center, int radius)
            : base(center) => Radius = radius;

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
        // Rectangle constructor takes a center point as well as the rectangle width and height.
        //
        public Rectangle(Point center, int width, int height)
            : base(center)
        {
            Width = width;
            Height = height;
        }

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
            Console.WriteLine("\nShape property {0} has changed.", propertyName);
            sender.Draw();
        }

        static void Main()
        {
            Console.WriteLine("\nCreate a list of shapes and add an event handler for each one.");

            // Create a generic list collection to hold any number of shape references. This collection
            // is defined in the "System.Collections.Generic" namespace. Other collections from that
            // namespace include:
            //
            //     Dictionary<>
            //     HashSet<>
            //     LinkedList<>
            //     Queue<>
            //     SortedDictionary<>
            //     SortedList<>
            //     SortedSet<>
            //     Stack<>
            //
            List<Shape> shapeList = new();

            // Add some shapes to the list.
            //
            shapeList.Add(new Shape());
            shapeList.Add(new Circle(new Point(10, 10), 50));
            shapeList.Add(new Rectangle(new Point(-10, -10), 50, 25));
            shapeList.Add(new Circle(new Point(20, 200), 5));
            shapeList.Add(new Rectangle(new Point(10, 10), 10, 10));

            // Cycle through the shapes in the list (note the "foreach" loop usage on the list) and add
            // the event handler to each shape in the list.
            //
            foreach (Shape s in shapeList)
                s.PropertyChanged += NotifyShapeChanged;

            // Cycle through the list again and change the center point for each shape. Watch the event
            // handler fire for each shape that is changed.
            //
            Console.WriteLine("\nChange the center point of each shape to (10, 10).");
            foreach (Shape s in shapeList)
                s.Center = new Point(10, 10);
        }
    }
}