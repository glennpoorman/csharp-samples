// ------------------------------------------------------------------------------------------------------
// Reflection
//
// Reflection in C# enables developers to obtain information about loaded assemblies and types defined
// within them. In this sample, we use reflection to print out the name of all the public members of our
// shape class types.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace Reflection
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

        // This method fires the event.
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
        public virtual void Draw()
        {
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

        // Public height property.
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
        // Static method takes in a "Type" object and writes information about all of the public members
        // associated with that type to the console. "Type" objects are availabe from any type in C# by
        // using the "typeof" operator directly on a type name or by calling "GetType" on an instance.
        //
        private static void ShowClassInfo(Type type)
        {
            Console.WriteLine($"\nMember info for type {type}");

            // Call "GetMembers" on the "Type" object to fetch an array of "MemberInfo" objects, one for
            // each public member. Please note the following:
            //
            // 1. "MemberInfo" is defined in System.Reflection and provides a lot of information on a
            //    single member.
            //
            // 2. "GetMembers" also has an overload that allows for specification on how and which
            //    members to fetch.
            //
            // 3. You can also call "GetMember(string)" specifying the name of the specific member to
            //    fetch info for.
            //
            // 4. You can filter on member type by calling methods such as "GetProperty",
            //    "GetProperties", "GetMethod", "GetMethods", "GetEvent", "GetEvents", etc.
            //
            MemberInfo[] infoArray = type.GetMembers();
            foreach (MemberInfo info in infoArray)
                Console.WriteLine($"  {info.MemberType,11} - {info.Name}");
        }

        static void Main()
        {
            // Call the static method to show member information for the struct "Point".
            //
            // Watch the output carefully and notice that you're probably getting more than you would
            // have expected. For example, the declaration of a property "X" will not only result in
            // printing of information for that property but it will also result in the printing of
            // informaton for methods called "set_X" and "get_X" which are the real property accessors
            // that are generally invisible to your code.
            //
            Console.Write("\nHit <ENTER> to see Point members: ");
            Console.ReadLine();
            ShowClassInfo(typeof(Point));

            // Call the static method to show member information for the class "Shape".
            //
            // Notice that, like the extra items that come along with properties, there are also extra
            // items that come along with events. Specifically, the "PropertyChanged" event will also
            // result in the printing of information for the methods "add_PropertyChanged" and
            // "remove_PropertyChanged". Like with the properties, these are the actual accessors that
            // are invisible to your code.
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
        }
    }
}
