using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data.SqlClient;
using System.Net;

namespace WebApplication1.Pages
{
    public class CustomerInfo
    {
        public String fname;
        public String lname;
        public String id;
    }
    public class IndexModel : PageModel
    {

        private readonly ILogger<IndexModel> _logger;
        public List<CustomerInfo> listCustomer = new List<CustomerInfo>();

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6;Initial Catalog=\"project database\";User ID=sa;Password=SHIMshim99@99";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM Customer";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            CustomerInfo customerInfo = new CustomerInfo();

                            customerInfo.fname = reader.GetString(0);
                            customerInfo.lname = reader.GetString(1);
                            customerInfo.id = "" + reader.GetInt32(2);
                            listCustomer.Add(customerInfo);



                        }
                    }
                }



            }
        }
        public void CreateCustomer()

        {
            Console.WriteLine("Test");
        }
        public void OnPost()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6;Initial Catalog=project database;User ID=sa;Password=SHIMshim99@99";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO Customer " +
                "(first_name, last_name) VALUES " +
                    "(@first_name, @last_name);";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@first_name", Request.Form["fname"].ToString());
                    command.Parameters.AddWithValue("@last_name", Request.Form["lname"].ToString());
                    _logger.LogInformation("CreateCustomer method invoked." + Request.Form["fname"].ToString());
                    command.ExecuteNonQuery();


                }

               

                


            }
            OnGet();
        }
    }
}