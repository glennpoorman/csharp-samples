// ------------------------------------------------------------------------------------------------------
// Streams
//
// Text file IO using stream writer and stream reader.
//
// This sample creates several shape objects and writes their data in string form to a text file on disk.
// The file is then reopened and read displaying the contents to the console. Afterward the file is
// deleted. Unlike the very early file IO sample that used a single call to open/write/close a file,
// this sample uses a stream writer/reader to achieve the same results.
//
// In C#, a stream is a sequence of bytes. That stream can come from many sources including files, text
// strings, network sockets, or other processes (just to name a few). A stream writer is used to write
// bytes to a stream. As you would expect, a stream reader is used to read bytes from a stream. In the
// case of file streams, both the stream writer and stream reader manage a file system handle and both
// implement "IDisposable".
// ------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;

namespace Streams
{
    // The "Point" struct.
    //
    public struct Point
    {
        // The X and Y auto-implemented properties of the point coordinates.
        //
        public int X { get; init; }
        public int Y { get; init; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{X}, {Y}";
    }

    // Define the "Shape" class.
    //
    public class Shape
    {
        // Center point property.
        //
        public Point Center { set; get; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{GetType().Name}, Center = ({Center})";

        // Virtual "Draw" method.
        //
        public virtual void Draw() => Console.WriteLine(this);
    }

    // Define a class "Circle" that derives from "Shape" and adds a radius.
    //
    public class Circle : Shape
    {
        // Public radius property.
        //
        public int Radius { set; get; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Radius = ({Radius})";
    }

    // Define a class "Rectangle" that derives from "Shape" and adds a width and height.
    //
    public class Rectangle : Shape
    {
        // Public width property.
        //
        public int Width { set; get; }

        // Public height property.
        //
        public int Height { set; get; }

        // Override "ToString" from the base "object" class.
        //
        public override string ToString() => $"{base.ToString()}, Width = ({Width}), Height = ({Height})";
    }

    class Program
    {
        /// <summary>
        /// String constant with the name of the text file we're going to read/write.
        /// </summary>
        const string ShapesFileName = "Shape.txt";

        static void Main()
        {
            // Create a generic list with some shapes in it.
            //
            List<Shape> shapeList = new()
            {
                new Shape()
                {
                    Center = new Point() { X = 11, Y = 12 }
                },
                new Circle()
                {
                    Radius = 15
                },
                new Rectangle()
                {
                    Center = new Point() { X = 10, Y = 10 },
                    Width = 100,
                    Height = 74
                }
            };

            // The "System.IO.StreamWriter" class is used to write text to a stream using a particular
            // encoding. In this simplest case, we use it to write text to a file on disk.
            //
            // Create a "StreamWriter" specifying a file in the local folder. Write the list of shapes to
            // the file and then close it. If a file by the same name already exists in the folder, it
            // will be overwritten.
            //
            // Note that the stream writer implements "IDisposable" and so we create the stream writer
            // within a "using" statement. When the stream writer goes out of scope, the file will be
            // closed and file handle released.
            //
            Console.WriteLine("Creating and writing file...");
            using (StreamWriter sw = new(ShapesFileName))
            {
                foreach (Shape shape in shapeList)
                    sw.WriteLine(shape);
            }

            // The "System.IO.StreamReader" class is used to read text from a stream. Again we use it
            // here to read from a file on disk.
            //
            // Create a "StreamReader" using the name of the file that was just written. Use the method
            // "ReadToEnd" to read all of the characters in the stream (the entirety of the file) into a
            // single string.
            //
            Console.WriteLine("\nRe-opening and reading file...");
            using (StreamReader sr = new(ShapesFileName))
            {
                Console.WriteLine(sr.ReadToEnd());
            }

            // Delete the file.
            //
            File.Delete(ShapesFileName);
            Console.WriteLine("File deleted.");
        }
    }
}
