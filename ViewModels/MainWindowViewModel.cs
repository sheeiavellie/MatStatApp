using MatStatApp.ViewModels.Base;

namespace MatStatApp.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title = "MatStat_#1";
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }


    }
}
