using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;

namespace WebApplication1.Pages
{
    public class ArticleInfo
    {
        public String id;
        public String name;
        public String price;
        public String quantity;
        public String description;
    }

    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;
        public List<ArticleInfo> listArticle = new List<ArticleInfo>();
        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6;Initial Catalog=\"project database\";User ID=sa;Password=SHIMshim99@99";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "SELECT * FROM articles";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ArticleInfo articleInfo = new ArticleInfo();

                            articleInfo.id = "" + reader.GetInt32(0);
                            articleInfo.name = reader.GetString(1);
                            articleInfo.price = "" + reader.GetInt32(2);
                            articleInfo.quantity = "" + reader.GetInt32(3);
                            articleInfo.description = reader.GetString(4);


                            listArticle.Add(articleInfo);



                        }


                    }



                }
            }
        }

        public void CreateArticle()

        {
            Console.WriteLine("Test");
        }
        public void OnPost()
        {
            String connectionString = "Data Source=LAPTOP-276J3PA6;Initial Catalog=project database;User ID=sa;Password=SHIMshim99@99";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                String sql = "INSERT INTO articles " +
               "(name, price,quantity,description) VALUES " +
                   "(@name, @price,@quantity,@description);";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@name", Request.Form["name"].ToString());
                    command.Parameters.AddWithValue("@price", Request.Form["price"].ToString());
                    command.Parameters.AddWithValue("@quantity", Request.Form["quantity"].ToString());
                    command.Parameters.AddWithValue("@description", Request.Form["description"].ToString());
                    _logger.LogInformation("CreateArticle method invoked." + Request.Form["id"].ToString());
                    _logger.LogInformation("CreateArticle method invoked." + Request.Form["name"].ToString());
                    _logger.LogInformation("CreateArticle method invoked." + Request.Form["price"].ToString());
                    _logger.LogInformation("CreateArticle method invoked." + Request.Form["quantity"].ToString());
                    _logger.LogInformation("CreateArticle method invoked." + Request.Form["description"].ToString());
                    command.ExecuteNonQuery();



                }

            }
            OnGet();
        }
      

    }    }