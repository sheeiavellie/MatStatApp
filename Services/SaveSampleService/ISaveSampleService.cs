using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.SaveSampleService
{
    internal interface ISaveSampleService
    {
        public void Save(string sample);
        //public void Save(string sample, string nu, string sigma);
    }
}
