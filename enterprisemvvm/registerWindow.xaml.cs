using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Text.RegularExpressions;

namespace enterprisemvvm
{
    /// <summary>
    /// Interaction logic for registerWindow.xaml
    /// </summary>
    public partial class registerWindow : Window
    {
        public registerWindow()
        {
            InitializeComponent();

        }
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {

            if (textBoxEmail.Text.Length == 0 || passwordBox1.Text.Length == 0)
            {
                errorEmail.Text = "Enter an email and Password.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errorEmail.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxPhone.Text, @"^\s*\+?\s*([0-9][\s-]*){9,}$"))
            {
                errorPhone.Text = "Enter a valid phone";
                
                errorPhone.Focus();
            }
            else DialogResult = true;
        }






    }
}
