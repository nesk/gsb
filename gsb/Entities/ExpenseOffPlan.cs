using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace gsb.Entities
{
    class ExpenseOffPlan : ExpenseBase
    {
        /*
         * Fields
         */

        #region Data fields
        private int id;
        private string month;
        private string label = "Nouvel élément";
        private DateTime date = DateTime.Today;
        private decimal cost = 0;
        #endregion

        /*
         * Constructors
         */

        public ExpenseOffPlan(string month)
        {
            this.month = month;
        }

        public ExpenseOffPlan(Dictionary<string, object> row)
        {
            this.Fill(row);
        }

        /*
         * Properties
         */

        #region Data properties
        public int Id
        {
            get { return this.id; }
            set
            {
                this.setModifiedStatus();
                this.id = value;
            }
        }

        public string Label
        {
            get { return this.label; }
            set
            {
                this.setModifiedStatus();
                this.label = value;
            }
        }

        public DateTime Date
        {
            get { return this.date; }
            set
            {
                this.setModifiedStatus();
                this.date = value;
            }
        }

        public decimal Cost
        {
            get { return this.cost; }
            set
            {
                this.setModifiedStatus();
                this.cost = value;
            }
        }
        #endregion

        /*
         * Methods
         */

        public override void Fill(Dictionary<string, object> row)
        {
            this.status = ExpenseState.Loaded;

            this.id = (int)row["id"];
            this.month = (string)row["month"];
            this.label = (string)row["label"];
            this.date = (DateTime)row["date"];
            this.cost = (decimal)row["cost"];
        }

        public override void Save()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;
            DbCommand cmd = connection.CreateCommand();

            if (this.status == ExpenseState.New)
            {
                const string query =
                    "INSERT INTO LigneFraisHorsForfait(idVisiteur, mois, libelle, date, montant) VALUES " +
                    "(@userId, @month, @label, @date, @cost);" +
                    "SELECT MAX(id) FROM LigneFraisHorsForfait";

                cmd.CommandText = query;
                cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, Database.Instance.UserId));
                cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, this.month));
                cmd.Parameters.Add(Database.CreateParameter("@label", DbType.String, this.label));
                cmd.Parameters.Add(Database.CreateParameter("@date", DbType.Date, this.date));
                cmd.Parameters.Add(Database.CreateParameter("@cost", DbType.Decimal, this.cost));

                this.id = (int)cmd.ExecuteScalar(); // Storing the id of the last insertion

                return; // We must exit the method to avoid a call to the ExecuteNonQuery() method
            }
            else if (this.status == ExpenseState.Modified)
            {
                const string query =
                    "UPDATE LigneFraisHorsForfait " +
                    "SET libelle=@label, date=@date, montant=@cost " +
                    "WHERE id=@id";

                cmd.CommandText = query;
                cmd.Parameters.Add(Database.CreateParameter("@id", DbType.Int32, this.id));
                cmd.Parameters.Add(Database.CreateParameter("@label", DbType.String, this.label));
                cmd.Parameters.Add(Database.CreateParameter("@date", DbType.Date, this.date));
                cmd.Parameters.Add(Database.CreateParameter("@cost", DbType.Decimal, this.cost));
            }
            else if (this.status == ExpenseState.Removed)
            {
                const string query =
                    "DELETE FROM LigneFraisHorsForfait " +
                    "WHERE id=@id";

                cmd.CommandText = query;
                cmd.Parameters.Add(Database.CreateParameter("@id", DbType.Int32, this.id));
            }

            if(this.status == ExpenseState.New || this.status == ExpenseState.Modified || this.status == ExpenseState.Removed)
                cmd.ExecuteNonQuery();

            // The entity has been saved, the status must be changed.
            this.status = ExpenseState.Loaded;
        }

        public override int CompareTo(object obj)
        {
            return DateTime.Compare(this.date, ((ExpenseOffPlan)obj).date);
        }

        public override string ToString()
        {
            return String.IsNullOrEmpty(this.label) ? "Nouvel élément" : this.label;
        }
    }
}
