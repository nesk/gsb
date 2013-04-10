using System;
using System.Data.Common;

namespace gsb.Entities
{
    abstract class ExpenseBase
    {
        public abstract void Fill(DbDataReader row);
        public abstract bool Save();

        // Forces the overriding of the ToString() method
        public abstract override string ToString();
    }
}
