// ------------------------------------------------------------------------------------------------------
// HelloStream
//
// Simple text file IO (revisited)
//
// This sample, again, writes the "Hello World!" string to a text file on disk. The file is then reopened
// and read displaying its contents to the console followed by a prompt to delete the file. Unlike the
// first very simple sample, this sample uses a stream writer/reader to achieve the same results.
//
// In C#, a stream is a sequence of bytes. That stream can come from many sources including files, text
// strings, network sockets, or other processes (just to name a few). A stream writer is used to write
// bytes to a stream. As you would expect, a stream reader is used to read bytes from a stream.
// ------------------------------------------------------------------------------------------------------

using System;
using System.IO;

namespace HelloStream
{
    class HelloStream
    {
        static void Main()
        {
            // The "System.IO.StreamWriter" class is used to write text to a stream using a particular
            // encoding. In this simplest case, we use it to write text to a a file on disk.
            //
            // Create a "StreamWriter" specifying a file in the local folder. Write a line of text to the
            // file and then close it. If a file by the same name already exists in the folder, it will
            // be overwritten.
            //
            // Note that "Close" is used to close the file and release the handle. In the next section
            // where the file is reopened and read, you will see an alternate and preferable way to open
            // and close the stream with a variation of the "using" keyword.
            //
            Console.WriteLine("Creating and writing file...");
            StreamWriter sw = new StreamWriter("HelloFile.txt");
            sw.WriteLine("Hello World!");
            sw.Close();

            // The "System.IO.StreamReader" class is used to read text from a stream. Again we use it
            // here to read from a file on disk.
            //
            // Create a "StreamReader" using the name of the file that was just written. Use a loop to
            // read lines of text from the file and write them to the console until the end of file is
            // reached.
            //
            // Please note the following:
            //
            // 1. The "ReadLine" method on the reader will return a null reference when the end of file
            //    is reached.
            //
            // 2. An alternative method of closing the file is shown below using another form of the
            //    "using" keyword. By creating the reader in the context of the "using" statement, we
            //    assure that the reader is closed when the "using" statement's scope is closed (the
            //    closing brace). This is applicable to both the reader, writer, and any other type that
            //    implements the interface "IDisposable" (we will discuss interfaces in a later sample).
            //
            //    This form is preferable to the explicit "Close" call shown above.
            //
            Console.WriteLine("\nRe-opening and reading file...");
            using (StreamReader sr = new StreamReader("HelloFile.txt"))
            {
                string strText;
                while ((strText = sr.ReadLine()) != null)
                    Console.WriteLine(strText);
            }

            // Prompt the user to see if the file should be deleted or not.
            //
            Console.Write("\nDelete the file [Y or N]? ");
            string strYesNo = Console.ReadLine();
            if (string.Compare(strYesNo, "Y", true) == 0)
            {
                File.Delete("HelloFile.txt");
                Console.WriteLine("File deleted.");
            }

            // Wait for <ENTER> to finish.
            //
            Console.Write("\nHit <ENTER> to finish: ");
            Console.ReadLine();
        }
    }
}
