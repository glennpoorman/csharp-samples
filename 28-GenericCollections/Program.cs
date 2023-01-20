// ------------------------------------------------------------------------------------------------------
// GenericCollections
//
// C# Generic Collections
//
// The introduction of generics in C# 2 also brought with it the introduction of generic collections.
// Like the standard templates in C++, this is a robust set of strongly typed collections where the data
// type is specified as a type parameter at construction time. The collections are part of the namespace
// "System.Collections.Generic" and include the following:
//
//     List<T>                        - dynamically sized array.
//     Queue<T>                       - first-in, first-out collection of objects.
//     Stack<T>                       - last-in, first-out collection of objects
//     Dictionary<TKey, TValue>       - collection of key/value pairs implemented as a hash table.
//     SortedDictionary<TKey, TValue> - collection of key/value pairs implemented as a binary tree.
//     SortedList<TKey, TValue>       - collection of key/value pairs implemented as an array.
//     HashSet<T>                     - set implemented as a hash table.
//     SortedSet<T>                   - set implemented as a binary tree.
//     LinkedList<T>                  - doubly linked list.
//
// These collections have become the defacto standard in C# since they were introduced essentially
// rendering the object based collections obsolete.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Initializers
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
        // The shape center property.
        //
        public Point Center { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
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

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Rectangle width/height auto-implemented properties.
        //
        public int Width { get; set; }
        public int Height { get; set; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Width = ({Width}), Height = ({Height})";
    }

    class Program
    {
        static void Main()
        {
            // The generic list is probably the most heavily used generic collection. The list is an
            // array imlementation that sizes dynamically to accommodate the contents and entries can
            // be referenced by index.
            //
            Console.WriteLine("System.Collections.Generic.List<T>");
            List<string> list = new() { "one", "two", "three", "four" };
            for (int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);

            // The "Queue" is a first-in, first-out collection of objects. The "Enqueue" method is used
            // to put items into the queue.
            //
            // Note that the queue collection cannot be initialized using the same collection initializer
            // we used for the list. That is because the intializer is dependent on the existence of an
            // "Add" method. The queue contains no such method. Most collections define a constructor
            // that will take in an initial set of values though. To that end, we could have written the
            // following:
            //
            //     string[] values = { "one", "two", "three", "four" };
            //     Queue<string> queue = new(values);
            //
            // In that case, the constructor would have gone through the initial list and called
            // "Enqueue" for each.
            //
            Console.WriteLine("\nSystem.Collections.Generic.Queue<T>");
            Queue<string> queue = new();
            queue.Enqueue("one");
            queue.Enqueue("two");
            queue.Enqueue("three");
            queue.Enqueue("four");

            // To iterate through the queue and write each item, we use a "while" loop and call the
            // "Dequeue" which removes the "next" item in the queue. We do this until there are no more
            // items left.
            //
            // Note that the items are printed in the same order that they were added.
            //
            while (queue.Count > 0)
                Console.WriteLine(queue.Dequeue());

            // The "Stack" is a last-in, first-out collection of objects. The "Push" method is used to
            // push items onto the stack.
            //
            // Note that like the queue, there is no "Add" method on the stack so we cannot use a
            // collection initializer.
            //
            Console.WriteLine("\nSystem.Collections.Generic.Stack<T>");
            Stack<string> stack = new();
            stack.Push("one");
            stack.Push("two");
            stack.Push("three");
            stack.Push("four");

            // Like the queue, use a "while" loop to iterate through the stack items and print them to
            // the console. Each time through the loop, we'll call "Pop" to remove the top item from the
            // stack until there are not more items left.
            //
            // Note that the items are printed in the reverse order that they were added.
            //
            while (stack.Count > 0)
                Console.WriteLine(stack.Pop());

            // The "Dictionary", "SortedDictionary", and "SortedList" collections are all map collections
            // of key/value pairs. The "Dictionary" is implemented as a hash table. "SortedDictionary" is
            // implemented as a binary search tree. "SortedList" is implemented as a sorted array that
            // dynamically sizes to accommodate its contents.
            //
            Console.WriteLine("\nSystem.Collections.Generic.Dictionary<TKey,TValue>");
            Dictionary<string, Shape> shapesDictionary = new()
            {
                ["shape-01"] = new Shape() { Center = new Point() { X = 11, Y = 12 } },
                ["shape-02"] = new Circle() { Center = new Point() { X = 14, Y = 15 }, Radius = 12 },
                ["shape-03"] = new Circle() { Center = new Point() { X = 17, Y = 18 }, Radius = 21 },
                ["shape-04"] = new Rectangle() { Center = new Point() { X = 20, Y = 21 }, Width = 12, Height = 14 }
            };

            // Note when iterating over the dictionary style collections that the individual items are
            // of type "KeyValuePair<TKey,TValue>" where the key and value types match those specified to
            // the collection itself when it was constructed. This is always a good place to use an
            // implicitly typed variable as in:
            //
            //     foreach (var pair in shapesDictionary)
            //
            foreach (KeyValuePair<string, Shape> pair in shapesDictionary)
                Console.WriteLine($"{pair.Key} - {pair.Value}");

            // There are two different set collections. The "HashSet" is implemented as a hash table
            // while the "SortedSet" is implemented as a binary tree. Both share the characterstics
            // of a set in that they do not allow duplicates and they can have boolean set operations
            // applied to them.
            //
            // Note that a duplicate entry does not throw any kind of error. It simply fails to add the
            // duplicate and, in the case where "Add" is called, will return a value of false.
            //
            // Create two sets below with some crossover entries.
            //
            Console.WriteLine("\nSystem.Collections.Generic.SortedSet<T>");
            SortedSet<string> set1 = new() { "bears", "cubs", "dogs", "horses", "zebras" };
            SortedSet<string> set2 = new() { "cubs", "dingos", "horses", "lions" };

            // Call the union operation on the first set. This will perform a boolean "union" modifying
            // the set that the method is called on.
            //
            set1.UnionWith(set2);

            // Write the resulting set to the console.
            //
            foreach (string str in set1)
                Console.WriteLine(str);

            // The "LinkedList" collection is a doubly linked list. A linked collection if discrete
            // pieces. It is particularly useful if you're maintaining a collection where you plan on
            // frequently inserting and/or removing items from the collection.
            //
            // Declare a linked list of strings. Note that like the queue and the stack, there is no
            // simple "Add" method for the linked list so we cannot use an initializer. To add the
            // initial values, you can make repeated calls to "AddLast" or as we've done here, you can
            // pass in an initial array of values to the list constructor.
            //
            Console.WriteLine("\nSystem.Collections.Generic.LinkedList<T>");
            string[] values = { "one", "two", "four", "five" };
            LinkedList<string> linkedList = new(values);

            // We want to add an additional string right the middle. So locate the node containing the
            // value "four". Use the node we get back (assuming it was found) and call the method to
            // insert an additional value just before it in the list.
            //
            LinkedListNode<string> node = linkedList.Find("four");
            if (node != null)
                linkedList.AddBefore(node, "three");

            // Write the list contents to the console.
            //
            foreach (string str in linkedList)
                Console.WriteLine(str);
        }
    }
}
