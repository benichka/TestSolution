using System;
using System.Data.SqlClient;

namespace SQL
{
    /// <summary>
    /// Simple project to test an SQL connection
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            // We can manually set the connection string
            var connectionStringManual = @"data source=(localdb)\MSSQLLocalDB;initial catalog=SchoolDB;integrated security=True;MultipleActiveResultSets=True;";

            // Or, we can retrieve it using the project properties; a connection can be added this way:
            // 1. menu Project -> Add New Data Source...
            // 2. choose Database
            // 3. choose Dataset (what's the utility of that?)
            // 4. choose an existing data connection or create another one:
            //  4a. if a new one is chosen:
            //    4a1 : for the localDB (that comes with Visual Studio), choose Microsoft SQL Server (SqlClient)
            //    4a2 : server name: (localdb)\MSSQLLocalDB (this one should be visible in the SQL Server Object Explorer)
            //    4a3 : choose the database to connect to
            //    4a4 : leave all the default checked box
            //    4a5 : the connection string is now available in the Settings.settings in the project
            var connectionString = SQL.Properties.Settings.Default.SchoolDBConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                // Open the SqlConnection.
                con.Open();

                // The following code uses an SqlCommand based on the SqlConnection.
                using (var sqlCommand = new SqlCommand("SELECT LastName FROM dbo.Student", con))
                {
                    using (SqlDataReader reader = sqlCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["LastName"]);
                        }
                    }
                }
            }

            // How to use a transaction
            // -> no need to rollback explicitely, it's done automatically if an exception is thrown
            using (var connectionUsing = new SqlConnection(connectionStringManual))
            {
                connectionUsing.Open();
                using (var transactionUsing = connectionUsing.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
                using (var commandUsing = new SqlCommand("UPDATE dbo.Student SET LastName = 'Liiu' WHERE LastName = 'Liu'", connectionUsing, transactionUsing))
                {
                    var rowsAffected = commandUsing.ExecuteNonQuery();
                    transactionUsing.Commit();
                }
            }
        }
    }
}
