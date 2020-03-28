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

namespace OtdelKadrov
{
    /// <summary>
    /// Логика взаимодействия для HelloUserWindow.xaml
    /// </summary>
    public partial class HelloUserWindow : Page
    {
        public event EventHandler Exit;
        public event EventHandler SI;
        public event EventHandler SU;
        public HelloUserWindow()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            if (Exit != null)
            {
                Exit(this, EventArgs.Empty);
            }
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (SU != null)
            {
                SU(this, EventArgs.Empty);
            }
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (SI != null)
            {
                SI(this, EventArgs.Empty);
            }
        }
    }
}
