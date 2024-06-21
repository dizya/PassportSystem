using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Windows.Markup;

namespace PassportSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
           InitializeComponent();
        }
        private void tbID_TextChanged(object sender, TextChangedEventArgs e) // при вводе ID появляется информация о человеке
        {
            SqlConnection sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");
            sqlConnection.Open();
            if (tbID.Text !="")
            {
                SqlCommand cmd = new SqlCommand("Select Name, Surname, Patronymic, Birthday, Country, Sex from Human where ID = @ID", sqlConnection);
                cmd.Parameters.AddWithValue("@ID", int.Parse(tbID.Text));
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                { 
                    tbName.Text = reader.GetValue(0).ToString();
                    tbSurname.Text = reader.GetValue(1).ToString();
                    tbPatronymic.Text = reader.GetValue(2).ToString();
                    tbDateOfBirth.Text = reader.GetValue(3).ToString();
                    tbCountry.Text = reader.GetValue(4).ToString();
                    tbSex.Text = reader.GetValue(5).ToString();
                }
                sqlConnection.Close();
            }
        }

        private void CheckButton_Click(object sender, RoutedEventArgs e) // по нажатию кнопки появляются серия, номер и даты выдачи паспорта
        {
            SqlConnection sqlConnectionForPassport = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");
            sqlConnectionForPassport.Open();

            try
            {
                SqlCommand cmdPass = new SqlCommand("Select SeriyaPassport, NomerPassport, DataVidachiPassport from Passport where ID = @ID", sqlConnectionForPassport);
                cmdPass.Parameters.AddWithValue("@ID", int.Parse(tbID.Text));
                SqlDataReader reader = cmdPass.ExecuteReader();
                while (reader.Read())
                {
                    tbSeriyaPassport.Text = reader.GetValue(0).ToString();
                    tbNomerPassport.Text = reader.GetValue(1).ToString();
                    tbDateVidachi.Text = reader.GetValue(2).ToString();
                }
                reader.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Сначала введите ID ");
            }
        }

        private void btConfirm_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection sqlConnectionForPushData = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\spiri\\source\\repos\\PassportSystem\\PassportSystem\\DemoDB.mdf;Integrated Security=True");

            string query = "INSERT INTO HumanSuccsesControl (Name, Surname, Patronymic, Birthday, Country, Sex, SeriyaPassport, NomerPassport, DataVidachiPassport)" +
                " VALUES (@name, @surname, @patronymic, @datebirthday, @country, @sex, @seriyapas, @nomerpas, @datevidachi)";

            SqlCommand cmdPushData = new SqlCommand(query, sqlConnectionForPushData);

            try
            {
                cmdPushData.Parameters.AddWithValue("@name", tbName.Text);
                cmdPushData.Parameters.AddWithValue("@surname", tbSurname.Text);
                cmdPushData.Parameters.AddWithValue("@patronymic", tbPatronymic.Text);
                cmdPushData.Parameters.AddWithValue("@datebirthday", Convert.ToDateTime(tbDateOfBirth.Text));
                cmdPushData.Parameters.AddWithValue("@country", tbCountry.Text);
                cmdPushData.Parameters.AddWithValue("@sex", tbSex.Text);
                cmdPushData.Parameters.AddWithValue("@seriyapas", tbSeriyaPassport.Text);
                cmdPushData.Parameters.AddWithValue("@nomerpas", tbNomerPassport.Text);
                cmdPushData.Parameters.AddWithValue("@datevidachi", Convert.ToDateTime(tbDateVidachi.Text));
                sqlConnectionForPushData.Open();
                cmdPushData.ExecuteNonQuery();
                sqlConnectionForPushData.Close();

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
                MessageBox.Show("Данные гражданина успешно загружены, поздравляем!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString()); 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewPerson newPerson = new NewPerson();//создание экземпляра класса нового окна
            newPerson.Show();//открытие нового окна
            this.Close();//закрытие текущего окна
        }

        private void tbSeriyaPassport_PreviewTextInput(object sender, TextCompositionEventArgs e) // метод обрабатывающий ввод букв в числовое поле серии паспорта
        {
            if (!TextOrNumber(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Вы пытаетесь ввести не числовое значение, убедитесь в корректном вводе данных");
            }
        }

        private void tbNomerPassport_PreviewTextInput(object sender, TextCompositionEventArgs e) // метод обрабатывающий ввод букв в числовое поле номера паспорта
        {
            if (!TextOrNumber(e.Text))
            {
                e.Handled = true;
                MessageBox.Show("Вы пытаетесь ввести не числовое значение, убедитесь в корректном вводе данных");
            }
        }

        private bool TextOrNumber (string text) // вспомогательный метод использующий регулярные выражения
        {
            Regex regex = new Regex("[^0-9]+"); // класс Regex позволят анализировать какие символы вводятся в поле, если вводится что угодно кроме цифр от 0 до 9, возникнет ошибка, сообщающая, что введеные данные некорректны
            return !regex.IsMatch(text);
        }
        //TRUNCATE TABLE tablename начать айди с 1 в бд
    }
}
