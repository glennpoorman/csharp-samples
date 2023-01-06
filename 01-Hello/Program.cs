// ------------------------------------------------------------------------------------------------------
// Hello
//
// The simplest C# program shows simple console IO.
//
// For simple C# applications, you can put executable statements at the top of one (and only one) source
// file in your project. Those statements are assumed to be the main entry point for the application.
// ------------------------------------------------------------------------------------------------------

// Write a text string to the console.
//
// C# programs use namespaces to provide a hierarchical means of organizing types and functionality. The
// "System" namespace contains a type "Console" which contains several static methods for writing to and
// reading from the console.
//
// Note that calling a static method in C# uses the same syntax as calling an instance method.
//
System.Console.WriteLine("Hello World!");

// Wait for <ENTER> to finish.
//
// The "Write" method behaves just like "WriteLine" except that no line feed is generated. "ReadLine"
// reads a line of text and will terminate when <ENTER> is hit.
//
System.Console.Write("\nHit <ENTER> to finish: ");
System.Console.ReadLine();
