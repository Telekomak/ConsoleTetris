using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Tetris
{
    class Object
    {
        public short Color { get; private set; }
        public Shape shape;
        public int Rotation { get; private set; }

        public Square[,] ActualRot;
        private Square[,,] Data;

        public Object(Shape shape)
        {
            Random r = new Random();

            Color = (short)r.Next(0, 16);
            this.shape = shape;
            Data = LoadObject();
            Rotate(0);
        }

        public void Rotate(int direction)
        { 
            Rotation = direction;

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

        private Square[,,] LoadObject()
        {
            string[] raw = File.ReadAllLines($@"Shapes\{shape.ToString().ToLower()}");
            Square[, ,] data = new Square[4,4,4];
            int line = 0;

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    char[] temp = raw[line].ToCharArray();
                    for (int k = 0; k < 4; k++)
                    {
                        data[i, j, k] = new Square(temp[k], ConsoleColor.Cyan);
                    }

                    line++;
                }

                if (shape == Shape.Square) break;
            }

            return data;
        }

        public void WriteShape()
        {
            Square[,] s = ActualRot;
            Console.ForegroundColor = s[0, 0].color;

            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(10, 5 + i);
                for (int j = 0; j < 4; j++)
                {
                    Console.Write(s[i,j].character);
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
