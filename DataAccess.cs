using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace lezione70z
{
    public static class DataAccess
    {
        // Dichiarazione della variabile globale
        private static readonly string connectionString = "Server=DESKTOP-F6A69FR\\SQLEXPRESS; Initial Catalog=Hotel; Integrated Security=true";

        // Proprietà per ottenere la connectionString
        public static string ConnectionString
        {
            get { return connectionString; }
        }

        // Metodo per verificare l'autenticazione
        public static bool IsHostAuthenticated(string nome, string cognome, string username, string password)
        {
            // Utilizzo della variabile connectionString
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM Dipendenti WHERE Nome = @Nome AND Cognome = @Cognome AND Username = @Username AND Password = @Password", connection))
                {
                    command.Parameters.AddWithValue("@Nome", nome);
                    command.Parameters.AddWithValue("@Cognome", cognome);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}
