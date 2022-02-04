// ------------------------------------------------------------------------------------------------------
// Events.cs
//
// Event handling in C#.
//
// C# builds event handling right into the language. Here the "Shape" class is modified along with the
// derived "Circle" and "Rectangle" classes to fire off an event whenever data on a shape object is
// changed. That event is used to "Draw" the shape.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Events
{
    // Declare a delegate type that will be used for our events. Events are essentially special forms of
    // delegates as we'll see when we declare the event inside of the class definition.
    //
    // NOTE: Microsoft defines several delegate types and many of them are generic so there is a good
    //       chance that you could write entire projects without ever having to define your own delegate
    //       types. For the purposes of these samples, we'll define our own to demonstrate how it works.
    //       For future reference though, see the "Action<T>", "Func<T>" and "Predicate" delegate types
    //       and think about how this sample could be re-written to use one of those.
    //
    public delegate void ChangeHandler(Shape s);

    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y coordinates of the shape center.
        //
        // Note that we're back to using fields instead of auto-implemented properties. That is because
        // our property implementations are now taking on the added tasks of firing events where
        // necessary.
        //
        private int x;
        private int y;

        // Declare an event called "PropertyChanged" that we will fire whenever a property on the shape
        // changes. Events are declared using the "event" keyword followed by a delegate type and the
        // event name. Here we will use the delegate type we declared up above.
        //
        // NOTE1: From the calling code, an event handler is added using the += operator and is removed
        //        using the -= operator.
        //
        //            Shape s = new Shape(1,2);
        //            s.PropertyChanged += MyDelegate; // Add handler.
        //            s.PropertyChanged -= MyDelegate; // Remove handler.
        //
        //        Note that only those operators can be used on an event and that unlike a delegate, you
        //        cannot assign to an event.
        //
        // NOTE2: So far this all looks very similar to working with delegates like we did in the
        //        previous sample and on the surface, it probably seems like the "event" keyword doesn't
        //        really add anything. As a matter of fact, in this particular sample you could remove
        //        the "event" keyword which would simply turn this item into a public delegate field and
        //        the program would still compile. What we are showing in this sample, however, is the
        //        simplest form of declaring an event.
        //
        //        Events provide greater control if you want it though. Suppose that, for whatever
        //        reason, you did not want to rely on simple delegate fields to store your event
        //        handlers. Suppose that you want to simply pass the setting or removing of event
        //        handlers to another object you're holding a reference to. Or suppose that you want to
        //        use a collection to hold your events. You can gain finer control over the setting and
        //        removing of your event handlers by using "add" and "remove" accessors. These are very
        //        similar to the "set" and "get" accessors on properties. As a matter of fact, you can
        //        think of the relationship from events to delegates as similar to the relationship from
        //        properties to fields.
        //
        //        To provide your own event handling functionality, you would use the "add" and "remove"
        //        event accessors as follows:
        //
        //           public event ChangeHandler PropertyChanged
        //           {
        //               add
        //               {
        //                   ... custom code to add event handler ...
        //               }
        //               remove
        //               {
        //                   ... custom code to remove event handler ...
        //               }
        //           }
        //
        //        From the caller's perspective, there is no difference. The adding and removing of event
        //        handlers is still done using the += operator (which calls "add") and the -= operator
        //        (which calls "remove").
        //
        public event ChangeHandler PropertyChanged;
 
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

        // X coordinate property. Here code is added to the "set" accessor to call "OnPropertyChanged"
        // (defined below) which fires an event.
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

        // This method fires the event.
        //
        // NOTE1: This code could have been put directly into the property "set" accessors above. Aside
        //        from the obvious desire to avoid repeating code, there is another reason to put this
        //        code in its own method. Event members cannot be accessed directly from derived classes
        //        except to add or remove event handlers. So in order to be able to call the delegate
        //        from a derived class, it is common practice to implement a protected member that can be
        //        called from the derived classes but won't be visible to the outside world.
        //
        // NOTE2: If no handlers are set for this event, the delegate object will be null. This is an
        //        acceptable and common scenario so we always to check for this before firing the event.
        //
        // NOTE3: Remember that delegates can be added together so it is possible that multiple event
        //        handlers have been set for this event. If that is the case, the single call below will
        //        result in a call to each of the event handlers that have been set.
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
        // Circle radius field. Just as with the shape, we're going back to using a private field as the
        // property implementation must now fire an event where appropriate.
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
        // Rectangle width/height fields (again going back to fields).
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
                  OnPropertyChanged(); }
        }

        // Public height property. Again the "set" accessor has been modified. to call "OnPropertyChanged"
        // from the base class firing off an event.
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

    public class Program
    {
        // Define a static event handler method that writes a message to the console that a shape has
        // changed.
        //
        private static void NotifyShapeChanged(Shape s)
        {
            Type type = s.GetType();

            Console.WriteLine("An object of type {0} has changed.", type);
        }

        // Define another static event handler method that simply calls the draw method on the shape that
        // has changed.
        //
        private static void DrawShapeOnChange(Shape s)
        {
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

            // Remove the other event handler from the rectangle. Change the rectangle width. Since there
            // are no event handlers left on the rectangle, note that there is no output that results
            // from the property change.
            //
            Console.WriteLine("\nRemove the other event handler from the rectangle, change the width.");
            Console.WriteLine("Note the absence of any output from the property change.");
            r.PropertyChanged -= NotifyShapeChanged;
            r.Width = 25;

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
