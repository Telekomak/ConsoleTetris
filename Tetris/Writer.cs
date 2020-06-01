using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Tetris
{
    public static class Writer
    {
        private static IUIElement[] elements;
        public static void Write()
        {
            elements = new IUIElement[] {new LinesClrdElement(), new ScoreElement(), new NextBlockElement()};

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"{elements[0].GetLine(i)} | {elements[1].GetLine(i)} | {elements[2].GetLine(i)}");
            }
        }
    }
}
