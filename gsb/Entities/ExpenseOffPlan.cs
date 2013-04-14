using System;
using System.Collections.Generic;

namespace gsb.Entities
{
    class ExpenseOffPlan : ExpenseBase
    {
        /*
         * Fields
         */

        #region Data fields
        private int id;
        private string label;
        private DateTime date;
        private decimal cost;
        #endregion

        /*
         * Constructors
         */

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

        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return this.label;
        }
    }
}
