using System;
using System.Collections.Generic;
using System.Data.Common;

namespace gsb.Entities
{
    class ExpenseNote : ExpenseBase
    {
        /*
         * Fields
         */

        private bool newEntity = true;
        private bool modified = false;

        #region Data fields
        private DateTime date;
        private int vouchersNb;
        private decimal approvedAmount;
        private string state;
        #endregion

        /*
         * Constructors
         */

        public ExpenseNote(Dictionary<string, object> row)
        {
            this.Fill(row);
        }

        /*
         * Methods
         */

        public override void Fill(Dictionary<string, object> row)
        {
            this.newEntity = false;

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
            throw new NotImplementedException();
        }
    }
}
