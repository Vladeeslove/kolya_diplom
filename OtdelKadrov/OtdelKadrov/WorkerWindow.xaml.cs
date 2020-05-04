using System;
using System.Collections.Generic;
using System.Data;
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
    /// Логика взаимодействия для WorkerWindow.xaml
    /// </summary>
    public partial class WorkerWindow : Window
    {
        SqlDataAdapter sda;
        DataTable dt;
        SqlConnection connection;
        public WorkerWindow(int id_user)
        {
            InitializeComponent();
        }
        private void FillDataGrid(string str, DataGrid dg)
        {
            string ConString = "Data Source=DESKTOP-OBVAQM4\\MSSQLSERVER01;Initial Catalog=OtdelKadrov;Integrated Security=True";
            string CmdString = string.Empty;
            using (SqlConnection con = new SqlConnection(ConString))
            {
                CmdString = str;
                SqlCommand cmd = new SqlCommand(CmdString, con);
                sda = new SqlDataAdapter(cmd);
                SqlCommandBuilder myCommandBuilder = new SqlCommandBuilder(sda as SqlDataAdapter);
                dt = new DataTable("Штатное расписание");
                sda.Fill(dt);
                dg.ItemsSource = dt.AsDataView();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataGrid("SELECT * FROM ШР", Shtat);
            FillDataGrid("SELECT * FROM personalInfo", personal);
            fillTreeView();
        }

        private void TabItem_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            FillDataGrid("SELECT * FROM personalInfo", personal);
        }
        private void fillTreeView()
        {
            string conString = "Data Source=DESKTOP-OBVAQM4\\MSSQLSERVER01;Initial Catalog=OtdelKadrov;Integrated Security=True";
            string CmdString = string.Empty;

            connection = new SqlConnection(conString);
            connection.Open();

            SqlConnection connection1 = new SqlConnection(conString);
            connection1.Open();
            SqlConnection connection2 = new SqlConnection(conString);
            connection2.Open();
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM отдел", connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            try
                            {
                                TreeViewItem obj = new TreeViewItem();
                                obj.Header = reader["название"].ToString();
                                obj.Name = "_" + reader["код_отдела"].ToString();


                                using (SqlCommand cmd1 = new SqlCommand($"SELECT * FROM [отдел и его должности] where код_отдела = " + reader["код_отдела"].ToString(), connection1))
                                {
                                    using (SqlDataReader reader1 = cmd1.ExecuteReader())
                                    {
                                        if (reader1.HasRows)
                                        {
                                            while (reader1.Read())
                                            {
                                                try
                                                {
                                                    TreeViewItem obj1 = new TreeViewItem();
                                                    obj1.Header = reader1["должность"].ToString();
                                                    obj1.Name = "__" + reader1["код_должности"].ToString();


                                                    ///
                                                    using (SqlCommand cmd2 = new SqlCommand($"SELECT * FROM personalInfo where код_отдела = " + reader["код_отдела"].ToString() + " and код_должности = " + reader1["код_должности"].ToString(), connection2))
                                                    {
                                                        using (SqlDataReader reader2 = cmd2.ExecuteReader())
                                                        {
                                                            if (reader2.HasRows)
                                                            {
                                                                while (reader2.Read())
                                                                {
                                                                    try
                                                                    {
                                                                        TreeViewItem obj2 = new TreeViewItem();
                                                                        obj2.Header = reader2["Фамилия сотрудника"].ToString() + " " + reader2["имя"].ToString() + " " + reader2["отчество"].ToString() + " (" + reader2["всего"].ToString() + " руб.)";
                                                                        obj2.Name = "___" + reader2["код_сотрудника"].ToString();

                                                                        obj1.Items.Add(obj2);

                                                                    }
                                                                    catch (Exception ex)
                                                                    {
                                                                        MessageBox.Show("??" + ex.Message);
                                                                    }
                                                                }
                                                            }
                                                            else
                                                            {

                                                            }
                                                        }
                                                    }

                                                    obj.Items.Add(obj1);
                                                    ///here
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("??" + ex.Message);
                                                }
                                            }
                                        }
                                        else
                                        {

                                        }
                                    }

                                }
                                otdels.Items.Add(obj);
                                //here

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("??" + ex.Message);
                            }
                        }
                    }
                    else
                    {

                    }
                }

            }


        }
        private void TabItem_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

            //  fillTreeView();



        }

        private void addOtdel_Click(object sender, RoutedEventArgs e)
        {
            Add_Otdel ao = new Add_Otdel();
            ao.ShowDialog();
        }
    }
}
