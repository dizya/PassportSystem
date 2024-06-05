using PassportSystem.Log_in_and_Sign_in;
using System;
using System.Collections.Generic;
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

namespace PassportSystem
{
    /// <summary>
    /// Логика взаимодействия для SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void btAuth_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnectionForRegistrUser = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");

            string loginRegistr = tbLogin.Text;
            string passwordRegister = tbPassword.Text;

            string sqlQuery = "insert into Users (Login, Password) values (@login, @password)";

            SqlCommand CmdRegistr = new SqlCommand(sqlQuery, sqlConnectionForRegistrUser);

            try
            {
                CmdRegistr.Parameters.AddWithValue("@login", tbLogin.Text);
                CmdRegistr.Parameters.AddWithValue("@password", tbPassword.Text);
                sqlConnectionForRegistrUser.Open();
                CmdRegistr.ExecuteNonQuery();
                sqlConnectionForRegistrUser.Close();

                MessageBox.Show("Вы успешно зарегестрировались");


                await Task.Delay(1000);

                UserValidationWindow userValidationWindow = new UserValidationWindow();
                userValidationWindow.Show();
                this.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Что-то пошло не так, проверьте введенные вами данные");
            }
            
            
            
            
        }

        private void btHaveAccount_Click(object sender, RoutedEventArgs e)
        {
            UserValidationWindow userValidationWindow = new UserValidationWindow();
            userValidationWindow.Show();
            this.Close();
        }
    }
}
