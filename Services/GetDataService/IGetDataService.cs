using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.GetDataService
{
    internal interface IGetDataService
    {
        public double[] GetData(string filePath);
    }
}
