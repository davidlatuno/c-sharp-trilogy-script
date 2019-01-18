using System;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string userInput;
            int initialValue;
            int endingValue;
            int arrayLength;
            int arrayIndex = 0;
            string[] allFolders = Directory.GetDirectories(".", "*", SearchOption.TopDirectoryOnly);
            string[] solvedFolders = Directory.GetDirectories(".", "Solved", SearchOption.AllDirectories);
            Console.Write("Enter Initial Value or type ALL: \n");
            userInput = Console.ReadLine();
            if (userInput.ToUpper() == "ALL")
            {
                Console.WriteLine("Get all the things!!!!!");
                foreach (string i in solvedFolders)
                {
                    
                    Console.WriteLine(i);
                }
            }
            else
            {
                /* Converts to integer type */
                initialValue = Convert.ToInt32(userInput);
                Console.WriteLine("You entered {0}", initialValue);
                Console.Write("Enter Ending Value: \n");
                userInput = Console.ReadLine();
                /* Converts to double type */
                endingValue = Convert.ToInt32(userInput);
                Console.WriteLine("You entered {0}", endingValue);
                arrayLength = (endingValue - initialValue) + 1;
                int[] folderArray = new int[arrayLength];
                for (int i = initialValue; i <= endingValue; i++)
                {
                    folderArray[arrayIndex] = i;
                    arrayIndex++;
                    if (Directory.Exists(allFolders[i-1] + "/Solved"))
                    {
                        Console.WriteLine(allFolders[i-1]);
                    }
                }
                for (int i = 0; i < arrayLength; i++)
                {
                    Console.WriteLine(folderArray[i]);
                }
                // Keep console window open
                Console.WriteLine("Press any key to exit.");
                Console.ReadKey();
            }
        }
    }
}
