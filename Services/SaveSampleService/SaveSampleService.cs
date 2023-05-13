using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.SaveSampleService
{
    internal class SaveSampleService : ISaveSampleService
    {
        public void Save(string sample)
        {
            DateTime dateTime = DateTime.Now;
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var file_name = "sample_" + dateTime.ToString().Replace(" ", "_").Replace(":", ".") + ".txt";

            File.WriteAllText(path + "\\" + file_name, sample + Environment.NewLine);
        }
        /*public void Save(string sample, string nu, string sigma)
        {
            DateTime dateTime = DateTime.Now;
            var path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var file_name = "sample_" + dateTime.ToString().Replace(" ", "_").Replace(":", ".") + ".txt";

            if (!File.Exists(path + "\\" + file_name))
            {
                using (StreamWriter sw = File.CreateText(path + "\\" + file_name))
                {
                    sw.WriteLine(sample);
                }
            }
        }*/
    }
}
