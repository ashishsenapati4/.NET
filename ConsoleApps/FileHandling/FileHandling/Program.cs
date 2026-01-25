// See https://aka.ms/new-console-template for more information

using System.Text;
string filePath = @"C:\Projects\FileHandling\FileHandling\data.bin";
//FileStream fs = new FileStream(filePath, FileMode.Create);

//using(BinaryWriter bw = new BinaryWriter(fs))
//{
//    bw.Write(900);
//    bw.Write("Ashish! Hello World!");
//}

//fs.Close();


//Reading from the same file data.bin
//using (FileStream rfs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
//{
//    byte[] buffer = new byte[1024];
//    int bytesRead;

//    while ((bytesRead = rfs.Read(buffer, 0, buffer.Length)) > 0)
//    {
//        char[] num = Encoding.UTF8.GetChars(buffer, 0, bytesRead);
//        string text = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//        Console.WriteLine(text);
//    }
//}

//Using BinaryReader.. Reads primitive data types (int, double, bool, string) from a binary stream.
//FileStream fs = new FileStream(filePath, FileMode.Open);
//BinaryReader br = new BinaryReader(fs);

//int number = br.ReadInt32();
//string text = br.ReadString();

//Console.WriteLine(number + " " + text);

//StreamReader - Reads text(character) from a byte stream.o
//StreamReader sr = new StreamReader(filePath);
//string text = sr.ReadToEnd();
//Console.WriteLine(text);
//sr.Close();

//StreamWriter - writes text(character) to a stream
string filepath2 = @"C:\Projects\FileHandling\FileHandling\sample.txt";
//StreamWriter sWriter = new StreamWriter(filepath2);
//sWriter.WriteLine("Hello World!");
//sWriter.WriteLine("Welcome to C#!");
//sWriter.Close();

//Reading above text file using StreamReader and printing to console..
//StreamReader sr = new StreamReader(filepath2);
//string text = sr.ReadToEnd();
//Console.WriteLine(text);
//sr.Close();

//StringReader - Reads text from a string, not a file
//string str = "line1\nline2\nline3";
//StringReader sr = new StringReader(str);
//string line;
//while((line = sr.ReadLine()) != null)
//{
//    Console.WriteLine(line);
//}

//StringWriter - Writes text to a StringBuffer instead of a file.
//StringWriter writer = new StringWriter();
//writer.WriteLine("Hello World!");
//writer.WriteLine("Welcome to the era of LLMs");
//string result = writer.ToString();
//Console.WriteLine(result);

//DirectoryInfo - perform operations on directories(creating, deleting, listing directories)
//DirectoryInfo dir = new DirectoryInfo(@"C:\Projects\FileHandling\FileHandling");
//if (!dir.Exists)
//{
//    dir.Create();
//}
//foreach(var file in dir.GetFiles())
//{
//    Console.WriteLine(file.Name);
//}

//FileInfo - performs operation on files
FileInfo file = new FileInfo(filepath2);

long slen;
string fname;
DateTime ctime;
Console.WriteLine(slen = file.Length);
Console.WriteLine(fname = file.FullName);
Console.WriteLine(ctime = file.CreationTime);

using(StreamWriter writer = new StreamWriter(filepath2,true))
{
    writer.WriteLine("-------File Information--------");
    writer.WriteLine($"File length :- {slen}");
    writer.WriteLine($"File name :- {fname}");
    writer.WriteLine($"File creation time :- {ctime}");
    writer.WriteLine("-------------------------------");
}

string filePath3 = @"C:\Projects\FileHandling\FileHandling\backup.txt";
file.CopyTo(filePath3, true);
