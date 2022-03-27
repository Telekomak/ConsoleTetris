using System;
using System.IO;
using System.Threading;

namespace Tetris
{
    public static class Writer
    {
        private static IUIElement[] elements;
        public static void Write()
        {
            Console.SetCursorPosition(0,19);
            elements = new IUIElement[] {new LinesClrdElement(), new ScoreElement(), new NextBlockElement()};

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{elements[0].GetLine(i)} | {elements[1].GetLine(i)} | {elements[2].GetLine(i)}");
            }
        }

        public static void GameOver()
        {
            Console.SetWindowSize(48, 23);
            Square[,] sign = Load();

            Console.SetCursorPosition(0, 2);

            for (int i = 0; i < sign.GetLength(0); i++)
            {
                for (int j = 0; j < sign.GetLength(1); j++)
                {
                    Console.ForegroundColor = sign[i, j].Color;
                    Console.Write(sign[i, j].Character);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine($"       Score: {WritingVariables.Score}, Lines cleared: {WritingVariables.LinesCleared}");
            Console.WriteLine("      --------Press Q to quit----------");
            Console.WriteLine("      -------Press R to restart--------");

            while (true)
            {
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.R)
                {
                    Console.Clear();
                    Program.mov = new Thread(Program.Movement);
                    Program.GameLoop();
                }
                
                else if (key == ConsoleKey.Q) {System.Environment.Exit(0); }
            }
        }

        private static Square[,] Load()
        {
            string[] raw = Properties.Resources.GameOver.Split('\n');
            Square[,] retData = new Square[raw.Length, 22];

            for (int i = 0; i < raw.Length; i++)
            {
                char[] temp = raw[i].ToCharArray();

                for (int j = 0; j < temp.Length; j++)
                {
                    retData[i, j] = new Square(temp[j], Object.PickRandomColor());
                }
            }

            return retData;
        }

    }
}
