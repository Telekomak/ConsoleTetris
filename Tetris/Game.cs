using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Tetris
{
    class Game
    {
        public Square[,] Field;
        public Square[,] ActField;
        public Object Player;

        public Game()
        {
            Field = LoadField();
            ActField = Field;
        }

        public void WriteField()
        {
            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {
                    //Console.ForegroundColor = Field[i, j].Color;
                    Console.Write(ActField[i,j].Character);
                }

                Console.WriteLine();
            }

            Console.ResetColor();
        }

        public void Move(int direction)
        {
            switch (direction)
            {
                case 0:
                    Player.Position[0]++;
                    break;

                case 1:
                    Player.Position[1]++;
                    break;

                case 2:
                    Player.Position[1]--;
                    break;

                default:
                    break;
            }

            WriteToField(Player, Player.Position);
        }

        private Square[,] LoadField()
        {
            string[] raw = File.ReadAllLines("Field.txt");
            Square[,] retData = new Square[raw.Length,14];
            

            for (int i = 0; i < raw.Length; i++)
            {
                char[] temp = raw[i].ToCharArray();

                for (int j = 0; j < 14; j++)
                {
                    retData[i,j] = new Square(temp[j], ConsoleColor.DarkGray);

                    if (i == raw.Length-1)
                    {
                        retData[i, j] = new Square(temp[j], ConsoleColor.DarkGray, Square.Attribute.Sticky);
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
                    if (Player.Position[0] != 0 && i == pos[0])
                    {
                        ActField[pos[0] - 1, j] = new Square("  ", ConsoleColor.DarkGray);
                    }

                    if (!(Field[i, j].Character == "██" && o.ActualRot[i - pos[0], j - pos[1]].Character == "  "))
                    {
                        ActField[i, j] = o.ActualRot[i - pos[0], j - pos[1]];
                    }

                    else if ((Field[i, j].Character == "██" && Field[i, j].attribute == Square.Attribute.InObject) && o.ActualRot[i - pos[0], j - pos[1]].attribute == Square.Attribute.InObject)
                    {
                        ActField[i, j] = o.ActualRot[i - pos[0], j - pos[1]];
                    }

                    
                }
            }
        }

        public void PriorityWriting()
        {

        }
    }
}
