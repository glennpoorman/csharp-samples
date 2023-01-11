// ------------------------------------------------------------------------------------------------------
// Parameters
//
// Function parameters in C#.
//
// By default, function parameters are passed by value in C#.
//
// 1. For value types, this means that a copy of the value is passed to the function.
//
// 2. For reference types, a copy of the reference is passed to the function and both the variable in
//    the caller and the variable in the function refer to the same object.
//
// C# also has reference parameters "ref" and output parameters "out".
//
// 1. Reference parameters specify that a parameter may be changed by the function and that its initial
//    value might be required to do so. These types of parameters must be initialized and their values
//    may or may not change when the function is called.
//
// 2. Output parameters specify that their value will be provided by the function. It is not required
//    that these parameters be initialized.
//
// In order to use "ref" or "out" parameters, both caller and callee have to specifically specify them.
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
            Console.Write("Enter integer 1: ");
            i = int.Parse(Console.ReadLine());

            Console.Write("Enter integer 2: ");
            j = int.Parse(Console.ReadLine());
        }

        static void Main()
        {
            // Call "GetInts" to read two integers from the console and return their values in the form
            // of "out" parameters. Please note the following:
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
        }
    }
}
