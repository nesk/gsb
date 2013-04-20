using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;

namespace gsb.Entities
{
    class ExpenseNote : ExpenseBase
    {
        /*
         * Fields
         */

        #region Data fields
        private DateTime date = DateTime.Today;
        private int vouchersNb = 0;
        private decimal approvedAmount = 0;
        private string state = "CR";

        private Dictionary<string, int> expensesInPlan = new Dictionary<string,int>();
        
        private List<ExpenseOffPlan> expensesOffPlan = new List<ExpenseOffPlan>();
        #endregion

        /*
         * Constructors
         */

        public ExpenseNote()
        {
            this.expensesInPlan["ETP"] = 0;
            this.expensesInPlan["KM"] = 0;
            this.expensesInPlan["NUI"] = 0;
            this.expensesInPlan["REP"] = 0;
        }

        public ExpenseNote(Dictionary<string, object> row)
        {
            this.Fill(row);
            this.LoadExpensesInPlan();
            this.LoadExpensesOffPlan();
        }

        /*
         * Properties
         */

        // An expense note shouldn't be removed, this property must be rewritten.
        public new ExpenseState Status
        {
            get { return this.status; }
        }

        #region Data properties
        public DateTime Date
        {
            get { return this.date; }
            set
            {
                this.status = ExpenseState.Modified;
                this.date = value;
            }
        }

        public int VouchersNb
        {
            get { return this.vouchersNb; }
            set
            {
                this.status = ExpenseState.Modified;
                this.vouchersNb = value;
            }
        }

        public decimal ApprovedAmount
        {
            get { return this.approvedAmount; }
            set
            {
                this.status = ExpenseState.Modified;
                this.approvedAmount = value;
            }
        }

        public string State
        {
            get { return this.state; }
            set
            {
                this.status = ExpenseState.Modified;
                this.state = value;
            }
        }

        public ReadOnlyDictionary<string, int> ExpensesInPlan
        {
            get { return new ReadOnlyDictionary<string, int>(this.expensesInPlan); }
        }

        public ReadOnlyCollection<ExpenseOffPlan> ExpensesOffPlan
        {
            get { return this.expensesOffPlan.AsReadOnly(); }
        }
        #endregion

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

        public override int CompareTo(object obj)
        {
            return -1 * DateTime.Compare(this.date, ((ExpenseNote)obj).date);
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
                    this.expensesInPlan[id] = (int)reader["quantity"];
            }
            reader.Close();
        }

        private void LoadExpensesOffPlan()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;

            const string query =
                "SELECT id, libelle AS label, date, montant AS cost " +
                "FROM LigneFraisHorsForfait " +
                "WHERE idVisiteur = @userId " +
                "AND mois = @month";

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, db.UserId));
            cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, this.date.Year.ToString().PadLeft(4, '0') + this.date.Month.ToString().PadLeft(2, '0')));

            DbDataReader reader = cmd.ExecuteReader();
            List<Dictionary<string, object>> rows = Database.GetDataReaderRows(reader);
            reader.Close();

            foreach (Dictionary<string, object> row in rows)
                this.expensesOffPlan.Add(new ExpenseOffPlan(row));
        }
    }
}
