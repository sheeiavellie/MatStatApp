using MatStatApp.Services.DialogService;
using MatStatApp.ViewModels;
using MatStatApp.Views.Windows;
using System.Windows;

namespace MatStatApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DialogService.RegisterDialog<DialogChart, DialogChartViewModel>();
            DialogService.RegisterDialog<DialogChartEmp, DialogChartEmpViewModel>();
        }
    }
}
