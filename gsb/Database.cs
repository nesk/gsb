using System;
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
            String name = ConfigurationManager.AppSettings["connectionName"];
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

        public static DbParameter createParameter(string name, DbType type, object value)
        {
            DbParameter param = Database.factory.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;

            return param;
        }

        public UserConnectionState connectUser(string login, string password)
        {
            DbCommand cmd = this.dbConnection.CreateCommand();
            cmd.CommandText = "SELECT id FROM Visiteur WHERE login=@login AND mdp=@password";

            cmd.Parameters.Add(Database.createParameter("@login", DbType.String, login));
            cmd.Parameters.Add(Database.createParameter("@password", DbType.String, password));

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
