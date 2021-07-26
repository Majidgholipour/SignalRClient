using System;
using System.IO;

namespace Client.Config
{
    public static class FileConfig
    {

        public static void writetoFile(int MaxId)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\maxEventLogIndex.txt";

                if (!File.Exists(path))
                {
                    File.CreateText(path);
                }
                else
                {
                    File.WriteAllText(path, MaxId.ToString());
                }
            }
            catch (Exception)
            {
            }

        }

        public static int readFromFile()
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + "\\maxEventLogIndex.txt";
                if (File.Exists(path))
                {
                    return int.Parse(File.ReadAllText(path));
                }
                else
                {
                    return 0;
                }

            }
            catch (Exception)
            {
                return 0;
            }

        }
    }
}
