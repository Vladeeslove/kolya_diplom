using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для SignIn.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        SqlConnection connection;
        public event EventHandler GoMain;
        public int id_user;
        public SignIn()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString());
            connection.Open();
        }

        private void btnSignInRun_Click(object sender, RoutedEventArgs e)
        {
                //string result = new SqlCommand("SELECT COUNT(*) FROM Сотрудник WHERE ??? = '" + login.Text.ToString() + "' and ??? = '" + hashMP5.ConvertToHash(password.Text.ToString()).ToString() + "'", connection).ExecuteScalar().ToString();
             //   if (Convert.ToInt32(result) > 0)
              //  {
               //     MessageBox.Show("Добро пожаловать, " + login.Text.ToString() + "!");

                id_user = 666;// Convert.ToInt32(new SqlCommand("SELECT id FROM Сотрудник WHERE ??? = '" + login.Text.ToString() + "' and ??? = '" + hashMP5.ConvertToHash(password.Text.ToString()).ToString() + "'", connection).ExecuteScalar().ToString());
                    if (GoMain != null)
                    {
                        GoMain(this, EventArgs.Empty);
                    }

              //  }
        }
        class hashMP5
        {
            static public string ConvertToHash(string arg)
            {
                string source = arg;
                using (MD5 md5Hash = MD5.Create())
                {
                    string hash = GetMd5Hash(md5Hash, source);
                    return hash;

                }

            }
            static string GetMd5Hash(MD5 md5Hash, string input)
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
        }


    }
}
