using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace gsb.Entities
{
    static class Expenses
    {
        public static List<ExpenseNote> GetExpenseNotes()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;

            const string query =
                "SELECT mois AS month, dateModif AS date, nbJustificatifs AS vouchers, montantValide AS amount, libelle AS state " +
                "FROM FicheFrais AS f " +
                "JOIN Etat AS e ON e.id = f.idEtat " +
                "WHERE f.idVisiteur = @userId";

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, db.UserId));

            DbDataReader reader = cmd.ExecuteReader();
            List<Dictionary<string, object>> rows = Database.GetDataReaderRows(reader);
            reader.Close();

            List<ExpenseNote> expenseNoteList = new List<ExpenseNote>();
            foreach (Dictionary<string, object> row in rows)
                expenseNoteList.Add(new ExpenseNote(row));

            return expenseNoteList;
        }
    }
}
