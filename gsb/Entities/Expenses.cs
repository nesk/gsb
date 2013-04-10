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
                "SELECT dateModif AS date, nbJustificatifs AS vouchers, montantValide AS amount, libelle AS state " +
                "FROM FicheFrais AS f " +
                "JOIN Etat AS e ON e.id = f.idEtat " +
                "WHERE f.idVisiteur = @userId";

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.CreateParameter("@userId", DbType.String, db.UserId));

            List<ExpenseNote> expenseNoteList = new List<ExpenseNote>();

            DbDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                expenseNoteList.Add(new ExpenseNote(reader));
            }
            reader.Close();

            return expenseNoteList;
        }
    }
}
