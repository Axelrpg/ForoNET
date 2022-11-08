using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySql.Data.MySqlClient;

namespace Foro.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string user, string pass)
        {
            login(user, pass);
        }

        private void login(string user, string pass)
        {
            using (MySqlConnection c = new MySqlConnection("Server=localhost;Database=foro;Uid=root;Password="))
            {
                MySqlCommand cmd = new MySqlCommand();
                c.Open();
                cmd.Connection = c;
                cmd.CommandText = $"SELECT * FROM users WHERE username='{user}' AND password='{pass}'";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Response.Redirect("Topics");
                    }
                    else
                    {
                        ViewData["Mensaje"] = "Error, no se encontro al usuario";
                    }
                }
            }

        }
    }
}