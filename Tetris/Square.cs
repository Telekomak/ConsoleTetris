using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    class Square
    {
        public short Color { get; private set; }
        public ConsoleColor color { get; private set; }

        public string character;

        public Square(string character, ConsoleColor color)
        {
            this.character = character;
            this.color = color;
            ReplaceChars();
        }

        private void ReplaceChars()
        {
            if (character == "1")
            {
                character = "██";
                return;
            }

            character = "  ";
        }
    }
}
