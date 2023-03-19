using MatStat;
using MatStatApp.Infrastructure.Commands;
using MatStatApp.Models;
using MatStatApp.Models.Charts;
using MatStatApp.ViewModels.Base;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MatStatApp.ViewModels
{
    internal class DialogChartViewModel : ViewModel
    {
        #region CloseApplication
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            if(p != null)
            {
                var t = p as DialogWindow;
                t.Close();
            }
        }
        #endregion

        private Functions fc = new Functions();

        private Gistogramm<int> _gist;
        public Gistogramm<int> gist
        {
            get => _gist;
            set => Set(ref _gist, value);
        }

        private ObservableCollection<Tuple<double, int>> _Table;
        public ObservableCollection<Tuple<double, int>> Table
        {
            get => _Table;
            set => Set(ref _Table, value);
        }

        public DialogChartViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            var rawData = fc.GetGistogrammData(Sample.sample);
            var rawDataOrdered = rawData.OrderBy(x => x.Item1);

            gist = new Gistogramm<int>("Значения", "Частота",
                rawDataOrdered.Select(x => x.Item2).ToArray(),
                rawDataOrdered.Select(x => x.Item1.ToString()).ToArray());

            Table = new ObservableCollection<Tuple<double, int>>(rawData);
        }
    }
}
