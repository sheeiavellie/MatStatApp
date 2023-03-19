using MatStatApp.Services.DialogService;
using MatStatApp.ViewModels;
using MatStatApp.Views.Windows;
using System.Windows;
using System.Windows.Input;

namespace MatStatApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DialogService.RegisterDialog<DialogChart, DialogChartViewModel>();
            //DialogService.RegisterDialog<DialogChartEmp, DialogChartEmpViewModel>();

            MainContextMenu.PlacementTarget = Menu;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window
            this.DragMove();
        }
    }
}
