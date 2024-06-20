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

        /// <summary>
        /// метод IsHaveAcc() является проверкой данных, если логин и пароль уже находятся в бд, то приложение не даст возможности зайти и закроется, реализовано через костыль пока что(( 
        /// </summary>
        /// <returns></returns>
        private bool IsHaveAcc()
        {
            bool HaveAcc = false;

            string login = tbLogin.Text;
            string password = tbPassword.Text;

            using (SqlConnection sqlConnectionForCheckAccount = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True"))
            {
                sqlConnectionForCheckAccount.Open();
                using (SqlCommand sqlCommandCheckAccount = new SqlCommand($"SELECT * FROM Users WHERE Login = '{login}' and Password = '{password}'", sqlConnectionForCheckAccount))
                {
                    using (SqlDataReader sqlDataReaderCheckAccount = sqlCommandCheckAccount.ExecuteReader())
                    {
                        if (sqlDataReaderCheckAccount.Read())
                        {
                            if (sqlDataReaderCheckAccount["Login"].ToString() != login && sqlDataReaderCheckAccount["Password"].ToString() != password)
                            {
                                HaveAcc = true;
                                MessageBox.Show("Данный аккаунт уже существует, перезапустите приложение и попробуйте снова");
                                sqlConnectionForCheckAccount.Close();
                                Environment.Exit(0);
                            }
                        }
                    }
                }
            }
                return HaveAcc;
        }

        public async void btAuth_Click(object sender, RoutedEventArgs e)
        {
            IsHaveAcc();

            string loginRegistr = tbLogin.Text;
            string passwordRegister = tbPassword.Text;

            StringBuilder errors = new StringBuilder(); //создание переменной для хранения ошибок
            if (string.IsNullOrWhiteSpace(loginRegistr)) errors.AppendLine("Вы ничего не ввели в поле Логин, пожалуйста введите корректные данные");
            if (string.IsNullOrWhiteSpace(passwordRegister)) errors.AppendLine("Вы ничего не ввели в поле Пароль, пожалуйста, введите корректные данные");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            SqlConnection sqlConnectionForRegistrUser = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");

            string sqlQuery = "insert into Users (Login, Password) values (@login, @password)";

                SqlCommand CmdRegistr = new SqlCommand(sqlQuery, sqlConnectionForRegistrUser);

                CmdRegistr.Parameters.AddWithValue("@login", tbLogin.Text);
                CmdRegistr.Parameters.AddWithValue("@password", tbPassword.Text);
                sqlConnectionForRegistrUser.Open();
                CmdRegistr.ExecuteNonQuery();
                sqlConnectionForRegistrUser.Close();

                MessageBox.Show("Вы успешно зарегестрировались");

                await Task.Delay(100);

                UserValidationWindow userValidationWindow = new UserValidationWindow();
                userValidationWindow.Show();
                this.Close();
        }
        private void btHaveAccount_Click(object sender, RoutedEventArgs e)
        {
            UserValidationWindow userValidationWindow = new UserValidationWindow();
            userValidationWindow.Show();
            this.Close();
        }
    }
}
