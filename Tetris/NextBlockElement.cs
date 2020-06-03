namespace Tetris
{
    class NextBlockElement:IUIElement
    {
        public string[] Frame;
        public NextBlockElement()
        {
            Frame = Game.Next.GetNext();
        }
        
        public string GetLine(int index)
        {
            if (index == 0)
            {
                return "Next:";
            }
            return Frame[index - 1];
        }
    }
}
