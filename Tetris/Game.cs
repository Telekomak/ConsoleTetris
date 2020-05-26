using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Input;

namespace Tetris
{
    class Game
    {
        public Square[,] Field;
        public Square[,] ActField;
        public Object Player;
        public bool IsWriting;
        public Game()
        {
            Field = LoadField();
            ActField = Field;
            IsWriting = false;
        }

        public void WriteField()
        {
            IsWriting = true;
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {

                    if (Field[i,j].attribute == Square.Attribute.InObject)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        //Console.BackgroundColor = ConsoleColor.Red;
                    }

                    if (Field[i,j].attribute == Square.Attribute.Sticky)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        //Console.BackgroundColor = ConsoleColor.Blue;
                    }
                    Console.Write(ActField[i,j].Character);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.ResetColor();
            IsWriting = false;
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

            WriteToField(Player,Player.Position, direction);
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

                    if (i == raw.Length-2)
                    {
                        retData[i, j] = new Square(temp[j], ConsoleColor.DarkGray, Square.Attribute.Sticky);
                    }
                }
            }

            return retData;
        }

        public void WriteToField(Object o, int[] pos, int direction)
        {
            for (int i = pos[0]; i < pos[0] + 4; i++)
            {
                for (int j = pos[1]; j < pos[1] + 4; j++)
                {

                    //if (j == pos[1])
                    //{
                    //    ActField[Player.Position[0], Player.Position[1] - 1] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                    //}

                    //if (j == pos[1]+3)
                    //{
                    //    ActField[Player.Position[0], Player.Position[1] + 1] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                    //}

                    //if (i == pos[0] && Player.Position[0] != 0)
                    //{
                    //    ActField[Player.Position[0] - 1, Player.Position[1]] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                    //}

                    if (direction == 1)
                    {
                        //maze v levo
                        if (j == pos[1] && ActField[i, j - 1].attribute == Square.Attribute.InObject)
                        {
                            ActField[i, j - 1] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                        }
                    }

                    else if (direction == 2)
                    {
                        // maze v pravo
                        if (j == pos[1]+3 && ActField[i, j + 1].attribute == Square.Attribute.InObject)
                        {
                            ActField[i, j + 1] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                        }
                    }

                    else if (direction == 0)
                    {
                        //maze nahore
                        if (i == pos[0] && pos[0] != 0 && ActField[i - 1, j].attribute == Square.Attribute.InObject)
                        {
                            ActField[i - 1, j] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                        }
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

                        if (Player.Position[0] <= 1)
                        {
                            GameOver();
                        }

                        AbortPlayer();
                    }
                }
            }

            if (!IsWriting)
            {
                //WriteField();
            }
        }

        private void GameOver()
        {
            for (int i = 0; i < Program.times.Count; i++)
            {
                Console.WriteLine(Program.times[i]);
            }

            Console.ReadKey();
            throw new  NotImplementedException("Game over");
        }

        private void AbortPlayer()
        {
            Random r = new Random();
            Object o = new Object(Object.Shape.Line);
            switch (r.Next(0,7))
            {
                case 0:
                    o = new Object(Object.Shape.Tee);
                    break;

                case 1:
                    o = new Object(Object.Shape.JShape);
                    break;

                case 2:
                    o = new Object(Object.Shape.ZShape);
                    break;

                case 3:
                    o = new Object(Object.Shape.SShape);
                    break;

                case 4:
                    o = new Object(Object.Shape.Square);
                    break;

                case 5:
                    o = new Object(Object.Shape.Line);
                    break;

                case 6:
                    o = new Object(Object.Shape.LShape);
                    break;
            }

            Player = o;
            Thread.Sleep(600);
        }

        public bool IsSticky(int[] pos)
        {

            if (Field[pos[0], pos[1]].Character == "██" && Field[pos[0], pos[1]].attribute == Square.Attribute.InObject && Field[pos[0] + 1, pos[1]].attribute == Square.Attribute.Sticky && Field[pos[0] + 1, pos[1]].Character == "██")
            {
                return true;
            }

            return false;
        }

        
    }
}
