using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

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

                    if (Field[i,j].attribute == Square.Attribute.InObject)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.BackgroundColor = ConsoleColor.Red;
                    }

                    if (Field[i,j].attribute == Square.Attribute.Sticky)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(ActField[i,j].Character);
                    Console.ResetColor();
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
                        ActField[pos[0] - 1, j] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                    }

                    if (!(Field[i, j].Character == "██" && o.ActualRot[i - pos[0], j - pos[1]].Character == "  "))
                    {
                        ActField[i, j] = o.ActualRot[i - pos[0], j - pos[1]];
                    }

                    if ((Field[i, j].Character == "██" && Field[i, j].attribute == Square.Attribute.InObject) && o.ActualRot[i - pos[0], j - pos[1]].attribute == Square.Attribute.InObject)
                    {
                        ActField[i, j] = o.ActualRot[i - pos[0], j - pos[1]];
                    }

                    if (IsSticky(new[] { i, j }))
                    {
                        for (int k = 0; k < 4; k++)
                        {
                            for (int l = 0; l < 4; l++)
                            {
                                o.ActualRot[k, l].attribute = Square.Attribute.Sticky;
                            }
                        }
                    }
                }
            }
        }


        public bool IsSticky(int[] pos)
        {
            bool ret = false;

            if (Field[pos[0], pos[1]].Character == "██" && Field[pos[0], pos[1]].attribute == Square.Attribute.InObject && Field[pos[0], pos[1] - 1].attribute == Square.Attribute.Sticky && Field[pos[0], pos[1] - 1].Character == "██")
            {
                ret = true;
            }

            if (Field[pos[0], pos[1]].Character == "██" && Field[pos[0], pos[1]].attribute == Square.Attribute.InObject && Field[pos[0], pos[1] + 1].attribute == Square.Attribute.Sticky && Field[pos[0], pos[1] + 1].Character == "██")
            {
                ret = true;
            }

            if (Field[pos[0], pos[1]].Character == "██" && Field[pos[0], pos[1]].attribute == Square.Attribute.InObject && Field[pos[0] + 1, pos[1]].attribute == Square.Attribute.Sticky && Field[pos[0] + 1, pos[1]].Character == "██")
            {
                ret = true;
            }

            return ret;
        }
        public ThreadStart Movement()
        {
            while (true)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.RightArrow:
                        Move(1);
                        break;

                    case ConsoleKey.LeftArrow:
                        Move(2);
                        break;

                    case ConsoleKey.UpArrow:
                        if (Player.Rotation < 3)
                        {
                            Player.Rotate(Player.Rotation + 1);
                        }

                        else
                        {
                            Player.Rotate(0);
                        }
                        break;

                    case ConsoleKey.DownArrow:
                        if (Player.Rotation > 0)
                        {
                            Player.Rotate(Player.Rotation - 1);
                        }

                        else
                        {
                            Player.Rotate(3);
                        }
                        break;
                }
            }
        }
    }
}
