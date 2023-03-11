using System.Numerics;

namespace Ahorcado
{
    class Program
    {
        const int NUMBEROFTRIES = 8;
        static readonly string[] POSSIBLEWORDS = { "computer", "headphones", "phone", "mouse pad", "keyboard", "laptop", "screen", "speaker", "cable", "printer", "camera", "software", "microphone", "charger" };
        enum State { Playing, Win, Failed }
        static State state = State.Playing;
        static void Main()
        {
            string word = GiveWord();
            int tries = NUMBEROFTRIES;
            while (state == State.Playing)
            {
                Loop(word, tries);
            }
            if (state == State.Win)
            {
                Console.SetCursorPosition(10, 7);
                Console.WriteLine($"{(char)5}  YOU WON CONGRATULATIONS  {(char)5}");
                Console.SetCursorPosition(0, 15);
                Console.WriteLine();
            }
            else if (state == State.Failed)
            {
                Console.WriteLine($"------\n|    |\n|    {(char)1}\n|   /|\\\n|   / \\\n|\n---\nThe word was {word}.\nYou lost. Try again.");
            }
        }
        static string GiveWord()
        {
            Random random = new Random();
            string word = POSSIBLEWORDS[random.Next(POSSIBLEWORDS.Length)];
            return word;
        }
        static void Loop(string word, int tries)
        {
            string failedLetters = string.Empty;
            string usedLetters = string.Empty;
            string showWord = string.Empty;
            char @try = ' ';
            int i = 0;
            while (i < NUMBEROFTRIES)
            {
                GuessWord(word, usedLetters, showWord);
                NumberOfTries(tries);
                LettersUsed(failedLetters);
                Drawing(tries);
                if (state == State.Win) { i = NUMBEROFTRIES; }
                else
                {
                    @try = InputChar(word);
                }
                if (!failedLetters.Contains(@try)) { failedLetters += @try; }
                if (!usedLetters.Contains(@try) || !word.Contains(@try)) { tries--; i++; }
                usedLetters += @try;
                Console.Clear();
            }
            if (state != State.Win) { state = State.Failed; }
        }
        static string GuessWord(string word, string usedLetters, string showWord)
        {
            Console.Write("Word you have to guess: ");
            for (int i = 0; i < word.Length; i++)
            {
                if (usedLetters.Contains(word[i]))
                {
                    showWord += word[i];
                }
                else if (word[i] == ' ')
                {
                    showWord += ' ';
                }
                else
                {
                    showWord += "-";
                }
            }
            if (showWord.Contains("-") == false) { state = State.Win; }
            Console.Write(showWord);
            Console.WriteLine();

            return showWord;
        }
        static void NumberOfTries(int tries)
        {
            Console.Write($"Tries left: {tries}");
            Console.WriteLine();
        }
        static void LettersUsed(string failedLetters)
        {
            string failedLetters2 = "";
            for (int i = 0; i < failedLetters.Length; i++)
            {
                if (failedLetters[i] != ' ')
                {
                    failedLetters2 += failedLetters[i];
                }
            }
            char[] orderLetters = failedLetters2.ToCharArray();
            char copy;
            for (int i = 0; i < orderLetters.Length - 1; i++)
            {
                for (int j = 0; j < orderLetters.Length - 1; j++)
                {
                    if (orderLetters[j] > orderLetters[j + 1])
                    {
                        copy = orderLetters[j + 1];
                        orderLetters[j + 1] = orderLetters[j];
                        orderLetters[j] = copy;
                    }
                }
            }
            Console.Write($"Used letters: [{string.Join(", ", orderLetters)}]");
            Console.WriteLine();
        }
        static void Drawing(int tries)
        {
            double percentage = Convert.ToDouble(tries) / NUMBEROFTRIES;

            if (percentage <= 0.125) { Console.WriteLine($"------\n|    |\n|    {(char)2}\n|   /|\\\n|   /\n|\n---"); }
            else if (percentage > 0.125 && 0.25 >= percentage) { Console.WriteLine($"------\n|    |\n|    {(char)2}\n|   /|\\\n|\n|\n---"); }
            else if (percentage > 0.25 && 0.375 >= percentage) { Console.WriteLine($"------\n|    |\n|    {(char)2}\n|   /|\n|\n|\n---"); }
            else if (percentage > 0.375 && 0.50 >= percentage) { Console.WriteLine($"------\n|    |\n|    {(char)2}\n|    |\n|\n|\n---"); }
            else if (percentage > 0.50 && 0.625 >= percentage) { Console.WriteLine($"------\n|    |\n|    {(char)2}\n|\n|\n|\n---"); }
            else if (percentage > 0.625 && 0.75 >= percentage) { Console.WriteLine("------\n|    |\n|\n|\n|\n|\n---"); }
            else if (percentage > 0.75 && 0.875 >= percentage) { Console.WriteLine("------\n|\n|\n|\n|\n|\n---"); }
            else if (percentage > 0.875 && 1 > percentage) { Console.WriteLine("-\n|\n|\n|\n|\n|\n---"); }
            else if (percentage == 1) { Console.WriteLine("-\n|\n|\n|\n|\n|\n---"); }
            Console.WriteLine();
        }
        static char InputChar(string word)
        {
            Console.Write("Introduce a letter or word to guess: ");
            string wordToLetter = Console.ReadLine();
            wordToLetter = wordToLetter.ToLower();
            string allLetters = "abcdefghijklmñnopqrstuvwxyzáíóéúàèìòùçüïöäëýÿ1234567890";
            char @try = ' ';
            if (word == wordToLetter)
            {
                state = State.Win;
            }
            else if (wordToLetter.Length != 1)
            {
                @try = ' ';

            }
            else if (allLetters.Contains(wordToLetter))
            {
                @try = Convert.ToChar(wordToLetter[0]);
            }
            return @try;
        }
    }
}