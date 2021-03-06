// ------------------------------------------------------------------------------------------------------
// Shapes.cs
//
// Partial class definitions in C#.
//
// C# offers the ability to split class definitions into multiple pieces. This comes in handy in places
// like the Forms Designer in Visual Studio. When you generate a new form using VS, two files are
// generated. One file contains a partial class definition that is open for you to add to and work on.
// The other contains a partial definition of the same class containing the code generated by the Forms
// Designer that creates and arranges controls and such. This way, that second file can simply be deleted
// and re-generated by the Forms Designer any time you make changes to your form without impacting any
// custom code that you may have added to the class.
//
// To see this in action, make sure you understand this sample and then fire up Visual Studio. Create a
// new C# "Windows Forms Application" project. Notice that by default, you'll start with a main form. The
// Forms Designer will display the empty form waiting for you to add to it. At the same time, you'll
// notice two files generated in the project folder. One will be named "Form1.cs" and the other will be
// named "Form1.Designer.cs". They will both contain a partial class definition for the same form class.
//
// In this sample, the "Shapes" library will be split up. The file "Shapes.cs" will contain the class
// definitions for "Shape", "Circle" and "Rectangle." The implementations for "Draw" for all of the
// classes will be removed and defined in another file "Draw.cs." The main "Partials.cs" file will define
// the main entry point making use of the classes.
//
// Look at these files in the following order:
//
//     Shapes.cs
//     Draw.cs
//     Partials.cs
//
// ------------------------------------------------------------------------------------------------------

using System;

namespace Shapes
{
    // Define the base "Shape" class.
    //
    // Use of the "partial" keyword denotes that this class is extensible. The only way a class
    // definition can be split into multiple pieces is if all of the pieces contain this keyword.
    //
    public partial class Shape
    {
        // The x and y coordinates of the shape center.
        //
        private int x;
        private int y;

        // Declare a "PropertyChanged" event.
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

        // This method fires the "PropertyChanged" event.
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
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    // This class is also made "partial."
    //
    public partial class Circle : Shape
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
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    // this class is also made "partial."
    //
    public partial class Rectangle : Shape
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
    }
}
