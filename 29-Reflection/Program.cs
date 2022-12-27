// ------------------------------------------------------------------------------------------------------
// Reflection
//
// Reflection in C# enables developers to obtain information about loaded assemblies and types defined
// within them.
//
// In this sample, we use reflection to print out the name of all the public members of our shape class
// types. Watch the output carefully though and notice that you're probably getting more than you would
// expect. For example, our declaration of a property "X" on the class "Shape" will not only result in
// printing of information for a property called "X" but it will also result in printing of information
// for methods called "set_X" and "get_X" which are the property accessors. Similarly, information for
// the "PropertyChanged" event also generates information for methods "add_PropertyChanged" and
// "remove_PropertyChanged".
// ------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace Reflection
{
    // Define the base "Shape" class.
    //
    public class Shape
    {
        // The x and y coordinates of the shape center.
        //
        private int x;
        private int y;

        // Declare an event called "PropertyChanged".
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
        // Static method takes in a "Type" object and writes information about all of the public members
        // associated with that type to the console. "Type" objects are availabe from any type in C# by
        // using the "typeof" operator or by calling "GetType" on an instance.
        //
        private static void ShowClassInfo(Type type)
        {
            Console.WriteLine("\nMember info for type {0}", type);

            // Call "GetMembers" on the "Type" object to fetch an array of "MemberInfo" objects, one for
            // each public member.
            //
            // NOTE1: "MemberInfo" is defined in System.Reflection and provides a lot of information on
            //        a single member.
            //
            // NOTE2: "GetMembers" also has an overload that allows for specification on how and which
            //        members to fetch.
            //
            // NOTE3: You can also call "GetMember(string)" specifying the name of the member to fetch
            //        info for.
            //
            // NOTE4: You can filter on member type by calling methods such as "GetProperty",
            //        "GetProperties", "GetMethod", "GetMethods", "GetEvent", "GetEvents", etc.
            //
            MemberInfo[] infoArray = type.GetMembers();
            foreach (MemberInfo info in infoArray)
                Console.WriteLine("  {0,11} - {1}", info.MemberType, info.Name);
        }

        public static void Main()
        {
            // Call the static method to show member information for the class "Shape".
            //
            Console.Write("\nHit <ENTER> to see Shape members: ");
            Console.ReadLine();
            ShowClassInfo(typeof(Shape));

            // Call the static method to show member information for the class "Circle".
            //
            Console.Write("\nHit <ENTER> to see Circle members: ");
            Console.ReadLine();
            ShowClassInfo(typeof(Circle));

            // Call the static method to show member information for the class "Rectangle".
            //
            Console.Write("\nHit <ENTER> to see Rectangle members: ");
            Console.ReadLine();
            ShowClassInfo(typeof(Rectangle));

            // Note that this also works on the builtin types. Call the static method to show member
            // information for the "bool" type.
            //
            Console.Write("\nHit <ENTER> to see bool members: ");
            Console.ReadLine();
            ShowClassInfo(typeof(bool));

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}