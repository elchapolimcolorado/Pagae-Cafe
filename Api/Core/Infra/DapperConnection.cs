using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using MySql;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace Core.Infra
{
    public class DapperConnection : IDisposable
    {
        private IDbConnection _dbConnection;

        public IDbConnection Conectar()
        {
            MySqlConnectionStringBuilder connString = new MySqlConnectionStringBuilder();

            connString.Port = 3306;
            connString.Server = "localhost";
            connString.UserID = "root";
            connString.Password = "1234";
            connString.Database = "Cafe";

            _dbConnection = new MySqlConnection(connString.ToString());
            _dbConnection.Open();

            return _dbConnection;
            
        }

        public void Dispose()
        {
            _dbConnection.Close();
        }
    }

}
