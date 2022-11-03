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

        public void OnGet()
        {
            login();
        }

        private void login()
        {
            using (MySqlConnection c = new MySqlConnection("Server=localhost;Database=foro;Uid=root;Password="))
            {
                MySqlCommand cmd = new MySqlCommand();
                c.Open();
                cmd.Connection = c;
                cmd.CommandText = "SELECT * FROM users WHERE username='admin' AND password='123'";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ViewData["Mensaje"] = "Si existe el usuario";
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