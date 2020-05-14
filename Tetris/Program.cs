using System;
using System.Diagnostics;
using System.Text;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.SetCursorPosition(100,10);
            Object o = new Object(Object.Shape.LShape);
            o.Rotate(3);
            //int rot = 0;

            //while (true)
            //{
            //    Console.SetCursorPosition(0, 0);
            //    rot = Int32.Parse(Console.ReadLine());
            //    o.WriteShape(o.Rotate(rot));
            //}

            Game g = new Game();
            g.WriteField();
            Console.Clear();
            g.WriteToField(o, new int[]{0,6});
            Console.ReadKey();
            
        }
    }
}
