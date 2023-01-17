// ------------------------------------------------------------------------------------------------------
// Attributes
//
// Attributes in C#.
//
// Attributes are a generalized way that C# allows you to add user defined declarative information to
// program entities that can be retrieved at runtime.
//
// This sample applies a "Description" attribute to each of our shape classes and also applies the same
// attribute the the public members. The attribute can also optionally take a "Url" property which
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
using System.Collections.Generic;
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
        public DescriptionAttribute(string text)
        {
            this.Text = text;
        }
    }

    // Define the "Shape" class. In between the square brackets, attach the attribute.
    //
    // NOTE1: C# adds a small twist in that if your attribute class ends with the word "Attribute", you
    //        can leave that part off which is why we just call the attribute "Description". This isn't
    //        mandatory. You can call your attribute class anything you want.
    //
    // NOTE2: Required constructor parameters (in this case, the text) are put in between the open and
    //        close parenthesis just like a regular constructor call.
    //
    //            [Description("Descriptive Text")]
    //
    //        In addition you can also specify optional properties using the property name separated from
    //        the constructor parameters by a comma.
    //
    //            [Description("Descriptive Text", Url="Url Text")]
    //
    [Description("Base Shape class", Url="http://www.shapes.com/shape.html")]
    public class Shape
    {
        // The x and y coordinates of the shape center.
        //
        private int x;
        private int y;

        // Declare an event called "PropertyChanged".
        //
        // In addition to adding attributes to the class, you can also add attributes to individual
        // members. Here one of our custom attributes is added to the event member.
        //
        // NOTE: We've left off the optional "Url" property here. In this sample, we'll only use that
        //       property on the classes themselves.
        //
        [Description("Event fires when any shape property changes")]
        public event Action<Shape,string> PropertyChanged;

        // First public constructor takes no arguments and initializes the fields x and y to zero.
        //
        [Description("Default shape constructor")]
        public Shape()
        {}

        // Second public constructor takes input parameters for x and y and assigns them to the data
        // fields.
        //
        [Description("Construct shape with specified center point")]
        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        // X coordinate property.
        //
        [Description("Shape center X coordinate property")]
        public int X
        {
            get { return x; }
            set { x = value;
                  OnPropertyChanged("X"); }
        }

        // Y coordinate property.
        //
        [Description("Shape center Y coordinate property")]
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
        [Description("Convert shape data to string form")]
        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }

        // Virtual "Draw" method.
        //
        [Description("Shape draw method")]
        public virtual void Draw()
        {
            Console.WriteLine("Center = {0}", this);
        }
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    [Description("Circle class", Url="http://www.shapes.com/circle.html")]
    public class Circle : Shape
    {
        // Circle radius field.
        //
        private int radius;

        // Circle constructor takes x,y coordinates of the circle center as well as the circle radius.
        //
        [Description("Construct circle with specified radius and center")]
        public Circle(int x, int y, int radius) : base(x, y)
        {
            this.radius = radius;
        }

        // Public radius property.
        //
        [Description("Circle radius property")]
        public int Radius
        {
            get { return radius; }
            set { radius = value;
                  OnPropertyChanged("Radius"); }
        }

        // Override the "Draw" method.
        //
        [Description("Circle draw method")]
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Radius = ({0})", radius);
        }
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    [Description("Rectangle class", Url="http://www.shapes.com/rectangle.html")]
    public class Rectangle : Shape
    {
        // Rectangle width/height fields.
        //
        private int width;
        private int height;

        // Rectangle constructor takes x,y coordinates of the center as well as the rectangle width and
        // height.
        //
        [Description("Construct rectangle with specifed center, width, height")]
        public Rectangle(int x, int y, int width, int height) : base(x, y)
        {
            this.width  = width;
            this.height = height;
        }

        // Public width property.
        //
        [Description("Rectangle width property")]
        public int Width
        {
            get { return width; }
            set { width = value;
                  OnPropertyChanged("Width"); }
        }

        // Public height property.
        //
        [Description("Rectangle height property")]
        public int Height
        {
            get { return height; }
            set { height = value;
                  OnPropertyChanged("Height"); }
        }

        // Override the "Draw" method.
        //
        [Description("Rectangle draw method")]
        public override void Draw()
        {
            base.Draw();
            Console.WriteLine("Width = ({0})", width);
            Console.WriteLine("Height = ({0})", height);
        }
    }

    public class Program
    {
        // Static method takes in a "Type" object and looks to see if the type has one of our custom
        // attributes attached to it and writes the attribute information to the console. The method then
        // iterates through the public members on the type and writes out information for any member that
        // has one of our custom attributes attached to it.
        //
        private static void ShowClassInfo(Type type)
        {
            // Write the type info to the console.
            //
            Console.WriteLine("\nMember info for type {0}", type);

            // "GetCustomAttribute" is a static method on the Attribute class. Given the type of object
            // we want to query and also the type of the specific attribute we're looking for, this
            // method looks to see if any attributes that fit our criteria are attached. If so, the
            // attribute is returned.
            //
            DescriptionAttribute a1 = Attribute.GetCustomAttribute(type,
                typeof(DescriptionAttribute)) as DescriptionAttribute;

            // If we found one of our attributes, write the text from the attribute to the console. If
            // the optional Url string is found, write that out as well.
            //
            if (a1 != null)
            {
                Console.WriteLine("Description: " + a1.Text);
                if (!String.IsNullOrEmpty(a1.Url))
                    Console.WriteLine("Url: " + a1.Url);
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
                DescriptionAttribute a2 = Attribute.GetCustomAttribute(info,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;

                // If we found one, write the member info and the attribute info to the console.
                //
                if (a2 != null)
                {
                    Console.WriteLine("\n  {0} - {1}", info.MemberType, info.Name);
                    Console.WriteLine("  Description: " + a2.Text);
                    if (!String.IsNullOrEmpty(a2.Url))
                        Console.WriteLine("  Url: " + a2.Url);
                }
            }
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

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}