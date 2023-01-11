// ------------------------------------------------------------------------------------------------------
// Types
//
// Types in C#.
//
// C# has both value types and reference types. There is no notion of pointers like in C++. Whether a
// variable is a value type or a reference type is determined by the type of the variable. The builtin
// types in C# are value types (short, int, long, char, float, double, enum, etc). Additionally the
// struct is a value type (we'll get into struct types in a later sample). Reference types in C# include
// arrays, classes, and delegates (classes and delegates be covered in more detail later).
//
// In C#, all types (value or reference) ultimately derive from the base class "System.Object" which is
// also referred to by the keyword "object".
// ------------------------------------------------------------------------------------------------------

using System;

namespace Types
{
    class Program
    {
        static void Main()
        {
            // Create and initialize a new int. The int is a value type and is created on the stack.
            //
            int i1 = 100;

            // Now create a second int copying from the first. Since these are value types, the value
            // of the first integer is copied into the second. The two variables are now two different
            // integer variables that happen to have the same value.
            //
            int i2 = i1;

            // To show this, assign a different value to the second variable and print both. The result
            // will be the display of the unchanged first variable and the second variable containing the
            // new value.
            //
            i2 = 101;
            Console.WriteLine("\nPrinting values from two \"int\" variables.");
            Console.WriteLine("i1 = {0}", i1);
            Console.WriteLine("i2 = {0}", i2);

            // Create an array and add some strings to it.
            //
            string[] a1 = { "Apples", "Bananas", "Cucumber" };

            // Since arrays are reference types, declaring a second array variable and assigning the
            // first to it results in two variables referencing the same array.
            //
            string[] a2 = a1;

            // Now change one of the entries using the first array variable and then print both arrays.
            // Note that the outputs are identical since the two array variables are both referencing the
            // same array.
            //
            a1[1] = "Bazinga";
            Console.WriteLine("\nPrinting entries from two array variables.");
            for (int i = 0; i < a1.Length; i++)
                Console.WriteLine("a1[{0}] = {1}", i, a1[i]);
            Console.WriteLine();
            for (int i = 0; i < a2.Length; i++)
                Console.WriteLine("a2[{0}] = {1}", i, a2[i]);

            // Since all types in C# derive either directly or indirectly from "object", any variable of
            // any type can be assigned to a variable of type "object." What this actually means though
            // depends on the type of data you're starting with.
            //
            // In the first assignment, a1 is a reference type so this behaves like an implicit cast in
            // C++ with the variable "o1" referencing the same array as "a1."
            //
            // In the second assignment, i1 is a value type. When a value type is assigned to a variable
            // of type "object", it is said to be "boxed." What happens is that a new object is
            // dynamically allocated and the value from i1 is copied into it.
            //
            object o1 = a1;
            object o2 = i1;

            // Assignment back requires some form of casting.
            //
            // In the following assignment, a cast is used to assign the object containing the boxed
            // integer back to an integer variable. In this case, the integer value is copied back and
            // is said to be "unboxed."
            //
            // NOTE: If the object type doesn't match the variable it's being cast to, an exception will
            //       be thrown.
            //
            int i3 = (int)o2;
            Console.WriteLine("\nPrinting \"int\" unboxed from \"object\".");
            Console.WriteLine(i3);

            // There are a couple of different options for casting reference types back.
            //
            // In the assignment below, the object reference is cast back into an array reference using
            // the familiar casting syntax. As was the case with the integer cast, if the object type
            // doesn't match the variable it's being cast to, an excpetion will be thrown.
            //
            string[] a3 = (string[])o1;

            // In the next assignment, the "as" operator is used. This operator differs from a cast in
            // a few noteworthy ways.
            //
            // 1. If the object were not a string array, the result would be a "null" reference and no
            //    exception would be thrown.
            //
            // 2. The "as" operator can only be used on reference types. Attempting to use it on value
            //    types will generate a compiler error.
            //
            // 3. Using the "as" operator will not execute any user defined conversions.
            //
            string[] a4 = o1 as string[];

            // Now that the objects are cast back into shapes, print them to the console.
            //
            Console.WriteLine("\nPrinting entries from two array variables (again).");
            for (int i = 0; i < a3.Length; i++)
                Console.WriteLine("a3[{0}] = {1}", i, a3[i]);
            Console.WriteLine();
            for (int i = 0; i < a4.Length; i++)
                Console.WriteLine("a4[{0}] = {1}", i, a4[i]);
        }
    }
}
