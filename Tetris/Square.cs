using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    class Square
    {
        public short Color { get; private set; }

        public Attribute attribute;
        public ConsoleColor color { get; private set; }

        public char character;

        public Square(char character, ConsoleColor color)
        {
            attribute = Attribute.NonSticky;
            this.character = ConstructChar(character);
            this.color = color;
        }

        public Square(char character, ConsoleColor color, Attribute a)
        {
            attribute = a;
            this.character = ConstructChar(character);
            this.color = color;
        }

        private char ConstructChar(char ch)
        {
            char retData;

            if (ch == '1')
            {
                retData = '█';
            }

            else
            {
                retData = ' ';
            }

            return retData;
        }

        public enum Attribute
        {
            Sticky = 0,
            NonSticky = 1
        }

    }
}
