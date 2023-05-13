using MatStat;
using MatStatApp.Infrastructure.Commands;
using MatStatApp.Services.SaveSampleService;
using MatStatApp.ViewModels.Base;
using Prism.Services.Dialogs;
using System;
using MatStatApp.Models;
using System.Windows.Input;

namespace MatStatApp.ViewModels
{
    internal class GenerateRNormViewModel : ViewModel
    {
        private ISaveSampleService _saveSampleService;

        private Functions functions = new Functions();
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p)
        {
            if (p != null)
            {
                var t = p as DialogWindow;
                t.Close();
            }
        }

        public ICommand SaveCommand { get; }

        private bool CanSaveCommandExecute(object p) => true;
        private void OnSaveCommandExecuted(object p)
        {
            var data = "";
            foreach (var num in Sample.generated_sample)
            {
                data += num.ToString().Replace(',', '.');
                data += ", ";
            }
            data = data.Remove(data.Length - 2);
            _saveSampleService.Save(data);
        }

        public ICommand GenerateCommand { get; }

        private bool CanGenerateCommandExecute(object p) => true;
        private void OnGenerateCommandExecuted(object p)
        {
            //Sample.generated_sample = functions.RNorm(Convert.ToDouble(Nu), Convert.ToDouble(Sigma), Convert.ToInt32(Size));
            Random rand = new(0);
            Sample.generated_sample = ScottPlot.DataGen.RandomNormal(rand, pointCount: Convert.ToInt32(Size), mean: Convert.ToDouble(Nu), stdDev: Convert.ToDouble(Sigma));
        }

        private string _Nu = "0";
        public string Nu
        {
            get => _Nu;
            set => Set(ref _Nu, value);
        }

        private string _Sigma = "1";
        public string Sigma
        {
            get => _Sigma;
            set => Set(ref _Sigma, value);
        }

        private string _Size = "1";
        public string Size
        {
            get => _Size;
            set => Set(ref _Size, value);
        }

        public GenerateRNormViewModel()
        {
            _saveSampleService = new SaveSampleService();

            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SaveCommand = new RelayCommand(OnSaveCommandExecuted, CanSaveCommandExecute);
            GenerateCommand = new RelayCommand(OnGenerateCommandExecuted, CanGenerateCommandExecute);
        }
    }
}
