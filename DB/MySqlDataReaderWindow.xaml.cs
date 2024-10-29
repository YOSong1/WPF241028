using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfDB
{
    /// <summary>
    /// MySqlDataReaderWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MySqlDataReaderWindow : Window
    {
        public ObservableCollection<User> Users { get; set; }

        public MySqlDataReaderWindow()
        {
            InitializeComponent();
            Users = new ObservableCollection<User>();
            userDataGrid.ItemsSource = Users;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string myConnection = "Server=localhost;Database=wpf;Port=3306;User=root;Password=1234";

                using (MySqlConnection connection = new MySqlConnection(myConnection))
                {
                    connection.Open();
                    MySqlCommand command = new MySqlCommand("SELECT * FROM users;", connection);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        Users.Clear();
                        while (reader.Read())
                        {
                            var user = new User
                            {
                                ID = reader.GetInt32("ID"),
                                Name = reader.GetString("Name"),
                                Password = reader.GetString("Password")
                            };
                            Users.Add(user);
                        }
                    }
                }

                MessageBox.Show("데이터 가져오기 완료!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}