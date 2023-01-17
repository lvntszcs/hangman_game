using System;


namespace hangman
{
    static class editable
    {
        public static char[] word = new char[1000];
    }

    static class generated
    {
        static string[] lines = System.IO.File.ReadAllLines(@"magyar-szavak.txt");

        static Random rnd = new Random();
        static int num = rnd.Next(157815); // names are not being processed from the txt

        static string words = lines[num];

        public static readonly char[] word = words.ToCharArray();
        
    }
    public class Program
    {
        public static bool test_mode = true;
        public static bool Check(char[] szo, char c)
        {
            foreach(char betu in szo)
            {
                if (betu == c)
                {
                    return true;
                }
            }
            return false;
        }

        public static void Write(char[] s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                Console.Write(s[i]+" ");
            }
        }

        public static void Edit(char c)
        {
            for(int i = 0; i < generated.word.Length; i++)
            {
                if (generated.word[i] == c) editable.word[i] = generated.word[i];
            }
        }

        public static bool Done()
        {
            for(int i = 0; i < editable.word.Length; i++)
            {
                if (editable.word[i] == '_')
                {
                    return false;
                }
            }
            return true;
        }

        public static void Main(string[] args)
        {
            int tries = 0;

            editable.word = new char[generated.word.Length];
            
            for(int i=0;i<generated.word.Length;i++)
            {
                editable.word[i] = '_';
            }

            if(test_mode)
                Console.WriteLine(generated.word);

            Write(editable.word);



            while(tries < 9)
            {
                Console.WriteLine("\nNumber of mistakes: {0}/8", tries);
                string s = Console.ReadLine();
                
                while(s == "")
                {
                    s = Console.ReadLine();
                }
                char c = s[0];

                Console.Clear();

                if (Check(generated.word, c))
                {
                    Edit(c);
                }
                else tries++;
                Console.Clear();
                Write(editable.word);

                bool guessed = Done();
                if (guessed) break;
            }

            string solution = new string (generated.word);

            if (tries < 9) Console.WriteLine("\nCongrats, you won!");
            else Console.WriteLine("\nYou lost! It was '{0}'", solution);
        }
    }
}