using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

                    StringBuilder errors = new StringBuilder();
                    if (string.IsNullOrWhiteSpace(tbSeriyaPassport.Text)) errors.AppendLine("Не введена серия паспорта");
                    if (string.IsNullOrWhiteSpace(tbNomerPassport.Text)) errors.AppendLine("Не введен номер паспорта");
                    if (string.IsNullOrWhiteSpace(tbDateVidachi.Text)) errors.AppendLine("Не введена дата выдачи паспорта");

                    if (errors.Length > 0)
                    {
                        MessageBox.Show(errors.ToString());
                        return;
                    }

                    cmdPassNew.ExecuteNonQuery();

                    MessageBox.Show("Паспортные данные успешно загружены");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                    StringBuilder sb = new StringBuilder();
                    if (string.IsNullOrWhiteSpace(tbName.Text)) sb.AppendLine("Не введено Имя");
                    if (string.IsNullOrWhiteSpace(tbSurname.Text)) sb.AppendLine("Не введена Фамилия");
                    if (string.IsNullOrWhiteSpace(tbPatronymic.Text)) sb.AppendLine("Не введено Отчество");
                    if (string.IsNullOrWhiteSpace(tbDateOfBirth.Text)) sb.AppendLine("Не введена Дата рождения");
                    if (string.IsNullOrWhiteSpace(tbCountry.Text)) sb.AppendLine("Не введена Страна");
                    if (string.IsNullOrWhiteSpace(tbSex.Text)) sb.AppendLine("Не введен Пол");

                    if (sb.Length > 0)
                    {
                        MessageBox.Show(sb.ToString());
                        return;
                    }

                    cmdNewPerson.ExecuteNonQuery();

                    MessageBox.Show("Данные о гражданине успешно загружены");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
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

                    StringBuilder sb = new StringBuilder();
                    if (string.IsNullOrWhiteSpace(tbSeriyaPassport.Text)) sb.AppendLine("Не введена серия паспорта");
                    if (string.IsNullOrWhiteSpace(tbNomerPassport.Text)) sb.AppendLine("Не введен номер паспорта");
                    if (string.IsNullOrWhiteSpace(tbDateVidachi.Text)) sb.AppendLine("Не введена дата выдачи паспорта");
                    if (string.IsNullOrWhiteSpace(tbName.Text)) sb.AppendLine("Не введено Имя");
                    if (string.IsNullOrWhiteSpace(tbSurname.Text)) sb.AppendLine("Не введена Фамилия");
                    if (string.IsNullOrWhiteSpace(tbPatronymic.Text)) sb.AppendLine("Не введено Отчество");
                    if (string.IsNullOrWhiteSpace(tbDateOfBirth.Text)) sb.AppendLine("Не введена Дата рождения");
                    if (string.IsNullOrWhiteSpace(tbCountry.Text)) sb.AppendLine("Не введена Страна");
                    if (string.IsNullOrWhiteSpace(tbSex.Text)) sb.AppendLine("Не введен Пол");

                    if (sb.Length > 0)
                    {
                        MessageBox.Show(sb.ToString());
                        return;
                    }

                    cmdPushNewPerson.ExecuteNonQuery();

                    MessageBox.Show("Гражданин успешно добавлен в базу данных");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Не удалось отправить данные, проверьте корректность введенных данных");
            }

        }

        private void tbSeriyaPassport_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!TextOrNumber(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Вы пытаетесь ввести не числовое значение, убедитесь в корректном вводе данных");
            }
        }

        private void tbNomerPassport_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!TextOrNumber(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Вы пытаетесь ввести не числовое значение, убедитесь в корректном вводе данных");
            }
        }

        private bool TextOrNumber(string text) // вспомогательный метод использующий регулярные выражения
        {
            Regex regex = new Regex("[^0-9]+"); // класс MainWindow строка 156
            return !regex.IsMatch(text);
        }
    }
}
