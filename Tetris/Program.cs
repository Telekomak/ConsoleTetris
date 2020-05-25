using System;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace Tetris
{
    class Program
    {
        private static Game g = new Game();
        public static Thread mov = new Thread(new ThreadStart(Movement));
        public static Thread wrt = new Thread(new ThreadStart(Writer));

        static void Main(string[] args)
        {
            Console.Title = "Shitty Tetris";
            Console.CursorVisible = false;
            Object o = new Object(Object.Shape.ZShape);
            g.Player = o;

            mov.Start();
            //wrt.Start();

            g.WriteToField(o, g.Player.Position);

            while (true)
            {
                //if (!g.IsWriting)
                //{
                //    g.Move(0);
                //}
                g.Move(0);
                Thread.Sleep(300);
            }
        }

        public static void Movement()
        {
            do
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();

                    switch (key.Key)
                    {
                        case ConsoleKey.RightArrow:
                            g.Move(1);
                            break;

                        case ConsoleKey.LeftArrow:
                            g.Move(2);
                            break;

                        case ConsoleKey.UpArrow:
                            if (g.Player.Rotation < 3)
                            {
                                g.Player.Rotate(g.Player.Rotation + 1);
                            }

                            else
                            {
                                g.Player.Rotate(0);
                            }

                            g.WriteToField(g.Player, g.Player.Position);
                            break;

                        case ConsoleKey.DownArrow:
                            g.Move(0);
                            break;
                    }
                }

            } while (true);

        }

        public static void Writer()
        {
            while (true)
            {
                g.WriteField();
                //Thread.Sleep(1);
            }
        }
    }
}
