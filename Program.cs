namespace OTUS_Files
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var DirHandler = new DirectoryHandler();
            string DirectoryPath1 = "D:\\Otus\\Test\\TestDir1";
            string DirectoryPath2 = "D:\\Otus\\Test\\TestDir2";
            Console.WriteLine(DirHandler.CreateDirectory(DirectoryPath1));
            Console.WriteLine(DirHandler.CreateDirectory(DirectoryPath2));

            var FileHandler = new FileHandler();
            Console.WriteLine($"\nСоздаю файлы в директории {DirectoryPath1}");
            Console.WriteLine(await FileHandler.FileCollectionCreator(DirectoryPath1, "File", "txt", 10));
            Console.WriteLine($"\nСоздаю файлы в директории {DirectoryPath2}");
            Console.WriteLine(await FileHandler.FileCollectionCreator(DirectoryPath2, "File", "txt", 10));

            Console.WriteLine($"\nЗаписываю в файлы их имена в директории {DirectoryPath1}");
            Console.WriteLine(await FileHandler.AllFileTextWriterName(DirectoryPath1));
            Console.WriteLine($"\nЗаписываю в файлы их имена в директории {DirectoryPath2}");
            Console.WriteLine(await FileHandler.AllFileTextWriterName(DirectoryPath2));

            Console.WriteLine($"\nДобавляю даты в файлы в директории {DirectoryPath1}");
            Console.WriteLine(await FileHandler.AddToAllFilesCurrentDate(DirectoryPath1));
            Console.WriteLine($"\nДобавляю даты в файлы в директории {DirectoryPath2}");
            Console.WriteLine(await FileHandler.AddToAllFilesCurrentDate(DirectoryPath2));

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВот текст из ранее записанных файлов в директории {DirectoryPath1}");
            Console.ResetColor();
            Console.WriteLine(await FileHandler.ReadAllFilesInDirectory(DirectoryPath1));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nВот текст из ранее записанных файлов в директории {DirectoryPath2}");
            Console.ResetColor();
            Console.WriteLine(await FileHandler.ReadAllFilesInDirectory(DirectoryPath2));
        }
    }
}
