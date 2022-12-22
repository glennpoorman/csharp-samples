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
            this.Name = name;
        }
    }

    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y coordinates of the shape center (fields).
        //
        private int x;
        private int y;

        // Declare the event using the "Sytem.EventHandler" delegate type.
        //
        public event EventHandler PropertyChanged;

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

        // X coordinate property. Note that the call to "OnPropertyChanged" has been modified to take a
        // string containing the name of the property.
        //
        public int X
        {
            get { return x; }
            set { x = value;
                  OnPropertyChanged("X"); }
        }

        // Y coordinate property.
        //
        public int Y
        {
            get { return y; }
            set { y = value;
                  OnPropertyChanged("Y"); }
        }

        // This method fires the event. In addition to the call, we also create and pass along a
        // "PropertyEventArgs" object which is, in turn, passed to the event handlers.
        //
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyEventArgs(name));
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

        // Public radius property. Again the "set" accessor has been modified to call "OnPropertyChanged"
        // from the base class firing off an event.
        //
        public int Radius
        {
            get { return radius; }
            set { radius = value;
                  OnPropertyChanged("Radius"); }
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

        // Public width property. Again the "set" accessor has been modified. to call "OnPropertyChanged"
        // from the base class firing off an event.
        //
        public int Width
        {
            get { return width; }
            set { width = value;
                  OnPropertyChanged("Width"); }
        }

        // Public height property. Again the "set" accessor has been modified. to call "OnPropertyChanged"
        // from the base class firing off an event.
        //
        public int Height
        {
            get { return height; }
            set { height = value;
                  OnPropertyChanged("Height"); }
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

    public class Program
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
        private static void DrawShapeOnChange(object sender, EventArgs args)
        {
            Shape s = (Shape) sender;

            s.Draw();
        }

        public static void Main()
        {
            // Create a circle. Add the event handler so that a message is printed to the console
            // whenever a property on the circle changes.
            //
            Console.WriteLine("\nCreate a circle, add an event handler, change the radius.");
            Circle c = new Circle(20, 21, 10);
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
            Rectangle r = new Rectangle(11, 12, 150, 100);
            r.PropertyChanged += NotifyShapeChanged;
            r.PropertyChanged += DrawShapeOnChange;
            r.Width = 175;

            // Now remove one of the event handlers. Change the rectangle height and note that only the
            // remaining event handler is called.
            //
            Console.WriteLine("\nRemove one event handler from the rectangle, change the height.");
            r.PropertyChanged -= DrawShapeOnChange;
            r.Height = 110;

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
