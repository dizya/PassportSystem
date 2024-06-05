using Microsoft.VisualBasic.ApplicationServices;
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

namespace PassportSystem.Log_in_and_Sign_in
{
    /// <summary>
    /// Логика взаимодействия для UserValidationWindow.xaml
    /// </summary>
    public partial class UserValidationWindow : Window
    {
        public UserValidationWindow()
        {
            InitializeComponent();
        }




        private void btAuth_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnectionForUsers = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");

            string loginUser = tbLogin.Text;
            string passwordUser = tbPassword.Text;
            
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dataTable = new DataTable();

            string query = $"select Id, Login, Password from Users where Login = '{loginUser}' and Password = '{passwordUser}'";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnectionForUsers);
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 1)
            {
                MessageBox.Show("Вход успешно выполнен");
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Такого аккаунта не существует");
            }

        }

        private void btRegistration_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
            this.Close();
        }

       
    }
}
