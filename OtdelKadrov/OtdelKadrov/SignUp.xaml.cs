using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// Логика взаимодействия для SignUpxaml.xaml
    /// </summary>
    public partial class SignUp : Page
    {
        public event EventHandler SI;
        SqlConnection connection;
        int code;
        public SignUp()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString.ToString());
            connection.Open();
        }

        private void btnSignUpRun_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (email.Text.ToString() == "IBCotdelkadrov@gmail.com")
                {
                    if (SI != null)
                    {
                        string result = new SqlCommand("INSERT INTO Сотрудник VALUES('','','','" + email.Text.ToString() + "','" + hashMP5.ConvertToHash(password.Text.ToString()) + "','" + login.Text.ToString() + "')", connection).ExecuteNonQuery().ToString();
                        SI(this, EventArgs.Empty);
                    }
                }
                else
                {

                    Random ran = new Random();
                    MailAddress from = new MailAddress("IBCotdelkadrov@gmail.com", "Отдел кадров ИВЦ");
                    MailAddress to = new MailAddress(email.Text.ToString());
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Подтвердите пароль";
                    code = ran.Next(1000, 9999);
                    m.Body = "<h2>" + login.Text.ToString() + ", доброго времени суток! Вас беспокоит приложение \"ИВЦ отдел кадров\" от Николая (топ 1 разработчик десктопных прилоений в минске и может еще где то), для завершения регистрации введите этот код ( " + code.ToString() + " ) в именованном поле приложения</h2>";
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                    // логин и пароль
                    smtp.Credentials = new NetworkCredential("IBCotdelkadrov@gmail.com", "durakblyat");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

                    forCodeL.Visibility = Visibility.Visible;
                    forCodeV.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Проверьте подключение к интернету");
            }
        }

        private void forCodeV_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox tb = e.Source as TextBox;
            if (String.Equals(tb.Text.ToString(), code.ToString()))
            {
                if (SI != null)
                {
                    string[] arrFio = FIO.Text.Split(' ');
                    /*
                     код_сотрудника int primary key not null,
                    номер_сотрудника int ,

                    фамилия nvarchar (32),
                    имя nvarchar (32),
                    отчество nvarchar(32),
                    дата_рождения datetime ,
                    пол nvarchar (32),
                    улица_проживания nvarchar (32),
                    номер_дома int ,
                    образование nvarchar (45),
                    моб_телефон int ,
                    дата_принятия_на_работу datetime,
                    стаж_работы datetime,
                    логин nvarchar (32),
                    пароль nvarchar (32)
                     */
                   // string result = new SqlCommand("INSERT INTO Сотрудники VALUES('"+arrFio[0]+ "','"+arrFio[1]+ "','"+arrFio[2]+ "','"++"',)", connection).ExecuteNonQuery().ToString();
                    //string result = new SqlCommand("INSERT INTO Сотрудники () VALUES('" + email.Text.ToString() + "','" + hashMP5.ConvertToHash(password.Text.ToString()) + "','" + login.Text.ToString() + "')", connection).ExecuteNonQuery().ToString();
                    SI(this, EventArgs.Empty);
                }
            }
        }
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

