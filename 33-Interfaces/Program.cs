// ------------------------------------------------------------------------------------------------------
// Interfaces
//
// Interfaces in C#
//
// You can think of an interface as something similar to a pure abstract class in C++. An interface
// contains definitions for a group of related functions that a non-abstract class or a struct must
// implement. There is no data and no implementations in an interface. It is a set of declarations for
// any number of methods, properties, events and/or indexers that any class or struct implementing that
// interface must provide.
//
// There are a number of uses for interfaces.
//
// 1. You can define interfaces to obfuscate the details of an object or set of objects keeping thier
//    definitions internal and only making the interfaces public.
//
// 2. Although C# does not support multiple inheritance, a class or struct can implement any number of
//    interfaces. It's important here to remember that implementing an interface is not the same as
//    inheritance. It's more like a contract and the number of contracts you promise to implement has
//    no impact on the storage footprint of the class.
//
// 3. Struct types in C# cannot not inherit from another struct or class. They can, however, implement
//    any number of interfaces.
//
// In this sample, we're going to define an interface called "IDrawable". This interface will define a
// contract that any class/struct that wants to be "drawn" must honor. In addition, we'll define a list
// designed to contain any number of objects that implement "IDrawable". That list will also contain a
// method "DrawAll" that iterates through all of its drawable objects and calls their "Draw" method.
// We'll use the "Shape" and "Circle" classes and also add a new and completely unrelated class "Person".
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Interfaces
{
    // Define the "IDrawable" interface. This looks much like a class or struct definition except that we
    // use the "interface" keyword instead.
    //
    public interface IDrawable
    {
        // Here we specify that anyone implementing "IDrawable" provide a method called "Draw".
        //
        // Note that prior to C# 8, access modifiers were NOT allowed in interfaces. It was assumed that
        // anything declared in an interface would be public. For that reason, "public" is the default
        // access for all interfaces members.
        //
        public void Draw();
    }

    // Define a list designed to contain objects that implement "IDrawable". We'll do this by deriving
    // from the generic list passing in "IDrawable" as the type parameter.
    //
    public class DrawableList : List<IDrawable>
    {
        // In addition to the method inherited from List<T>, add a method "DrawAll" that will iterate
        // through all of the objects in this list call their "Draw" method.
        //
        public void DrawAll()
        {
            foreach (IDrawable drawable in this)
                drawable.Draw();
        }
    }

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
    // Specify that the class implement "IDrawable". Syntactically this looks the same as inheriting from
    // another class.
    //
    public class Shape : IDrawable
    {
        // The shape center property.
        //
        public Point Center { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // The "Draw" method as defined in "IDrawable".
        //
        public void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    // This class is still considered to implement "IDrawable" because it derives from "Shape".
    //
    public class Circle : Shape
    {
        // Circle radius auto-implemented property.
        //
        public int Radius { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    // Define a struct "Person" containing a first and last name.
    //
    // This type is completely unrelated from "Shape" and shares no ancestry. We specify here, hwoever,
    // that it implements "IDrawable" which will make this type usable in the "DrawableList".
    //
    public struct Person : IDrawable
    {
        // First and last name properties.
        public string FirstName { get; init; }
        public string LastName { get; init; }

        // The "Draw" method as defined in "IDrawable".
        //
        public void Draw() => Console.WriteLine($"Hello! My name is {FirstName} {LastName}!");
    }

    class Program
    {
        static void Main()
        {
            // Create a drawable list and intialize the list with several new shape objects as well as
            // a person object.
            //
            DrawableList drawableList = new()
            {
                new Shape()
                {
                    Center = new Point() { X = 10, Y = 22 }
                },
                new Circle()
                {
                    Center = new Point() { X = 30, Y = 40 },
                    Radius = 101
                },
                new Person()
                {
                    LastName = "Poorman",
                    FirstName = "Glenn"
                },
                new Circle()
                {
                    Center = new Point() { X = 101, Y = 202 },
                    Radius = 12
                }
            };

            // Call the method on the list to iterate through the drawables and call their "Draw" method.
            //
            drawableList.DrawAll();
        }
    }
}
