using System;
using System.Diagnostics;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Object o = new Object(Object.Shape.LShape);
            int rot = 0;

            while (true)
            {
                Console.SetCursorPosition(0, 0);
                rot = Int32.Parse(Console.ReadLine());
                o.WriteShape(o.Rotate(rot));
                Console.ReadKey();
            }
        }
    }
}
