using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.GetFileService
{
    internal class GetFileService : IGetFileService
    {
        public string GetFilePath()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Текстовый файл с массивом данных | *.txt";
            fileDialog.Title = "Выберите файл с данными...";

            bool? success = fileDialog.ShowDialog();
            if(success == true)
            {
                string path = fileDialog.FileName;

                return path;
            }
            else
            {
                return null;
            }
        }
    }
}
