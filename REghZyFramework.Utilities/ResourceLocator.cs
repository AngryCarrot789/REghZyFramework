using System;
using System.IO;

namespace REghZyFramework.Utilities
{
    public static class ResourceLocator
    {
        public const string LOCATOR_FILE_NAME = "zzignore_sz47.txt";

        private static string APP_DIRECTORY = "";

        /// <summary>
        /// Returns the main directory containing all the app data and files.
        /// </summary>
        /// <param name="maximumBackwards">
        /// Maximum number of times the function can "go back in history", in terms of the file parents and stuff
        /// </param>
        /// <returns></returns>
        public static string GetApplicationDirectory(int maximumBackwards = 5)
        {
            if (!Directory.Exists(APP_DIRECTORY))
            {
                string launchPath = Environment.CurrentDirectory;
                const string ignorableFile = LOCATOR_FILE_NAME;
                string directory = launchPath;

                bool Search(string path)
                {
                    foreach (string file in Directory.GetFiles(path))
                    {
                        if (Path.GetFileName(file) == ignorableFile)
                            return true;
                    }

                    return false;
                }

                for (int i = 0; i < maximumBackwards; i++)
                {
                    bool hasFound = Search(directory);
                    if (!hasFound)
                    {
                        directory = Directory.GetParent(directory).FullName;
                    }

                    if (hasFound)
                    {
                        APP_DIRECTORY = directory;
                        return APP_DIRECTORY;
                    }
                }

                return "";
            }
            else
            {
                return APP_DIRECTORY;
            }
        }
    }
}
