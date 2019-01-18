﻿using System;

namespace ConsoleApp1
{
    class Program
    {
        static System.Collections.Specialized.StringCollection log = new System.Collections.Specialized.StringCollection();
        static void Main(string[] args)
        {
            string userInput;
            int initialValue;
            int endingValue;
            int arrayLength;
            int arrayIndex = 0;

            Console.Write("Enter Initial Value or type ALL: \n");
            userInput = Console.ReadLine();
            if (userInput.ToUpper() == "ALL")
            {
                Console.WriteLine("Get all the things!!!!!");
                // Start with drives if you have to search the entire computer.
                string[] drives = Environment.GetLogicalDrives();

                foreach (string dr in drives)
                {
                    System.IO.DriveInfo di = new System.IO.DriveInfo(dr);

                    // Here we skip the drive if it is not ready to be read. This
                    // is not necessarily the appropriate action in all scenarios.
                    if (!di.IsReady)
                    {
                        Console.WriteLine("The drive {0} could not be read", di.Name);
                        continue;
                    }
                    System.IO.DirectoryInfo rootDir = di.RootDirectory;
                    WalkDirectoryTree(rootDir);
                }

                // Write out all the files that could not be processed.
                Console.WriteLine("Files with restricted access:");
                foreach (string s in log)
                {
                    Console.WriteLine(s);
                }
                // Keep the console window open in debug mode.
                Console.WriteLine("Press any key");
                Console.ReadKey();
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

        static void WalkDirectoryTree(System.IO.DirectoryInfo root)
        {
            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;

            // First, process all the files directly under this folder
            try
            {
                files = root.GetFiles("*.*");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    Console.WriteLine(fi.FullName);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo);
                }
            }
        }
    }
}
