// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");
//FileStream wordsStream = new FileStream("Words.txt", FileMode.Open, FileAccess.Read, FileShare.Read);
//Console.Write(wordsStream.Length);
//try with two 1 dimensional arrays double the size of row so you don't forget
using System;
class Game
{
    static Random Random()
    {
        return new Random();
    }
    static void Main()
    {
        Console.WriteLine("Hello, welcome to the memory game.");
        Console.WriteLine("Please choose your difficulty, input E for easy, H for hard\n");
        ConsoleKeyInfo userInput;
        do
        {
            userInput = Console.ReadKey(true);
            if (userInput.Key != ConsoleKey.E & userInput.Key != ConsoleKey.H) Console.WriteLine("Please press E for easy or H for hard difficulty:");
        } while (userInput.Key != ConsoleKey.E & userInput.Key != ConsoleKey.H);
        Console.WriteLine("\nLet's start the game at "+userInput.Key.ToString()+" mode");
        GameMatrixCreate(userInput);
    }
    static string[] WordFileReader()
    {
        string[] puzzleWords = File.ReadAllLines("Words.txt");
        return puzzleWords;
    }
    static string[] GameWordPrepper(int wordAmount)
    {
        string[] wordBank = WordFileReader();
        string[] wordArray = new string[wordAmount];
        for (int i = 0; i < wordAmount; i++)
        {
            wordArray[i] = wordBank[Random().Next(wordBank.Length)]; 
        }
        return wordArray;

    }
    static string[,] Initialize2dArray(string[] wordArray)
    {
        string[,] Initialized2dArray = new string[wordArray.Length,wordArray.Length];
        for (int i = 0; i < wordArray.Length; i++)
        {
            Initialized2dArray[i,0] = wordArray[i];
            Initialized2dArray[1,i] = "0";
        }
        return Initialized2dArray;
    }
    /* gone way too deep here, if I get lost and i just wrote it, it can't be good. Time for a different approach
    static void PlayGame(int wordAmount, int guessesLeft, string[,] wordArrayA2D , string[,] wordArrayB2D)
    {
        char[,] testTable1 = new char[2,4] { { 'a', 'b', 'c', 'd' }, { '1', '2', '3', '4' } };
        char[,] testTable2 = new char[2,8] { { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' }, { '1', '2', '3', '4', '5', '6', '7', '8' } };
        bool improperInput = true;
        string checker = null;
        int attemptCounter = 0;
        while (guessesLeft > 0)
        {
            Console.Clear();
            improperInput = true;
            Console.WriteLine("\n-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@");
            Console.WriteLine("\n\tTotal pair amount: " + wordAmount);
            Console.WriteLine("\n\tGuesses left: " + guessesLeft);
            Console.WriteLine("\n\t1 \t2 \t3 \t4");
            Console.Write("\nA:\t");

            for (int i = 0; i < wordArrayA2D.GetLength(0); i++)
            {
                if (wordArrayA2D[i, 1] == "1")
                {
                    Console.Write(wordArrayA2D[i, 0] + "\t\t ");
                }
                else Console.Write("X\t\t ");
            }
            Console.Write("\nB:\t");
            for (int i = 0; i < wordArrayB2D.GetLength(0); i++)
            {
                if (wordArrayB2D[i, 1] == "1")
                {
                    Console.Write(wordArrayB2D[i, 0] + "\t\t ");
                }
                else Console.Write("X\t\t ");
            }

            while (improperInput)
            {
                Console.WriteLine("\n Please choose and type in row and column eg. 'A1'\n");
                char[] userChoice = Console.ReadLine().ToLower().ToCharArray();
                if (userChoice.Length == 2)//test if input is proper length
                {
                    for (int i = 0; i < wordAmount; i++)
                    {

                        if (testTable1[0, i] == userChoice[0])//check which row the user picked
                        {
                            for (int j = 0; j < wordAmount; j++)
                            {
                                if (testTable1[1, j] == userChoice[1])//check which column the user picked
                                {
                                    if (userChoice[0] == 'a')//if row A
                                    {
                                        wordArrayA2D[i, 1] = "1";//reveal word
                                        attemptCounter++;
                                        if (attemptCounter == 2)//checking if two words are uncovered
                                        {
                                            if (wordArrayA2D[i, 0] != checker)//do the two uncovered words match
                                            {
                                                wordArrayA2D[i, 1] = "0";//hide current word
                                                for (int k = 0; k < wordAmount; k++)//find the previous word
                                                {
                                                    if (wordArrayB2D[k, 0] == checker) wordArrayB2D[k, 1] = "0";//hide previous word
                                                }
                                                //checker = "test";//hmm debug reset? dunno
                                                attemptCounter = 0;//reset amount of words uncovered
                                                guessesLeft--;//subtract an attempt
                                            }
                                        }
                                        else checker = wordArrayA2D[i, 0];//set currently revealed word as previous

                                    }
                                    if (userChoice[0] == 'b')
                                    {
                                        wordArrayB2D[j, 1] = "1";
                                        attemptCounter++;
                                        if (attemptCounter == 2)
                                        {
                                            if (wordArrayB2D[i, 0] != checker)
                                            {
                                                wordArrayB2D[i, 1] = "0";
                                                for (int k = 0; k < wordAmount; k++)
                                                {
                                                    if (wordArrayA2D[k, 0] == checker) wordArrayA2D[k, 1] = "0";
                                                }
                                                //checker = "test";
                                                attemptCounter = 0;
                                                guessesLeft--;
                                            }
                                        }
                                        else checker = wordArrayB2D[i, 0];
                                    }
                                    improperInput = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < wordAmount; j++)
                            {
                                if (testTable1[1, j] == userChoice[1])//check which column the user picked
                                {
                                    if (userChoice[0] == 'a')//if row A
                                    {
                                        wordArrayA2D[i, 1] = "1";//reveal word
                                        attemptCounter++;
                                        if (attemptCounter == 2)//checking if two words are uncovered
                                        {
                                            if (wordArrayA2D[i, 0] != checker)//do the two uncovered words match
                                            {
                                                wordArrayA2D[i, 1] = "0";//hide current word
                                                for (int k = 0; k < wordAmount; k++)//find the previous word
                                                {
                                                    if (wordArrayB2D[k, 0] == checker) wordArrayB2D[k, 1] = "0";//hide previous word
                                                }
                                                //checker = "test";//hmm debug reset? dunno
                                                attemptCounter = 0;//reset amount of words uncovered
                                                guessesLeft--;//subtract an attempt
                                            }
                                        }
                                        else checker = wordArrayA2D[i, 0];//set currently revealed word as previous

                                    }
                                    if (userChoice[0] == 'b')
                                    {
                                        wordArrayB2D[j, 1] = "1";
                                        attemptCounter++;
                                        if (attemptCounter == 2)
                                        {
                                            if (wordArrayB2D[i, 0] != checker)
                                            {
                                                wordArrayB2D[i, 1] = "0";
                                                for (int k = 0; k < wordAmount; k++)
                                                {
                                                    if (wordArrayA2D[k, 0] == checker) wordArrayA2D[k, 1] = "0";
                                                }
                                                //checker = "test";
                                                attemptCounter = 0;
                                                guessesLeft--;
                                            }
                                        }
                                        else checker = wordArrayB2D[i, 0];
                                    }
                                    improperInput = false;
                                    break;
                                }
                            }
                        }
                        
                    }

                }
                else Console.WriteLine("\nImproper Input");
            }

        }
    }
    */
    static void GameMatrixCreate(ConsoleKeyInfo difficultyKey)
    {
        int wordAmount = 0;
        int guessesLeft = 0;
        if (difficultyKey.Key == ConsoleKey.E)
        {
            wordAmount = 4;
            guessesLeft = 10;
        }
        else
        {
            wordAmount = 8;
            guessesLeft = 15;
        }
        string[] wordArrayA = GameWordPrepper(wordAmount);
        string[] wordArrayB = new string[wordAmount];
        wordArrayB = wordArrayA.OrderBy(x => Random().Next()).ToArray();
        string[,] wordArrayA2D = Initialize2dArray(wordArrayA);
        string[,] wordArrayB2D = Initialize2dArray(wordArrayB);

        if (wordAmount == 4)
        {
           // PlayGame(wordAmount, guessesLeft, wordArrayA2D, wordArrayB2D);
        }
        if (wordAmount == 8)
        {
            Console.WriteLine("\n\t\t1 \t\t2 \t\t3 \t\t4 \t\t5 \t\t6 \t\t7 \t\t8");
            Console.Write("\nA:\t");

            for (int i = 0; i < wordArrayA.Length; i++)
            {
                Console.Write(wordArrayA[i]+ "\t\t ");
            }
            Console.Write("\nB:\t");
            for (int i = 0; i < wordArrayB.Length; i++)
            {
                Console.Write(wordArrayB[i]+ "\t\t ");
            }
        }
    }
}