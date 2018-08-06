using System;
using System.IO;

public class SaveMachine
{
    public static void WriteIt(string path, string content)
    {
        try
        {
            // Create a file to write to.
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(content);
            }
        }
        catch(Exception e) { Console.WriteLine(e); }
    }
}
