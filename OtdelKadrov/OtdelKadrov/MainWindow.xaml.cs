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
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HelloUserWindow hus;
        SignUp su;
        SignIn si;
        public MainWindow()
        {
            InitializeComponent();
            hus = new HelloUserWindow();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            hus.Exit += FunExit;
            hus.SI += FunSI;
            hus.SU += FunSU;
            SignFrame.Navigate(hus);
        }
        private void FunExit(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите выйти из '???'?", "Выход из '???'", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {

            }
            else
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        private void FunSI(object sender, EventArgs e)
        {
            si = new SignIn();
            si.GoMain += In;
            SignFrame.Navigate(si);
        }
        private void FunSI2(object sender, EventArgs e)
        {
            si = new SignIn();
            si.GoMain += In;
            SignFrame.Navigate(si);
        }
        private void FunSU(object sender, EventArgs e)
        {
            su = new SignUp();
            su.SI += FunSI2;
            SignFrame.Navigate(su);
        }
        private void In(object sender, EventArgs e)
        {
            SignIn mySI = sender as SignIn;

                WorkerWindow obj = new WorkerWindow(mySI.id_user);
                obj.Show();
            
          
            this.Close();
            //  ГлавноеОкноПриложения mwa = new ГлавноеОкноПриложения(mySI.id_user);
            // mwa.Show();
            // this.Close();
        }
    }
}
