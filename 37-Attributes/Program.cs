// ------------------------------------------------------------------------------------------------------
// Attributes
//
// Attributes in C#.
//
// Attributes are a generalized way that C# allows you to add user defined declarative information to
// program entities that can be retrieved at runtime.
//
// This sample applies a "Description" attribute to each of our shape classes and also applies the same
// attribute to the the public members. The attribute can also optionally take a "Url" property which
// directs you to a ficticious website containing more detailed information on the classes.
//
// With the custom attributes applied, the sample then queries each class type for the custom attribute
// and writes the attribute data to the console. The sample also uses reflection to fetch all of the
// public members from the classes and print out information for those public members that have one of
// the custom attributes attached.
//
// Note in the output of this sample, that we only print the info on those members where we explicitly
// attached a custom attribute. That means that the "extras" that printed in the previous sample do not
// print in this sample.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Reflection;

namespace Attributes
{
    // To attach user defined attributes, you must first define the attribute class. Atribute classes
    // must derive directly or indirectly from the System.Attribute class.
    //
    // Here we define a class that will contain descriptive information that can be attached to program
    // entities. The attribute class will contain a string which will contain the text of the description.
    //
    // The attribute class will also contain an optional URL string that will contain a link to a
    // fictitious webpage that has more help on the entity in question.
    //
    public class DescriptionAttribute : Attribute
    {
        // Description text is a read-only string property.
        //
        public string Text { get; }

        // Url string property.
        //
        public string Url { get; set; }

        // Public constructor takes the description text as an input parameter.
        //
        public DescriptionAttribute(string text) => Text = text;
    }

    // The "Point" struct.
    //
    // Just before the class declaration, attach the attribute by placing the attribute name in between
    // square brackets. Please note the following:
    //
    // 1. C# adds a small twist in that if your attribute class ends with the word "Attribute", you can
    //    leave that part off which is why we just call the attribute "Description" here. This isn't
    //    mandatory. You can call your attribute class anything you want.
    //
    // 2. Required constructor parameters (in this case, the description) are put in between the open and
    //    close parenthesis just like a regular constructor call.
    //
    //        [Description("Descriptive Text")]
    //
    //    In addition you can also specify optional properties using the property name separated from the
    //    constructor parameters by a comma.
    //
    //        [Description("Descriptive Text"), Url="Url Text")]
    //
    //    Note that this is similar, but not exactly like property initialization.
    //
    [Description("2D Point class", Url ="https://www.shapes.com/help/point.html")]
    public struct Point
    {
        // X coordinate property.
        //
        // Like the class itself, we can attach attributes to individual members by declaring the
        // attribute just before the property/method/etc declaration. Declare an attribute describing
        // the X property. Note that we left off the "Url" property here. In this example, we'll reserve
        // that property for the types themselves but not their individual members.
        //
        [Description("The point X coordinate")]
        public int X { get; init; }

        // Y coordinate property.
        //
        [Description("The point Y coordinate")]
        public int Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        [Description("Convert the point data into a string")]
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class.
    //
    [Description("Base Shape class", Url="https://www.shapes.com/help/shape.html")]
    public class Shape
    {
        // The "PropertyChanged" event.
        //
        [Description("Event fires when any shape property is changed")]
        public event Action<Shape, string> PropertyChanged;

        // The shape center field.
        //
        private Point center;

        // Center point property.
        //
        [Description("The Shape center point property")]
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
        [Description("Convert the point data into a string")]
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        [Description("Shape draw method")]
        public virtual void Draw()
        {
            Console.WriteLine(this);
        }
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    [Description("Simple Circle class", Url="https://www.shapes.com/help/circle.html")]
    public class Circle : Shape
    {
        // Circle radius field.
        //
        private int radius;

        // Public radius property.
        //
        [Description("Circle radius property")]
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
        [Description("Convert the Circle data into a string")]
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    [Description("Simple Rectangle class", Url="https://www.shapes.com/help/rectangle.html")]
    public class Rectangle : Shape
    {
        // Rectangle width/height fields (again going back to fields).
        //
        private int width;
        private int height;

        // Public width property.
        //
        [Description("Rectangle width property")]
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
        [Description("Rectangle height property")]
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
        [Description("Convert the Rectangle data into a string")]
        public override string ToString() => $"{base.ToString()}, Width = ({Width}), Height = ({Height})";
    }

    class Program
    {
        // Static method takes in a "Type" object and looks to see if the type has one of our custom
        // attributes attached to it and writes the attribute information to the console. The method then
        // iterates through the public members on the type and writes out information for any member that
        // has one of our custom attributes attached to it.
        //
        private static void ShowClassInfo(Type type)
        {
            Console.WriteLine($"\nMember info for type {type}");

            // Using the static "GetCustomAttribute" method defined on the "Attribute" class, query the
            // type specified in the first parameter to see if it contains an attribute of the type
            // specified in the second parameter. If so, then the attribute is returned.
            //
            var a1 = Attribute.GetCustomAttribute(type, typeof(DescriptionAttribute)) as DescriptionAttribute;

            // If we found one of our attributes, write the text from the attribute to the console. If
            // the optional Url string is found, write that out as well.
            //
            if (a1 != null)
            {
                Console.WriteLine($"Description: {a1.Text}");
                if (!string.IsNullOrEmpty(a1.Url))
                    Console.WriteLine($"Url: {a1.Url}");
            }

            // Call "GetMembers" on the "Type" object to fetch an array of "MemberInfo" objects, one for
            // each public member.
            //
            MemberInfo[] infoArray = type.GetMembers();
            foreach (MemberInfo info in infoArray)
            {
                // Again use "GetCustomAttribute" to see if one of our custom attributes is attached to
                // the member type.
                //
                var a2 = Attribute.GetCustomAttribute(info, typeof(DescriptionAttribute)) as DescriptionAttribute;

                // If we found one, write the member info and the attribute info to the console.
                //
                if (a2 != null)
                {
                    Console.WriteLine($"\n  {info.MemberType} - {info.Name}");
                    Console.WriteLine($"  Description: {a2.Text}");
                    if (!string.IsNullOrEmpty(a2.Url))
                        Console.WriteLine($"  Url: {a2.Url}");
                }
            }
        }

        static void Main()
        {
            // Call the static method to show member information and attributes for the struct "Point".
            //
            Console.Write("\nHit <ENTER> to see Point members: ");
            Console.ReadLine();
            ShowClassInfo(typeof(Point));

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
        }
    }
}