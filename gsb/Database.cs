using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.ProviderBase;

using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;

namespace gsb
{
    sealed class Database
    {
        /*
         * Static properties
         */

        private static Database instance = null;
        private static readonly object padlock = new object();

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
        
        /*
         * Properties
         */

        private DbConnection connection;

        public DbConnection Connection
        {
            get
            {
                return connection;
            }
        }

        /*
         * Constructor
         */

        private Database()
        {
            String name = ConfigurationManager.AppSettings["connectionName"];
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings[name];
            DbProviderFactory factory = DbProviderFactories.GetFactory(settings.ProviderName);

            this.connection = factory.CreateConnection();
            this.connection.ConnectionString = settings.ConnectionString;

            this.connection.Open();
        }
    }
}
