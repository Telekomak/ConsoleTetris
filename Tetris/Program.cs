using System;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Media;

namespace Tetris
{
    class Program
    {
        private static Game g = new Game();
        public static Thread mov = new Thread(new ThreadStart(Movement));
        public static Thread music = new Thread(new ThreadStart(Music));

        static void Main(string[] args)
        {
            Console.WindowHeight = 25;
            Console.WindowWidth = 30;
            Console.Title = "Shitty Tetris";
            //Writer.DrawFrame();
            Console.CursorVisible = false;
            g.Player = new Object(Object.Shape.ZShape);
            Game.Next = new Object(Object.Shape.SShape);

            mov.Start();
            music.Start();
            //wrt.Start();

            
            g.WriteToField(g.Player, g.Player.Position, 3);
            Stopwatch s = new Stopwatch();
            while (true)
            {
                s.Start();
                g.Move(0);
                Thread.Sleep(300);
                
                s.Reset();
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

                            g.WriteToField(g.Player, g.Player.Position, 3);
                            break;

                        case ConsoleKey.DownArrow:
                            g.Move(0);
                            break;
                    }
                }

            } while (true);

        }

        static void Music()
        {

            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = $@"{Directory.GetCurrentDirectory()}\music.wav";
            player.PlayLooping();
        }
    }
}
