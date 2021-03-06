﻿using System;
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

        #region Private data fields

        private string month = DateTime.Today.Year.ToString().PadLeft(4, '0') + DateTime.Today.Month.ToString().PadLeft(2, '0');
        private DateTime date = DateTime.Today;
        private int vouchersNb = 0;
        private decimal approvedAmount = 0;
        private string state = "Fiche créée, saisie en cours";

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
            get {
                // This loop modifies the returned status if one of the off-plan expenses isn't marked as loaded
                foreach (ExpenseOffPlan expense in this.expensesOffPlan)
                {
                    if (expense.Status != ExpenseState.Loaded)
                        this.setModifiedStatus();
                }

                return this.status;
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

        public int VouchersNb
        {
            get { return this.vouchersNb; }
            set
            {
                this.setModifiedStatus();
                this.vouchersNb = value;
            }
        }

        public decimal ApprovedAmount
        {
            get { return this.approvedAmount; }
            set
            {
                this.setModifiedStatus();
                this.approvedAmount = value;
            }
        }

        public string State
        {
            get {
                return this.state;
            }
            set
            {
                this.setModifiedStatus();
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

        /*
         * Methods
         */

        public void SetExpenseInPlan(string name, int value)
        {
            this.expensesInPlan[name] = value;
        }

        public ExpenseOffPlan AddExpenseOffPlan()
        {
            this.setModifiedStatus();

            ExpenseOffPlan expense = new ExpenseOffPlan(this.month);
            this.expensesOffPlan.Add(expense);
            return expense;
        }

        public void RemoveExpenseOffPlan(ExpenseOffPlan expense)
        {
            expense.Status = ExpenseState.Removed;
            this.setModifiedStatus();
        }

        public override void Fill(Dictionary<string, object> row)
        {
            this.status = ExpenseState.Loaded;

            this.month = (string)row["month"];
            this.date = (DateTime)row["date"];
            this.vouchersNb = (int)row["vouchers"];
            this.approvedAmount = (decimal)row["amount"];
            this.state = (string)row["state"];
        }

        public override void Save()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;
            DbCommand cmd = connection.CreateCommand();

            cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, db.UserId));
            cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, this.month));
            cmd.Parameters.Add(Database.CreateParameter("@etp", DbType.Int32, this.expensesInPlan["ETP"]));
            cmd.Parameters.Add(Database.CreateParameter("@km", DbType.Int32, this.expensesInPlan["KM"]));
            cmd.Parameters.Add(Database.CreateParameter("@nui", DbType.Int32, this.expensesInPlan["NUI"]));
            cmd.Parameters.Add(Database.CreateParameter("@rep", DbType.Int32, this.expensesInPlan["REP"]));

            if (this.status == ExpenseState.New)
            {
                const string query =
                    // Creates the expense note
                    "INSERT INTO FicheFrais VALUES " +
                    "(@userId, @month, @vouchersNb, @amount, @date, @state); " +

                    // Creates the in-plan expenses
                    "INSERT INTO LigneFraisForfait VALUES " +
                    "(@userId, @month, 'ETP', @etp), " +
                    "(@userId, @month, 'KM', @km), " +
                    "(@userId, @month, 'NUI', @nui), " +
                    "(@userId, @month, 'REP', @rep)";

                cmd.CommandText = query;
                cmd.Parameters.Add(Database.CreateParameter("@vouchersNb", DbType.Int32, this.vouchersNb));
                cmd.Parameters.Add(Database.CreateParameter("@amount", DbType.Int32, this.approvedAmount));
                cmd.Parameters.Add(Database.CreateParameter("@date", DbType.Date, this.date));
                cmd.Parameters.Add(Database.CreateParameter("@state", DbType.String, "CR"));
            }
            else if (this.status == ExpenseState.Modified)
            {
                // Updates only the in-plan expenses
                const string query =
                    "UPDATE LigneFraisForfait " +
                    "SET quantite=@etp " +
                    "WHERE idVisiteur=@userId AND mois=@month AND idFraisForfait='ETP';" +
                    "UPDATE LigneFraisForfait " +
                    "SET quantite=@km " +
                    "WHERE idVisiteur=@userId AND mois=@month AND idFraisForfait='KM';" +
                    "UPDATE LigneFraisForfait " +
                    "SET quantite=@NUI " +
                    "WHERE idVisiteur=@userId AND mois=@month AND idFraisForfait='NUI';" +
                    "UPDATE LigneFraisForfait " +
                    "SET quantite=@rep " +
                    "WHERE idVisiteur=@userId AND mois=@month AND idFraisForfait='REP'";

                cmd.CommandText = query;
            }
            // An expense note shouldn't be removed, there's no need to implement this.

            if (this.status == ExpenseState.New || this.status == ExpenseState.Modified)
            {
                cmd.ExecuteNonQuery();

                // Saves the off-plan expenses
                foreach (ExpenseOffPlan expense in this.expensesOffPlan)
                {
                    expense.Save();
                    if (expense.Status == ExpenseState.Removed)
                        this.expensesOffPlan.Remove(expense);
                }
            }

            // The entity has been saved, the status must be changed.
            this.status = ExpenseState.Loaded;
        }

        public override int CompareTo(object obj)
        {
            return -1 * DateTime.Compare(this.date, ((ExpenseNote)obj).date);
        }

        public override string ToString()
        {
            return this.date.ToString("MMMM yyyy");
        }

        #region Private methods

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
            cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, this.month));

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
                "SELECT id, mois AS month, libelle AS label, date, montant AS cost " +
                "FROM LigneFraisHorsForfait " +
                "WHERE idVisiteur = @userId " +
                "AND mois = @month";

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, db.UserId));
            cmd.Parameters.Add(Database.CreateParameter("@month", DbType.String, this.month));

            DbDataReader reader = cmd.ExecuteReader();
            List<Dictionary<string, object>> rows = Database.GetDataReaderRows(reader);
            reader.Close();

            foreach (Dictionary<string, object> row in rows)
                this.expensesOffPlan.Add(new ExpenseOffPlan(row));
        }

        #endregion
    }
}
