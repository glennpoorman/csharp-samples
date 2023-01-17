// ------------------------------------------------------------------------------------------------------
// Collections
//
// Object collections in C#.
//
// As was mentioned already, all types in C# ultimately derive from "System.Object" (a.k.a. "object").
// That means that any type in C# can be added to and managed by a collection designed to handle data of
// type "object". The namespace "System.Collections" contains definitions for several such collections
// including:
//
//     System.Collections.ArrayList  - list implementated as an array whose size is dynamically increased
//     System.Collections.Hashtable  - collection of key/value pairs organized by the key's hash code
//     System.Collections.Queue      - first-in, first-out collection of objects
//     System.Collections.SortedList - collection of key/value pairs sorted by key
//     System.Collections.Stack      - last-in, first-out collection of objects
//
// PLEASE NOTE that since the introduction of generics and generic collections (which we'll cover later),
// these collections are rarely used anymore.
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections;

namespace Collections
{
    class Program
    {
        static void Main()
        {
            // Start off with yet another array. There's nothing new here except to reiterate the fact
            // that an array is ultimately a class deriving from "System.Object" just like everything
            // else and is also a collection just like the others we'll cover here.
            //
            Console.WriteLine("System.Array");
            string[] stringArray = { "one", "two", "three", "four" };

            // Write the contents of the array to the console. We could use a "foreach" here but we'll
            // use the regular "for" loop to drive home the notion that arrays are collections that are
            // generally referenced by index.
            //
            for (int i = 0; i < stringArray.Length; i++)
                Console.WriteLine(stringArray[i]);

            // The "ArrayList" is a list collection. The collection should be treated as simply a list of
            // objects. The word "Array" refers to the implementation as the list is implemented as an
            // array that dynamically resizes when necessary. Please note the following:
            //
            // 1. There is no capacity given on creation. The size increases as needed to make room for
            //    the items that are added.
            //
            // 2. As the collection is designed to take data of type "object", you can mix and match the
            //    data. Note that we're adding string, int, double, and bool types. You're not likely to
            //    mix types to this degree in any real world applications but ... you can.
            //
            Console.WriteLine("\nSystem.Collections.ArrayList");
            ArrayList arrayList = new ArrayList();
            arrayList.Add("text");
            arrayList.Add(11);
            arrayList.Add(12.5);
            arrayList.Add(true);

            // Write contents out using a "foreach" loop.
            //
            // Note that if you know the data types in the collection, you can use that type in the
            // "foreach" statement. For example, if we knew that we'd added all strings to the list, we
            // could write the statement.
            //
            //     foreach (string str in arrayList)
            //
            // When the loop is written that way, the compiler will automatically performa a cast from
            // the collection to the variable. If the collection contains an item or items that aren't
            // of the type specified or derived from that type, an exception will be thrown.
            //
            foreach (object obj in arrayList)
                Console.WriteLine(obj);

            // The "Hashtable" is one of two "dictionary" collections meaning it is a collection of
            // key/value pairs. The collection is organized by the key's hash code. Every type of object
            // in C# has the capacity to generate a hash code for this purpose. Note that adding items
            // to the table means specifying both a key and value for each. Both the keys and the values
            // are simply "object" types so can mix/match the types as you see fit.
            //
            Console.WriteLine("\nSystem.Collections.Hashtable");
            Hashtable hashTable = new Hashtable();

            // Say we want to use letters of the alphabet as keys and then add a type of produce that
            // starts with that letter as the value. Note there are two different ways we can add those
            // items.
            //
            // 1. The "Add" method takes in a key and value pair and adds it to the table.
            //
            // 2. Dictionary types contain an indexer "[]" that takes in a key object. This is a really
            //    nice shorthand for both adding items as well as modifying existing items. Note that
            //    we'll cover indexers in more depth later on.
            //
            hashTable.Add("a", "apples");
            hashTable.Add("b", "bananas");
            hashTable["k"] = "kiwis";
            hashTable["l"] = "lemons";

            // Once we've added values, we can turn around and write the contents of the table out to the
            // console. Note as we loop through the items in the table that each item comes back as an
            // object of type "DictionaryEntry". We can then use the "Key" and "Value" properties on the
            // entry to get back to the original keys and values.
            //
            foreach (DictionaryEntry item in hashTable)
                Console.WriteLine($"{item.Key} - {item.Value}");

            // The "Queue" is a first-in, first-out collection of objects. The "Enqueue" method is used
            // to put items into the queue.
            //
            Console.WriteLine("\nSystem.Collections.Queue");
            Queue queue = new();
            queue.Enqueue("dogs");
            queue.Enqueue("cats");
            queue.Enqueue("fish");
            queue.Enqueue("rodents");

            // To iterate through the queue and write each item, we use a "while" loop and call the
            // "Dequeue" which removes the "next" item in the queue. We do this until there are no more
            // items left.
            //
            // Note that the items are printed in the same order that they were added.
            //
            while (queue.Count != 0)
                Console.WriteLine(queue.Dequeue());

            // The "SortedList" is another "dictionary" style collection. The sorted list is a list of
            // key/value pairs. The collection is constantly kept sorted by sorting new items into the
            // correct place when they're added. The sorting is determined by the key value.
            //
            Console.WriteLine("\nSystem.Collections.SortedList");
            SortedList sortedList = new();

            // Do the produce table again like we did in the hash table example. Here we'll just add the
            // items in any old order we see fit.
            //
            sortedList["k"] = "kiwis";
            sortedList["a"] = "apples";
            sortedList["s"] = "strawberries";
            sortedList["b"] = "bananas";
            sortedList["o"] = "oranges";
            sortedList["l"] = "lemons";

            // Write out the list contents using a "foreach" loop. Please note the following:
            //
            // 1. Like the hash table, each item in the collection is of type "DictionaryEntry".
            //
            // 2. When the collection is written, the output will be sorted by key value.
            //
            foreach (DictionaryEntry item in sortedList)
                Console.WriteLine($"{item.Key} - {item.Value}");

            // The "Stack" is a last-in, first-out collection of objects. The "Push" method is used to
            // push items onto the stack.
            //
            Console.WriteLine("\nSystem.Collections.Stack");
            Stack stack = new();
            stack.Push("dogs");
            stack.Push("cats");
            stack.Push("fish");
            stack.Push("rodents");

            // Like the queue, use a "while" loop to iterate through the stack items and print them to
            // the console. Each time through the loop, we'll call "Pop" to remove the top item from the
            // stack until there are not more items left.
            //
            // Note that the items are printed in the reverse order that they were added.
            //
            while (stack.Count != 0)
                Console.WriteLine(stack.Pop());
        }
    }
}
