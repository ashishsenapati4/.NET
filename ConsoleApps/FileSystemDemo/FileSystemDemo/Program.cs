using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSystemDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string rootPath = @"C:\Projects\DumyDir";
            //string[] dirs = Directory.GetDirectories(rootPath,"*",SearchOption.AllDirectories);
            //foreach(string dir in dirs)
            //{
            //    Console.WriteLine(dir);
            //}

            var files = Directory.GetFiles(rootPath, "*.*", SearchOption.AllDirectories);
            //foreach(string str in files)
            //{
            //    //Console.WriteLine(str);
            //    //Console.WriteLine(Path.GetFileName(str));
            //    //Console.WriteLine(Path.GetFileNameWithoutExtension(str));
            //    //Console.WriteLine(Path.GetFullPath(str));

            //    FileInfo fileInfo = new FileInfo(str);

            //    Console.WriteLine($"{fileInfo.Name} - Size: {fileInfo.Length} bytes - Creation: {fileInfo.CreationTime}");

                
            //}



            Console.ReadLine();
        }
    }
}
