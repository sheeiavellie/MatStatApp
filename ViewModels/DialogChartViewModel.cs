using MatStat;
using MatStatApp.Models;
using MatStatApp.Models.Charts;
using MatStatApp.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.ViewModels
{
    internal class DialogChartViewModel : ViewModel
    {
        private Functions fc = new Functions();

        private Gistogramm<int> _gist;
        public Gistogramm<int> gist
        {
            get => _gist;
            set => Set(ref _gist, value);
        }

        public DialogChartViewModel()
        {
            var t = fc.GetGistogrammData(Sample.sample);

            gist = new Gistogramm<int>("Значения", "Частота",
                t.Select(x => x.Item2).OrderBy(x => x).ToArray(),
                t.Select(x => x.Item1.ToString()).OrderBy(x => x).ToArray());
        }
    }
}
