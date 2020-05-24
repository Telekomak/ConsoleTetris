using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Tetris
{
    class Program
    {
        private static Game g = new Game();
        //static Thread mov = new Thread(new ThreadStart(g.Movement()));

        static void Main(string[] args)
        {
            Object o = new Object(Object.Shape.ZShape);
            g.Player = o;
            //mov.Start();

            g.WriteToField(o, g.Player.Position);

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

                Thread.Sleep(400);
                //Console.SetCursorPosition(5, 10);
                //g.CleanSprite(g.Player.Position);
                //g.WriteField();
            }
            Console.ReadKey();
        }
    }
}
