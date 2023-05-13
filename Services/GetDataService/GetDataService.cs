using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.GetDataService
{
    internal class GetDataService : IGetDataService
    {
        public string temp_Nu = "";
        public string temp_Sigma = "";
        private double ParseDouble(string value)
        {
            double result;
            // Try parsing in the current culture
            if (!double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.CurrentCulture, out result) &&
                // Then try in US english
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.GetCultureInfo("en-US"), out result) &&
                // Then in neutral language
                !double.TryParse(value, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out result))
            {
                result = 0;
            }
            return result;
        }
        private string[] LoadData(string filePath)
        {
            if (filePath != null)
            {
                String input = File.ReadAllText(filePath);

                var input_splitted = input.Split('\n');
                return input_splitted;
            }
            return null;
        }
        public double[] GetData(string filePath)
        {
            var raw = LoadData(filePath);
            if(raw != null)
                return Array.ConvertAll(raw[0].Split('\u002C'), ParseDouble);
            return null;
        }

        /*public string GetNu(string filePath)
        {
            var raw = LoadData(filePath);
            if (raw[1] != null)
                return raw[1];

            return null;
        }

        public string GetSigma(string filePath)
        {
            var raw = LoadData(filePath);
            if (raw[2] != null)
                return raw[2];

            return null;
        }*/
    }
}
