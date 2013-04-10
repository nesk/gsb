using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace gsb.Entities
{
    static class Expenses
    {
        static public List<ExpenseNote> getExpenseNotes()
        {
            Database db = Database.Instance;
            DbConnection connection = db.DbConnection;

            const string query =
                "SELECT f.nbJustificatifs, f.montantValide, f.dateModif, e.libelle AS etat " +
                "FROM FicheFrais AS f" +
                "JOIN Etat AS e ON e.id = f.idEtat " +
                "WHERE ffr.idVisiteur = @userId";

            DbCommand cmd = connection.CreateCommand();
            cmd.CommandText = query;
            cmd.Parameters.Add(Database.createParameter("@userId", DbType.String, db.UserId));

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
