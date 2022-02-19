//after redoing it 2nd time: it sort of works(not fully), was good practice but it's still awful code, not proud of it.
//I'll try again from the ground up on objects, if it fails... hey future me, back to this? ouch.

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
            if (userInput.Key != ConsoleKey.E && userInput.Key != ConsoleKey.H) Console.WriteLine("Please press E for easy or H for hard difficulty:");
        } while (userInput.Key != ConsoleKey.E && userInput.Key != ConsoleKey.H);
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
    /*
    static string[,] Initialize2dArray(string[] wordArray)
    {
        string[,] Initialized2dArray = new string[wordArray.Length,wordArray.Length];
        for (int i = 0; i < wordArray.Length; i++)
        {
            Initialized2dArray[i,0] = wordArray[i];
            Initialized2dArray[1,i] = "0";
        }
        return Initialized2dArray;
    }*/
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
        //char[] lastInput= new char[] { 'f', '9' };
        bool playedYet = false;
        char rowChoice = '9';
        char lastInputRowChoice='9';
        int columnChoice = 9;
        int lastInputColumnChoice=9;
        bool wasLastRowA = false;
        bool justGotPair = false;
        bool faultyInput = false;
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
        int[] arrayParamA = new int[wordAmount+1];
        int[] arrayParamB = new int[wordAmount+1];
        for(int i = 0; i <= wordAmount; i++)//if not looping this will work
        {
            arrayParamA[i] = 0;
            arrayParamB[i] = 0;
        }
        wordArrayB = wordArrayA.OrderBy(x => Random().Next()).ToArray();
        // string[,] wordArrayA2D = Initialize2dArray(wordArrayA);
        // string[,] wordArrayB2D = Initialize2dArray(wordArrayB);

        while (guessesLeft > 0)
        {
            Console.WriteLine("\n Please choose and type in row and column eg. 'A1'\n");
            char[] userChoice = Console.ReadLine().ToLower().ToCharArray();
            //Console.Clear();
            if (userChoice.Length == 2)
            {
                 faultyInput = false;
                 lastInputRowChoice = rowChoice;
                 lastInputColumnChoice = columnChoice;
                 rowChoice = userChoice[0];
                 columnChoice = int.Parse(userChoice[1].ToString());//todo: fix exceptions from wrong user input



                if (wordAmount == 4)
                {

                        if (rowChoice == 'a')
                        {

                            if (wasLastRowA == true && justGotPair == false && playedYet == true)
                            {
                                
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                                Console.WriteLine("Cannot choose the same row twice ");
                                arrayParamA[columnChoice - 1] = 0;
                                arrayParamA[lastInputColumnChoice - 1] = 0;
                                //wasLastRowA = false;
                                faultyInput = true;
                                
                            }

                            if (playedYet == true && wasLastRowA == false && faultyInput==false)
                            {
                                if (IsInputProper(lastInputColumnChoice, columnChoice, arrayParamA, playedYet,rowChoice,lastInputRowChoice) == true)
                                {
                                    if (wordArrayA[columnChoice - 1] == wordArrayB[lastInputColumnChoice - 1] && justGotPair == false)
                                    {
                                        arrayParamA[columnChoice - 1] = 1;
                                        arrayParamB[lastInputColumnChoice - 1] = 1;
                                        wasLastRowA = true;
                                        justGotPair = true;
                                    }
                                    /*  if (wordArrayA[columnChoice - 1] == wordArrayB[lastInputColumnChoice - 1] && justGotPair == true)
                                      {
                                          arrayParamA[columnChoice - 1] = 1;
                                          wasLastRowA = true;
                                          justGotPair = false;
                                      }*/
                                    if (wordArrayA[columnChoice - 1] != wordArrayB[lastInputColumnChoice - 1] && justGotPair == false)
                                    {
                                        arrayParamB[lastInputColumnChoice - 1] = 0;
                                        arrayParamA[columnChoice - 1] = 1;
                                        wasLastRowA = true;
                                        guessesLeft--;
                                        justGotPair = false;
                                    }
                                    if (wordArrayA[columnChoice - 1] != wordArrayB[lastInputColumnChoice - 1] && justGotPair == true)
                                    {
                                        arrayParamA[columnChoice - 1] = 1;//changed here
                                        //arrayParamB[lastInputColumnChoice - 1] = 1;//and here
                                        wasLastRowA = true;
                                        //guessesLeft--;
                                        justGotPair = false;
                                    }

                                }
                                else
                                    {
                                    if (lastInputColumnChoice == columnChoice && lastInputRowChoice == rowChoice)
                                    {
                                        Console.WriteLine("Choose a different field than what you just picked");
                                    }
                                    else
                                    {
                                        arrayParamB[lastInputColumnChoice - 1] = 0;
                                        guessesLeft--;
                                        //wasLastRowA = false;
                                    }

                            }
                            DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                            }
                            if (wasLastRowA == true && justGotPair == true && faultyInput == false)//test
                            {
                                if (IsInputProper(lastInputColumnChoice, columnChoice, arrayParamA, playedYet, rowChoice, lastInputRowChoice) == true)
                                {
                                    arrayParamA[columnChoice - 1] = 1;
                                }
                                Console.Clear();
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                                Console.WriteLine("you're in lastrowtrue pair true a property");
                            }
                            /*if (wasLastRowA == true && justGotPair == false && playedYet == true)
                            {
                                /*arrayParamA[columnChoice - 1] = 0;
                                columnChoice = lastInputColumnChoice;
                                Console.Clear();
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                            }*/
                            if (playedYet == false)
                            {
                                if (IsInputProper(lastInputColumnChoice, columnChoice, arrayParamA, playedYet,rowChoice, lastInputRowChoice) == true)
                                {
                                    playedYet = true;
                                    arrayParamA[columnChoice - 1] = 1;
                                    wasLastRowA = true;
                                }
                                else
                                {
                                    playedYet = false;
                                }
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                            }
                        }
                        if (rowChoice == 'b')
                        {
                            if (wasLastRowA == false && playedYet == true && justGotPair == false)
                            {
                                
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                                Console.WriteLine("Cannot choose the same row twice");
                                //wasLastRowA = true;
                                arrayParamB[columnChoice - 1] = 0;
                                arrayParamB[lastInputColumnChoice - 1] = 1;
                                faultyInput = true;
                            }
                            if (playedYet == true && wasLastRowA == true && faultyInput == false)
                            {
                                if (IsInputProper(lastInputColumnChoice, columnChoice, arrayParamB, playedYet, rowChoice, lastInputRowChoice) == true)
                                {
                                    if (wordArrayB[columnChoice - 1] == wordArrayA[lastInputColumnChoice - 1] && justGotPair == false)
                                    {
                                        arrayParamB[columnChoice - 1] = 1;
                                        arrayParamA[lastInputColumnChoice - 1] = 1;
                                        wasLastRowA = false;
                                        justGotPair = true;
                                    }
                                    /*if(wordArrayB[columnChoice - 1] == wordArrayA[lastInputColumnChoice - 1] && justGotPair == true)
                                    {
                                        arrayParamB[columnChoice - 1] = 1;
                                        wasLastRowA = false;
                                        justGotPair = false;
                                    }*/
                                    if (wordArrayB[columnChoice - 1] != wordArrayA[lastInputColumnChoice - 1] && justGotPair == false)
                                    {
                                        arrayParamA[lastInputColumnChoice - 1] = 0;
                                        arrayParamB[columnChoice - 1] = 1;
                                        wasLastRowA = false;
                                        guessesLeft--;
                                        justGotPair = false;
                                    }
                                    if (wordArrayB[columnChoice - 1] != wordArrayA[lastInputColumnChoice - 1] && justGotPair == true)
                                    {
                                        arrayParamB[columnChoice - 1] = 1;//swawpped/changed here A and B
                                        //arrayParamA[lastInputColumnChoice - 1] = 1;//and here
                                        wasLastRowA = false;
                                        //guessesLeft--;
                                        justGotPair = false;
                                    }
                                }

                                else
                                {
                                    if(lastInputColumnChoice==columnChoice&&lastInputRowChoice==rowChoice)
                                    {
                                        Console.WriteLine("Choose a different field than what you just picked");
                                    }
                                    else
                                    {
                                        arrayParamB[lastInputColumnChoice - 1] = 0;
                                        guessesLeft--;
                                        //wasLastRowA = false;
                                    }

                                }

                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                            }
                            if (wasLastRowA == false && justGotPair == true && faultyInput==false)//test
                            {
                                if (IsInputProper(lastInputColumnChoice, columnChoice, arrayParamA, playedYet, rowChoice, lastInputRowChoice) == true)
                                {
                                 arrayParamB[columnChoice - 1] = 1;
                                }
                                Console.Clear();
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                                Console.WriteLine("you're in lastrowtrue pair true b property");
                            }
                            /*if (wasLastRowA == false && justGotPair == false && playedYet==true)
                            {
                                /*arrayParamB[columnChoice - 1] = 0;
                                columnChoice = lastInputColumnChoice;
                                Console.Clear();
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                            }*/
                            if (playedYet == false)
                            {
                                if (IsInputProper(lastInputColumnChoice, columnChoice, arrayParamB, playedYet, rowChoice, lastInputRowChoice) == true)
                                {
                                    playedYet = true;
                                    arrayParamB[columnChoice - 1] = 1;
                                    wasLastRowA = false;
                                }
                                else
                                {
                                    playedYet = false;
                                }
                                DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                            }
                        }

                    /*
                                        if (userChoice[0] == 'b' && int.Parse(userChoice[1].ToString()) < wordAmount)
                                        {
                                            Console.WriteLine("\nA: \tX \tX \tX \tX");
                                            Console.Write("\nB:\t");
                                            for (int j = 0; j < wordAmount; j++)
                                            {
                                                if (int.Parse(userChoice[1].ToString()) == j + 1)
                                                {
                                                    Console.Write(wordArrayB[j]);
                                                }
                                                else Console.Write("X\t");
                                            }
                                        }
                                        DisplayGameStatus(guessesLeft, wordArrayA, wordArrayB, arrayParamA, arrayParamB);
                                    }
                                    if (wordAmount == 8)
                                    {
                                        Console.WriteLine("\n\t\t1 \t\t2 \t\t3 \t\t4 \t\t5 \t\t6 \t\t7 \t\t8");
                                        Console.Write("\nA:\t");

                                        for (int i = 0; i < wordArrayA.Length; i++)
                                        {
                                            Console.Write(wordArrayA[i] + "\t\t ");
                                        }
                                        Console.Write("\nB:\t");
                                        for (int i = 0; i < wordArrayB.Length; i++)
                                        {
                                            Console.Write(wordArrayB[i] + "\t\t ");
                                        }*/
                }
            }


            
            // PlayGame(wordAmount, guessesLeft);
            // PlayGame(wordAmount, guessesLeft, wordArrayA2D, wordArrayB2D);


        }
    }
    //static void PlayGame(int wordAmount, int guessesLeft)
    //{
    //
    //}
    static Boolean IsInputProper(int lastInput, int columnChoice,int[] arrayParam, bool playedYet, char lastRowInput, char currentRowInput)
    {
        if (playedYet == false)
        {
            if (columnChoice > 0 && columnChoice <= arrayParam.Length)
            {
                return true;
            }
            return false;
        }
        if (lastInput == columnChoice && playedYet==true && lastRowInput==currentRowInput) return false;
        if (lastInput != columnChoice && columnChoice > 0 && columnChoice < arrayParam.Length && playedYet==true && lastRowInput!=currentRowInput)
        {
            if (arrayParam[columnChoice-1]!=1)
            {
                return true;
            }
            else return false;

        }
        if (lastInput == columnChoice && columnChoice > 0 && columnChoice < arrayParam.Length && playedYet == true && lastRowInput != currentRowInput)
        {
            if (arrayParam[columnChoice - 1] != 1)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
    static void DisplayGameStatus(int guessesLeft, string[] wordArrayA, string[] wordArrayB, int[] statusArrayA, int[] statusArrayB)
    {
        Console.WriteLine("\n-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@");
        Console.WriteLine("\n\tTotal pair amount: " + wordArrayA.Length);
        Console.WriteLine("\n\tGuesses left: " + guessesLeft);
        Console.WriteLine("\n\t\t1 \t2 \t3 \t4");
        Console.Write("\tA:");
        for (int i = 0; i < wordArrayA.Length; i++)
        {
            if (statusArrayA[i] == 1)
            {
                Console.Write("\t" + wordArrayA[i]);
            }
            else Console.Write("\tX");
        }
        Console.Write("\n\tB:");
        for (int i = 0; i < wordArrayB.Length; i++)
        {
            if (statusArrayB[i] == 1)
            {
                Console.Write("\t" + wordArrayB[i]);
            }
            else Console.Write("\tX");
        }
        Console.WriteLine("\n-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@-@");
    }
}