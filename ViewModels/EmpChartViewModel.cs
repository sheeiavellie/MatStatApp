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
    internal class EmpChartViewModel : ViewModel
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

        private EmpFunction _emp;
        public EmpFunction emp
        {
            get => _emp;
            set => Set(ref _emp, value);
        }

        private ObservableCollection<Tuple<string, double>> _Table;
        public ObservableCollection<Tuple<string, double>> Table
        {
            get => _Table;
            set => Set(ref _Table, value);
        }

        public EmpChartViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);

            var data = fc.GetEmpData(Sample.sample);

            var theor_data = new List<Tuple<double, double>>();

            foreach(var e in Sample.sample.OrderBy(i => i))
            {
                theor_data.Add(new Tuple<double, double>(e, fc.Calculate_Theor(e, fc.GetMedian(Sample.sample), fc.GetVarianceStandartDeviation(Sample.sample))));
            }

            emp = new EmpFunction(data, theor_data);

            var x = new List<Tuple<string, double>>();

            foreach(var item in data)
            {
                x.Add(new Tuple<string, double>(string.Join(" - ", item.Value), item.Key));
            }

            Table = new ObservableCollection<Tuple<string, double>>(x);
        }
    }
}
