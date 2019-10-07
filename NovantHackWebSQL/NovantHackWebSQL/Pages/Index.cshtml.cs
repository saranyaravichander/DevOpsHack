using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace NovantHackWebSQL.Pages
{
    public class IndexModel : PageModel
    {
        private IConfiguration config;

        public IndexModel(IConfiguration configuration)
        {
            config = configuration;
        }

        public void OnGet()
        {
            string connectionString = config.GetSection("Data").GetSection("SQLConnectionString").Value;
            
            // Provide the query string with a parameter placeholder.
            string queryString =
                "SELECT * from dbo.Test";

            using (SqlConnection connection =
                new SqlConnection(connectionString))
            {
                // Create the Command and Parameter objects.
                SqlCommand command = new SqlCommand(queryString, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                    }
                    reader.Close();

                    ViewData["SQLStatus"] = "Connected to SQL";
                }
                catch (Exception ex)
                {
                    ViewData["SQLStatus"] = "Error connecting to SQL";
                }
            }
        }
    }
}
