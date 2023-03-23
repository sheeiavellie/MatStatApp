using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.GetDataService
{
    internal class GetDataService : IGetDataService
    {
        public double[] GetData(string filePath)
        {
            if(filePath != null)
            {
                String input = File.ReadAllText(filePath);
                input = input.Trim(' ');

                var array = Array.ConvertAll(input.Split('\u002C'), Double.Parse);

                return array;
            }

            return null;
        }
    }
}
