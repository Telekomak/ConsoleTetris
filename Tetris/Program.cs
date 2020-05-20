using System;
using System.Diagnostics;
using System.Text;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Object o = new Object(Object.Shape.ZShape);
            o.Rotate(1);
            //int rot = 0;

            //while (true)
            //{
            //    Console.SetCursorPosition(0, 0);
            //    rot = Int32.Parse(Console.ReadLine());
            //    o.WriteShape(o.Rotate(rot));
            //}

            Game g = new Game();
            g.Player = o;
            
            
            Stopwatch s = new Stopwatch();
            g.WriteToField(o, g.Player.Position);
            g.Move(2);
            g.Player.Rotate(2);
            
            while (true)
            {
                
                Console.SetCursorPosition(10, 10);
                g.WriteField();
                g.Move(0);

                if (g.Player.Position[0] == 13)
                {
                    g.Player = new Object(Object.Shape.Tee);
                    continue;
                }

                System.Threading.Thread.Sleep(400);
                //Console.SetCursorPosition(5, 10);
                //g.CleanSprite(g.Player.Position);
                //g.WriteField();
            }
            Console.ReadKey();
        }
    }
}
