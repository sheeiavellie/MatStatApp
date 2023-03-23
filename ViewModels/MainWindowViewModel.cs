using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using MatStat;
using MatStatApp.Infrastructure.Commands;
using MatStatApp.Models;
using MatStatApp.Models.Charts;
using MatStatApp.Services.DialogService;
using MatStatApp.Services.GetDataService;
using MatStatApp.Services.GetFileService;
using MatStatApp.ViewModels.Base;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Input;

namespace MatStatApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private IGetFileService _getFileService;
        private IGetDataService _getDataService;
        private IDialogService _dialogService;

        private string _placeholder = "Тут пока пусто :(";

        private string _ResField = "Тут пока пусто :(";
        public string ResField
        {
            get => _ResField;
            set => Set(ref _ResField, value);
        }
        private string _TextField = "Тут пока пусто :(";
        public string TextField
        {
            get => _TextField;
            set => Set(ref _TextField, value);
        }

        private ObservableCollection<string> _Methods;
        public ObservableCollection<string> Methods
        {
            get => _Methods;
            set => Set(ref _Methods, value);
        }

        private string _Title = "MatStat_1";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }        

        #region Commands

        #region CloseApplication
        public ICommand CloseApplicationCommand { get; }

        private bool CanCloseApplicationCommandExecute(object p) => true;
        private void OnCloseApplicationCommandExecuted(object p) => Application.Current.Shutdown();
        #endregion

        public ICommand SelectFileCommand { get; }

        private bool CanSelectFileCommandExecute(object p) => true;
        private void OnSelectFileCommandExecuted(object p)
        {
            var path = _getFileService.GetFilePath();
            Sample.sample = _getDataService.GetData(path);
            TextField = string.Join(", ", Sample.sample);
        }

        public ICommand FunctionCommand { get; }

        private bool CanFunctionCommandExecute(object p) => true;
        private void OnFunctionCommandExecuted(object p)
        {
            string methodName = p.ToString();

            var parameters = new object[] { Sample.sample };
            var t = Type.GetType(Assembly.CreateQualifiedName("MatStat", "MatStat.Functions"));
            object classInst = Activator.CreateInstance(t);


            var ret = t.GetMethod(methodName).Invoke(classInst, parameters);
            var result = "";

            if (ret is double[])
            {
                result = String.Join(", ", (double[])ret);
            }
            else
                result = Convert.ToString(ret);

            ResField = result;
        }

        public ICommand ShowGraphCommand { get; }

        private bool CanShowGraphCommandExecute(object p) => true;
        private void OnShowGraphCommandExecuted(object p)
        {
            string type = p.ToString();

            if(type == "Gist")
            {
                _dialogService.ShowDialog<DialogChartViewModel>(result => { var test = result; });
            }
            else
            {
                _dialogService.ShowDialog<EmpChartViewModel>(result => { var test = result; });
            }

        }

        #endregion
        public MainWindowViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);
            SelectFileCommand = new RelayCommand(OnSelectFileCommandExecuted, CanSelectFileCommandExecute);
            FunctionCommand = new RelayCommand(OnFunctionCommandExecuted, CanFunctionCommandExecute);
            ShowGraphCommand = new RelayCommand(OnShowGraphCommandExecuted, CanShowGraphCommandExecute);

            _getDataService = new GetDataService();
            _getFileService = new GetFileService();
            _dialogService = new DialogService();

            var lst = Type.GetType(Assembly.CreateQualifiedName("MatStat", "MatStat.Functions"))
                .GetMethods(BindingFlags.Public | BindingFlags.Instance)
                .Select(x => x.Name)
                .ToList();
            lst.RemoveRange(16, lst.Count - 16);

            Methods = new ObservableCollection<string>(lst);
        }
    }
}
