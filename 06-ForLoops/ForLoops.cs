// ------------------------------------------------------------------------------------------------------
// ForLoops.cs
//
// "for" and "foreach" in C#.
//
// C# adds "foreach" in addition to the standard "for" loop allowing for a somewhat cleaner loop syntax.
//
// This sample creates a simple array of strings and writes the array to the console twice using a "for"
// and then a "foreach" loop.
// ------------------------------------------------------------------------------------------------------

using System;

namespace ForLoops
{
    public class Program
    {
        public static void Main()
        {
            // Create a six element array of strings using intializers.
            //
            string[] names = { "One", "Two", "Three", "Four", "Five", "Six" };

            // The C and C++ style "for" loop still works the same as always.
            //
            Console.WriteLine("Printing array using regular \"for\" loop:");
            for (int i = 0; i < names.Length; i++)
                Console.WriteLine("    " + names[i]);

            // C# has a "foreach" loop that makes array iteration cleaner.
            //
            Console.WriteLine("\nNow printing using a C# \"foreach\" loop:");
            foreach (string n in names)
                Console.WriteLine("    " + n);

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
