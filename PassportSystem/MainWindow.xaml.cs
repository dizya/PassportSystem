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

                MessageBox.Show("Данные гражданина успешно загружены, поздравляем!");
            }
            catch (Exception)
            {
                MessageBox.Show("Вы ввели что-то некорректно");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NewPerson newPerson = new NewPerson();//создание экземпляра класса нового окна
            newPerson.Show();//открытие нового окна
            this.Close();//закрытие текущего окна
        }
        //TRUNCATE TABLE tablename начать айди с 1 в бд
    }
}
