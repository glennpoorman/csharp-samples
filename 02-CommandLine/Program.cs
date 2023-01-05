// ------------------------------------------------------------------------------------------------------
// CommandLine
//
// Command line arguments.
//
// This sample takes command line arguments and echoes them back to console output. To see the program
// work, command line arguments need to be specified either by running the program from a DOS prompt or
// by adding the arguments to the project properties in Visual Studio.
// ------------------------------------------------------------------------------------------------------

// Write the number of command line arguments to the console. Please note the following:
//
// 1. In a simple C# application consisting of top level statements (like this one), the command line
//    arguments are simply available as an array of strings named "args".
//
// 2. All arrays in C# have a "Length" property that returns the current length of the array (more on
//    arrays later). Note that "Length" is a "property" as opposed to a data member or method. There
//    will be more on properties in later samples.
//
// 3. The numeric value of the "Length" property is written to the output string similar to the way a
//    token like "%d" is used in the C "printf" function. In this case, any additional parameters are
//    referenced using a zero based index in between curly braces. In this case, the "Length" property
//    is printed in the place of the "{0}" token. The delimiter "\n" is the same as in C++ or C.
//
System.Console.WriteLine("Number of arguments = {0}\n", args.Length);

// Again using the "Length" property, loop through the command line arguments printing each to the
// console. Again use the "{}" syntax to specify additional arguments. In this case, the loop index is
// printed in place of the "{0}" token and the value of the argument itself is printed in place of the
// "{1}" token.
//
for (int i = 0; i < args.Length; i++)
    System.Console.WriteLine("args[{0}] = {1}", i, args[i]);

// Wait for <ENTER> to finish.
//
System.Console.Write("\nHit <ENTER> to finish: ");
System.Console.ReadLine();
