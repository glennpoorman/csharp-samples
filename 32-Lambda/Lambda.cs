// ------------------------------------------------------------------------------------------------------
// Lambda.cs
//
// Lambda expressions in C#.
//
// In C# 3.0, anonymous methods were essentially deprecated and replaced by lambda expressions. These are
// a much more syntactically attractive way to specify delegate code inline.
//
// Like anonymous methods, lambda expressions allow you to put code "inline" when setting an event
// handler or passing a delegate in a method call.
//
// Consider the following syntax from the "Anonymous" sample.
//
//     circle.PropertyChanged += delegate(Shape s) { s.Draw(); };
//
// The keyword "delegate" is required here as well as the full parameter list. Using a lambda expression,
// the same line can be re-written as follows.
//
//     circle.PropertyChanged += s => s.Draw();
//
// Some things to note are the removal of the "delegate" keyword, the "into" syntax "=>", and the removal
// of the redundant type specification on the parameter list.
//
// In the parameter list, we also removed the parenthesis. That is optional though and only applies when
// the delegate takes a single parameter. If the delegate took no parameters or more than one, the
// parenthesis would need to be there.
//
// Consider a delegate that takes no parameters.
//
//     object.property += () => some_code();
//
// Or a delegate that takes two parameters.
//
//     object.property += (a,b) => some_code();
//
// Also note that we were able to remove the curly braces but only because in this particular case, the
// inline code was only one line. Additional lines of code would require the curly braces.
//
//     object.property += (p) => { some_code1();
//                                 some_code2(); };
//
// This sample mostly copies the "Anonymous" sample but adds an additional event handler just to show
// differing parameter lists.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Lambda
{
    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y coordinates of the shape center.
        //
        private int x;
        private int y;

        // An optional user-specified shape name.
        //
        private string name;

        // The "PropertyChanged" event member.
        //
        public event Action<Shape> PropertyChanged;

        // The "NameChanged" event member.
        //
        public event Action<string,string> NameChanged;

        // First public constructor takes no arguments and initializes the fields to zero/null.
        //
        public Shape()
        {}

        // Second public constructor takes input parameters for x and y and assigns them to the data
        // fields. The name remains empty.
        //
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // Shape name property.
        //
        // Note that setting the name property fires off both a property change event (since this is a
        // property) and also a name change event. Not a very realistic sample but for our purposes it
        // will suffice.
        //
        public string Name
        {
            get { return name; }
            set { string before = name;
                  name = value;
                  OnNameChanged(before, name);
                  OnPropertyChanged(); }
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

        // Proteced method fires the "NameChanged" event.
        //
        protected void OnNameChanged(string before, string after)
        {
            if (NameChanged != null)
                NameChanged(before, after);
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
            if (!string.IsNullOrEmpty(name))
                Console.WriteLine("Name = {0}", name);
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

            // Now add a property change event handler. Notice several things here:
            //
            // 1. We use a lambda expression to add the event handling code "inline." This saves us from
            //    having to create a separate method for a simple event handling method and also saves us
            //    from creating a delegate object.
            //
            // 2. In the lambda expression, we start with the parameter list followed by the into "=>"
            //    syntax and then the actual code. The parameter list must exactly match the parameter
            //    list of the delegate type (in this case ... "Action<Shape>").
            //
            circle.PropertyChanged += s => s.Draw();

            // Now change one of the properties on the circle object and watch the event fire (resulting
            // the shape data to the console).
            //
            Console.WriteLine("\nChanging the circle radius property.");
            circle.Radius = 75;

            // The next few lines are included only to point out an oddity that that exists when working
            // with both anonymous methods and lambda expressions.
            //
            // Use the "-=" operator to try and remove the event handler from the circle. Unlike regular
            // event handlers, handlers using lambda expressions CANNOT be removed from an object using
            // the "-=" operator. Compiling this code will not, however, generate a compiler error (which
            // has me puzzled). So you might think you're good but notice after our attempt to remove the
            // handler that the circle still prints if any properties are changed.
            //
            circle.PropertyChanged -= s => s.Draw();
            Console.WriteLine("\nAttempted to remove event handler.");
            Console.WriteLine("Changing the circle radius again.");
            circle.Radius = 50;

            // Now create a rectangle and give it a name.
            //
            Rectangle rect = new Rectangle(11, 12, 100, 50) { Name = "Rect1" };
            Console.WriteLine("\nRectangle created.");
            rect.Draw();

            // Now add a property change event handler just like we did on the circle.
            //
            // NOTE: In this case we ignore the "s" parameter and reference the "rect" variable directly.
            //       Note that in the "inline" code we can reference any variables that are currently in
            //       scope.
            //
            rect.PropertyChanged += s => rect.Draw();

            // Now add a name change event handler. The delegate for this event has two parameters that
            // are both strings (as was designated in the delegate). The first "b" is the name before the
            // change and the second "a" is the name after the change.
            //
            // Note here that we have both parameters and that our inline code is quite a bit more
            // involved than the simple one-liner that we put on the property change.
            //
            // In the inline code, print the name change choosing the language of the message depending
            // on if either or neither of the strings are empty.
            //
            rect.NameChanged += (b,a) =>
            {
                if (b != a)
                {
                    if (string.IsNullOrEmpty(b))
                        Console.WriteLine("Added name \"{0}\"", a);
                    else if (string.IsNullOrEmpty(a))
                        Console.WriteLine("Cleared name \"{0}\"", b);
                    else
                        Console.WriteLine("Changed \"{0}\" to \"{1}\"", b, a);
                }
            };

            // Now change the rectangle name and watch the name change event fire first followed by the
            // property change event.
            //
            Console.WriteLine("\nChanging the rectangle name.");
            rect.Name = "Rect2";

            // Change the rectangle name one more time setting the name string to null. Watch both events
            // fire again and note the language of the message on the name change event.
            //
            Console.WriteLine("\nChanging the rectangle name one more time.");
            rect.Name = null;

            // Wait for <enter> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
