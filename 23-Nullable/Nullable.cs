// ------------------------------------------------------------------------------------------------------
// Nullable.cs
//
// The "Nullable<T>" type is a generic type in the "System" namespace that allows value types to be
// treated like reference types. In other words, the "Nullable" can represent the full range of values
// for its underlying value type plus an additional null value. This can be useful when you need to
// differentiate between a valid value and no value.
//
// The full syntax for declaring a nullable object (using bool as an example) is
//
//     Nullable<bool> trueBool = true;
//     Nullable<bool> nullBool = null;
//
// C# also introduced the shorthand "?" syntax (much more useful)
//
//     bool? trueBool = true;
//     bool? nullBool = false;
//
// Once you have your objects, checking to see if they contain good value or not is a matter of using one
// of the following two syntax:
//
//     if (nullBool.HasValue) ...
//     if (nullBool == null) ...
//
// ------------------------------------------------------------------------------------------------------

using System;

namespace Nullable
{
    class Program
    {
        static void Main()
        {
            // Create a nullable int object and assign the value of 12.
            //
            // Note that this object can be used exactly like a regular int in all cases.
            //
            int? nullableInt = 12;
            
            // Use the "HasValue" method to see if the object is null or not. If it has a value, print
            // the value to the console. Otherwise print that it is null.
            //
            if (nullableInt.HasValue)
                Console.WriteLine("Value = {0}", nullableInt);
            else
                Console.WriteLine("Value is null");

            // Set the object to null. Using a regular int, this would generate a compiler error.
            //
            nullableInt = null;

            // Again check to see if the object is null or not. Instead of using the "HasValue" method
            // this time, simply check if the object is equal (or not equal in this case) to null.
            //
            if (nullableInt != null)
                Console.WriteLine("Value = {0}", nullableInt);
            else
                Console.WriteLine("Value is null");

            // The "GetValueOrDefault" method is really handy. It simply checks if the object has a value
            // or not. If it does, then that value is returned. Otherwise a default value is returned. In
            // this case, it is the system default (which for an int is 0).
            //
            int defaultValue = nullableInt.GetValueOrDefault();
            Console.WriteLine("Default value is {0}", defaultValue);

            // Again we call "GetValueOrDefault" but this time we call the one that allows us to specify
            // the default. So here an object with a valid value will return that value. Otherwise 36 is
            // returned. The line below is equivalent to writing the following:
            //
            //     defaultValue = (nullableInt != null) ? nullableInt : 36;
            //
            defaultValue = nullableInt.GetValueOrDefault(36);
            Console.WriteLine("Custom default value is {0}", defaultValue);

            // Wait for <enter> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}