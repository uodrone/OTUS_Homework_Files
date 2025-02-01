using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTUS_Files
{
    internal class DirectoryHandler
    {
        /// <summary>
        /// Создаем директорию
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CreateDirectory (string path)
        {
            if (Directory.Exists(path))
                return "Такая директория уже существует";

            DirectoryInfo dir = new DirectoryInfo(path);
            dir.Create();
            return $"Директория {path} создана";
        }
    }
}
