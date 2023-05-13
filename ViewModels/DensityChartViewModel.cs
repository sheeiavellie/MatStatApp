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
using PythonPlotter;
using ScottPlot;
using System.Drawing;
using System.Reflection;
using System.IO;
using System.Windows.Media.Imaging;

namespace MatStatApp.ViewModels
{
    internal class DensityChartViewModel : ViewModel
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


        

        private BitmapImage _Graph;
        public BitmapImage Graph
        {
            get => _Graph;
            set => Set(ref _Graph, value);
        }
        public DensityChartViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            //var nu = Convert.ToDouble(Sample.Nu);
            //var sigma = Convert.ToDouble(Sample.Sigma);
            var sample = Sample.sample;

            var plt = new ScottPlot.Plot(1000, 1000);

            // create a histogram with a fixed number of bins
            ScottPlot.Statistics.Histogram hist = new(min: sample.Min() - 10, max: sample.Max() + 10, binCount: 30);

            // add random data to the histogram
            hist.AddRange(sample);

            // display histogram probabability as a bar plot
            double[] probabilities = hist.GetProbability();
            var bar = plt.AddBar(values: probabilities, positions: hist.Bins);
            bar.BarWidth = 1;
            bar.FillColor = ColorTranslator.FromHtml("#9bc3eb");
            bar.BorderColor = ColorTranslator.FromHtml("#82add9");

            // display histogram probability curve as a line plot
            plt.AddFunction(hist.GetProbabilityCurve(sample, true), Color.Black, 2);

            // customize the plot style
            plt.Title("Плотность вероятности");
            plt.YAxis.Label("Вероятность");
            plt.XAxis.Label("Значение");
            plt.SetAxisLimits(yMin: 0);

            plt.SaveFig("!hist.png");
        }
    }
}
