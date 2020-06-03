using System;

namespace Tetris
{
    class Square
    {
        public ConsoleColor Color { get; set; }

        public Attribute attribute;

        public string Character;

        public Square(char character, ConsoleColor color)
        {
            attribute = Attribute.NonSticky;
            this.Character = ConstructChar(character);
            Color = color;
        }

        public Square(char character, ConsoleColor color, Attribute a)
        {
            attribute = a;
            Character = ConstructChar(character);
            Color = color;
        }

        public Square(string character, ConsoleColor color, Attribute a)
        {
            attribute = a;
            Character = character;
            Color = color;
        }
        //Picks the character based on char it gets (1 or 0)
        private string ConstructChar(char ch)
        {
            string retData;

            if (ch == '1')
            {
                retData = "██";
            }

            else
            {
                retData = "  ";
            }

            return retData;
        }

        public enum Attribute
        {
            Sticky = 0,
            NonSticky = 1,
            InObject = 2
        }

    }
}
