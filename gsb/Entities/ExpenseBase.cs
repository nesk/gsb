using System;
using System.Collections.Generic;
using System.Data.Common;

namespace gsb.Entities
{
    public enum ExpenseState
    {
        New, Loaded, Modified, Removed
    }

    abstract class ExpenseBase
    {
        public abstract void Fill(Dictionary<string, object> row);
        public abstract bool Save();

        // Forces the overriding of the ToString() method
        public abstract override string ToString();
    }
}
