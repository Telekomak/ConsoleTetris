using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tetris
{
    class Game
    {
        public Square[,] field;
        public Object Player;

        public Game()
        {
            field = LoadField();
        }

        public void WriteField()
        {
            Console.ForegroundColor = field[0,0].color;

            for (int i = 0; i < field.GetLength(0); i++)
            {
                for (int j = 0; j < 14; j++)
                {
                    Console.Write(field[i,j].character);
                }

                Console.WriteLine();
            }
        }

        private Square[,] LoadField()
        {
            string[] raw = File.ReadAllLines("field.txt");
            Square[,] retData = new Square[raw.Length,14];
            

            for (int i = 0; i < raw.Length; i++)
            {
                char[] temp = raw[i].ToCharArray();

                for (int j = 0; j < 14; j++)
                {
                    retData[i,j] = new Square(temp[j], ConsoleColor.Red);

                    if (i == raw.Length-1)
                    {
                        retData[i, j] = new Square(temp[j], ConsoleColor.Red, Square.Attribute.Sticky);
                    }
                }
            }

            return retData;
        }

        public void WriteToField(Object o, int[] pos)
        {
            for (int i = pos[0]; i < pos[0] + 4; i++)
            {
                for (int j = pos[1]; j < pos[1] + 4; j++)
                {
                    field[i,j] = o.ActualRot[i - pos[0], j - pos[1]];
                }
            }

            WriteField();
        }
    }
}
