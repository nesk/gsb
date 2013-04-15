using System;
using System.Collections.Generic;
using System.Data.Common;

namespace gsb.Entities
{
    public enum ExpenseState
    {
        New, Loaded, Modified, Removed
    }

    abstract class ExpenseBase : IComparable
    {
        /*
         * Fields
         */

        protected ExpenseState status = ExpenseState.New;

        /*
         * Properties
         */

        public ExpenseState Status
        {
            get { return this.status; }
            set
            {
                if (value == ExpenseState.Removed)
                    this.status = value;
            }
        }

        /*
         * Methods
         */

        public abstract void Fill(Dictionary<string, object> row);
        public abstract bool Save();
        public abstract int CompareTo(object obj);

        // Forces the overriding of the ToString() method
        public abstract override string ToString();
    }
}
