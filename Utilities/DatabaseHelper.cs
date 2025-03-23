using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace E2ETestAutomation.Utilities
{
    public static class DatabaseHelper
    {
        /// <summary>
        /// Opens a MySQL connection using the connection string from configuration,
        /// queries for a price from the "imaginary_orders" table, and returns it as a decimal.
        /// </summary>
        /// <returns>The retrieved price.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the database cannot be accessed, the query returns no data,
        /// or the price cannot be converted.
        /// </exception>
        public static async Task<decimal?> GetPriceAsync()
        {
            // Get the connection string from config
            string connectionString = ConfigReader.GetMySqlConnectionString();

            using var connection = new MySqlConnection(connectionString);

            try
            {
                await connection.OpenAsync();
            }
            catch (MySqlException ex)
            {
                throw new InvalidOperationException("Database does not exist or is not accessible: " + ex.Message, ex);
            }

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT price FROM imaginary_orders LIMIT 1;";

            var result = await command.ExecuteScalarAsync();

            if (result == null)
            {
                throw new InvalidOperationException("No price found in the database.");
            }

            try
            {
            
                return Convert.ToDecimal(result);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to convert the retrieved price to a decimal: " + ex.Message, ex);
            }
        }
    }
}