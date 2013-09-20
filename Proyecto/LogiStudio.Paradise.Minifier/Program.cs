
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Yahoo.Yui.Compressor;

namespace LogiStudio.Paradise.Minifier
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating temp folder...");
            string srcPath = ConfigurationManager.AppSettings["RootFolder"];
            string tempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            if (Directory.Exists(tempPath))
                Directory.Delete(tempPath, true);

            DirectoryCopy(srcPath, tempPath, true);
            Console.WriteLine("Done!");

            Minify(srcPath, tempPath, "css");
            Minify(srcPath, tempPath, "js");

            Console.WriteLine("Deleting temp folder...");
            Directory.Delete(tempPath, true);
            Console.WriteLine("Done!");
            Console.WriteLine("Process completed!");
            Console.Read();
        }

        private static void Minify(string srcPath, string tempPath, string type)
        {
            string extension;
            if (type == "js")
                extension = "*.js";
            else if (type == "css")
                extension = "*.css";
            else
                throw new Exception("Type must be 'js' or 'css'");

            List<string> files = new List<string>();
            foreach (string file in Directory.EnumerateFiles(tempPath, extension, SearchOption.AllDirectories))
            {
                if (!file.ToLower().EndsWith(".min.js") && !file.ToLower().EndsWith(".min.css"))
                    files.Add(file);
            }

            foreach (string file in files)
            {
                try
                {
                    Console.WriteLine("Minifying " + Path.GetFileName(file) + "...");
                    string dstFile = file.Replace(tempPath, srcPath);

                    Process process = new Process();
                    Compressor compressor;

                    process.StartInfo.FileName = "java";

                    if (type == "js")
                        compressor = new JavaScriptCompressor();
                    else
                        compressor = new CssCompressor();

                    var compressed = compressor.Compress(File.ReadAllText(file));
                    File.WriteAllText(dstFile, compressed);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }

        private static void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

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
