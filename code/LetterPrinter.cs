using System;

namespace SortedArray.code
{
    public class LetterPrinter
    {
        private readonly ILetter[] _letters;

        public LetterPrinter(ILetter[] letters)
        {
            _letters = letters;
        }

        public void Print()
        {
            foreach (ILetter letter in _letters)
            {
                Console.WriteLine(letter.GetType());
            }
        }
    }
}