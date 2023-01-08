// ------------------------------------------------------------------------------------------------------
// HelloFile
//
// Simple text file IO
//
// This sample writes the "Hello World!" string to a text file on disk. The file is then reopened and
// read displaying its contents to the console. A prompt is then displayed asking if the file should be
// deleted.
// ------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace HelloFile
{
    class HelloFile
    {
        static void Main()
        {
            // The "System.IO.File" class provides static methods for the creation, copying, deletion,
            // moving, and opening of a single file.
            //
            // Call the static "File.WriteAllText" method. The first argument is the full path name of
            // the file and the second is the text to write to the file. Calling this method creates the
            // specified file, writes the entirety of the string to the file, and then closes the file.
            // If the target fle already exists, it is overwritten.
            //
            // Note that we include a "using" directive for "System.IO" up top so we don't need to fully
            // qualify the "File" class with the namespace it lives in.
            //
            Console.WriteLine("Creating and writing file...");
            File.WriteAllText("HelloFile.txt", "Hello World!");

            // To read the contents of the file back again, call the static "File.ReadAllText" method.
            // The first and only argument is the full path name of the file (here we use the file we
            // just created). Calling this method opens the file, reads the entirety of the file into
            // a single text string, closes the file, and returns that string to the caller.
            //
            Console.WriteLine("\nRe-opening and reading file...");
            string strText = File.ReadAllText("HelloFile.txt");
            Console.WriteLine(strText);

            // Prompt the user to see if the file should be deleted or not.
            //
            Console.Write("\nDelete the file [Y or N]? ");
            string strYesNo = Console.ReadLine();

            // Call the "ToUpper" method on the string to force the string to all uppercase. Then
            // use a simple equality test to see how the prompt was answered.
            //
            if (strYesNo.ToUpper() == "Y")
            {
                // Use the static "File.Delete" method to remove the file from the local folder.
                //
                File.Delete("HelloFile.txt");
                Console.WriteLine("File deleted.");
            }
        }
    }
}
