// ------------------------------------------------------------------------------------------------------
// Lambda
//
// Lambda expressions in C#.
//
// In C# 3, anonymous methods were essentially deprecated and replaced by lambda expressions. These are
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

        // The "NameChanged" event.
        //
        public event Action<string, string> NameChanged;

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

        // Optional shape name property.
        //
        private string name;

        // Optiona shape name property.
        //
        // Note that setting the name property fires off both a property change event (since this is a
        // property) and also a name change event. Not a very realistic sample but for our purposes it
        // will suffice.
        //
        public string Name
        {
            get { return name; }
            set
            {
                string before = name;
                name = value;
                OnNameChanged(before, name);
                OnPropertyChanged("Name");
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

        // Proteced method fires the "NameChanged" event.
        //
        protected void OnNameChanged(string before, string after)
        {
            NameChanged?.Invoke(before, after);
        }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw()
        {
            if (!string.IsNullOrEmpty(Name))
                Console.WriteLine($"Name = {name}");

            Console.WriteLine(this);
        }
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

            // Now add a property change event handler. Notice several things here:
            //
            // 1. We use a lambda expression to add the event handling code "inline." This saves us from
            //    having to create a separate method for a simple event handling method and also saves us
            //    from creating a delegate object.
            //
            // 2. In the lambda expression, we start with the parameter list followed by the into "=>"
            //    syntax and then the actual code. The parameter list must exactly match the parameter
            //    list of the delegate type (in this case ... "Action<Shape,string>").
            //
            circle.PropertyChanged += (s, p) =>
            {
                Console.WriteLine($"Circle property {p} has changed.");
                s.Draw();
            };

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
            circle.PropertyChanged -= (s, p) =>
            {
                Console.WriteLine($"Circle property {p} has changed.");
                s.Draw();
            };
            Console.WriteLine("\nAttempted to remove event handler.");
            Console.WriteLine("Changing the circle radius again.");
            circle.Radius = 50;

            // Now create a rectangle and give it a name.
            //
            Rectangle rect = new()
            {
                Center = new Point() { X = 11, Y = 12 },
                Width = 100,
                Height = 50,
                Name = "Rect1"
            };
            Console.WriteLine("\nRectangle created.");
            rect.Draw();

            // Now add a property change event handler just like we did on the circle.
            //
            // Note that in this case we ignore the "s" and "p" parameters and reference the "rect"
            // variable directly. Note that in the "inline" code we can reference any variables that are
            // currently in scope.
            //
            rect.PropertyChanged += (s, p) => rect.Draw();

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
            rect.NameChanged += (b, a) =>
            {
                if (b != a)
                {
                    if (string.IsNullOrEmpty(b))
                        Console.WriteLine($"Added name \"{a}\"");
                    else if (string.IsNullOrEmpty(a))
                        Console.WriteLine($"Cleared name \"{b}\"");
                    else
                        Console.WriteLine($"Changed \"{b}\" to \"{a}\"");
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
        }
    }
}
