using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace SqlCommander
{
    public static class MySqlConnector
    {
        public const string UserID = "root";
        public const string Server = "127.0.0.1";
        public const int Port = 3333;
        public const string Password = "root";
        public const string Database = "lightgbm";
        public static MySqlConnection Open()
        {
            try
            {
                MySqlConnectionStringBuilder sb = new MySqlConnectionStringBuilder();
                sb.Server = Server;
                sb.Port = Port;
                sb.UserID = UserID;
                sb.Password = Password;
                sb.Database = Database;
                MySqlConnection mycon = new MySqlConnection(sb.ToString());
                mycon.Open();
                return mycon;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return null;
            }
        }
    }
}
