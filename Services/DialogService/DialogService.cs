using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace MatStatApp.Services.DialogService
{
    internal class DialogService : IDialogService
    {
        DialogWindow dialog;

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
            dialog = new DialogWindow();   
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

            dialog.WindowStyle = WindowStyle.None;
            dialog.AllowsTransparency = true;
            dialog.MouseLeftButtonDown += Dialog_MouseLeftButtonDown;
            dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            dialog.Name = "amogus";
            dialog.Background = Brushes.Transparent;

            dialog.ShowDialog();
        }

        private void Dialog_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            dialog.DragMove();
        }
    }
}
