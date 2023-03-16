using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatStatApp.Services.DialogService
{
    internal interface IDialogService
    {
        void ShowDialog(string name, Action<string> callback);
        void ShowDialog<TViewModel>(Action<string> callback);
    }
}
