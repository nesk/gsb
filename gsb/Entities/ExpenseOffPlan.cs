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
            throw new NotImplementedException();
        }
    }
}
