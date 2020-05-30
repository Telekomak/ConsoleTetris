using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public int Score;
        public Game()
        {
            Field = LoadField();
            ActField = Field;
            IsWriting = false;
            Score = 0;
        }

        public void WriteField()
        {
            IsWriting = true;
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Field.GetLength(0); i++)
            {
                for (int j = 0; j < Field.GetLength(1); j++)
                {

                    if (ActField[i,j].attribute == Square.Attribute.InObject)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        //Console.BackgroundColor = ConsoleColor.Red;
                    }

                    if (ActField[i,j].attribute == Square.Attribute.Sticky)
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
            Console.WriteLine(Score);
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

            Score++;
            WriteToField(Player,Player.Position, direction);
        }

        private Square[,] LoadField()
        {
            string[] raw = File.ReadAllLines("Field.txt");
            Square[,] retData = new Square[raw.Length,16];
            

            for (int i = 0; i < raw.Length; i++)
            {
                char[] temp = raw[i].ToCharArray();

                for (int j = 0; j < 16; j++)
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
            bool atBottom = false;
            for (int i = pos[0]; i < pos[0] + 4; i++)
            {
                for (int j = pos[1]; j < pos[1] + 4; j++)
                {
                    if ((ActField[i, j].Character == "██" && ActField[i, j].attribute == Square.Attribute.NonSticky) && o.ActualRot[i - pos[0], j - pos[1]].Character == "██")
                    {
                        if (direction == 1)
                        {
                            Move(2);
                            return;
                        }

                        Move(1);
                        return;
                        
                    }

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
                        atBottom = true;
                    }
                }
            }

            if (atBottom)
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

                CheckLines(new[] { Player.Position[0], Player.Position[0] + 1, Player.Position[0] + 2, Player.Position[0] + 3 });
                AbortPlayer();
            }

            if (!IsWriting)
            {
                WriteField();
            }
        }

        private void GameOver()
        {
            //for (int i = 0; i < times.Count; i++)
            //{
            //    Console.WriteLine(times[i]);
            //}

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
        }

        private void CheckLines(int[] lines)
        {
            List<int> clearLines = new List<int>();
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = 3; j < 13; j++)
                {
                    if (ActField[lines[i], j].Character == "  " || lines[i] == 16)
                    {
                        break;
                    }

                    if (j == 12)
                    {
                        clearLines.Add(lines[i]);
                    }
                }
            }

            if (clearLines.Count != 0)
            {
                ClearLines(clearLines);
            }
        }

        private void ClearLines(List<int> lines)
        {
            bool isRetarded = false;
            Score += 100 * lines.Count;

            for (int i = 0; i < lines.Count; i++)
            {
                if (i < lines.Count - 1 && lines.Count != 1)
                {
                    if (lines[i] - lines[i + 1] < -1)
                    {
                        isRetarded = true;
                    }
                }
                
                for (int j = 3; j < 13; j++)
                {
                    ActField[lines[i], j] = null;
                }
            }

            int offset = 0;
            if (isRetarded)
            {
                for (int i = lines[lines.Count-1]; i >= lines[0]; i--)
                {
                    if (ActField[i,3] != null)
                    {
                        for (int j = 3; j < 12; j++)
                        {
                            ActField[lines[lines.Count - 1] - offset, j] = ActField[i, j];
                            ActField[i,j] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                        }
                        offset++;
                    }
                }
            }

            for (int i = lines[0] - 1; i >= 0; i--)
            {
                for (int j = 3; j < 13; j++)
                {
                    ActField[i + lines.Count + offset, j] = ActField[i, j];
                    ActField[i, j] = new Square("  ", ConsoleColor.DarkGray, Square.Attribute.NonSticky);
                }
            }
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
