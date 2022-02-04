// ------------------------------------------------------------------------------------------------------
// MoreGenerics.cs
//
// In addition to the ability to create generic classes, C# also provides many pre-defined generic
// classes. These are mostly collections but also come in the form of delegates.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace MoreGenerics
{
    // Define the base "Shape" class (back to the old non-generic class).
    //
    public class Shape
    {
        // The x and y coordinates of the shape center.
        //
        private int x;
        private int y;

        // Declare an event called "PropertyChanged". Instead of using a delegate of our own making, this
        // time we'll use a pre-defined generic delegate defined in the System namespace. There are
        // actually several forms of the "Action" delegate pre-defined. All of these delegates return
        // void.
        //
        //     Action        - defines a delegate taking no parameters.
        //     Action<T>     - defines a delegate taking 1 parameter.
        //     Action<T1,T2> - defines a delegate taking 2 parameter.
        //        :
        //        :
        //     Action<T1,T2 ... T16> - taking 16 parameters.
        //
        // Other delegates to look into defined in System are "Predicate" and "Func" (along with all of
        // their variations).
        //
        // In this case, we'll use the form of "Action" that takes 2 parameters. The first parameter will
        // be the "Shape" that the property changed on and the second will be a string representing the
        // name of the property that changed.
        //
        public event Action<Shape,string> PropertyChanged;

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

        // This method fires the event.
        //
        // NOTE: We used the "Action<T1,T2>" delegate and declared that the two parameters be a "Shape"
        //       reference and a string. For the first parameter we pass this shape. For the second we
        //       pass the incoming property name.
        //
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, name);
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

        // Public width property.
        //
        public int Width
        {
            get { return width; }
            set { width = value;
                  OnPropertyChanged("Width"); }
        }

        // Public height property.
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

        public static void Main()
        {
            Console.WriteLine("\nCreate a list of shapes and add an event handler for each one.");

            // Create a collection to hold shapes. This collection is defined in the
            // System.Collections.Generic namespace. Other collections from that namespace include:
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
            List<Shape> shapeList = new List<Shape>();

            // Add some shapes to the list.
            //
            shapeList.Add(new Shape(0,0));
            shapeList.Add(new Circle(10,10,50));
            shapeList.Add(new Rectangle(-10,-10,50,25));
            shapeList.Add(new Circle(20,20,5));
            shapeList.Add(new Rectangle(10,10,10,10));

            // Cycle through the shapes in the list (note the "foreach" loop usage on the list) and add
            // the event handler to each shape in the list.
            //
            foreach (Shape s in shapeList)
                s.PropertyChanged += NotifyShapeChanged;

            // Cycle through the list again and increment the X coordinate of each shape by 10. Watch the
            // event handler fire for each shape.
            //
            Console.WriteLine("\nIncrement the X coordinate of each shape.");
            foreach (Shape s in shapeList)
                s.X += 10;

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}