using System;
using System.Threading;
using System.IO;
using System.Media;

namespace Tetris
{
    class Program
    {
        private static Game g;
        public static Thread mov = new Thread(new ThreadStart(Movement));
        public static Thread music = new Thread(new ThreadStart(Music));
        public static bool Loop;

        static void Main(string[] args)
        {
            music.Start();
            GameLoop();
        }

        public static void GameLoop()
        {
            Loop = true;

            Console.WindowHeight = 24;
            Console.WindowWidth = 31;
            Console.Title = "Shitty Tetris";
            Console.CursorVisible = false;

            g = new Game();

            g.Player = new Object(Object.Shape.ZShape);
            Game.Next = new Object(Object.Shape.SShape);

            mov.Start();
            

            g.WriteToField(g.Player, g.Player.Position, 3);
            while (Loop)
            {
                g.Move(0);
                Thread.Sleep(300);

            }
            Console.Clear();
            Writer.GameOver();
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

            } while (Loop);

            return;
        }

        static void Music()
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = $@"{Directory.GetCurrentDirectory()}\music.wav";
            player.PlayLooping();
        }
    }
}
