using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WebApplication1.Pages
{
    public class InvoiceInfo
    {
        public String id;
        public String idcustomer;
        public String idarticles;
        public String totalprice;
        public String paymentmethod;
        public String invoicedate;
    }
    public class invoiceModel : PageModel
    {
        private readonly ILogger<invoiceModel> _logger;
        public List<InvoiceInfo> listInvoice = new List<InvoiceInfo>();
        public invoiceModel(ILogger<invoiceModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6;Initial Catalog=\"project database\";User ID=sa;Password=SHIMshim99@99";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                connection.Open();
                String sql = "SELECT * FROM sale_invoice";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            InvoiceInfo invoiceInfo = new InvoiceInfo();
                            invoiceInfo.id = "" + reader.GetInt32(0);
                            invoiceInfo.idcustomer = "" + reader.GetInt32(1);
                            invoiceInfo.idarticles = "" + reader.GetInt32(2);
                            invoiceInfo.totalprice = "" + reader.GetDouble(3);
                            invoiceInfo.paymentmethod= reader.GetString(4);
                            invoiceInfo.invoicedate= "" + reader.GetDateTime(5);
                            listInvoice.Add(invoiceInfo);

                        }
                    }

                }
            }
            
        }
        public void CreateInvoice()

        {
            Console.WriteLine("Test");
        }
        public void OnPost()
        {

            String connectionString = "Data Source=LAPTOP-276J3PA6;Initial Catalog=project database;User ID=sa;Password=SHIMshim99@99";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO sale_invoice " +
                    "(id_customer, id_articles,total_price,payment_method,invoice_date) VALUES " +
                    "(@id_customer, @id_articles, @total_price, @payment_method,@invoice_date );";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_customer", Request.Form["id_customer"].ToString());
                    command.Parameters.AddWithValue("@id_articles", Request.Form["id_articles"].ToString());
                    command.Parameters.AddWithValue("@total_price", Request.Form["total_price"].ToString());
                    command.Parameters.AddWithValue("@payment_method", Request.Form["payment_method"].ToString());
                    command.Parameters.AddWithValue("@invoice_date", Request.Form["invoice_date"].ToString());
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["idcustomer"]);
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["idarticles"]);
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["totalprice"]);
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["totalprice"]);
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["paymentmethod"]);
                    _logger.LogInformation("CreateInvoice method invoked." + Request.Form["invoicedate"]);

                    command.ExecuteNonQuery();
                }

            }
            OnGet();
        }
    }
}
