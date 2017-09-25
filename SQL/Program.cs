using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Transactions;

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
            var connectionStringSchoolDBManual = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SchoolGradesDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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
            var connectionStringSchoolDB = SQL.Properties.Settings.Default.SchoolDBConnectionString;

            // We can also retrieve the connection string based on the name that is declared in the App.config
            var connectionStringNorthwind = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionStringSchoolDB))
            {
                // Open the SqlConnection.
                con.Open();

                // The following code uses an SqlCommand based on the SqlConnection.
                using (var sqlCommand = new SqlCommand("SELECT LastName FROM dbo.Students", con))
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

            // For transaction usage, see https://msdn.microsoft.com/en-us/data/dn456843 and https://stackoverflow.com/questions/22382892/database-begintransaction-vs-transactions-transactionscope

            // How to use a transaction
            // -> no need to rollback explicitely, it's done automatically if an exception is thrown
            using (var connectionUsing = new SqlConnection(connectionStringSchoolDBManual))
            {
                connectionUsing.Open();
                using (var transactionUsing = connectionUsing.BeginTransaction(System.Data.IsolationLevel.RepeatableRead))
                using (var commandUsing = new SqlCommand("UPDATE dbo.Students SET LastName = 'Liiu' WHERE LastName = 'Liu'", connectionUsing, transactionUsing))
                {
                    var rowsAffected = commandUsing.ExecuteNonQuery();
                    transactionUsing.Commit();
                }
            }

            // Other method to use a transaction
            // TransactionScope is in System.Transactions.dll
            using (TransactionScope transactionScope = new TransactionScope())
            {
                using (SqlConnection connection = new SqlConnection(connectionStringNorthwind))
                {
                    connection.Open();

                    SqlCommand command1 = new SqlCommand($"INSERT INTO Employees([FirstName], [LastName]) VALUES('Benoit', 'Masson-Bedeau')", connection);

                    SqlCommand command2 = new SqlCommand($"INSERT INTO Employees([FirstName], [LastName]) VALUES('Laurie', 'Boulard')", connection);

                    command1.ExecuteNonQuery();
                    command2.ExecuteNonQuery();
                }
                transactionScope.Complete();
            }
        }
    }
}
