// ------------------------------------------------------------------------------------------------------
// StringFormat
//
// Formatting strings in C#
//
// String formatting basically means inserting the values of other variables, objects, or expressions
// into a string. Unless you've been living in a cave, you've already been doing this for the entirety
// of your career. C and C++ allowed this through functions like "printf" or "sprintf". C++ provided
// the class "std::stringstream" allowing you to write values to strings. MFC provided the "CString"
// class which contained the "Format" method.
//
// C# provides two methods for formatting strings.
//
// 1. Composite Formatting. This method takes a composite format string along with a list of objects.
//    The string consists of fixed text mixed with indexed placeholders. When the string is formatted,
//    each placeholder is replaced with the string representation of its corresponding object in the
//    original list of objects.
//
// 2. String interpolation. This was introduced in C# 6 and provides a better and more readable way
//    to format strings. Prefacing a string with the "$" character identifies it as an interpolated
//    string which may contain interpolation expressions. These expressions are snippets of code
//    contained in curly braces whose result is formatted into the string.
// ------------------------------------------------------------------------------------------------------

using System;

namespace StringFormat
{
    class Program
    {
        static void Main()
        {
            // Create an array of strings.
            //
            string[] produce = { "Apples", "Bananas", "Cherries", "Eggplant", "Figs",
                                 "Grapes", "Mangos", "Pears", "Strawberries" };

            // Write the array length to the console and then loop through the individual array items
            // writing each item along with its index to the console.
            //
            // Here we use composite formatting. Note the first call to "WriteLine". The composite
            // format string contains fixed text along with a single indexed placeholder "{0}". The
            // additional argument "produce.Length" is then evaluated and the result is formatted
            // into the string. Similarly, the call to "WriteLine" in the for loop has a format string
            // with two placeholders. The first will contain the loop index while the second will
            // contain the array item.
            //
            // Note also that this is the same method used by the "string.Format" method. That is a
            // static method on the "string" class that also takes a composite format string along with
            // a list of objects. Instead of writing to the console though, the resulting string is
            // created and returned to the caller.
            //
            //     string result = string.Format("Array length = {0}", produce.Length);
            //
            // The caller is then free to do what they will with the resulting string.
            //
            Console.WriteLine("Composite formatting: Array length = {0}", produce.Length);
            for (int i = 0; i < produce.Length; i++)
                Console.WriteLine("produce[{0}] = {1}", i, produce[i]);

            // Again, write the array length to the console and then loop through the individual array
            // items writing each item along with its index to the console.
            //
            // Here we use string interpolation. On both calls to "WriteLine", the string is prefaced
            // with the "$" character telling the compiler that this is an interpolated string. Instead
            // of the curly braces representing indexed placeholders then, they represent snippets of
            // code to be evaluated with the result being formatted into the string. These snippets can
            // be variables or expressions.
            //
            // Note that while composite formatting relied on functions specifically written to handle
            // it, string interpolation is a function of how the compiler interprets string literals.
            // Consider the call to "WriteLine". C# provides a version of that call that takes a format
            // string followed by an array of objects representing the object list.
            //
            //     public static void WriteLine(string format, params object[] arg);
            //
            // The function then parses the string and replaces the placeholders with the values of the
            // items in the object array.
            //
            // With string interpolation however, the work is done before the call is ever made. Once the
            // string is created and formatted, the version of "WriteLine" that is called is simply:
            //
            //     public static void WriteLine(string value);
            //
            // With that in mind, there is no need to provide a version of "string.Format" as this can
            // now be done with a simple assignment.
            //
            //     string result = $"String interpolation: Array length = {produce.Length}";
            //
            Console.WriteLine($"\nString interpolation: Array length = {produce.Length}");
            for (int i = 0; i < produce.Length; i++)
                Console.WriteLine($"produce[{i}] = {produce[i]}");

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
