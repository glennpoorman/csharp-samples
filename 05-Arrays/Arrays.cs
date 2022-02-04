// ------------------------------------------------------------------------------------------------------
// Arrays.cs
//
// Arrays in C#.
//
// C# does arrays just a little differently than C or C++. In C#, arrays are always objects and always
// created dynamically, either via "new" or via an initializer list.
//
// This sample creates single and multi-dimensional arrays using a variety of initialization methods and
// writes the contents to the console.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Arrays
{
    public class Program
    {
        public static void Main()
        {
            // Create a five element array using an initializer list. This syntax is similar to using an
            // initializer list in C or C++ except that the end result is still a dynamically created
            // array where the length is implied by the number of initializers in the list.
            //
            int[] iArray1 = { 21, 22, 23, 24, 25 };

            // Use a for loop and write the array values out to the console.
            //
            Console.WriteLine("Printing 1st array created using initializers.");
            for (int i = 0; i < iArray1.Length; i++)
                Console.WriteLine("iArray1[{0}] = {1}", i, iArray1[i]);

            // Now create another five element array. This time use the "new" operator to allocate
            // storage for the array. The size is specified (can be hard coded or a variable) and the
            // elements are all intialized to zero.
            //
            // Note that in C#, there is never a need to deallocate this storage as C# does its own
            // garbage collection.
            //
            int[] iArray2 = new int[5];

            // Stuff values into the array manually.
            //
            iArray2[0] = 11;
            iArray2[1] = 12;
            iArray2[2] = 13;
            iArray2[3] = 14;
            iArray2[4] = 15;

            // Again use a for loop and write the array values to the console.
            //
            Console.WriteLine("\nPrinting 2nd array created using \"new.\"");
            for (int i = 0; i < iArray2.Length; i++)
                Console.WriteLine("iArray2[{0}] = {1}", i, iArray2[i]);

            // C# also allows for multidimensional arrays. These array declarations use commas to denote
            // the rank of the array. For example, for a four dimensional array of strings, you would
            // write:
            //
            //     string[,,,] 4dimArray = new string[d1,d2,d3,d4];
            //
            // In the code below, create a two dimensional 4x4 array. All of the values are initialized
            // to zero on creation. Just to give it some other values, use a pair of loops to initialize
            // the array as if it were a 4x4 identity matrix (setting all elements where i==j to 1).
            //
            int[,] iArray3 = new int[4, 4];
            for (int i = 0; i < iArray3.GetLength(0); i++)
                for (int j = 0; j < iArray3.GetLength(1); j++)
                    iArray3[i, j] = (i == j) ? 1 : 0;

            // Now use a pair of loops to print the array in matrix form. Note the use of the "GetLength"
            // "method instead of the "Length" property. This method is needed to differentiate between
            // the lengths of the various array dimensions.
            //
            Console.WriteLine("\nPrinting a two dimensional array.");
            for (int i = 0; i < iArray3.GetLength(0); i++)
            {
                for (int j = 0; j < iArray3.GetLength(1); j++)
                    Console.Write(iArray3[i, j] + " ");
                Console.WriteLine();
            }

            // C# also differentiates between a multidimensional array and an array of arrays. The array
            // of arrays allows each element of the initial array to have a unique array length. C# also
            // calls these "jagged arrays."
            //
            // Create an array of four arrays. Note that after the initial array is created, the
            // underlying arrays must be newed up individually. Also note that they are all different
            // lengths.
            //
            int[][] iArray4 = new int[4][];
            iArray4[0] = new int[3];
            iArray4[1] = new int[9];
            iArray4[2] = new int[2];
            iArray4[3] = new int[5];

            // Stuff the array or arrays full of values.
            //
            iArray4[0][0] = 1;
            iArray4[0][1] = 2;
            iArray4[0][2] = 3;
            iArray4[1][0] = 4;
            iArray4[1][1] = 5;
            iArray4[1][2] = 6;
            iArray4[1][3] = 7;
            iArray4[1][4] = 8;
            iArray4[1][5] = 9;
            iArray4[1][6] = 10;
            iArray4[1][7] = 11;
            iArray4[1][8] = 12;
            iArray4[2][0] = 13;
            iArray4[2][1] = 14;
            iArray4[3][0] = 15;
            iArray4[3][1] = 16;
            iArray4[3][2] = 17;
            iArray4[3][3] = 18;
            iArray4[3][4] = 19;

            // Again use a pair of loops to print the array row by row. Note that technically speaking,
            // these are single dimension arrays which means that the "Length" property can be used on
            // each one.
            //
            Console.WriteLine("\nPrinting a jagged array.");
            for (int i = 0; i < iArray4.Length; i++)
            {
                for (int j = 0; j < iArray4[i].Length; j++)
                    Console.Write(iArray4[i][j] + " ");
                Console.WriteLine();
            }

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
