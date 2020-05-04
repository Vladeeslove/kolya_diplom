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
using System.Windows.Shapes;

namespace OtdelKadrov
{
    /// <summary>
    /// Логика взаимодействия для modalWindow.xaml
    /// </summary>
    public partial class modalWindow : Window
    {
        string[] strGen;
        string readyString = "INSERT INTO ";
        public modalWindow(string titleWindow,string nameTable, string strQuery)
        {
            InitializeComponent();
            this.Title = titleWindow.ToUpper();
            this.strGen = strQuery.Split(' ');
            this.Visibility = Visibility.Hidden;
            this.readyString += nameTable;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int countRow = 0;
            for (int i = 0; i < strGen.Length; i += 4)
            {
                run(strGen[i], strGen[i + 1], Convert.ToBoolean(strGen[i + 2]), strGen[i + 3]);
                countRow++;
            }
            MessageBox.Show("Указанно количество строк: " + countRow);
        }

        private void run(string vid, string type, bool isNull, string titleBox)
        {
            if (vid == "PK")
            {

            }
            else
                if (vid == "FK")
                {

                }
                else if(vid == "AT")
                     {

                     }


            switch (type)
            {
                case "int": break;
                case "text": break;
                case "img": break;
                case "pas": break;
                case "phone": break;
                case "datetime": break;
                case "date": break;
            }


            if (isNull)
            {

            }
            else
            {

            }
            
            

        }
    }
}
