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
    /// Логика взаимодействия для NewPerson.xaml
    /// </summary>
    public partial class NewPerson : Window
    {
        public NewPerson()
        {
            InitializeComponent();
        }

        private void btLastPageButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void btPushNewPassportCheckButton_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnectionForNewPersonPassport = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");
            sqlConnectionForNewPersonPassport.Open();

            string query = "insert into NewPersonPassport (NewSeriyaPassport, NewNomerPassport, NewDataVidachiPassport) " +
                "values (@seriya_passport, @nomer_passport, @data_vidachi_passport)";

            try
            {
                SqlCommand cmdPassNew = new SqlCommand(query, sqlConnectionForNewPersonPassport);
                {
                    cmdPassNew.Parameters.AddWithValue(@"seriya_passport", tbSeriyaPassport.Text);
                    cmdPassNew.Parameters.AddWithValue(@"nomer_passport", tbNomerPassport.Text );
                    cmdPassNew.Parameters.AddWithValue(@"data_vidachi_passport", tbDateVidachi.Text);

                    cmdPassNew.ExecuteNonQuery();

                    MessageBox.Show("Паспортные данные успешно загружены");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Введеные вами данные некорректны");
            }

            sqlConnectionForNewPersonPassport.Close();
        }

        private void btPushNewDataPerson_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnectionNewPerson = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");
            sqlConnectionNewPerson.Open();

            string query = "insert into NewHuman (NewName, NewSurname, NewPatronymic, NewBirthday, NewCountry, NewSex) " +
                "values (@new_name, @new_surname, @new_patronymic, @new_birthday, @new_country, @new_sex)";

            try
            {
                SqlCommand cmdNewPerson = new SqlCommand(query, sqlConnectionNewPerson);
                {
                    cmdNewPerson.Parameters.AddWithValue(@"new_name", tbName.Text);
                    cmdNewPerson.Parameters.AddWithValue(@"new_surname", tbSurname.Text);
                    cmdNewPerson.Parameters.AddWithValue(@"new_patronymic", tbPatronymic.Text);
                    cmdNewPerson.Parameters.AddWithValue(@"new_birthday", tbDateOfBirth.Text);
                    cmdNewPerson.Parameters.AddWithValue(@"new_country", tbCountry.Text);
                    cmdNewPerson.Parameters.AddWithValue(@"new_sex", tbSex.Text);

                    cmdNewPerson.ExecuteNonQuery();

                    MessageBox.Show("Данные о гражданине успешно загружены");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Данные введены некорректно");
            }

            sqlConnectionNewPerson.Close();
        }

        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlconPushNewPersonControl = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");
            sqlconPushNewPersonControl.Open();

            string query = "insert into HumanSuccsesControl (Name, Surname, Patronymic, Birthday, Country, Sex, SeriyaPassport, NomerPassport, DataVidachiPassport)" +
                " values (@new_name, @new_surname, @new_patronymic, @new_birthday, @new_country, @new_sex, @seriya_passport, @nomer_passport, @data_vidachi_passport)";

            try
            {
                SqlCommand cmdPushNewPerson = new SqlCommand(query, sqlconPushNewPersonControl);
                {
                    cmdPushNewPerson.Parameters.AddWithValue(@"new_name", tbName.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"new_surname", tbSurname.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"new_patronymic", tbPatronymic.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"new_birthday", tbDateOfBirth.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"new_country", tbCountry.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"new_sex", tbSex.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"seriya_passport", tbSeriyaPassport.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"nomer_passport", tbNomerPassport.Text);
                    cmdPushNewPerson.Parameters.AddWithValue(@"data_vidachi_passport", tbDateVidachi.Text);

                    cmdPushNewPerson.ExecuteNonQuery();

                    MessageBox.Show("Гражданин успешно добавлен в базу данных");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось отправить данные, проверьте корректность введенных данных");
            }

        }
    }
}
