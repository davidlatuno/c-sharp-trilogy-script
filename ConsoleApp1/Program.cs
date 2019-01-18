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
            DirectoryInfo parentFolder = Directory.GetParent(".");
            string parentName = parentFolder.ToString();
            string[] parentNameArray = parentName.Split('\\');
            Console.Write("Enter Initial Value or type ALL: \n");
            userInput = Console.ReadLine();
            if (userInput.ToUpper() == "ALL")
            {
                Console.WriteLine("Get all the things!!!!!");
                foreach (string i in solvedFolders)
                {
                    string target = "..\\..\\..\\..\\UCSD201807FSF5\\" + parentNameArray[parentNameArray.Length - 1] + @"\01-Activities" + i.Substring(1);
                    Console.WriteLine(target);
                    Console.WriteLine(parentNameArray[parentNameArray.Length - 1]);
                    DirectoryCopy(i, target, true);
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
                        //Directory.Delete(allFolders[i - 1] + "/Solved", true);
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

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            DirectoryInfo[] dirs = dir.GetDirectories();
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location.
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
    }
}
