using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace gsb
{
    public enum UserConnectionState
    {
        Success, WrongCredentials
    }

    sealed class Database
    {
        /*
         * Fields
         */

        private static Database instance = null;
        private static readonly object padlock = new object();
        private static DbProviderFactory factory;

        private DbConnection dbConnection;
        private Boolean userConnected = false;
        private string userId = null;

        /*
         * Constructor
         */

        private Database()
        {
            string name = ConfigurationManager.AppSettings["db-type"];
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            Database.factory = DbProviderFactories.GetFactory(settings.ProviderName);

            this.dbConnection = Database.factory.CreateConnection();
            this.dbConnection.ConnectionString = settings.ConnectionString;

            this.dbConnection.Open();
        }

        /*
         * Properties
         */

        public static Database Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null) instance = new Database();
                    return instance;
                }
            }
        }

        public DbConnection DbConnection
        {
            get
            {
                return dbConnection;
            }
        }

        public Boolean UserConnected
        {
            get
            {
                return userConnected;
            }
        }

        public string UserId
        {
            get
            {
                return userId;
            }
        }

        /*
         * Methods
         */

        public static DbParameter CreateParameter(string name, DbType type, object value)
        {
            DbParameter param = Database.factory.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;

            return param;
        }

        public static List<Dictionary<string, object>> GetDataReaderRows(DbDataReader reader)
        {
            List<Dictionary<string, object>> values = new List<Dictionary<string, object>>();

            while (reader.Read())
            {
                Dictionary<string, object> row = new Dictionary<string, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                    row[reader.GetName(i)] = reader.GetValue(i);
                values.Add(row);
            }

            return values;
        }

        public UserConnectionState ConnectUser(string login, string password)
        {
            const string query = "SELECT id FROM Visiteur WHERE login=@login AND mdp=@password";
            
            DbCommand cmd = this.dbConnection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.CreateParameter("@login", DbType.String, login));
            cmd.Parameters.Add(Database.CreateParameter("@password", DbType.String, password));

            DbDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                this.userId = (string)reader["id"];
                this.userConnected = true;
                reader.Close();
                return UserConnectionState.Success;
            }
            else
            {
                reader.Close();
                return UserConnectionState.WrongCredentials;
            }
        }
    }
}
