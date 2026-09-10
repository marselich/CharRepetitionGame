using System;

namespace Assets._Project.Develop.Runtime.Gameplay.Generators
{
    public class CharsGenerator : ICharsChecker, ICharsEqualed
    {
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        private const string Digits = "0123456789";

        private readonly Random _random = new();

        private string _generateString;

        public bool IsCharsEquel { get; private set; }
        public bool IsCharsEquelInLength { get; private set; }
        public string GenerateString => _generateString;

        public void Generate(CharsType charType, int count)
        {
            switch (charType)
            {
                case CharsType.Letters:
                    GenerateRandomChars(count, Letters);
                    break;

                case CharsType.Digits:
                    GenerateRandomChars(count, Digits);
                    break;

                default:
                    throw new ArgumentException($"{charType} not found");
            }
        }

        public void Check(string chars)
        {
            if (_generateString == null || chars == null)
                return;

            IsCharsEquel = _generateString.StartsWith(chars);
            IsCharsEquelInLength = _generateString == chars;
        }

        private void GenerateRandomChars(int charsCount, string chars)
        {
            string temp = string.Empty;

            for (int i = 0; i < charsCount; i++)
                temp += chars[_random.Next(chars.Length)];

            _generateString = temp;
        }
    }
}