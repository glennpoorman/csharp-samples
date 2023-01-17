// ------------------------------------------------------------------------------------------------------
// MoreEvents
//
// More event handling in C#.
//
// The "Events" example showed the basics of event handling in C#. In addition to the mechanics though,
// C# provides some base types for event handling delegates and their arguments. These are especially
// useful to know about if you plan on getting into any Windows Forms or WPF programming as these are
// used all over those frameworks.
//
// This sample uses the same shape classes from the "Events" sample but makes use of some existing C#
// types to do the event handling.
//
// For starters, the custom delegate type can be removed and the pre-defined "EventHandler" delegate can
// be used instead. That delegate is defined in the "System" namespace as follows:
//
//     public delegate void EventHandler(object sender, EventArgs args);
//
// The signature of this event handler has a "sender" parameter which is the object firing the event. The
// second argument is the event arguments.
// ------------------------------------------------------------------------------------------------------

using System;

namespace MoreEvents
{
    // "EventArgs" serves as a base class for event arguments. Here we derive from it creating our own
    // event arguments for the "PropertyChanged" event. In our case, the only argument will be the name
    // of the property that has changed.
    //
    public class PropertyEventArgs : EventArgs
    {
        // Define the property name as a read-only auto-implemented property.
        //
        public string Name { get; }

        // Args constructor requires the property name.
        //
        public PropertyEventArgs(string name)
        {
            Name = name;
        }
    }

    // The "Point" struct.
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
        // Declare the event using the "System.EventHandler" delegate type.
        //
        public event EventHandler PropertyChanged;

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

        // Center point property. Note that the call to "OnPropertyChange" has been modified to take a
        // string containing the name of the property.
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

        // This method fires the event. In addition to the call, we also create and pass along a
        // "PropertyEventArgs" object which is, in turn, passed to the event handlers.
        //
        protected void OnPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyEventArgs(name));
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

        // Circle radius field. Just as with the shape, we're going back to using a private field as the
        // property implementation must now fire an event where appropriate.
        //
        private int radius;

        // Public radius property. Again the property name is now passed to "OnPropertyChanged".
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
        // Define a static event handler method that writes a message to the console that a shape has
        // changed and write the name of the property that has changed.
        //
        private static void NotifyShapeChanged(object sender, EventArgs args)
        {
            PropertyEventArgs p = (PropertyEventArgs)args;

            Type type = sender.GetType();

            Console.WriteLine("The {0} property on an object of type {1} has changed.", p.Name, type);
        }

        // Define another static event handler method that simply calls the draw method on the shape that
        // has changed.
        //
        // Note that the shape itself is now coming in the the "sender" parameter and has to be cast.
        //
        private static void DrawShapeOnChange(object sender, EventArgs args)
        {
            Shape s = (Shape) sender;

            s.Draw();
        }

        static void Main()
        {
            // Create a circle. Add the event handler so that a message is printed to the console
            // whenever a property on the circle changes.
            //
            Console.WriteLine("\nCreate a circle, add an event handler, change the radius.");
            Circle c = new (new Point(20, 21), 10);
            c.PropertyChanged += NotifyShapeChanged;
            c.Radius = 15;

            // Add an additional event handler on the circle that draws the circle data and change the
            // radius again. Both handlers will be called.
            //
            Console.WriteLine("\nAdd additional event handler and change the radius again.");
            c.PropertyChanged += DrawShapeOnChange;
            c.Radius = 11;

            // Create a rectangle. Add both of the event handlers and then change the rectangle width.
            // Both handlers will be called.
            //
            Console.WriteLine("\nCreate a rectangle, add both event handlers, change the width.");
            Rectangle r = new(new Point(11, 12), 150, 100);
            r.PropertyChanged += NotifyShapeChanged;
            r.PropertyChanged += DrawShapeOnChange;
            r.Width = 175;

            // Now remove one of the event handlers. Change the rectangle height and note that only the
            // remaining event handler is called.
            //
            Console.WriteLine("\nRemove one event handler from the rectangle, change the height.");
            r.PropertyChanged -= DrawShapeOnChange;
            r.Height = 110;
        }
    }
}
