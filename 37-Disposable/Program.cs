// ---------------------------------i---------------------------------------------------------------------
// Disposable
//
// In C#, it is not uncommon to see code designed to handle resources that are not managed by .NET. The
// most common types of unmanaged resources are objects that wrap operating system resources, such as
// files, windows, network connections, or database connections. That's just a small sampling though. As
// you interact with 3rd party libraries, you might run across any number of things that fall into this
// category.
//
// To help manage these types of resources, C# provides the "IDisposable" interface contained within the
// "System" namespace. The idea is that you define a class or struct to manage a resource (or resources)
// and define that same class or struct to implement "IDisposable". The interface "IDisposable" requires
// just one method "Dispose" which is meant to be implemented to free/delete your resource(s) when you're
// finished.
//
//     public void Dispose();
//
// "IDisposable" isn't just for physical resources though. You can also use it for temporary changes in
// state. Consider an Windows application where you want to temporarily change the cursor. You might
// implement a class to save the current cursor, change it to something else (like a "busy" cursor) and,
// then implement "Dispose" to reinstate the original cursor. You might want to do something similar with
// the value of a registry value. Or an application setting in the app you're working with. There are a
// number of scenarios where this can be very useful.
// ------------------------------------------------------------------------------------------------------

using System;

namespace Disposable
{
    // To show just one very simple example, we'll implement a struct designed to temporarily change the
    // text color in the console window. Start the class definition here and specify that this class
    // implements "IDisposable".
    //
    public struct TextColorOverride : IDisposable
    {
        // Declare a private field that, upon construction, will be used to save the original console
        // text color.
        //
        private ConsoleColor oldColor;

        // The one and only constructor must take a valid console color. We'll use the "ConsoleColor"
        // type defined in "System".
        //
        public TextColorOverride(ConsoleColor newColor)
        {
            // Save off the current console foreground color and then set the new color using the input
            // parameter.
            //
            oldColor = Console.ForegroundColor;
            Console.ForegroundColor = newColor;
        }

        // This method (required by "IDisposable") will be called either directly or indirectly to
        // restore the original console foreground color.
        //
        public void Dispose()
        {
            Console.ForegroundColor = oldColor;
        }
    }

    class Program
    {
        static void Main()
        {
            // Now we'll test it all out. Start by writing a line of text which will display using the
            // default console foreground color.
            //
            Console.WriteLine("Here is some default colored text.");

            // The first, most obvious, but least recommended use of a disposable object is to create it
            // and then call "Dispose" directly. Here we create a text color override changing the color
            // to green. Then we write out some text and then call "Dispose" directly which will reset
            // the color back to its previous color (the default).
            //
            TextColorOverride ctc1 = new(ConsoleColor.Green);
            Console.WriteLine("Here is some green text.");
            ctc1.Dispose();

            // Create another text color override. Here we use a variation of the "using" keyword. The
            // "using" statement can be used to create objects that implement "IDisposable". The objects
            // are accessible in the code block in between the opening and closing curly braces. When the
            // code block is closed, the disposable object goes out of scope and "Dispose" is called
            // automatically. This is the preferred method of working with disposable objects.
            //
            using (TextColorOverride ctc2 = new(ConsoleColor.Yellow))
            {
                Console.WriteLine("Here is some yellow text.");
            }

            // Below we declare two text color overrides within "using" statements but here we show that
            // they can be nested. We'll create the first object changing the color to cyan. Within that
            // using block, we'll create another object changing the color to magenta. When the inner
            // object goes out of scope, the color will be restored to what it was previously (cyan).
            //
            // Note how in these "using" statements, we're not actually naming any variables. We could
            // have (like we did above). But in these cases we're not actually referencing the disposable
            // object. It just needs to exist. So we can leave off the variable type and name.
            //
            using (new TextColorOverride(ConsoleColor.Cyan))
            {
                Console.WriteLine("Some cyan text.");
                using (new TextColorOverride(ConsoleColor.Magenta))
                {
                    Console.WriteLine("    Nested magenta text.");
                }
                Console.WriteLine("Back to cyan.");
            }

            // To show the last way to use disposable objects, we'll call a local method that displays
            // error messages in red. See the comments on the method below.
            //
            DisplayError("Pretending something went wrong!");

            // By the time we're back here, we should be back to the default console color.
            //
            Console.WriteLine("Done (back to default color)!");
        }

        // This method is called to display an error message. The given text is prepended with some
        // additional text and the color is hard coded to be red.
        //
        public static void DisplayError(string msg)
        {
            // Below we use a variation of the "using" statement called a "using declaration". This
            // statement looks just like a regular variable declaration except that the line starts with
            // the "using" keyword. In this varation, there are no curly braces. The disposable object
            // is created and the scope/lifetime of the object is the same as any other object. As long
            // as the object is in scope, the console foreground color will be red. As soon as the object
            // goes out of scope (in this case, when the function ends), the color is restored to the
            // previous value.
            //
            using TextColorOverride ctc = new(ConsoleColor.Red);
            Console.WriteLine($"ERROR: (red text) - {msg}");
        }
    }
}
