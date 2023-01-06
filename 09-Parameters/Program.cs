// ------------------------------------------------------------------------------------------------------
// Parameters
//
// Different kinds of function parameters in C#.
//
// C# doesn't use pointers the way C++ or C does and only uses references as parameters to and from
// methods. The syntax of those references is different than C++ as well.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Parameters
{
    class Program
    {
        // Static method prints two specified integer values to the console. These are regular pass by
        // value style parameters the same as in C or C++.
        //
        static void PrintInts(int i, int j)
        {
            Console.WriteLine($"Integer 1 = {i}.");
            Console.WriteLine($"Integer 2 = {j}.");
        }

        // Static method swaps two specified integer values. These are pass by reference parameters
        // denoted using the "ref" keyword. That keyword also denotes that the parameter values coming
        // in will be used and so it is important that these parameters be initialized. Failing to do
        // so will generate a compiler error.
        //
        static void SwapInts(ref int i, ref int j)
        {
            int temp = i;
            i = j;
            j = temp;
        }

        // Static method prompts for and reads two integer values from the console. These are output
        // parameters denoted using the "out" keyword. These are different from "ref" parameters in that
        // they are not required to have an initial value.
        //
        // Note that the "Parse" method is used to convert from a string to an int. This method is
        // defined on all of the simple types and assumes that the string value is convertible to an
        // integer. If the value is not convertible to an integer, an exception will be thrown.
        //
        static void GetInts(out int i, out int j)
        {
            string input;

            Console.Write("Enter integer 1: ");
            input = Console.ReadLine();
            i = int.Parse(input);

            Console.Write("Enter integer 2: ");
            input = Console.ReadLine();
            j = int.Parse(input);
        }

        static void Main()
        {
            // Call "GetInts" to read two integers from the console and return their values in the
            // form of "out" parameters. Please note the following:
            //
            // 1. The "out" keyword is required on both sides of the call.
            //
            // 2. We've declared the variables inline right in the call. Prior to C# 7, the code below
            //    would have had to be written as:
            //
            //        int i, j;
            //        GetInts(out i, out j);
            //
            //    While you can still write your code that way, C# 7 introduced the ability to declare
            //    "out" variables inline when a method is called that expects an "out" variable. This
            //    only works for "out" variables as it isn't required that they be initialized.
            //
            Console.WriteLine("Fetching integers from console.");
            GetInts(out int i, out int j);

            // Call "SwapInts" to swap the two integer values. This method uses "ref" parameters. Unlike
            // "out" parameters, "ref" parameters require an initial value which, in this case, the
            // compiler assumes happened in the call to "GetInts." These parameters may then be modified
            // inside of the method call.
            //
            // Note that the "ref" keyword is required on both sides of the call.
            //
            Console.WriteLine("\nSwapping...");
            SwapInts(ref i, ref j);

            // Call "PrintInts" to write the integer values to the console. This method uses the regular
            // pass by value style parameters.
            //
            Console.WriteLine("\nPrinting results.");
            PrintInts(i, j);

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
