// ------------------------------------------------------------------------------------------------------
// Shapes.cs
//
// Interfaces in C#
//
// Interfaces are similar to pure abstract classes in C++. In C++, a class made up entirely of pure
// virtual functions is said to be a pure abstract class meaning the class itself contained no code. Only
// a list of methods that any non-abstract derived classes were required to implement.
//
// In C#, an interface works the same way. The interface can contain methods, properties, events and
// indexers. None of these members are allowed to contain any code. Furthermore, all interface members
// are considered public and so access specifications are not allowed.
//
// Look at these files in the following order:
//
//     Shapes.cs
//     Interfaces.cs
//
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

// In this sample, create a namespace called "Shapes." The namespace will contain a class called
// "Collection." The idea is that a shapes collection will "collect" any number of shapes allowing a user
// of the collection to kick off a "Draw" of all the shapes at once.
//
// Furthermore, this particular module doesn't really care what the shapes are. All we care about is that
// they honor a certain protocol. First, any object considering itself a "Shape" must have a "Draw"
// method. Let's also say that any "Shape" must have a "ShapeType" property. For the property, we only
// care that there is a "get" accessor. We don't care about a "set."
//
// So with that, we can define an interface which will force anyone using this module to honor the
// expected protocol.
//
namespace Shapes
{
    // Define the "IShape" interface. This looks much like a class or struct definition except we use the
    // "interface" keyword instead.
    //
    public interface IShape
    {
        // Here we specify that anyone implementing "IShape" provide a string property called "ShapeType"
        // with a "get" accessor.
        //
        // That doesn't necessarily mean the property has to be read only. Whoever implements the
        // interface is free to also provide a "set" accessor if they want. This only means that this
        // module doesn't care.
        //
        // Also note that although the syntax is similar, this is not an auto-implemented property. As
        // properties and methods in an interface are abstract, the statement below defines an abstract
        // "ShapeType" property.
        //
        string ShapeType { get; }

        // Here we specify that anyone implementing "IShape" provide a "Draw" method.
        //
        void Draw();
    }

    // Define a shapes collection class. This class will "collect" any number of shape objects in the
    // form of shape interface references. This module doesn't define any shapes proper but instead
    // relies on users of the module to define the shapes and forces a protocol on them via the "IShape"
    // interface.
    //
    public class Collection
    {
        // Collection class contains a generic list of IShape interface references.
        //
        // NOTE: Also note that in C#, non-static fields can be initialized right in the declaration. A
        //       "new" list will be created for each instance of the collection upon creation alleviating
        //       the need for a constructor. Keep in mind, however, that this only works as long as the
        //       intializer does not reference any other data fields and does not reference "this." In
        //       those cases, you would need a constructor.
        //
        List<IShape> shapes = new List<IShape>();

        // "AddShape" takes a reference to an object that implements "IShape" and adds it to this
        // collection's list.
        //
        public void AddShape(IShape shape)
        {
            shapes.Add(shape);
        }

        // "Draw" iterates through all of the shapes in this collection. For each shape, the shape type
        // property is used to write the type to the console. Then the shape is drawn via the "Draw"
        // method.
        //
        public void Draw()
        {
            foreach (IShape shape in shapes)
            {
                Console.WriteLine("\nShape type = {0}", shape.ShapeType);
                shape.Draw();
            }
        }
    }
}
