using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace gsb
{
    sealed class Database
    {
        /*
         * Static fields
         */

        private static Database instance = null;
        private static readonly object padlock = new object();
        private static DbProviderFactory factory;

        /*
         * Fields
         */

        private DbConnection connection;

        /*
         * Constructor
         */

        private Database()
        {
            String name = ConfigurationManager.AppSettings["connectionName"];
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            Database.factory = DbProviderFactories.GetFactory(settings.ProviderName);

            this.connection = Database.factory.CreateConnection();
            this.connection.ConnectionString = settings.ConnectionString;

            this.connection.Open();
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

        public DbConnection Connection
        {
            get
            {
                return connection;
            }
        }

        /*
         * Static methods
         */

        public static DbParameter createParameter(string name, DbType type, object value)
        {
            DbParameter param = Database.factory.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Value = value;

            return param;
        }

        /*
         * Methods
         */

        public Boolean connectUser(string login, string password)
        {
            DbCommand cmd = this.connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Visiteur WHERE login=@login AND mdp=@password";

            cmd.Parameters.Add(Database.createParameter("@login", DbType.String, login));
            cmd.Parameters.Add(Database.createParameter("@password", DbType.String, password));

            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                
            }
            reader.Close();
            return true;
        }
    }
}
