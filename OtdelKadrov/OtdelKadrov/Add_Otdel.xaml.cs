using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для AddOtdel.xaml
    /// </summary>
    public partial class Add_Otdel : Window
    {
        SqlConnection connection;
        public Add_Otdel()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region ПРОВЕРКА ОШИБОК
                if (название_отдела.Text.ToString() == "")
                {
                    //если ошибка =>
                    throw new Exception("пиздец пошло по пизде");
                }
                #endregion ПРОВЕРКА ОШИБОК

                #region ОТПРАВКА ЗАПРОСА В БД

                connection.Open();
                int result = new SqlCommand("INSERT INTO отдел(название) VALUES('" + название_отдела.Text.ToString() + "')", connection).ExecuteNonQuery();
                
                #endregion ОТПРАВКА ЗАПРОСА В БД

                #region ПРОВЕРКА РЕЗУЛЬТАТА
                if (result == 1)
                {
                    MessageBox.Show("Добавлено!");
                    connection.Close();
                    this.Close();
                }
                #endregion ПРОВЕРКА РЕЗУЛЬТАТА
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show("ошибка" + ex);
            }
            

            
        }
    }
}
