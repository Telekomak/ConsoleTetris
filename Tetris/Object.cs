using System;
using System.IO;

namespace Tetris
{
    class Object
    {
        public ConsoleColor Color { get; private set; }
        public Shape shape;

        public int[] Position;
        
        public int Rotation { get; private set; }

        public Square[,] ActualRot;
        private Square[,,] Data;

        public Object(Shape shape)
        {
            Position = new[] {0, 6};
            Color = PickRandomColor();
            this.shape = shape;
            Data = LoadObject();//shape must be defined!
            Rotate(0);
        }

        public void Rotate(int direction)
        { 
            Rotation = direction;

            if (shape == Shape.Square)
            {
                direction = 0;
            }
            Square[,] retData = new Square[4,4];

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    retData[i,j] = Data[direction, i, j];
                }
            }

            ActualRot = retData;
        }
        //Returns the shape in string[] for Writer class
        public string[] GetNext()
        {
            string[] kokot = new string[4];
            string[] retData = new string[4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    kokot[j] = ActualRot[i, j].Character;
                }

                retData[i] = String.Join("", kokot);
            }

            return retData;
        }

        private Square[,,] LoadObject()
        {
            string[] raw;

            switch (shape)
            {
                case Shape.Line:
                    raw = Properties.Resources.Line.Split('\n');
                    break;
                case Shape.Square:
                    raw = Properties.Resources.Square.Split('\n');
                    break;
                case Shape.LShape:
                    raw = Properties.Resources.LShape.Split('\n');
                    break;
                case Shape.JShape:
                    raw = Properties.Resources.JShape.Split('\n');
                    break;
                case Shape.Tee:
                    raw = Properties.Resources.Tee.Split('\n');
                    break;
                case Shape.ZShape:
                    raw = Properties.Resources.ZShape.Split('\n');
                    break;
                case Shape.SShape:
                    raw = Properties.Resources.SShape.Split('\n');
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            Square[, ,] data = new Square[4,4,4];
            int line = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    char[] temp = raw[line].ToCharArray();
                    for (int k = 0; k < 4; k++)
                    {
                        data[i, j, k] = new Square(temp[k], Color, Square.Attribute.InObject);
                    }
                    line++;
                }

                if (shape == Shape.Square) break;
            }

            return data;
        }

        public static ConsoleColor PickRandomColor()
        {
            Random r = new Random();

            switch (r.Next(0, 7))
            {
                case 1:
                    return ConsoleColor.DarkBlue;

                case 2:
                    return ConsoleColor.DarkGreen;

                case 3:
                    return ConsoleColor.DarkRed;

                case 4:
                    return ConsoleColor.DarkYellow;

                case 5:
                    return ConsoleColor.DarkMagenta;
                case 6:
                    return ConsoleColor.DarkGray;
            }

            return ConsoleColor.DarkRed;
        }

        public void WriteShape()
        {
            Square[,] s = ActualRot;

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(10, 5 + i);
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(s[i,j].Character);
                    System.Threading.Thread.Sleep(100);
                }

                Console.WriteLine();
            }

        }

        public enum Shape
        {
            Line = 0,
            Square = 1,
            LShape = 2,
            JShape = 3,
            Tee = 4,
            ZShape = 5,
            SShape = 6
        }
    }
}
