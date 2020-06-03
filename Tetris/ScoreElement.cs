using System;

namespace Tetris
{
    class ScoreElement:IUIElement
    {
        public static string Value;
        public ScoreElement()
        {
            Value = ConstructValue();
        }

        private string ConstructValue()
        {
            string raw = Convert.ToString(WritingVariables.Score);

            if (raw.Length != 4)
            {
                int count = 4 - raw.Length;
                char[] temp = new char[count];

                for (int i = 0; i < count; i++)
                {
                    temp[i] = '0';
                }

                raw = String.Join("", new[] {new String(temp), raw});
            }

            return raw;
        }

        public string[] Frame = new[]
        {
            "SCORE:  ",
            "        ",
            "┌──────┐",
            $"│ {Value} │",
            "└──────┘",
            "        "
        };

        public string GetLine(int index)
        {
            return Frame[index];
        }


    }
}
