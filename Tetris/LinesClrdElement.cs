using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    class LinesClrdElement:IUIElement
    {
        public static string Value;
        public LinesClrdElement()
        {
            Value = ConstructValue();
        }

        private string ConstructValue()
        {
            string raw = Convert.ToString(WritingVariables.LinesCleared);

            if (raw.Length != 4)
            {
                int count = 4 - raw.Length;
                char[] temp = new char[count];

                for (int i = 0; i < count; i++)
                {
                    temp[i] = '0';
                }

                raw = String.Join("", new[] { new String(temp), raw });
            }

            return raw;
        }

        public string[] Frame = new[]
        {
            "LINES   ",
            "CLEARED:",
            "┌──────┐",
            $"│ {Value} │",
            "└──────┘"
        };

        public string GetLine(int index)
        {
            return Frame[index];
        }
        
    }
}
