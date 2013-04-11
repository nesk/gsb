using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace gsb.Entities
{
    class ExpenseNote : ExpenseBase
    {
        /*
         * Fields
         */

        private ExpenseState status = ExpenseState.New;

        #region Data fields
        private DateTime date;
        private int vouchersNb;
        private decimal approvedAmount;
        private string state;

        private Dictionary<string, int> expensesInPlan = new Dictionary<string,int>();
        
        private List<ExpenseOffPlan> expensesOffPlan = new List<ExpenseOffPlan>();
        #endregion

        /*
         * Constructors
         */

        public ExpenseNote(Dictionary<string, object> row)
        {
            this.Fill(row);
            this.LoadExpensesInPlan();
        }

        /*
         * Methods
         */

        public override void Fill(Dictionary<string, object> row)
        {
            this.status = ExpenseState.Loaded;

            this.date = (DateTime)row["date"];
            this.vouchersNb = (int)row["vouchers"];
            this.approvedAmount = (decimal)row["amount"];
            this.state = (string)row["state"];
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.date.ToString("MMMM yyyy");
        }

        private void LoadExpensesInPlan()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;

            const string query =
                "SELECT f.id AS id, quantite AS quantity " +
                "FROM LigneFraisForfait AS l " +
                "JOIN FraisForfait AS f ON f.id = idFraisForfait " +
                "WHERE idVisiteur = @userId " +
                "AND mois = @month";

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, db.UserId));
            cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, this.date.Year.ToString().PadLeft(4, '0') + this.date.Month.ToString().PadLeft(2, '0')));

            string[] validIDs = { "ETP", "KM", "NUI", "REP" };
            
            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string id = (string)reader["id"];
                if (Array.IndexOf(validIDs, id) != -1)
                    expensesInPlan[id] = (int)reader["quantity"];
            }
            reader.Close();

            // Here, throw an error if the expenseInPlan dictionnary does not contain all the IDs specified in the validIDs array.
        }
    }
}
