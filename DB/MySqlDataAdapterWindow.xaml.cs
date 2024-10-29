using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows;


namespace WpfDB
{
    public partial class MySqlDataAdapterWindow : Window
    {
        public MySqlDataAdapterWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // MySQL 연결 문자열 설정
                string myConnection = "Server=localhost;Database=wpf;Port=3306;User=root;Password=1234";

                // MySqlConnection 클래스를 사용하여 연결 생성
                MySqlConnection connection = new MySqlConnection(myConnection);

                // MySqlDataAdapter를 사용하여 데이터를 가져올 쿼리 설정
                MySqlDataAdapter myDataAdapter = new MySqlDataAdapter();
                myDataAdapter.SelectCommand = new MySqlCommand("select * from users;", connection);

                // MySqlCommandBuilder를 사용하여 데이터 업데이트 작업을 자동으로 생성
                MySqlCommandBuilder cb = new MySqlCommandBuilder(myDataAdapter);

                // 데이터베이스 연결 열기
                connection.Open();

                // 데이터를 저장할 DataSet 생성
                DataSet ds = new DataSet();

                // 데이터베이스에서 데이터를 가져와 ds에 저장
                myDataAdapter.Fill(ds, "users");

                // DataGrid에 데이터 표시
                userDataGrid.ItemsSource = ds.Tables["users"].DefaultView;

                MessageBox.Show("데이터 가져오기 완료!");

                // 데이터베이스 연결 닫기
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}