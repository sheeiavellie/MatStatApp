using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatStatApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for DialogChart.xaml
    /// </summary>
    public partial class GenerateRNorm : UserControl
    {
        private static readonly Regex _regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text, object sender)
        {
            if (text == "0" && (sender as TextBox).Name == "Sigma" && (sender as TextBox).Text == "")
                return false;

            return !_regex.IsMatch(text);
        }
        public GenerateRNorm()
        {
            InitializeComponent();
        }

        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text, sender);
        }
        private void notEmpty(object sender, TextChangedEventArgs args)
        {
            if((sender as TextBox).Text == "" && (sender as TextBox).Name == "Nu")
            {
                (sender as TextBox).Text = "0";
            }
            if ((sender as TextBox).Text == "")
            {
                (sender as TextBox).Text = "1";
            }
        }
    }
}
