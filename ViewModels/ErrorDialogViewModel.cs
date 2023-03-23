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
    internal class ErrorDialogViewModel : ViewModel
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

        public ErrorDialogViewModel()
        {
            CloseApplicationCommand = new RelayCommand(OnCloseApplicationCommandExecuted, CanCloseApplicationCommandExecute);            
        }
    }
}
