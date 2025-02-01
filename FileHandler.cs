using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTUS_Files
{
    internal class FileHandler
    {
        /// <summary>
        /// Создаем N файлов в указанной директории
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <param name="FileName"></param>
        /// <param name="FileExtension"></param>
        /// <param name="FileCount"></param>
        /// <returns></returns>
        public async Task<string> FileCollectionCreator(string DirectoryName, string FileName, string FileExtension, int FileCount)
        {
            List<string> FileList = new List<string>();

            if (Directory.Exists(DirectoryName)) {
                for (int i = 1; i <= FileCount; i++) {
                    string FileFullName = $"{FileName}{i.ToString()}.{FileExtension}";
                    string FilePath = Path.Combine(DirectoryName, FileFullName);

                    if (File.Exists(FilePath)) {
                        FileList.Add($"Файл {FileFullName} уже существует в этой директории");
                    } 
                    else
                    {
                        FileList.Add($"Файл {FileFullName} успешно создан!");
                        await using (File.Create(FilePath));
                    }
                }
            }

            return string.Join(",\n", FileList);
        }

        /// <summary>
        /// Прописываем в файл его имя
        /// </summary>
        /// <param name="DirectoryName"></param>
        /// <returns></returns>
        public async Task<string> AllFileTextWriterName (string DirectoryName)
        {
            string[] files = Directory.GetFiles(DirectoryName);
            List<string> FileList = new List<string>();

            if (files.Length > 0)
            {
                Console.WriteLine("Найдены следующие файлы:");
                foreach (string file in files)
                {

                    if (FileWritePermissionChecker(file))
                    {
                        FileList.Add($"Запись в файл {file} прошла успешно!");
                        await File.WriteAllTextAsync(file, Path.GetFileName(file), Encoding.UTF8);
                    }
                    else
                    {
                        FileList.Add($"Записать информацию в {file} не удалось");
                    }                    
                }
            }
            else
            {
                FileList.Add("В указанной папке нет файлов.");                
            }

            return string.Join(",\n", FileList);
        }

        public async Task<string> AddToAllFilesCurrentDate (string DirectoryName)
        {
            string[] files = Directory.GetFiles(DirectoryName);
            List<string> FileList = new List<string>();

            if (files.Length > 0)
            {
                foreach (string file in files)
                {

                    if (FileWritePermissionChecker(file))
                    {
                        FileList.Add($"В {file} успешно добавлена текущая дата!");
                        await File.AppendAllTextAsync(file, $"\n{DateTime.Now.ToString("D", new CultureInfo("ru-RU"))}", Encoding.UTF8);
                    }
                    else
                    {
                        FileList.Add($"Добавить информацию в {file} не удалось");
                    }
                }
            }
            else
            {
                FileList.Add("В указанной папке нет файлов.");
            }

            return string.Join(",\n", FileList);
        }

        public async Task<string> ReadAllFilesInDirectory(string DirectoryName)
        {
            string[] files = Directory.GetFiles(DirectoryName);
            List<string> FileList = new List<string>();

            if (files.Length > 0)
            {
                foreach (string file in files)
                {
                    string[] lines = await File.ReadAllLinesAsync(file);
                    string text = string.Join(" ", lines);
                    FileList.Add($"Файл {Path.GetFileName(file)}: {text}");
                }
            }
            else
            {
                FileList.Add("В указанной папке нет файлов.");
            }

            return string.Join(",\n", FileList);
        }

        /// <summary>
        /// Проверяем доступен ли файл для записи
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private bool FileWritePermissionChecker (string FilePath)
        {
            try
            {
                var FileInfo = new FileInfo(FilePath);
                if (FileInfo.IsReadOnly)
                    return false;

                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }            
        }
    }
}
