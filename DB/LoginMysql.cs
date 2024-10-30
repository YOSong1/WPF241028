using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfLogonDB
{
    class LoginMysql
    {
        public void Initialize()
        {
            Debug.WriteLine("DataBase Initialize");
            string connectionPath = $"SERVER=localhost;DATABASE=wpf;UID=root;PASSWORD=1234";
            App.connection = new MySqlConnection(connectionPath);
        }
        public MySqlCommand CreateCommand(string query)
        {
            MySqlCommand command = new MySqlCommand(query, App.connection);
            return command;
        }
        public bool OpenMySqlConnection()
        {
            try
            {
                App.connection.Open();
                return true;
            }
            catch (MySqlException e)
            {
                switch (e.Number)
                {
                    case 0:
                        Debug.WriteLine("Unable to Connect to Server");
                        break;
                    case 1045:
                        Debug.WriteLine("Please check your ID or PassWord");
                        break;
                }
                return false;
            }
        }

        public bool CloseMySqlConnection()
        {
            try
            {
                App.connection.Close();
                return true;
            }
            catch (MySqlException e)
            {
                Debug.WriteLine(e.Message);
                return false;
            }
        }

        public void MySqlQueryExecuter(string userQuery)
        {
            string query = userQuery;
            if (OpenMySqlConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, App.connection);
                if (command.ExecuteNonQuery() == 1)
                {
                    Debug.WriteLine("값 저장 성공");
                    App.DataSaveResult = true;
                }
                else
                {
                    Debug.WriteLine("값 저장 실패");
                    App.DataSaveResult = false;
                }
                CloseMySqlConnection();
            }
        }

        public List<string>[] Select(string tableName, int columnCnt, string id, string pw)
        {
            string query = "SELECT id, pw FROM " + tableName;

            List<string>[] element = new List<string>[columnCnt];

            for (int index = 0; index < element.Length; index++)
            {
                element[index] = new List<string>();
            }

            if (this.OpenMySqlConnection() == true)
            {
                MySqlCommand command = CreateCommand(query);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    element[0].Add(dataReader["id"].ToString());
                    element[1].Add(dataReader["pw"].ToString());
                }

                if (element != null)
                {
                    for (int i = 0; i < element[0].Count; i++)
                    {
                        if (element[0][i] == id) 
                        {
                            if (element[1][i] == pw) 
                            {
                                App.DataSearchResult = true; 
                                break;
                            }
                        }
                    }
                }

                dataReader.Close();
                this.CloseMySqlConnection();
            }

            return element;
        }        
    }
}

