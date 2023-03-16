using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MatStatApp.Services.DialogService
{
    internal class DialogService : IDialogService
    {
        static Dictionary<Type, Type> _mappings = new Dictionary<Type, Type>();

        public static void RegisterDialog<TView, TViewModel>()
        {
            _mappings.Add(typeof(TViewModel), typeof(TView));
        }
        

        public void ShowDialog<TViewModel>(Action<string> callback)
        {
            var type = _mappings[typeof(TViewModel)];
            ShowDialogInternal(type, callback, typeof(TViewModel));
        }

        public void ShowDialog(string name, Action<string> callback)
        {
            var type = Type.GetType($"MatStatApp.Views.Windows.{name}");
            ShowDialogInternal(type, callback, null);
        }
        public void ShowDialogInternal(Type type, Action<string> callback, Type vmType)
        {
            var dialog = new DialogWindow();

            EventHandler closeEventHandler = null;
            closeEventHandler = (s, e) =>
            {
                callback(dialog.DialogResult.ToString());
                dialog.Closed -= closeEventHandler;
            };
            dialog.Closed += closeEventHandler;

            var content = Activator.CreateInstance(type);

            if(vmType != null)
            {
                var vm = Activator.CreateInstance(vmType);

                (content as FrameworkElement).DataContext = vm;
            }           

            dialog.Content = content;

            dialog.ShowDialog();
        }
    }
}
