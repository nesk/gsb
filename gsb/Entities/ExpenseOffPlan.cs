using System;
using System.Collections.Generic;
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
        private string label = "Nouvel élément";
        private DateTime date = DateTime.Today;
        private decimal cost = 0;
        #endregion

        /*
         * Constructors
         */

        public ExpenseOffPlan() { }

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
                this.status = ExpenseState.Modified;
                this.id = value;
            }
        }

        public string Label
        {
            get { return this.label; }
            set
            {
                this.status = ExpenseState.Modified;
                this.label = value;
            }
        }

        public DateTime Date
        {
            get { return this.date; }
            set
            {
                this.status = ExpenseState.Modified;
                this.date = value;
            }
        }

        public decimal Cost
        {
            get { return this.cost; }
            set
            {
                this.status = ExpenseState.Modified;
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
            this.label = (string)row["label"];
            this.date = (DateTime)row["date"];
            this.cost = (decimal)row["cost"];
        }

        public override void Save()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;
            DbCommand cmd = connection.CreateCommand();

            String month = this.date.Year.ToString().PadLeft(4, '0') + this.date.Month.ToString().PadLeft(2, '0');

            if (this.status == ExpenseState.New)
            {
                const string query =
                    "INSERT INTO LigneFraisHorsForfait VALUES " +
                    "('', @userId, @month, @label, @date, @cost)";

                cmd.CommandText = query;
                cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, Database.Instance.UserId));
                cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, month));
                cmd.Parameters.Add(Database.CreateParameter("@label", DbType.String, this.label));
                cmd.Parameters.Add(Database.CreateParameter("@date", DbType.Date, this.date));
                cmd.Parameters.Add(Database.CreateParameter("@cost", DbType.Decimal, this.cost));

                cmd.ExecuteNonQuery(); // We need to immediatly execute the query to obtain the ID below

                const string idQuery = "SELECT SCOPE_IDENTITY()";

                DbCommand idCmd = connection.CreateCommand();
                idCmd.CommandText = idQuery;
                
                this.id = (int)idCmd.ExecuteScalar(); // Storing the id of the last insertion

                return; // We must exit the method to avoid a new call to the ExecuteNonQuery() method
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
            return this.label;
        }
    }
}
